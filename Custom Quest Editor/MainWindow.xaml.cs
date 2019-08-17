using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Custom_Quest_Editor.Crypto;

namespace Custom_Quest_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Int32[] MapIDs = {100, 101, 102, 103, 104, 105, 106, 107, 200, 201, 202, 301, 302, 303, 400, 401, 403, 405, 406, 407, 408, 409, 501, 502, 503, 504, 505 };
        public byte[] ObjectiveIDs = { 0x00, 0x01, 0x02, 0x11, 0x21, 0x31 };
        public byte[] QuestTypeIDs = { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20 };
        
        #region Strings
        public string[] Items = {"Nothing" , "Potion" , "Mega Potion" , "Max Potion" , "Ancient Potion" , "Antidote " , "Herbal Medicine" ,
            "Nulberry" , "Energy Drink  " , "Ration  " , "Rare Steak" , "Well - done Steak " , "Burnt Meat" , "Cool Drink" , "Nutrients " ,
            "Mega Nutrients" , "Immunizer " , "Astera Jerky  " , "Dash Juice " , "Mega Dash Juice" , "Might Seed  " , "Demondrug " , "Mega Demondrug " ,
            "Might Pill  " , "Adamant Seed   " , "Armorskin " , "Mega Armorskin " , "Adamant Pill   " , "Lifepowder " , "Herbal Powder  " , "Demon Powder   " ,
            "Hardshell Powder " , "Honey" , "Herb " , "Antidote Herb" , "Fire Herb" , "Flowfern " , "Snow Herb" , "Sleep Herb" , "Ivy" , "Smokenut" , "Dragonfell Berry" ,
            "Blue Mushroom" , "Mandragora" , "Nitroshroom " , "Devils Blight" , "Parashroom " , "Toadstool" , "Exciteshroom" , "Bitterbug" , "Flashbug" , "Godbug" ,
            "Thunderbug" , "Baitbug " , "Kelbi Horn" , "Dash Extract" , "Nourishing Extract" , "Screamer Sac" , "Catalyst " , "Tranq Bomb" , "Flash Pod " , "Screamer Pod" ,
            "Smoke Bomb  " , "Poison Smoke Bomb " , "Farcaster" , "Raw Mea" , "Poisoned Meat" , "Tinged Meat " , "Drugged Meat" , "Gunpowder" , "Small Barrel" ,
            "Barrel Bomb" , "Bounce Bomb" , "Mega Bounce Bomb" , "Large Barrel" , "Large Barrel Bomb" , "Mega Barrel Bomb" , "Spider Web" , "Net" , "Trap Tool" ,
            "Pitfall Trap" , "Shock Trap" , "Rolled - up Dung" , "Dung" , "Dung Pod" , "Arowana Bait" , "Gunpowderfish Bait" , "Goldenfish Bait " , "Boomerang" ,
            "Binoculars " , "Powercharm " , "Powertalon " , "Armorcharm " , "Armortalon " , "Needleberry" , "Blastnut" , "Dragonstrike Nut" , "Slashberry" , "Latchberry" ,
            "Bomberry" , "Flamenut" , "Blazenut" , "Gunpowder Level 2" , "Gunpowder Level 3" , "Whetfish Fin   " , "Whetfish Fin + " , "Sushifish Scale" , "Great Sushifish Scale" ,
            "Gunpowderfish Scale" , "Great Gunpowderfish Scale" , "Burst Arowana Scale" , "Great Burst Arowana Scale" , "Bomb Arowana Scale" , "Great Bomb Arowana Scale" ,
            "Whetstone" , "Capture Net" , "Fishing Rod" , "BBQ Spit   " , "Ghillie Mantle " , "Temporal Mantle" , "Health Booster " , "Rocksteady Mantle" , "Challenger Mantle" ,
            "Vitality Mantle " , "Fireproof Mantle " , "Waterproof Mantle" , "Iceproof Mantle" , "Thunderproof Mantle" , "Dragonproof Mantle " , "Cleanser Booster   " ,
            "Glider Mantle " , "Evasion Mantle" , "Impact Mantle " , "Apothecary Mantle" , "Immunity Mantle " , "Affinity Booster" , "Bandit Mantle " , "Normal Ammo 1" ,
            "Normal Ammo 2" , "Normal Ammo 3" , "Pierce Ammo 1" , "Pierce Ammo 2" , "Pierce Ammo 3" , "Spread Ammo 1" , "Spread Ammo 2" , "Spread Ammo 3" , "Sticky Ammo 1" ,
            "Sticky Ammo 2" , "Sticky Ammo 3" , "Cluster Bomb 1" , "Cluster Bomb 2" , "Cluster Bomb 3" , "Flaming Ammo  " , "Water Ammo  " , "Thunder Ammo" , "Freeze Ammo " ,
            "Dragon Ammo " , "Poison Ammo 1" , "Poison Ammo 2" , "Paralysis Ammo 1" , "Paralysis Ammo 2" , "Sleep Ammo 1" , "Sleep Ammo 2" , "Exhaust Ammo 1" , "Exhaust Ammo 2" ,
            "Recover Ammo 1" , "Recover Ammo 2" , "Wyvern Ammo " , "Slicing Ammo" , "Tranq Ammo  " , "Demon Ammo  " , "Armor Ammo  " , "(None) Bow Coating" , "Close - range " ,
            "Empty Phial " , "Power Coating   " , "Poison Coating  " , "Paralysis Coating" , "Sleep Coating   " , "Exhaust Coating " , "Blast Coating   " , "First - aid Med   " ,
            "First - aid Med + " , "EZ Ration " , "EZ Lifepowder " , "EZ Max Potion " , "EZ Large Barrel Bomb" , "EZ Shock Trap" , "EZ Pitfall Trap " , "EZ Herbal Powder" , "EZ Demon Powder " ,
            "EZ Hardshell Powder" , "EZ Dung Pod " , "EZ Flash Pod" , "EZ Screamer Pod" , "Throwing Knife " , "Poison Knife   " , "Sleep Knife" , "Paralysis Knife " , "Tranq Knife " ,
            "EZ Farcaster" , "EZ Tranq Bomb" , "Ballista Ammo" , "One - shot Binder" , "Cannon Ammo4" , "Iron Ore   " , "Machalite Ore" , "Dragonite Ore" , "Carbalite Ore" , "Fucium Ore" ,
            "Earth Crystal" , "Coral Crystal" , "Dragonvein Crystal" , "Lightcrystal" , "Novacrystal " , "Firecell Stone" , "Aquacore Ore  " , "Spiritcore Ore" , "Dreamcore Ore " , "Dragoncore Ore" ,
            "Armor Sphere  " , "Armor Sphere + " , "Advanced Armor Sphere " , "Hard Armor Sphere " , "Heavy Armor Sphere" , "Sturdy Bone " , "Quality Bone" , "Ancient Bone" , "Boulder Bone" , "Coral Bone  " ,
            "Warped Bone " , "Brutal Bone " , "Dragonbone Relic" , "Unknown Skull   " , "Great Hornfly   " , "Sinister Cloth  " , "Monster Bone S" , "Monster Bone M" , "Monster Bone L" , "Monster Bone + " ,
            "Monster Keenbone" , "Monster Hardbone" , "Elder Dragon Bone" , "Sharp Claw" , "Piercing Claw" , "Monster Fluid" , "Monster Broth" , "Poison Sac" , "Toxin Sac " , "Paralysis Sac " , "Omniplegia Sac" ,
            "Sleep Sac  " , "Coma Sac   " , "Flame Sac  " , "Inferno Sac" , "Aqua Sac   " , "Torrent Sac" , "Frost Sac  " , "Freezer Sac" , "Electro Sac" , "Thunder Sac" , "Bird Wyvern Gem" , "Wyvern Gem " ,
            "Elder Dragon Blood" , "Mosswine Hide" , "Warm Pelt" , "High - quality Pelt " , "Vespoid Shell" , "Vespoid Carapace  " , "Vespoid Wing" , "Vespoid Innerwing " , "Hornetaur Shell" , "Hornetaur Wing" ,
            "Hornetaur Head" , "Hornetaur Carapace" , "Hornetaur Innerwing" , "Gajau Skin" , "Gajau Whisker" , "Gajau Scale" , "Grand Gajau Whisker" , "Wingdrake Hide " , "Wingdrake Hide + " , "Barnos Hide + " ,
            "Barnos Talon   " , "Kestodon Shell " , "Kestodon Scalp " , "Kestodon Carapace" , "Gastodon Carapace" , "Gastodon Horn" , "Jagras Scale " , "Jagras Hide" , "Jagras Scale + " , "Jagras Hide + " ,
            "Shamos Scale " , "Shamos Hide " , "Shamos Scale + " , "Shamos Hide + " , "Girros Scale " , "Girros Hide" , "Girros Fang" , "Girros Scale + " , "Girros Hide + " , "Great Jagras Scale" ,
            "Great Jagras Hide " , "Great Jagras Mane " , "Great Jagras Claw " , "Great Jagras Scale" , "Great Jagras Hide + " , "Great Jagras Claw + " , "Kulu - Ya - Ku Scale" , "Kulu - Ya - Ku Hide " ,
            "Kulu - Ya - Ku Plume" , "Kulu - Ya - Ku Beak " , "Kulu - Ya - Ku Scale + " , "Kulu - Ya - Ku Hide + " , "Kulu - Ya - Ku Plume + " , "Kulu - Ya - Ku Beak + " , "Pukei - Pukei Scale " ,
            "Pukei - Pukei Shell " , "Pukei - Pukei Quill " , "Pukei - Pukei Sac" , "Pukei - Pukei Tail  " , "Pukei - Pukei Scale + " , "Pukei - Pukei Carapace" , "Pukei - Pukei Wing" , "Pukei - Pukei Sac + " ,
            "Barroth Shell" , "Barroth Ridge" , "Barroth Claw " , "Barroth Scalp" , "Barroth Tail " , "Fertile Mud" , "Barroth Carapace" , "Barroth Ridge + " , "Barroth Claw + " , "Jyuratodus Scale" ,
            "Jyuratodus Shell" , "Jyuratodus Fang " , "Jyuratodus Fin  " , "Jyuratodus Scale + " , "Jyuratodus Carapace" , "Jyuratodus Fang + " , "Jyuratodus Fin + " , "Tobi - Kadachi Scale " ,
            "Tobi - Kadachi Pelt  " , "Tobi - Kadachi Membrane" , "Tobi - Kadachi Claw" , "Tobi - Kadachi Electrode" , "Tobi - Kadachi Scale + " , "Tobi - Kadachi Pelt + " , "Tobi - Kadachi Claw + " ,
            "Tobi - Kadachi Electrode + " , "Anjanath Scale" , "Anjanath Pelt " , "Anjanath Fang " , "Anjanath Nosebone" , "Anjanath Tail  " , "Anjanath Plate " , "Anjanath Scale + " , "Anjanath Pelt + " ,
            "Anjanath Fang + " , "Anjanath Nosebone + " , "Anjanath Gem   " , "Rathian Scale  " , "Rathian Shell  " , "Rathian Webbing" , "Rathian Spike  " , "Rathian Plate  " , "Rathian Scale + " ,
            "Rathian Carapace" , "Rathian Spike + " , "Rathian Ruby    " , "Pink Rathian Scale + " , "Pink Rathian Carapace" , "Tzitzi - Ya - Ku Scale" , "Tzitzi - Ya - Ku Hide " , "Tzitzi - Ya - Ku Claw " ,
            "Tzitzi - Ya - Ku Photophore" , "Tzitzi - Ya - Ku Scale + " , "Tzitzi - Ya - Ku Hide + " , "Tzitzi - Ya - Ku Claw + " , "Tzitzi - Ya - Ku Photophore + " , "Paolumu Pelt   " , "Paolumu Scale  " ,
            "Paolumu Shell  " , "Paolumu Webbing" , "Paolumu Pelt + " , "Paolumu Scale + " , "Paolumu Carapace + " , "Paolumu Wing" , "Great Girros Scale" , "Great Girros Hide " , "Great Girros Hood " ,
            "Great Girros Fang " , "Great Girros Tail " , "Great Girros Scale + " , "Great Girros Hide + " , "Great Girros Hood + " , "Great Girros Fang + " , "Radobaan Scale" , "Radobaan Shell" ,
            "Radobaan Oilshell" , "Wyvern Bonemass" , "Radobaan Jaw   " , "Radobaan Marrow" , "Radobaan Scale + " , "Radobaan Carapace" , "Radobaan Medulla " , "Legiana Scale  " , "Legiana Hide" ,
            "Legiana Claw   " , "Legiana Webbing" , "Legiana Tail Webbing" , "Legiana Plate " , "Legiana Scale + " , "Legiana Hide + " , "Legiana Claw + " , "Legiana Wing  " , "Legiana Gem" ,
            "Odogaron Scale" , "Odogaron Sinew" , "Odogaron Claw " , "Odogaron Fang " , "Odogaron Tail " , "Odogaron Plate" , "Odogaron Scale + " , "Odogaron Sinew + " , "Odogaron Claw + " ,
            "Odogaron Fang + " , "Odogaron Gem   " , "Rathalos Scale " , "Rathalos Shell " , "Rathalos Webbing" , "Rathalos Tail   " , "Rath Wingtalon  " , "Rathalos Marrow " , "Rathalos Plate" ,
            "Rathalos Scale + " , "Rathalos Carapace" , "Rathalos Wing" , "Rathalos Medulla" , "Rathalos Ruby" , "Azure Rathalos Scale + " , "Azure Rathalos Carapace" , "Azure Rathalos Tail" ,
            "Azure Rathalos Wing" , "Diablos Shell" , "Diablos Ridge" , "Diablos Tailcase" , "Diablos Fang" , "Twisted Horn" , "Diablos Marrow" , "Diablos Carapace" , "Diablos Ridge + " ,
            "Majestic Horn " , "Blos Medulla" , "Black Diablos Carapace" , "Black Diablos Ridge + " , "Black Spiral Horn + " , "Kirin Hide" , "Kirin Tail" , "Kirin Mane" , "Kirin Thunderhorn" ,
            "Kirin Hide + " , "Kirin Thundertail" , "Kirin Azure Horn " , "Zorah Magdaros Inner Scale " , "Zorah Magdaros Heat Scale  " , "Zorah Magdaros Carapace" , "Zorah Magdaros Ridge" ,
            "Zorah Magdaros Pleura" , "Zorah Magdaros Brace " , "Zorah Magdaros Magma" , "Zorah Magdaros Gem" , "Dodogama Scale + " , "Dodogama Hide + " , "Dodogama Jaw   " , "Dodogama Talon " ,
            "Dodogama Tail  " , "Lavasioth Scale + " , "Lavasioth Carapace" , "Lavasioth Fang + " , "Lavasioth Fin + " , "Uragaan Scale + " , "Uragaan Carapace" , "Uragaan Jaw " , "Uragaan Scute " ,
            "Uragaan Marrow" , "Uragaan Ruby  " , "Lava Nugget" , "Bazelgeuse Scale + " , "Bazelgeuse Carapace" , "Bazelgeuse Tail" , "Bazelgeuse Fuse" , "Bazelgeuse Talon" , "Bazelgeuse Wing " ,
            "Bazelgeuse Gem  " , "Immortal Dragonscale" , "Nergigante Carapace " , "Nergigante Barbs" , "Nergigante Tail " , "Nergigante Horn + " , "Nergigante Talon" , "Nergigante Regrowth Plate" ,
            "Nergigante Gem" , "Deceased Scale" , "Vaal Hazak Carapace" , "Vaal Hazak Membrane" , "Vaal Hazak Tail" , "Vaal Hazak Fang + " , "Vaal Hazak Talon" , "Vaal Hazak Wing" , "Vaal Hazak Miasmacryst" ,
            "Vaal Hazak Gem" , "Teostra Carapace" , "Teostra Mane " , "Teostra Tail " , "Teostra Horn + " , "Fire Dragon Scale + " , "Teostra Claw + " , "Teostra Webbing" , "Teostra Powder " , "Teostra Gem" ,
            "Daora Carapace" , "Daora Dragon Scale + " , "Daora Webbing" , "Daora Horn + " , "Daora Tail " , "Daora Claw + " , "Daora Gem  " , "Xeno'jiiva Soulscale" , "Xeno'jiiva Shell" , "Xeno'jiiva Veil" ,
            "Xeno'jiiva Tail" , "Xeno'jiiva Horn" , "Xeno'jiiva Claw" , "Xeno'jiiva Wing" , "Xeno'jiiva Crystal" , "Xeno'jiiva Gem" , "??? Scale" , "??? Shell" , "??? Membrane" , "??? Tail" , "??? Horn" , "??? Claw" ,
            "??? Wing" , "??? Crystal " , "??? Gem" , "Mysterious Feystone" , "Glowing Feystone " , "Worn Feystone" , "Warped Feystone" , "Sullied Streamstone" , "Shining Streamstone" , "Streamstone Shard  " ,
            "Streamstone" , "Gleaming Streamstone" , "Warrior's Streamstone: Sword" , "Warrior's Streamstone: Blade" , "Warrior's Streamstone: Hammer" , "Warrior's Streamstone: Lance" , "Warrior's Streamstone: Axe" ,
            "Warrior's Streamstone: Shaft" , "Warrior's Streamstone: Ranged" , "Hero's Streamstone: Sword" , "Hero's Streamstone: Blade" , "Hero's Streamstone: Hammer" , "Hero's Streamstone: Lance" , "Hero's Streamstone: Axe" ,
            "Hero's Streamstone: Shaft" , "Hero's Streamstone: Ranged" , "Voucher" , "First Wyverian Print" , "Deluxe First Wyverian Print" , "Steel Wyverian Print" , "Silver Wyverian Print" , "Gold Wyverian Print" ,
            "Commendation" , "High Commendation" , "Research Commission" , "Pukei Coin" , "Kulu Coin" , "Rathian Coin" , "Tzitzi Coin" , "Barroth Coin" , "Gama Coin" , "Rathalos Coin" , "Brute Coin " , "Flying Coin" ,
            "Pinnacle Coin" , "Hunter King Coin" , "Ace Hunter Coin " , "Steel Egg " , "Silver Egg" , "Golden Egg" , "Chipped Scale" , "Large Scale" , "Beautiful Scale" , "Lustrous Scale" , "Glimmering Scale" ,
            "Bhernastone" , "Dundormarin" , "Loc Lac Ore" , "Val Habar Quartz" , "Minegardenite" , "Golden Scale " , "Golden Scale+" , "Platinum Scale " , "Platinum Scale+" , "Gilded Scale   " , "Gilded Scale+" ,
            "White Liver" , "Wyvern Tear" , "Large Wyvern Tear" , "Dragon Treasure" , "Old Dragon Treasure" , "Sunbloom" , "Shinebloom" , "Goldbloom" , "Gourmet Shroomcap  " , "Exquisite Shroomcap" , "Spirit Shroomcap" ,
            "Bauble Cactus" , "Jewel Cactus " , "Kingly Cactus" , "Hardfruit" , "Rockfruit" , "Wildfruit" , "Super Abalone " , "Choice Abalone" , "Precious Abalone" , "Light Pearl" , "Deep Pearl " , "Innocent Pearl  " ,
            "Forgotten Fossil" , "Legendary Fossil" , "Mystical Fossil " , "Underground Fruit" , "Tainted Fruit   " , "Elysian Fruit   " , "Gaia Amber " , "Dragonvein Amber" , "Ancient Amber   " , "Blue Beryl" ,
            "True Beryl" , "Abyssal Beryl  " , "Sunkissed Grass" , "Moonlit Mushroom" , "Dragonbloom" , "Divineapple" , "Violet Abalone" , "Platinum Pearl" , "Wicked Fossil " , "Heavenberry" , "Twilight Stone" ,
            "Noahstone" , "Wyvern Egg" , "Herbivore Egg" , "Lump of Meat " , "Shepherd Hare" , "Pilot Hare  " , "Woodland Pteryx" , "Forest Pteryx  " , "Cobalt Flutterfly " , "Phantom Flutterfly" , "Climbing Joyperch " ,
            "Forest Gekko   " , "Wildspire Gekko" , "Gloom Gekko" , "Moonlight Gekko" , "Vaporonid " , "Scavantula" , "Revolture " , "Blissbill " , "Omenfly " , "Augurfly" , "Scalebat" , "Dung Beetle" , "Bomb Beetle" ,
            "Pink Parexus" , "Great Pink Parexus" , "Burst Arowana" , "Bomb Arowana " , "Great Burst Arowana" , "Great Bomb Arowana " , "Elegant Coralbird  " , "Dapper Coralbird   " , "Andangler" , "Downy Crake" ,
            "Bristly Crake " , "Hopguppy " , "Petricanths" , "Paratoad " , "Sleeptoad" , "Nitrotoad" , "Wiggler " , "Wiggler Queen" , "Vigorwasp" , "Giant Vigorwasp" , "Flying Meduso  " , "Carrier Ant" , "Hercudrome " ,
            "Gold Hercudrome " , "Prism Hercudrome" , "Emperor Hopper  " , "Tyrant Hopper   " , "Flashfly " , "Grandfather Mantagrell" , "Iron Helmcrab" , "Soldier Helmcrab" , "Emerald Helmcrab" , "Whetfish" ,
            "Great Whetfish " , "Gastronome Tuna" , "Great Gastronome Tuna" , "King Marlin" , "Great King Marlin " , "Goldenfish" , "Platinumfish" , "Great Goldenfish" , "Great Platinumfish" , "Goldenfry" ,
            "Great Goldenfry" , "Sushifish" , "Great Sushifish" , "Gunpowderfish" , "Great Gunpowderfish" , "Antidote Jewel 1" , "Antipara Jewel 1" , "Pep Jewel 1" , "Steadfast Jewel 1" , "Antiblast Jewel 1" ,
            "Suture Jewel 1" , "Def Lock Jewel 1" , "Earplug Jewel 3" , "Wind Resist Jewel 2" , "Footing Jewel 2" , "Fertilizer Jewel 1" , "Heat Resist Jewel 2" , "Attack Jewel 1" , "Defense Jewel 1" ,
            "Vitality Jewel 1" , "Recovery Jewel 1" , "Fire Res Jewel 1" , "Water Res Jewel 1" , "Ice Res Jewel 1" , "Thunder Res Jewel 1" , "Dragon Res Jewel 1" , "Resistor Jewel 1" , "Blaze Jewel 1" ,
            "Stream Jewel 1" , "Frost Jewel 1" , "Bolt Jewel 1" , "Dragon Jewel 1" , "Venom Jewel 1" , "Paralyzer Jewel 1" , "Sleep Jewel 1" , "Blast Jewel 1" , "Poisoncoat Jewel 3" , "Paracoat Jewel 3" ,
            "Sleepcoat Jewel 3" , "Blastcoat Jewel 3" , "Powercoat Jewel 3" , "Release Jewel 3" , "Expert Jewel 1" , "Critical Jewel 2" , "Tenderizer Jewel 2" , "Charger Jewel 2" , "Handicraft Jewel 3" ,
            "Draw Jewel 2" , "Destroyer Jewel 2" , "KO Jewel 2" , "Drain Jewel 1" , "Rodeo Jewel 2" , "Flight Jewel 2" , "Throttle Jewel 2" , "Challenger Jewel 2" , "Flawless Jewel 2" , "Potential Jewel 2" ,
            "Fortitude Jewel 1" , "Furor Jewel 2" , "Sonorous Jewel 1" , "Magazine Jewel 2" , "Trueshot Jewel 1" , "Artillery Jewel 1" , "Heavy Artillery Jewel 1" , "Sprinter Jewel" , "Physique Jewel" ,
            "Flying Leap Jewel " , "Refresh Jewel 2" , "Hungerless Jewel 1" , "Evasion Jewel 2" , "Jumping Jewel 2" , "Ironwall Jewel 1" , "Sheath Jewel " , "Friendship Jewel 1" , "Enduring Jewel 1" ,
            "Satiated Jewel 1" , "Gobbler Jewel 1" , "Grinder Jewel 1" , "Bomber Jewel 1" , "Fungiform Jewel 1" , "Angler Jewel 1" , "Chef Jewel 1" , "Transporter Jewel 1" , "Gathering Jewel 1" ,
            "Honeybee Jewel 1" , "Carver Jewel 1" , "Protection Jewel 1" , "Meowster Jewel 1" , "Botany Jewel 1" , "Geology Jewel 1" , "Mighty Jewel 2" , "Stonethrower Jewel 1" , "Tip Toe Jewel 1" ,
            "Brace Jewel 3" , "Scoutfly Jewel 1" , "Crouching Jewel 1" , "Longjump Jewel 1" , "Smoke Jewel 15" , "Mirewalker Jewel 1" , "Climber Jewel 1" , "Radiosity Jewel 1" , "Research Jewel 1" ,
            "Specimen Jewel 1" , "Miasma Jewel 1" , "Scent Jewel 1" , "Slider Jewel 1" , "Intimidator Jewel 1" , "Hazmat Jewel 1" , "Mudshield Jewel 1" , "Element Resist Jewel 1" , "Slider Jewel 2" ,
            "Medicine Jewel 1" , "Forceshot Jewel 3" , "Pierce Jewel 3" , "Spread Jewel 3" , "Enhancer Jewel 2" , "Crisis Jewel 1" , "Dragonseal Jewel 3" , "Discovery Jewel 2" , "Detector Jewel 1" ,
            "Maintenance Jewel 1" , "Vigorwasp Delivery" , "Vigorwasp Station " , "Flashfly Cage  " , "Thunderbug Cage" , "Shieldspire Taunt" , "Shieldspire Bash " , "Coral Cheerhorn" , "Coral Cheerbongo" ,
            "Plunderblade" , "Palarang" , "Meowlotov Assault" , "Rath-of-Meow" , "Invalid Message" , "Invalid Message" , "Invalid Message" , "Invalid Message" , "Stone " , "Redpit" , "Brightmoss" ,
            "Scatternut" , "Torch Pod " , "Bomb Pod  " , "Thorn Pod " , "Piercing Pod" , "Dragon Pod  " , "Crystalburst" , "Puddle Pod  " , "Chillshroom " , "Tailraider Voucher" , "Emerald Shell" ,
            "Gajalaka Sketch" , "Mighty Bow Jewel 2" , "Mind's Eye Jewel 2" , "Shield Jewel 2" , "Sharp Jewel 2" , "Elementless Jewel 2" , "Deviljho Scale" , "Deviljho Hide " , "Deviljho Tallfang" ,
            "Deviljho Talon " , "Deviljho Scalp " , "Deviljho Tail  " , "Deviljho Saliva" , "Deviljho Gem   " , "Kulve Taroth Golden Scale " , "Kulve Taroth Golden Shell " , "Kulve Taroth Golden Nugget" ,
            "Kulve Taroth Golden Spiralhorn" , "Kulve Taroth Golden Tailshell " , "Kulve Taroth Golden Glimstone " , "Golden Fragment" , "Golden Chunk   " , "Lunastra Scale + " , "Lunastra Wing  " ,
            "Lunastra Gem   " , "Lunastra Carapace" , "Lunastra Mane" , "Lunastra Tail" , "Lunastra Horn" , "Behemoth Mane" , "Behemoth Bone" , "Behemoth Great Horn" , "Behemoth Shearclaw " , "Behemoth Tail  " ,
            "Aetheryte Shard" , "Spring Blossom Ticket" , "Summer Twilight Ticket" , "Autumn Harvest Ticket" , "Winter Star Ticket " , "Appreciation Ticket" , "Spring Insect Field Guide" ,
            "Summer Insect Field Guide" , "Vaal Hazak Ticket" , "Kirin Ticket" , "Teostra Ticket" , "Kushala Daora Ticket" , "Unavailable" , "Zorah Magdaros Ticket " , "Xeno'jiiva Ticket" ,
            "Black Bandage" , "Black Crystal Ticket" , "Kulu-Ya-Ku Ticket" , "Wiggler Ticket " , "unknown" , "unknown" , "Mega Man Ticket" , "Unknown" , "Unknown" , "Unknown" , "Azure Star Shard" ,
            "Azure Stargem" , "Red Orb" , "Master Craftsman's Blueprint" , "Dissolved Weapon" , "Melded Weapon" , "Sublimated Weapon" , "Bushi Ticket" , "Blossom Fireworks " , "Twilight Fireworks" ,
            "Harvest Fireworks " , "Star Fireworks" , "Appreciation Fireworks" , "Gold Scalebat" , "Gold Helmcrab" , "Shiny Gold Helmcrab " , "Copper Calappa" , "Gold Calappa  " , "Tsuchinoko" , "Cactuar" ,
            "Gold Chip Fragment" , "Gold Nugget Fragment" , "Unavailable" , "Unknown" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Downy Crake Ticket" , "Bristly Crake Ticket" ,
            "First Fleet Ticket" , "Fifth Fleet Ticket" , "Lunastra Ticket" , "Transparent Stone" , "Flowering Cactuar Cutting" , "Cactuar" , "Faux Ticket" , "Faux Ticket II" , "Faux Ticket III" , "Beetle Ticket" ,
            "Invalid Message" , "Unavailable" , "Glamour Prism" , "Unknown" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" ,
            "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" ,
            "Unavailable" , "Unavailable" , "Unavailable" , "Unavailable" , "Unknown" };
        public string[] MapNames = {"Ancient Forest Incomplete" , "Ancient Forest" , "Wildspire Waste" , "Coral Highlands" ,
            "Rotten Vale" , "Elder Recess" , "Great Ravine" , "Great Ravine (Story Map to Coral Highlands)" , "Infinity of Nothing" ,
            "Special Arena" , "Arena (Challenge)" , "Astera" , "Gathering Hub" , "Research Base" , "Crashes game" , "Ancient Forest (Flooded / Intro)" ,
            "Everstream" , "Confluence of Fates" , "Ancient Forest (Tutorial)" , "Infinity of Nothing" , "Debug Map" , "Caverns of El Dorado" ,
            "Living Quarters" , "Private Quarters" , "Suite" , "Training Camp" , "Chamber of Five" };
        public string[] TimeList = {"Match universal", "Start late in the night", "Start at dawn", "Start early in the day",
            "Start at noon", "Start late in the day", "Start at dusk", "Start early in the night",
            "Start at midnight", "Pause day/night cicle once the quest has been initiated"};
        public string[] WeatherList = { "Random", "Disable Weather", "Always Weather A", "Always Weather B" };
        public string[] IconList = { "Anjanath" , "Great Jagras" , "Pukei-Pukei" , "Nergigante" , "Xeno'jiiva" ,
            "Xeno'jiiva" , "Zorah Magdaros" , "Kulu-Ya-Ku" , "Tzitzi-Ya-Ka" , "Jyuratodus" , "Tobi-Kadachi" ,
            "Paolumu" , "Legiana" , "Great Girros" , "Odogaron" , "Radobaan" , "Vaal Hazak" , "Dodogama" ,
            "Kulve Tarroth" , "Bazelgeuse" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "Rathian" , "Pink Rathian" ,
            "Rathalos" , "Azure Rathalos" , "Diablos" , "Black Diablos" , "Kirin" , "EMPTY" , "Kushala Daora" ,
            "Lunastra" , "Teostra" , "Lavasioth" , "UNIDENTIFIED" , "Barroth" , "Uragaan" , "EMPTY" , "EMPTY" ,
            "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "Skull(Monster near death)" ,
            "Hungry / Exhausted(Monster)" , "EMPTY" , "Gajalaka" , "Palico" , "Grymalkine" , "'Hunt' icon" ,
            "'unknown monster' icon" , "UNIDENTIFIED" , "UNIDENTIFIED small scoutly icon ? " , "bigger scoutfly icon" ,
            "number 3 as seen when multiple monsters of the same kind appear" , "number 4" , "number 5" , "number 1" ,
            "number 2" , "Palico / Grymalkine trap marker" , "EMPTY" , "Egg Delivery" , "Delivery" , "EMPTY" , "EMPTY" ,
            "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" ,
            "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "Aptonoth" , "Apceros" , "Kelbi 1" , "Kelbi 2" ,
            "Mosswine" , "Hornetaur" , "Vespoid" , "Gajau" , "Jagras" , "Mernos" , "Kestodon 1" , "Kestodon 2" , "Raphinos" , "Shamos" ,
            "Barnos" , "Girros" , "EMPTY" , "Gastodon" , "Noios" , "EMPTY" , "Magmacore ? " , "broken pillar" , "Barrel 1" , "Barrel 2" ,
            "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" , "EMPTY" ,
            "EMPTY" , "EMPTY" , "EMPTY" , "-- - ? " , "NO ICON" };
        public string[] QuestTypeList = {"Hunting quest (Table spawn)","Slaying quest (Table spawn)","Capture quest (Table spawn)",
            "Delivery quest (Table spawn)","Hunting quest (Sequential spawn)","Special quest (Table spawn)"};
        public string[] SeqSpawnRules = { "Force spawn", "?", "?", "Spawn if the map is empty", "?", "Retarded spawn", "Block spawn" };
        public string[] ObjectiveList = { "None", "Multi monster quest", "Deliver", "Capture", "Slay", "Hunt" };
        public string[] HRRqlst = { "29", "30", "49", "50", "100" };
        #endregion
        #region Monster Stats Strings
        public string[] MonsterNames = { "None", "Anjanath", "Rathalos", "[s] Aptonoth", "[s] Jagras", "Zorah Magdaros",
            "[s] Mosswine", "[s] Gajau", "Great Jagras", "[s] Kestodon M", "Rathian", "Pink Rathian", "Azure Rathalos",
            "Diablos", "Black Diablos", "Kirin", "Behemoth", "Kushala Daora", "Lunastra", "Teostra", "Lavasioth", "Deviljho",
            "Barroth", "Uragaan", "Leshen", "Pukei-Pukei", "Nergigante", "Xeno'Jiiva", "Kulu-Ya-Ku", "Tzitzi-Ya-Ku", "Jyuratodus",
            "Tobi-Kadachi", "Paolumu", "Legiana", "Great Girros", "Odogaron", "Radobaan", "Vaal Hazak", "Dodogama", "Kulve Taroth",
            "Bazelgeuse", "[s] Apceros", "[s] Kelbi M", "[s[ Kelbi F", "[s] Hornetaur", "[s] Vespoid", "[s] Mernos", "[s] Kestodon F",
            "[s] Raphinos", "[s] Shamos", "[s] Barnos", "[s] Girros", "Ancient Leshen", "[s] Gastodon", "[s] Noios", "[s] Magmacore 1",
            "[s] Magmacore 2", "[s] Gajalaka", "[s] Small Barrel", "[s] Large Barrel", "[s] Training Pole", "NON-VALID", "(???) Rathian",
            "[s] Golden tribe", "unknown 3f"};
        public string[] MonsterHP = { "10%", "13%", "16%", "19%", "22%", "25%", "28%", "31%", "34%", "37%", "40%", "43%", "46%", "49%", "52%", "55%", "58%", "61%", "64%", "67%", "70%", "73%", "76%", "79%", "82%", "85%", "88%", "91%", "94%", "97%", "100%", "103%", "106%", "109%", "112%", "115%", "118%", "121%", "124%", "127%", "130%", "133%", "136%", "139%", "142%", "145%", "148%", "151%", "154%", "157%", "160%", "163%", "166%", "169%", "172%", "175%", "178%", "181%", "184%", "187%", "190%", "193%", "196%", "199%", "202%", "205%", "208%", "211%", "214%", "217%", "220%", "223%", "226%", "229%", "232%", "235%", "238%", "241%", "244%", "247%", "250%", "260%", "270% ", "280%", "290%", "300%", "310%", "320%", "330%", "340%", "350%", "360%", "500%", "335%", "390%", "395%", "370%", "450%", "450%", "745 %" };
        public string[] MonsterAt = { "50%", "65%", "70%", "75%", "85%", "90%", "92%", "94%", "96%", "98%", "100%", "102%", "104%", "106%", "108%", "110%", "112%", "114%", "116%", "118%", "120%", "123%", "126%", "129%", "132%", "135%", "138%", "141%", "144%", "147%", "150%", "155%", "160%", "165%", "170%", "175%", "180%", "185%", "190%", "195%", "200%", "205%", "210%", "215%", "220%", "225%", "230%", "235%", "240%", "245%", "250%", "255%", "260%", "265%", "270%", "275%", "280%", "285%", "290%", "295%", "300%", "305%", "310%", "315%", "320%", "325%", "330%", "335%", "340%", "345%", "350%", "360%", "370%", "380%", "390%", "400%", "410%", "420%", "430%", "440%", "450%", "460%", "470%", "480%", "490%", "500%", "510%", "520%", "530%", "540%", "550%", "560%", "900%", "761%", "760%", "755%", "780%", "760%", "770%", "800%" };
        public string[] MonsterDe = { "70%", "75%", "80%", "85%", "90%", "95%", "100%", "105%", "110%", "115%", "120%", "125%", "130%", "135%", "140%", "145%", "150%", "155%", "160%", "165%", "170%", "175%", "180%", "185%", "190%", "195%", "200%", "205%", "210%", "215%", "220%", "225%", "230%", "235%", "240%", "245%", "250%", "255%", "260%", "265%", "270%", "275%", "280%", "285%", "290%", "295%", "300%", "305%", "310%", "315%", "320%", "325%", "330%", "335%", "340%", "345%", "350%", "355%", "360%", "365%", "370%", "375%", "380%", "385%", "390%", "395%", "400%", "405%", "410%", "415%", "420%", "425%", "430%", "435%", "440%", "445%", "450%", "455%", "460%", "465%", "470%", "475%", "480%", "485%", "490%", "495%", "500%", "300%", "300%", "300%", "300%", "300%", "100%", "100%", "100%", "100%", "100%", "100%", "100%", "100%" };
        public string[] MonsterPa = { "70%,", "75%,", "80%,", "85%,", "90%,", "95%,", "100%,", "110%,", "120%,", "130%,", "135%,", "140%,", "145%,", "150%,", "155%,", "160%,", "165%,", "170%,", "175%,", "180%,", "185%,", "190%,", "195%,", "200%,", "205%,", "210%,", "215%,", "220%,", "225%,", "230%,", "235%,", "240%,", "245%,", "250%,", "255%,", "260%,", "265%,", "270%,", "275%,", "280%,", "285%,", "290%,", "295%,", "300%,", "305%,", "310%,", "315%,", "320%,", "325%,", "330%,", "335%,", "340%,", "345%,", "350%,", "355%,", "360%,", "365%,", "370%,", "375%,", "380%,", "385%,", "390%,", "395%,", "400%,", "405%,", "410%,", "415%,", "420%,", "425%,", "430%,", "435%,", "440%,", "445%,", "450%,", "455%,", "460%,", "465%,", "470%,", "475%,", "480%,", "485%,", "490%,", "495%,", "500%,", "505%,", "510%,", "515%,", "520%,", "525%,", "530%,", "545%,", "540%,", "1000%,", "298%,", "330%,", "320%,", "350%,", "360%,", "340%,", "435 %" };
        public string[] MonsterSt = { "70%", "75%", "80%", "85%", "90%", "95%", "100%", "105%", "110%", "115%", "120%", "125%", "130%", "135%", "140%", "145%", "150%", "155%", "160%", "165%", "170%", "175%", "180%", "185%", "190%", "195%", "200%", "205%", "210%", "215%", "220%", "225%", "230%", "235%", "240%", "245%", "250%", "255%", "260%", "265%", "270%", "275%", "280%", "285%", "290%", "295%", "300%", "305%", "310%", "315%", "320%", "325%", "330%", "335%", "340%", "345%", "350%", "355%", "360%", "365%", "370%", "375%", "380%", "385%", "390%", "395%", "400%", "405%", "410%", "415%", "420%", "425%", "430%", "435%", "440%", "445%", "450%", "455%", "460%", "465%", "470%", "475%", "480%", "485%", "490%", "495%", "500%", "300%", "300%", "300%", "300%", "300%", "300%", "130%", "140%", "140%", "140%", "140%", "140%", "140%" };
        public string[] MonsterSp = { "70%", "75%", "80%", "85%", "90%", "95%", "100%", "105%", "110%", "115%", "120%", "125%", "130%", "135%", "140%", "145%", "150%", "155%", "160%", "165%", "170%", "175%", "180%", "185%", "190%", "195%", "200%", "205%", "210%", "215%", "220%", "225%", "230%", "235%", "240%", "245%", "250%", "255%", "260%", "265%", "270%", "275%", "280%", "285%", "290%", "295%", "300%", "305%", "310%", "315%", "320%", "325%", "330%", "335%", "340%", "345%", "350%", "355%", "360%", "365%", "370%", "375%", "380%", "385%", "390%", "395%", "400%", "405%", "410%", "415%", "420%", "425%", "430%", "435%", "440%", "445%", "450%", "455%", "460%", "465%", "470%", "475%", "480%", "485%", "490%", "495%", "500%", "300%", "300%", "300%", "300%", "300%", "300%", "170%", "170%", "170%", "170%", "170%", "170%", "170%" };
        public string[] MonsterKO = { "70%", "75%", "80%", "85%", "90%", "95%", "100%", "105%", "110%", "115%", "120%", "125%", "130%", "135%", "140%", "145%", "150%", "155%", "160%", "165%", "170%", "175%", "180%", "185%", "190%", "195%", "200%", "205%", "210%", "215%", "220%", "225%", "230%", "235%", "240%", "245%", "250%", "255%", "260%", "265%", "270%", "275%", "280%", "285%", "290%", "295%", "300%", "305%", "310%", "315%", "320%", "325%", "330%", "335%", "340%", "345%", "350%", "355%", "360%", "365%", "370%", "375%", "380%", "385%", "390%", "395%", "400%", "405%", "410%", "415%", "420%", "425%", "430%", "435%", "440%", "445%", "450%", "455%", "460%", "465%", "470%", "475%", "480%", "485%", "490%", "495%", "500%", "300%", "300%", "300%", "300%", "300%", "300%", "130%", "120%", "110%", "120%", "120%", "110%", "125%" };
        public string[] MonsterEx = { "70%", "75%", "80%", "85%", "90%", "95%", "100%", "105%", "110%", "115%", "120%", "125%", "130%", "135%", "140%", "145%", "150%", "155%", "160%", "165%", "170%", "175%", "180%", "185%", "190%", "195%", "200%", "205%", "210%", "215%", "220%", "225%", "230%", "235%", "240%", "245%", "250%", "255%", "260%", "265%", "270%", "275%", "280%", "285%", "290%", "295%", "300%", "305%", "310%", "315%", "320%", "325%", "330%", "335%", "340%", "345%", "350%", "355%", "360%", "365%", "370%", "375%", "380%", "385%", "390%", "395%", "400%", "405%", "410%", "415%", "420%", "425%", "430%", "435%", "440%", "445%", "450%", "455%", "460%", "465%", "470%", "475%", "480%", "485%", "490%", "495%", "500%", "300%", "300%", "300%", "300%", "300%", "300%", "180%", "200%", "200%", "200%", "200%", "200%", "200%" };
        public string[] MonsterMo = { "70%", "75%", "80%", "85%", "90%", "95%", "100%", "105%", "110%", "115%", "120%", "125%", "130%", "135%", "140%", "145%", "150%", "155%", "160%", "165%", "170%", "175%", "180%", "185%", "190%", "195%", "200%", "205%", "210%", "215%", "220%", "225%", "230%", "235%", "240%", "245%", "250%", "255%", "260%", "265%", "270%", "275%", "280%", "285%", "290%", "295%", "300%", "305%", "310%", "315%", "320%", "325%", "330%", "335%", "340%", "345%", "350%", "355%", "360%", "365%", "370%", "375%", "380%", "385%", "390%", "395%", "400%", "405%", "410%", "415%", "420%", "425%", "430%", "435%", "440%", "445%", "450%", "455%", "460%", "465%", "470%", "475%", "480%", "485%", "490%", "495%", "500%", "300%", "300%", "300%", "300%", "300%", "300%", "180%", "200%", "200%", "200%", "200%", "200%", "200%" };
        #endregion
        private Cipher cipher;
        public byte[] data;
        public byte[] data2;
        public byte[] data3;
        public byte[] data4;
        public byte[] SaveAsData;
        private readonly string key = "TZNgJfzyD2WKiuV4SglmI6oN5jP2hhRJcBwzUooyfIUTM4ptDYGjuRTP";
        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog sfd = new SaveFileDialog();
        public MainWindow()
        {
            InitializeComponent();
          //  SpawnHelp.Text = "Help and Spawn Notes (mostly from NekotagaYuhatora's comments on the Template): " +
          //      "First and foremost, the quest only uses one of the two options, the Sequential Spawn Rules or the Table Spawn Rules depending on Quest Type, which can be found in " +
          //      "the Common Tab. You can basically ignore the other one or ideally zero all the values in it. The Global Spawn Delay, as the name indicates, is shared between the two. " +
          //      "The Sequential Spawn Rules options are all self explanatory, even if some are not fully understood or explored. The Table Spawn Rules need a bit more explaining, however. " +
          //      "The two deciders, for ";
            ComboBox[] MID = { M1ID, M2ID, M3ID, M4ID, M5ID, M6ID, M7ID };
            TextBox[] MSobj = { M1sobjID, M2sobjID, M3sobjID, M4sobjID, M5sobjID, M6sobjID, M7sobjID };
            TextBox[] MonsterSize = { M1Siz, M2Siz, M3Siz, M4Siz, M5Siz, M6Siz, M7Siz };
            ComboBox[] MHtP = { M1HtP, M2HtP, M3HtP, M4HtP, M5HtP, M6HtP, M7HtP };
            ComboBox[] MAtk = { M1Atk, M2Atk, M3Atk, M4Atk, M5Atk, M6Atk, M7Atk };
            ComboBox[] MDef = { M1Def, M2Def, M3Def, M4Def, M5Def, M6Def, M7Def };
            ComboBox[] MHAR = { M1HAR, M2HAR, M3HAR, M4HAR, M5HAR, M6HAR, M7HAR };
            ComboBox[] MSeT = { M1SeT, M2SeT, M3SeT, M4SeT, M5SeT, M6SeT, M7SeT };
            ComboBox[] MPHP = { M1PHP, M2PHP, M3PHP, M4PHP, M5PHP, M6PHP, M7PHP };
            ComboBox[] MBSt = { M1BSt, M2BSt, M3BSt, M4BSt, M5BSt, M6BSt, M7BSt };
            ComboBox[] MStB = { M1StB, M2StB, M3StB, M4StB, M5StB, M6StB, M7StB };
            ComboBox[] MBKO = { M1BKO, M2BKO, M3BKO, M4BKO, M5BKO, M6BKO, M7BKO };
            ComboBox[] MBEx = { M1BEx, M2BEx, M3BEx, M4BEx, M5BEx, M6BEx, M7BEx };
            ComboBox[] MBMo = { M1BMo, M2BMo, M3BMo, M4BMo, M5BMo, M6BMo, M7BMo };
            ComboBox[] MSSpw = { M1SSpw, M2SSpw, M3SSpw, M4SSpw, M5SSpw };
            ComboBox[] MonIcons = { Icon1, Icon2, Icon3, Icon4, Icon5 };
            ComboBox[] SmlMonIcons = { SmlIcon1, SmlIcon2, SmlIcon3, SmlIcon4, SmlIcon5 };
            ComboBox[] MapIcons = { MapIcon01, MapIcon02, MapIcon03, MapIcon04, MapIcon05,
                MapIcon06, MapIcon07, MapIcon08, MapIcon09, MapIcon10, MapIcon11, MapIcon12,
                MapIcon13, MapIcon14, MapIcon15, MapIcon16, MapIcon17, MapIcon18, MapIcon19,
                MapIcon20, MapIcon21, MapIcon22, MapIcon23, MapIcon24, MapIcon25, MapIcon26,
                MapIcon27, MapIcon28, MapIcon29, MapIcon30, MapIcon31, MapIcon32, MapIcon33,
                MapIcon34, MapIcon35, MapIcon36, MapIcon37, MapIcon38, MapIcon39, MapIcon40,
                MapIcon41, MapIcon42, MapIcon43, MapIcon44, MapIcon45, MapIcon46, MapIcon47,
                MapIcon48, MapIcon49, MapIcon50, MapIcon51 };
            #region Common and objective
            for (int i = 0; i < 16; i++)
                Stars.Items.Add(i.ToString() + "*");
            Rank.Items.Add("Low Rank");
            Rank.Items.Add("High Rank");
            for (int i = 0; i < MapNames.Length; i++)
                Map.Items.Add(MapNames[i]);
            PSpawn.Items.Add("Forced (Camp 1)");
            PSpawn.Items.Add("Choose (With Drunk Bird)");
            PSpawn.Items.Add("Choose (No Drunk Bird)");
            for (int i = 0; i < TimeList.Length; i++)
                Time.Items.Add(TimeList[i]);
            for (int i = 0; i < WeatherList.Length; i++)
                Weather.Items.Add(WeatherList[i]);
            HRReq.Items.Add("None");
            for (int i = 1; i < 17; i++)
                HRReq.Items.Add(i.ToString());
            for (int i = 0; i < HRRqlst.Length; i++)
                HRReq.Items.Add(HRRqlst[i]);
            for (int i = 0; i < 3; i++)
                BGM.Items.Add("Default");
            BGM.Items.Add("None/Khezu Theme?");
            BGM.Items.Add("Extreme Behemoth?");
            for (int i = 5; i < 21; i++)
                BGM.Items.Add("Default?");
            BGM.Items.Add("A Rush of Blood/MM music");
            for (int i = 22; i < 24; i++)
                BGM.Items.Add("Default? ");
            BGM.Items.Add("Code:Red/DMC Music");
            for (int i = 25; i < 99; i++)
                BGM.Items.Add("Default?");
            for (int i = 0; i < MonIcons.Length; i++)
                for (int j = 0; j < IconList.Length; j++)
                    MonIcons[i].Items.Add(IconList[j]);
            for (int i = 0; i < QuestTypeList.Length; i++)
                QType.Items.Add(QuestTypeList[i]);
            for (int i = 1; i < 5; i++)
                NPlayers.Items.Add(i.ToString());
            for (int i = 0; i < ObjectiveList.Length; i++)
            {
                MObjT1.Items.Add(ObjectiveList[i]);
                MObjT2.Items.Add(ObjectiveList[i]);
            }
            #endregion
            #region Monsters, Spawn, and Map Icons
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < MonsterNames.Length; j++)
                    MID[i].Items.Add(MonsterNames[j]);

                for (int j = 0; j < 100; j++)
                {
                    MHtP[i].Items.Add(MonsterHP[j]);
                    MAtk[i].Items.Add(MonsterAt[j]);
                    MDef[i].Items.Add(MonsterDe[j]);
                    MHAR[i].Items.Add(j.ToString());
                    MSeT[i].Items.Add(j.ToString());
                    MPHP[i].Items.Add(MonsterPa[j]);
                    MBSt[i].Items.Add(MonsterSt[j]);
                    MStB[i].Items.Add(MonsterSp[j]);
                    MBKO[i].Items.Add(MonsterKO[j]);
                    MBEx[i].Items.Add(MonsterEx[j]);
                    MBMo[i].Items.Add(MonsterMo[j]);
                }
            }
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < SeqSpawnRules.Length; j++)
                    MSSpw[i].Items.Add(SeqSpawnRules[j]);
            for (int i = 0; i < SmlMonIcons.Length; i++)
            {
                SmlMonIcons[i].Items.Add("None");
                for (int j = 0; j < IconList.Length; j++)
                    SmlMonIcons[i].Items.Add(IconList[j]);
            }
            for (int i = 0; i < MapIcons.Length; i++)
                for (int j = 0; j < 255; j++)
                    MapIcons[i].Items.Add("Unknown #" + j.ToString());
            #endregion
            }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            cipher = new Cipher(key);
            ofd.Filter = "MHW Quest File|*.mib";
            if (ofd.ShowDialog() == true)
            {
                SaveM.IsEnabled = true;
                SaveAs.IsEnabled = true;
            }
            #region Defenitions
            ComboBox[] MID = { M1ID, M2ID, M3ID, M4ID, M5ID, M6ID, M7ID };
            TextBox[] MSobj = { M1sobjID, M2sobjID, M3sobjID, M4sobjID, M5sobjID, M6sobjID, M7sobjID };
            CheckBox[] Tempered = { M1TFlag, M2TFlag, M3TFlag, M4TFlag, M5TFlag, M6TFlag, M7TFlag };
            TextBox[] MonsterSize = { M1Siz, M2Siz, M3Siz, M4Siz, M5Siz, M6Siz, M7Siz };
            ComboBox[] MHtP = { M1HtP, M2HtP, M3HtP, M4HtP, M5HtP, M6HtP, M7HtP };
            ComboBox[] MAtk = { M1Atk, M2Atk, M3Atk, M4Atk, M5Atk, M6Atk, M7Atk };
            ComboBox[] MDef = { M1Def, M2Def, M3Def, M4Def, M5Def, M6Def, M7Def };
            ComboBox[] MHAR = { M1HAR, M2HAR, M3HAR, M4HAR, M5HAR, M6HAR, M7HAR };
            ComboBox[] MSeT = { M1SeT, M2SeT, M3SeT, M4SeT, M5SeT, M6SeT, M7SeT };
            ComboBox[] MPHP = { M1PHP, M2PHP, M3PHP, M4PHP, M5PHP, M6PHP, M7PHP };
            ComboBox[] MBSt = { M1BSt, M2BSt, M3BSt, M4BSt, M5BSt, M6BSt, M7BSt };
            ComboBox[] MStB = { M1StB, M2StB, M3StB, M4StB, M5StB, M6StB, M7StB };
            ComboBox[] MBKO = { M1BKO, M2BKO, M3BKO, M4BKO, M5BKO, M6BKO, M7BKO };
            ComboBox[] MBEx = { M1BEx, M2BEx, M3BEx, M4BEx, M5BEx, M6BEx, M7BEx };
            ComboBox[] MBMo = { M1BMo, M2BMo, M3BMo, M4BMo, M5BMo, M6BMo, M7BMo };
            ComboBox[] MSSpw = { M1SSpw, M2SSpw, M3SSpw, M4SSpw, M5SSpw };
            ComboBox[] MonIcons = { Icon1, Icon2, Icon3, Icon4, Icon5 };
            ComboBox[] SmlMonIcons = { SmlIcon1, SmlIcon2, SmlIcon3, SmlIcon4, SmlIcon5 };
            ComboBox[] MapIcons = { MapIcon01, MapIcon02, MapIcon03, MapIcon04, MapIcon05,
                MapIcon06, MapIcon07, MapIcon08, MapIcon09, MapIcon10, MapIcon11, MapIcon12,
                MapIcon13, MapIcon14, MapIcon15, MapIcon16, MapIcon17, MapIcon18, MapIcon19,
                MapIcon20, MapIcon21, MapIcon22, MapIcon23, MapIcon24, MapIcon25, MapIcon26,
                MapIcon27, MapIcon28, MapIcon29, MapIcon30, MapIcon31, MapIcon32, MapIcon33,
                MapIcon34, MapIcon35, MapIcon36, MapIcon37, MapIcon38, MapIcon39, MapIcon40,
                MapIcon41, MapIcon42, MapIcon43, MapIcon44, MapIcon45, MapIcon46, MapIcon47,
                MapIcon48, MapIcon49, MapIcon50, MapIcon51 };
            TextBox[] SpawnText = { M6SpC, SpawnDelay, M7SpC, M3SpC, M4SpC, M5SpC, M6SpD, M7SpD };
            TextBox[] ArenaText = { RankA, RankB, RankC, FenCD, FenUT };
            #endregion
            data = null;
            data2 = null;
            data = (File.ReadAllBytes(ofd.FileName));
            data2 = cipher.Decipher(data);
            Int32 RV = 0;
            #region Common and Objectives
            QID.Text = BitConverter.ToInt32(new byte[] { data2[6], data2[7], data2[8], data2[9] }, 0).ToString();
            Stars.SelectedIndex = data2[10];
            Rank.SelectedIndex = data2[19];
            RV = BitConverter.ToInt32(new byte[] { data2[23], data2[24], data2[25], data2[26] }, 0);
            for (int i = 0; i < MapIDs.Length; i++)
                if (RV == MapIDs[i])
                    Map.SelectedIndex = i;
            PSpawn.SelectedIndex = data2[27];
            if (data2[31] == 0)
                FSpawn.IsChecked = false;
            else FSpawn.IsChecked = true;
            Weather.SelectedIndex = data2[43];
            Reward.Text = BitConverter.ToUInt32(new byte[] { data2[51], data2[52], data2[53], data2[54] }, 0).ToString();
            Penalty.Text = BitConverter.ToUInt32(new byte[] { data2[55], data2[56], data2[57], data2[58] }, 0).ToString();
            Timer.Text = BitConverter.ToUInt32(new byte[] { data2[63], data2[64], data2[65], data2[66] }, 0).ToString();
            for (int i = 0; i < 5; i++)
                MonIcons[i].SelectedIndex = BitConverter.ToUInt16(new byte[] { data2[68 + 2 * i], data2[69 + 2 * i] }, 0);
            HRReq.SelectedIndex = data2[78];
            for (int i = 0; i < ObjectiveIDs.Length; i++)
                if (data2[83] == ObjectiveIDs[i])
                    MObjT1.SelectedIndex = i;
            if (data2[84] == 04)
                MObj1MM.IsChecked = true;
            else MObj1MM.IsChecked = false;
            MObjID1.SelectedIndex = BitConverter.ToUInt16(new byte[] { data2[87], data2[88] }, 0);
            MObjC1.Text = BitConverter.ToUInt16(new byte[] { data2[89], data2[90] }, 0).ToString();
            for (int i = 0; i < ObjectiveIDs.Length; i++)
                if (data2[91] == ObjectiveIDs[i])
                    MObjT2.SelectedIndex = i;
            if (data2[92] == 04)
                MObj2MM.IsChecked = true;
            else MObj2MM.IsChecked = false;
            MObjID2.SelectedIndex = BitConverter.ToUInt16(new byte[] { data2[95], data2[96] }, 0);
            MObjC2.Text = BitConverter.ToUInt16(new byte[] { data2[97], data2[98] }, 0).ToString();
            if (data2[99] == 1)
                MultiO.IsChecked = false;
            else MultiO.IsChecked = true;
            BGM.SelectedIndex = data2[120];
            for (int i = 0; i < QuestTypeIDs.Length; i++)
                if (data2[128] == QuestTypeIDs[i])
                    QType.SelectedIndex = i;
            if (data2[130] == 0)
            {
                ATFlag.IsChecked = false;
                PSGear.IsChecked = false;
            }
            if (data2[130] == 1)
            {
                ATFlag.IsChecked = false;
                PSGear.IsChecked = true;
            }
            if (data2[130] == 2)
            {
                ATFlag.IsChecked = true;
                PSGear.IsChecked = false;
            }
            if (data2[130] == 3)
            {
                ATFlag.IsChecked = true;
                PSGear.IsChecked = true;
            }
            RRemID.Text = BitConverter.ToUInt32(new byte[] { data2[132], data2[133], data2[134], data2[135] }, 0).ToString();
            SRemID.Text = BitConverter.ToUInt32(new byte[] { data2[144], data2[145], data2[146], data2[147] }, 0).ToString();
            HRpoint.Text = BitConverter.ToUInt32(new byte[] { data2[160], data2[161], data2[162], data2[163] }, 0).ToString();
            #endregion
            #region Monsters
            for (int i = 0; i < 7; i++)
            {
                if (BitConverter.ToUInt32(new byte[] { data2[172 + 65 * i], data2[173 + 65 * i], data2[174 + 65 * i], data2[175 + 65 * i] }, 0) == 4294967295)
                    MID[i].SelectedIndex = 0;
                else MID[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[172 + 65 * i], data2[173 + 65 * i], data2[174 + 65 * i], data2[175 + 65 * i] }, 0) + 1;
                MSobj[i].Text = BitConverter.ToInt32(new byte[] { data2[176 + 65 * i], data2[177 + 65 * i], data2[178 + 65 * i], data2[179 + 65 * i] }, 0).ToString();
                if (data2[184 + 65 * i] == 0)
                    Tempered[i].IsChecked = false;
                else if (data2[184 + 65 * i] == 1)
                    Tempered[i].IsChecked = true;
                MHtP[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[185 + 65 * i], data2[186 + 65 * i], data2[187 + 65 * i], data2[188 + 65 * i] }, 0);
                MAtk[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[189 + 65 * i], data2[190 + 65 * i], data2[191 + 65 * i], data2[192 + 65 * i] }, 0);
                MDef[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[193 + 65 * i], data2[194 + 65 * i], data2[195 + 65 * i], data2[196 + 65 * i] }, 0);
                MHAR[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[197 + 65 * i], data2[198 + 65 * i], data2[199 + 65 * i], data2[200 + 65 * i] }, 0);
                MSeT[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[205 + 65 * i], data2[206 + 65 * i], data2[207 + 65 * i], data2[208 + 65 * i] }, 0);
                MPHP[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[213 + 65 * i], data2[214 + 65 * i], data2[215 + 65 * i], data2[216 + 65 * i] }, 0);
                MBSt[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[217 + 65 * i], data2[218 + 65 * i], data2[219 + 65 * i], data2[220 + 65 * i] }, 0);
                MStB[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[221 + 65 * i], data2[222 + 65 * i], data2[223 + 65 * i], data2[224 + 65 * i] }, 0);
                MBKO[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[225 + 65 * i], data2[226 + 65 * i], data2[227 + 65 * i], data2[228 + 65 * i] }, 0);
                MBEx[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[229 + 65 * i], data2[230 + 65 * i], data2[231 + 65 * i], data2[232 + 65 * i] }, 0);
                MBMo[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[233 + 65 * i], data2[234 + 65 * i], data2[235 + 65 * i], data2[236 + 65 * i] }, 0);
                MonsterSize[i].Text = BitConverter.ToInt32(new byte[] { data2[201 + 65 * i], data2[202 + 65 * i], data2[203 + 65 * i], data2[204 + 65 * i] }, 0).ToString();
            }
            sMsobj.Text = BitConverter.ToInt32(new byte[] { data2[627], data2[628], data2[629], data2[630] }, 0).ToString();
            sMHP.Text = BitConverter.ToInt32(new byte[] { data2[631], data2[632], data2[633], data2[634] }, 0).ToString();
            sMAt.Text = BitConverter.ToInt32(new byte[] { data2[635], data2[636], data2[637], data2[638] }, 0).ToString();
            sMDe.Text = BitConverter.ToInt32(new byte[] { data2[639], data2[640], data2[641], data2[642] }, 0).ToString();
            MPMod.Text = BitConverter.ToInt32(new byte[] { data2[644], data2[645], data2[646], data2[647] }, 0).ToString();
            #endregion
            #region Spawn, Map Icons, and Arena
            for (int i = 0; i < 5; i++)
                MSSpw[i].SelectedIndex = data2[652 + 4 * i];
            for (int i=0;i<SpawnText.Length;i++)
                SpawnText[i].Text= data2[672 + 4 * i].ToString();
            for (int i=0;i<MapIcons.Length;i++)
                MapIcons[i].SelectedIndex = BitConverter.ToInt32(new byte[] { data2[704 + 4 * i], data2[705 + 4 * i], data2[706 + 4 * i], data2[707 + 4 * i] }, 0);
            for (int i=0;i<5;i++)
            {
                if (data2[908 + 4 * i] == 0)
                    SmlMonIcons[i].SelectedIndex = 0;
                else SmlMonIcons[i].SelectedIndex = data2[928 + 4 * i]+1;
            }
            SetID.Text= BitConverter.ToInt32(new byte[] { data2[948], data2[949], data2[950], data2[951] }, 0).ToString();
            RV = BitConverter.ToInt32(new byte[] { data2[952], data2[952], data2[954], data2[955] }, 0);
            if (RV == 0)
                NPlayers.SelectedIndex = 3;
            else NPlayers.SelectedIndex = RV - 1;
            for (int i = 0; i < 3; i++)
                ArenaText[i].Text = BitConverter.ToInt32(new byte[] { data2[956 + 4 * i], data2[957 + 4 * i], data2[958+4*i], data2[959+4*i] }, 0).ToString();
            if (data2[980] == 128)
                Fence.IsChecked = true;
            for (int i = 0; i < 2; i++)
                ArenaText[3+i].Text = BitConverter.ToInt32(new byte[] { data2[988 + 4 * i], data2[989 + 4 * i], data2[990 + 4 * i], data2[991 + 4 * i] }, 0).ToString();
            #endregion
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            data = null;
            data3 = null;
            data4 = null;
            cipher = new Cipher(key);
            #region Defenitions
            ComboBox[] MID = { M1ID, M2ID, M3ID, M4ID, M5ID, M6ID, M7ID };
            TextBox[] MSobj = { M1sobjID, M2sobjID, M3sobjID, M4sobjID, M5sobjID, M6sobjID, M7sobjID };
            CheckBox[] Tempered = { M1TFlag, M2TFlag, M3TFlag, M4TFlag, M5TFlag, M6TFlag, M7TFlag };
            TextBox[] MonsterSize = { M1Siz, M2Siz, M3Siz, M4Siz, M5Siz, M6Siz, M7Siz };
            ComboBox[] MHtP = { M1HtP, M2HtP, M3HtP, M4HtP, M5HtP, M6HtP, M7HtP };
            ComboBox[] MAtk = { M1Atk, M2Atk, M3Atk, M4Atk, M5Atk, M6Atk, M7Atk };
            ComboBox[] MDef = { M1Def, M2Def, M3Def, M4Def, M5Def, M6Def, M7Def };
            ComboBox[] MHAR = { M1HAR, M2HAR, M3HAR, M4HAR, M5HAR, M6HAR, M7HAR };
            ComboBox[] MSeT = { M1SeT, M2SeT, M3SeT, M4SeT, M5SeT, M6SeT, M7SeT };
            ComboBox[] MPHP = { M1PHP, M2PHP, M3PHP, M4PHP, M5PHP, M6PHP, M7PHP };
            ComboBox[] MBSt = { M1BSt, M2BSt, M3BSt, M4BSt, M5BSt, M6BSt, M7BSt };
            ComboBox[] MStB = { M1StB, M2StB, M3StB, M4StB, M5StB, M6StB, M7StB };
            ComboBox[] MBKO = { M1BKO, M2BKO, M3BKO, M4BKO, M5BKO, M6BKO, M7BKO };
            ComboBox[] MBEx = { M1BEx, M2BEx, M3BEx, M4BEx, M5BEx, M6BEx, M7BEx };
            ComboBox[] MBMo = { M1BMo, M2BMo, M3BMo, M4BMo, M5BMo, M6BMo, M7BMo };
            ComboBox[] MSSpw = { M1SSpw, M2SSpw, M3SSpw, M4SSpw, M5SSpw };
            ComboBox[] MonIcons = { Icon1, Icon2, Icon3, Icon4, Icon5 };
            ComboBox[] SmlMonIcons = { SmlIcon1, SmlIcon2, SmlIcon3, SmlIcon4, SmlIcon5 };
            ComboBox[] MapIcons = { MapIcon01, MapIcon02, MapIcon03, MapIcon04, MapIcon05,
                MapIcon06, MapIcon07, MapIcon08, MapIcon09, MapIcon10, MapIcon11, MapIcon12,
                MapIcon13, MapIcon14, MapIcon15, MapIcon16, MapIcon17, MapIcon18, MapIcon19,
                MapIcon20, MapIcon21, MapIcon22, MapIcon23, MapIcon24, MapIcon25, MapIcon26,
                MapIcon27, MapIcon28, MapIcon29, MapIcon30, MapIcon31, MapIcon32, MapIcon33,
                MapIcon34, MapIcon35, MapIcon36, MapIcon37, MapIcon38, MapIcon39, MapIcon40,
                MapIcon41, MapIcon42, MapIcon43, MapIcon44, MapIcon45, MapIcon46, MapIcon47,
                MapIcon48, MapIcon49, MapIcon50, MapIcon51 };
            TextBox[] SpawnText = { M6SpC, SpawnDelay, M7SpC, M3SpC, M4SpC, M5SpC, M6SpD, M7SpD };
            TextBox[] ArenaText = { RankA, RankB, RankC, FenCD, FenUT };
            #endregion
            data = (File.ReadAllBytes(ofd.FileName));
            data3 = cipher.Decipher(data);
            #region Common and Objectives
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(QID.Text));
            data3[6] = buffer[0];
            data3[7] = buffer[1];
            data3[8] = buffer[2];
            data3[9] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToByte(Stars.SelectedIndex));
            data3[10] = buffer[0];
            buffer = BitConverter.GetBytes(Convert.ToByte(Rank.SelectedIndex));
            data3[19] = buffer[0];
            for (int i = 0; i < MapIDs.Length; i++)
            {
                if(Map.SelectedIndex==i)
                {
                    buffer = BitConverter.GetBytes(MapIDs[i]);
                    data3[23] = buffer[0];
                    data3[24] = buffer[1];
                    data3[25] = buffer[2];
                    data3[26] = buffer[3];
                }
            }
            buffer = BitConverter.GetBytes(Convert.ToByte(PSpawn.SelectedIndex));
            data3[27] = buffer[0];
            if (FSpawn.IsChecked == true)
                data3[31] = 1;
            else data3[31] = 0;
            buffer = BitConverter.GetBytes(Convert.ToByte(Weather.SelectedIndex));
            data3[43] = buffer[0];
            buffer = BitConverter.GetBytes(Convert.ToInt32(Reward.Text));
            data3[51] = buffer[0];
            data3[52] = buffer[1];
            data3[53] = buffer[2];
            data3[54] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(Penalty.Text));
            data3[55] = buffer[0];
            data3[56] = buffer[1];
            data3[57] = buffer[2];
            data3[58] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(Timer.Text));
            data3[63] = buffer[0];
            data3[64] = buffer[1];
            data3[65] = buffer[2];
            data3[66] = buffer[3];
            for (int i = 0; i < 5; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToUInt16(MonIcons[i].SelectedIndex));
                data3[68 + 2 * i] = buffer[0];
                data3[69 + 2 * i] = buffer[1];
            }
            buffer = BitConverter.GetBytes(Convert.ToByte(HRReq.SelectedIndex));
            data3[78] = buffer[0];
            for (int i = 0; i < ObjectiveIDs.Length; i++)
            {
                if (MObjT1.SelectedIndex == i)
                {
                    buffer = BitConverter.GetBytes(ObjectiveIDs[i]);
                    data3[83] = buffer[0];
                }
            }
            if (MObj1MM.IsChecked == true)
                data3[84] = 04;
            else data3[84] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID1.SelectedIndex));
            data3[87] = buffer[0];
            data3[88] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC1.Text));
            data3[89] = buffer[0];
            data3[90] = buffer[1];
            for (int i = 0; i < ObjectiveIDs.Length; i++)
            {
                if (MObjT2.SelectedIndex == i)
                {
                    buffer = BitConverter.GetBytes(ObjectiveIDs[i]);
                    data3[91] = buffer[0];
                }
            }
            if (MObj2MM.IsChecked == true)
                data3[92] = 04;
            else data3[92] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID2.SelectedIndex));
            data3[95] = buffer[0];
            data3[96] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC2.Text));
            data3[97] = buffer[0];
            data3[98] = buffer[1];
            if (MultiO.IsChecked == true)
                data3[99] = 2;
            else data3[99] = 1;
            buffer = BitConverter.GetBytes(Convert.ToByte(BGM.SelectedIndex));
            data3[120] = buffer[0];
            for (int i = 0; i < QuestTypeIDs.Length; i++)
            {
                if (QType.SelectedIndex == i)
                {
                    buffer = BitConverter.GetBytes(QuestTypeIDs[i]);
                    data3[128] = buffer[0];
                }
            }
            data3[130] = Convert.ToByte(2 * Convert.ToInt32(ATFlag.IsChecked) + Convert.ToInt32(PSGear.IsChecked));
            buffer = BitConverter.GetBytes(Convert.ToInt32(RRemID.Text));
            data3[132] = buffer[0];
            data3[133] = buffer[1];
            data3[134] = buffer[2];
            data3[135] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(SRemID.Text));
            data3[144] = buffer[0];
            data3[145] = buffer[1];
            data3[146] = buffer[2];
            data3[147] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(HRpoint.Text));
            data3[160] = buffer[0];
            data3[161] = buffer[1];
            data3[162] = buffer[2];
            data3[163] = buffer[3];
            #endregion
            #region Monsters
            for (int i = 0; i < 7; i++)
            {
                if (MID[i].SelectedIndex == 0)
                {
                    data3[172 + 65 * i] = 255;
                    data3[173 + 65 * i] = 255;
                    data3[174 + 65 * i] = 255;
                    data3[175 + 65 * i] = 255;
                }
                else
                {
                    buffer= BitConverter.GetBytes(Convert.ToInt32(MID[i].SelectedIndex-1));
                    data3[172 + 65 * i] = buffer[0];
                    data3[173 + 65 * i] = buffer[1];
                    data3[174 + 65 * i] = buffer[2];
                    data3[175 + 65 * i] = buffer[3];
                }
                buffer = BitConverter.GetBytes(Convert.ToInt32(MSobj[i].Text));
                data3[176 + 65 * i] = buffer[0];
                data3[177 + 65 * i] = buffer[1];
                data3[178 + 65 * i] = buffer[2];
                data3[179 + 65 * i] = buffer[3];
                if (Tempered[i].IsChecked == true)
                    data3[184 + 65 * i] = 1;
                else data3[184 + 65 * i] = 0;
                buffer = BitConverter.GetBytes(Convert.ToInt32(MHtP[i].SelectedIndex));
                data3[185 + 65 * i] = buffer[0];
                data3[186 + 65 * i] = buffer[1];
                data3[187 + 65 * i] = buffer[2];
                data3[188 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MAtk[i].SelectedIndex));
                data3[189 + 65 * i] = buffer[0];
                data3[190 + 65 * i] = buffer[1];
                data3[191 + 65 * i] = buffer[2];
                data3[192 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MDef[i].SelectedIndex));
                data3[193 + 65 * i] = buffer[0];
                data3[194 + 65 * i] = buffer[1];
                data3[195 + 65 * i] = buffer[2];
                data3[196 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MHAR[i].SelectedIndex));
                data3[197 + 65 * i] = buffer[0];
                data3[198 + 65 * i] = buffer[1];
                data3[199 + 65 * i] = buffer[2];
                data3[200 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MSeT[i].SelectedIndex));
                data3[205 + 65 * i] = buffer[0];
                data3[206 + 65 * i] = buffer[1];
                data3[207 + 65 * i] = buffer[2];
                data3[208 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MPHP[i].SelectedIndex));
                data3[213 + 65 * i] = buffer[0];
                data3[214 + 65 * i] = buffer[1];
                data3[215 + 65 * i] = buffer[2];
                data3[216 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBSt[i].SelectedIndex));
                data3[217 + 65 * i] = buffer[0];
                data3[218 + 65 * i] = buffer[1];
                data3[219 + 65 * i] = buffer[2];
                data3[220 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MStB[i].SelectedIndex));
                data3[221 + 65 * i] = buffer[0];
                data3[222 + 65 * i] = buffer[1];
                data3[223 + 65 * i] = buffer[2];
                data3[224 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBKO[i].SelectedIndex));
                data3[225 + 65 * i] = buffer[0];
                data3[226 + 65 * i] = buffer[1];
                data3[227 + 65 * i] = buffer[2];
                data3[228 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBEx[i].SelectedIndex));
                data3[229 + 65 * i] = buffer[0];
                data3[230 + 65 * i] = buffer[1];
                data3[231 + 65 * i] = buffer[2];
                data3[232 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBMo[i].SelectedIndex));
                data3[233 + 65 * i] = buffer[0];
                data3[234 + 65 * i] = buffer[1];
                data3[235 + 65 * i] = buffer[2];
                data3[236 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MonsterSize[i].Text));
                data3[201 + 65 * i] = buffer[0];
                data3[202 + 65 * i] = buffer[1];
                data3[203 + 65 * i] = buffer[2];
                data3[204 + 65 * i] = buffer[3];
            }

            buffer = BitConverter.GetBytes(Convert.ToInt32(sMsobj.Text));
            data3[627] = buffer[0];
            data3[628] = buffer[1];
            data3[629] = buffer[2];
            data3[630] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMHP.Text));
            data3[631] = buffer[0];
            data3[632] = buffer[1];
            data3[633] = buffer[2];
            data3[634] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMAt.Text));
            data3[635] = buffer[0];
            data3[636] = buffer[1];
            data3[637] = buffer[2];
            data3[638] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMDe.Text));
            data3[639] = buffer[0];
            data3[640] = buffer[1];
            data3[641] = buffer[2];
            data3[642] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(MPMod.Text));
            data3[644] = buffer[0];
            data3[645] = buffer[1];
            data3[646] = buffer[2];
            data3[647] = buffer[3];
            #endregion
            #region Spawn, Map Icons, and Arena
            for (int i = 0; i < 5; i++)
                data3[652 + 4 * i] = Convert.ToByte(MSSpw[i].SelectedIndex);
            for (int i = 0; i < SpawnText.Length; i++)
                data3[672 + 4 * i] = Convert.ToByte(SpawnText[i].Text);
            for (int i = 0; i < MapIcons.Length; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(MapIcons[i].SelectedIndex));
                data3[704 + 4 * i] = buffer[0];
                data3[705 + 4 * i] = buffer[1];
                data3[706 + 4 * i] = buffer[2];
                data3[707 + 4 * i] = buffer[3];
            }
            for (int i = 0; i < 5; i++)
            {
                if (SmlMonIcons[i].SelectedIndex == 0)
                {
                    data3[908 + 4 * i] = 0;
                    data3[928 + 4 * i] = 0;
                }
                else
                    data3[908 + 4 * i] = Convert.ToByte(SmlMonIcons[i].SelectedIndex);
            }
            buffer = BitConverter.GetBytes(Convert.ToInt32(SetID.Text));
            data3[948] = buffer[0];
            data3[949] = buffer[1];
            data3[950] = buffer[2];
            data3[951] = buffer[3];
            if (NPlayers.SelectedIndex == 3)
            {
                data3[952] = 0;
                data3[953] = 0;
                data3[954] = 0;
                data3[955] = 0;
            }
            else
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(NPlayers.SelectedIndex+1));
                data3[952] = buffer[0];
                data3[953] = buffer[1];
                data3[954] = buffer[2];
                data3[955] = buffer[3];
            }
            for (int i = 0; i < 3; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(ArenaText[i].Text));
                data3[956 + 4 * i] = buffer[0];
                data3[957 + 4 * i] = buffer[1];
                data3[958 + 4 * i] = buffer[2];
                data3[959 + 4 * i] = buffer[3];
            }
            if (MObj2MM.IsChecked == true)
                data3[980] = 128;
            else data3[980] = 0;

            for (int i = 0; i < 2; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(ArenaText[3+i].Text));
                data3[988 + 4 * i] = buffer[0];
                data3[989 + 4 * i] = buffer[1];
                data3[990 + 4 * i] = buffer[2];
                data3[991 + 4 * i] = buffer[3];
            }
            #endregion
            data4 = cipher.Encipher(data3);
            File.WriteAllBytes(ofd.FileName, data4);
        }
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            data = null;
            data3 = null;
            data4 = null;
            cipher = new Cipher(key);
            #region Defenitions
            ComboBox[] MID = { M1ID, M2ID, M3ID, M4ID, M5ID, M6ID, M7ID };
            TextBox[] MSobj = { M1sobjID, M2sobjID, M3sobjID, M4sobjID, M5sobjID, M6sobjID, M7sobjID };
            CheckBox[] Tempered = { M1TFlag, M2TFlag, M3TFlag, M4TFlag, M5TFlag, M6TFlag, M7TFlag };
            TextBox[] MonsterSize = { M1Siz, M2Siz, M3Siz, M4Siz, M5Siz, M6Siz, M7Siz };
            ComboBox[] MHtP = { M1HtP, M2HtP, M3HtP, M4HtP, M5HtP, M6HtP, M7HtP };
            ComboBox[] MAtk = { M1Atk, M2Atk, M3Atk, M4Atk, M5Atk, M6Atk, M7Atk };
            ComboBox[] MDef = { M1Def, M2Def, M3Def, M4Def, M5Def, M6Def, M7Def };
            ComboBox[] MHAR = { M1HAR, M2HAR, M3HAR, M4HAR, M5HAR, M6HAR, M7HAR };
            ComboBox[] MSeT = { M1SeT, M2SeT, M3SeT, M4SeT, M5SeT, M6SeT, M7SeT };
            ComboBox[] MPHP = { M1PHP, M2PHP, M3PHP, M4PHP, M5PHP, M6PHP, M7PHP };
            ComboBox[] MBSt = { M1BSt, M2BSt, M3BSt, M4BSt, M5BSt, M6BSt, M7BSt };
            ComboBox[] MStB = { M1StB, M2StB, M3StB, M4StB, M5StB, M6StB, M7StB };
            ComboBox[] MBKO = { M1BKO, M2BKO, M3BKO, M4BKO, M5BKO, M6BKO, M7BKO };
            ComboBox[] MBEx = { M1BEx, M2BEx, M3BEx, M4BEx, M5BEx, M6BEx, M7BEx };
            ComboBox[] MBMo = { M1BMo, M2BMo, M3BMo, M4BMo, M5BMo, M6BMo, M7BMo };
            ComboBox[] MSSpw = { M1SSpw, M2SSpw, M3SSpw, M4SSpw, M5SSpw };
            ComboBox[] MonIcons = { Icon1, Icon2, Icon3, Icon4, Icon5 };
            ComboBox[] SmlMonIcons = { SmlIcon1, SmlIcon2, SmlIcon3, SmlIcon4, SmlIcon5 };
            ComboBox[] MapIcons = { MapIcon01, MapIcon02, MapIcon03, MapIcon04, MapIcon05,
                MapIcon06, MapIcon07, MapIcon08, MapIcon09, MapIcon10, MapIcon11, MapIcon12,
                MapIcon13, MapIcon14, MapIcon15, MapIcon16, MapIcon17, MapIcon18, MapIcon19,
                MapIcon20, MapIcon21, MapIcon22, MapIcon23, MapIcon24, MapIcon25, MapIcon26,
                MapIcon27, MapIcon28, MapIcon29, MapIcon30, MapIcon31, MapIcon32, MapIcon33,
                MapIcon34, MapIcon35, MapIcon36, MapIcon37, MapIcon38, MapIcon39, MapIcon40,
                MapIcon41, MapIcon42, MapIcon43, MapIcon44, MapIcon45, MapIcon46, MapIcon47,
                MapIcon48, MapIcon49, MapIcon50, MapIcon51 };
            TextBox[] SpawnText = { M6SpC, SpawnDelay, M7SpC, M3SpC, M4SpC, M5SpC, M6SpD, M7SpD };
            TextBox[] ArenaText = { RankA, RankB, RankC, FenCD, FenUT };
            #endregion
            data = (File.ReadAllBytes(ofd.FileName));
            data3 = cipher.Decipher(data);
            #region Common and Objectives
            byte[] buffer = BitConverter.GetBytes(Convert.ToInt32(QID.Text));
            data3[6] = buffer[0];
            data3[7] = buffer[1];
            data3[8] = buffer[2];
            data3[9] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToByte(Stars.SelectedIndex));
            data3[10] = buffer[0];
            buffer = BitConverter.GetBytes(Convert.ToByte(Rank.SelectedIndex));
            data3[19] = buffer[0];
            for (int i = 0; i < MapIDs.Length; i++)
            {
                if (Map.SelectedIndex == i)
                {
                    buffer = BitConverter.GetBytes(MapIDs[i]);
                    data3[23] = buffer[0];
                    data3[24] = buffer[1];
                    data3[25] = buffer[2];
                    data3[26] = buffer[3];
                }
            }
            buffer = BitConverter.GetBytes(Convert.ToByte(PSpawn.SelectedIndex));
            data3[27] = buffer[0];
            if (FSpawn.IsChecked == true)
                data3[31] = 1;
            else data3[31] = 0;
            buffer = BitConverter.GetBytes(Convert.ToByte(Weather.SelectedIndex));
            data3[43] = buffer[0];
            buffer = BitConverter.GetBytes(Convert.ToInt32(Reward.Text));
            data3[51] = buffer[0];
            data3[52] = buffer[1];
            data3[53] = buffer[2];
            data3[54] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(Penalty.Text));
            data3[55] = buffer[0];
            data3[56] = buffer[1];
            data3[57] = buffer[2];
            data3[58] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(Timer.Text));
            data3[63] = buffer[0];
            data3[64] = buffer[1];
            data3[65] = buffer[2];
            data3[66] = buffer[3];
            for (int i = 0; i < 5; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToUInt16(MonIcons[i].SelectedIndex));
                data3[68 + 2 * i] = buffer[0];
                data3[69 + 2 * i] = buffer[1];
            }
            buffer = BitConverter.GetBytes(Convert.ToByte(HRReq.SelectedIndex));
            data3[78] = buffer[0];
            for (int i = 0; i < ObjectiveIDs.Length; i++)
            {
                if (MObjT1.SelectedIndex == i)
                {
                    buffer = BitConverter.GetBytes(ObjectiveIDs[i]);
                    data3[83] = buffer[0];
                }
            }
            if (MObj1MM.IsChecked == true)
                data3[84] = 04;
            else data3[84] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID1.SelectedIndex));
            data3[87] = buffer[0];
            data3[88] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC1.Text));
            data3[89] = buffer[0];
            data3[90] = buffer[1];
            for (int i = 0; i < ObjectiveIDs.Length; i++)
            {
                if (MObjT2.SelectedIndex == i)
                {
                    buffer = BitConverter.GetBytes(ObjectiveIDs[i]);
                    data3[91] = buffer[0];
                }
            }
            if (MObj2MM.IsChecked == true)
                data3[92] = 04;
            else data3[92] = 0;
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjID2.SelectedIndex));
            data3[95] = buffer[0];
            data3[96] = buffer[1];
            buffer = BitConverter.GetBytes(Convert.ToUInt16(MObjC2.Text));
            data3[97] = buffer[0];
            data3[98] = buffer[1];
            if (MultiO.IsChecked == true)
                data3[99] = 2;
            else data3[99] = 1;
            buffer = BitConverter.GetBytes(Convert.ToByte(BGM.SelectedIndex));
            data3[120] = buffer[0];
            for (int i = 0; i < QuestTypeIDs.Length; i++)
            {
                if (QType.SelectedIndex == i)
                {
                    buffer = BitConverter.GetBytes(QuestTypeIDs[i]);
                    data3[128] = buffer[0];
                }
            }
            data3[130] = Convert.ToByte(2 * Convert.ToInt32(ATFlag.IsChecked) + Convert.ToInt32(PSGear.IsChecked));
            buffer = BitConverter.GetBytes(Convert.ToInt32(RRemID.Text));
            data3[132] = buffer[0];
            data3[133] = buffer[1];
            data3[134] = buffer[2];
            data3[135] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(SRemID.Text));
            data3[144] = buffer[0];
            data3[145] = buffer[1];
            data3[146] = buffer[2];
            data3[147] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(HRpoint.Text));
            data3[160] = buffer[0];
            data3[161] = buffer[1];
            data3[162] = buffer[2];
            data3[163] = buffer[3];
            #endregion
            #region Monsters
            for (int i = 0; i < 7; i++)
            {
                if (MID[i].SelectedIndex == 0)
                {
                    data3[172 + 65 * i] = 255;
                    data3[173 + 65 * i] = 255;
                    data3[174 + 65 * i] = 255;
                    data3[175 + 65 * i] = 255;
                }
                else
                {
                    buffer = BitConverter.GetBytes(Convert.ToInt32(MID[i].SelectedIndex - 1));
                    data3[172 + 65 * i] = buffer[0];
                    data3[173 + 65 * i] = buffer[1];
                    data3[174 + 65 * i] = buffer[2];
                    data3[175 + 65 * i] = buffer[3];
                }
                buffer = BitConverter.GetBytes(Convert.ToInt32(MSobj[i].Text));
                data3[176 + 65 * i] = buffer[0];
                data3[177 + 65 * i] = buffer[1];
                data3[178 + 65 * i] = buffer[2];
                data3[179 + 65 * i] = buffer[3];
                if (Tempered[i].IsChecked == true)
                    data3[184 + 65 * i] = 1;
                else data3[184 + 65 * i] = 0;
                buffer = BitConverter.GetBytes(Convert.ToInt32(MHtP[i].SelectedIndex));
                data3[185 + 65 * i] = buffer[0];
                data3[186 + 65 * i] = buffer[1];
                data3[187 + 65 * i] = buffer[2];
                data3[188 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MAtk[i].SelectedIndex));
                data3[189 + 65 * i] = buffer[0];
                data3[190 + 65 * i] = buffer[1];
                data3[191 + 65 * i] = buffer[2];
                data3[192 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MDef[i].SelectedIndex));
                data3[193 + 65 * i] = buffer[0];
                data3[194 + 65 * i] = buffer[1];
                data3[195 + 65 * i] = buffer[2];
                data3[196 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MHAR[i].SelectedIndex));
                data3[197 + 65 * i] = buffer[0];
                data3[198 + 65 * i] = buffer[1];
                data3[199 + 65 * i] = buffer[2];
                data3[200 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MSeT[i].SelectedIndex));
                data3[205 + 65 * i] = buffer[0];
                data3[206 + 65 * i] = buffer[1];
                data3[207 + 65 * i] = buffer[2];
                data3[208 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MPHP[i].SelectedIndex));
                data3[213 + 65 * i] = buffer[0];
                data3[214 + 65 * i] = buffer[1];
                data3[215 + 65 * i] = buffer[2];
                data3[216 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBSt[i].SelectedIndex));
                data3[217 + 65 * i] = buffer[0];
                data3[218 + 65 * i] = buffer[1];
                data3[219 + 65 * i] = buffer[2];
                data3[220 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MStB[i].SelectedIndex));
                data3[221 + 65 * i] = buffer[0];
                data3[222 + 65 * i] = buffer[1];
                data3[223 + 65 * i] = buffer[2];
                data3[224 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBKO[i].SelectedIndex));
                data3[225 + 65 * i] = buffer[0];
                data3[226 + 65 * i] = buffer[1];
                data3[227 + 65 * i] = buffer[2];
                data3[228 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBEx[i].SelectedIndex));
                data3[229 + 65 * i] = buffer[0];
                data3[230 + 65 * i] = buffer[1];
                data3[231 + 65 * i] = buffer[2];
                data3[232 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MBMo[i].SelectedIndex));
                data3[233 + 65 * i] = buffer[0];
                data3[234 + 65 * i] = buffer[1];
                data3[235 + 65 * i] = buffer[2];
                data3[236 + 65 * i] = buffer[3];
                buffer = BitConverter.GetBytes(Convert.ToInt32(MonsterSize[i].Text));
                data3[201 + 65 * i] = buffer[0];
                data3[202 + 65 * i] = buffer[1];
                data3[203 + 65 * i] = buffer[2];
                data3[204 + 65 * i] = buffer[3];
            }

            buffer = BitConverter.GetBytes(Convert.ToInt32(sMsobj.Text));
            data3[627] = buffer[0];
            data3[628] = buffer[1];
            data3[629] = buffer[2];
            data3[630] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMHP.Text));
            data3[631] = buffer[0];
            data3[632] = buffer[1];
            data3[633] = buffer[2];
            data3[634] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMAt.Text));
            data3[635] = buffer[0];
            data3[636] = buffer[1];
            data3[637] = buffer[2];
            data3[638] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(sMDe.Text));
            data3[639] = buffer[0];
            data3[640] = buffer[1];
            data3[641] = buffer[2];
            data3[642] = buffer[3];
            buffer = BitConverter.GetBytes(Convert.ToInt32(MPMod.Text));
            data3[644] = buffer[0];
            data3[645] = buffer[1];
            data3[646] = buffer[2];
            data3[647] = buffer[3];
            #endregion
            #region Spawn, Map Icons, and Arena
            for (int i = 0; i < 5; i++)
                data3[652 + 4 * i] = Convert.ToByte(MSSpw[i].SelectedIndex);
            for (int i = 0; i < SpawnText.Length; i++)
                data3[672 + 4 * i] = Convert.ToByte(SpawnText[i].Text);
            for (int i = 0; i < MapIcons.Length; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(MapIcons[i].SelectedIndex));
                data3[704 + 4 * i] = buffer[0];
                data3[705 + 4 * i] = buffer[1];
                data3[706 + 4 * i] = buffer[2];
                data3[707 + 4 * i] = buffer[3];
            }
            for (int i = 0; i < 5; i++)
            {
                if (SmlMonIcons[i].SelectedIndex == 0)
                {
                    data3[908 + 4 * i] = 0;
                    data3[928 + 4 * i] = 0;
                }
                else
                    data3[908 + 4 * i] = Convert.ToByte(SmlMonIcons[i].SelectedIndex);
            }
            buffer = BitConverter.GetBytes(Convert.ToInt32(SetID.Text));
            data3[948] = buffer[0];
            data3[949] = buffer[1];
            data3[950] = buffer[2];
            data3[951] = buffer[3]; if (NPlayers.SelectedIndex == 3)
            {
                data3[952] = 0;
                data3[953] = 0;
                data3[954] = 0;
                data3[955] = 0;
            }
            else
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(NPlayers.SelectedIndex + 1));
                data3[952] = buffer[0];
                data3[953] = buffer[1];
                data3[954] = buffer[2];
                data3[955] = buffer[3];
            }
            for (int i = 0; i < 3; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(ArenaText[i].Text));
                data3[956 + 4 * i] = buffer[0];
                data3[957 + 4 * i] = buffer[1];
                data3[958 + 4 * i] = buffer[2];
                data3[959 + 4 * i] = buffer[3];
            }
            if (MObj2MM.IsChecked == true)
                data3[980] = 128;
            else data3[980] = 0;

            for (int i = 0; i < 2; i++)
            {
                buffer = BitConverter.GetBytes(Convert.ToInt32(ArenaText[3 + i].Text));
                data3[988 + 4 * i] = buffer[0];
                data3[989 + 4 * i] = buffer[1];
                data3[990 + 4 * i] = buffer[2];
                data3[991 + 4 * i] = buffer[3];
            }
            #endregion
            data4 = cipher.Encipher(data3);
            sfd.Filter = "MHW Quest File|*.mib";
            if (QID.Text.Length == 0)
                sfd.FileName = "questData_00000" + QID.Text + ".mib";
            else if (QID.Text.Length == 1)
                sfd.FileName = "questData_0000" + QID.Text + ".mib";
            else if (QID.Text.Length == 2)
                sfd.FileName = "questData_000" + QID.Text + ".mib";
            else if (QID.Text.Length == 3)
                sfd.FileName = "questData_00" + QID.Text + ".mib";
            else if (QID.Text.Length == 4)
                sfd.FileName = "questData_0" + QID.Text + ".mib";
            else if (QID.Text.Length > 4)
                sfd.FileName = "questData_" + QID.Text + ".mib";
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                File.WriteAllBytes(sfd.FileName, data4);
            }
        }
        private void PlayerSpawnChanged(object sender, RoutedEventArgs e)
        {
            if (PSpawn.SelectedIndex == 0)
                FSpawn.IsChecked = true;
            else
                FSpawn.IsChecked = false;
        }
        private void FixedSpawn(object sender, RoutedEventArgs e)
        {
            if (FSpawn.IsChecked == true)
                PSpawn.SelectedIndex = 0;
            else if (PSpawn.SelectedIndex == 0)
                PSpawn.SelectedIndex = 1;
        }
        private void MultiO_Checked(object sender, RoutedEventArgs e)
        {
            if (MultiO.IsChecked == true)
                MObj2.IsEnabled = true;
            else if (MultiO.IsChecked == false)
                MObj2.IsEnabled = false;
        }
        private void MObjT1_Changed(object sender, RoutedEventArgs e)
        {
            MObjID1.Items.Clear();
            if (MObjT1.SelectedIndex == 2)
                for (int i = 0; i < Items.Length; i++)
                    MObjID1.Items.Add(Items[i]);
            else
                for (int i = 1; i < MonsterNames.Length; i++)
                    MObjID1.Items.Add(MonsterNames[i]);
            MObjID1.SelectedIndex = 0;
        }
        private void MObjT2_Changed(object sender, RoutedEventArgs e)
        {

            MObjID2.Items.Clear();
            if (MObjT2.SelectedIndex == 2)
                for (int i = 0; i < Items.Length; i++)
                    MObjID2.Items.Add(Items[i]);
            else
                for (int i = 1; i < MonsterNames.Length; i++)
                    MObjID2.Items.Add(MonsterNames[i]);
            MObjID2.SelectedIndex = 0;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This tool was made by Aradi147. " +
                "\nCredits go to Asterisk, Vuze, Zindea, NekotagaYuhatora, Material, hexhexhex, TITAN" +
                "\nJunkBunny, Fandirus, Mace ya face,Bedtime, kkkkyue,eliottbw, and everyone that worked on" +
                "\ndocumenting the MIB file. I don't really even know half of them.");
        }
        private void Contact_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("To contact me and other modders, please vist \n https://discord.gg/gJwMdhK");
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
