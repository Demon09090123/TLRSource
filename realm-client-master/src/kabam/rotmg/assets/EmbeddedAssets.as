package kabam.rotmg.assets
{
    import com.company.assembleegameclient.util.AnimatedChar;

import flash.display.BitmapData;
import flash.display.BitmapData;
    import kabam.rotmg.assets.model.AnimatedAssetTemplate;
    import kabam.rotmg.assets.model.AssetTemplate;

import mx.core.ByteArrayAsset;

public class EmbeddedAssets {
        public static const soundEffectDict:Array = [
            "button_click",
            "death_screen",
            "enter_realm",
            "error",
            "inventory_move_item",
            "level_up",
            "loot_appears",
            "no_mana",
            "use_key",
            "use_potion"
        ];

        private static function generateAData(bd:*, mask:*, w:int, h:int, rw:int, rh:int, dir:int):Object {
            return {
                "bitmapData":bd.bitmapData,
                "mask": (mask == null) ? null : mask.bitmapData,
                "cw":w,
                "ch":h,
                "w":rw,
                "h":rh,
                "dir":dir
            };
        }
        private static function generateData(bd:*, w:int, h:int):Object {
            return {
                "bitmapData":(bd is BitmapData) ? bd : bd.bitmapData,
                "cw":w,
                "ch":h
            };
        }

        public static const assetsDict:Object = {
            "lofiChar8x8": generateData(new (EmbeddedAssets_lofiCharEmbed_), 8, 8),
            "lofiChar16x8": generateData(new (EmbeddedAssets_lofiCharEmbed_), 16,  8 ),
            "lofiChar16x16": generateData(new (EmbeddedAssets_lofiCharEmbed_), 16,16),
            "lofiChar28x8": generateData(new (EmbeddedAssets_lofiChar2Embed_),8,8),
            "lofiChar216x8": generateData(new (EmbeddedAssets_lofiChar2Embed_),16,8),
            "lofiChar216x16": generateData(new (EmbeddedAssets_lofiChar2Embed_),16,16),
            "lofiCharBig": generateData(new (EmbeddedAssets_lofiCharBigEmbed_),16,16),
            "lofiEnvironment": generateData(new (EmbeddedAssets_lofiEnvironmentEmbed_),8,8),
            "lofiEnvironment2": generateData(new (EmbeddedAssets_lofiEnvironment2Embed_),8,8),
            "lofiEnvironment3": generateData(new (EmbeddedAssets_lofiEnvironment3Embed_),8,8),
            "lofiInterface": generateData(new (EmbeddedAssets_lofiInterfaceEmbed_),8,8),
            "redLootBag": generateData(new (EmbeddedAssets_redLootBagEmbed_),8,8),
            "lofiInterfaceBig": generateData(new (EmbeddedAssets_lofiInterfaceBigEmbed_),16,16),
            "lofiInterface2": generateData(new (EmbeddedAssets_lofiInterface2Embed_),8,8),
            "lofiObj": generateData(new (EmbeddedAssets_lofiObjEmbed_),8,8),
            "lofiObj2": generateData(new (EmbeddedAssets_lofiObj2Embed_),8,8),
            "lofiObj3": generateData(new (EmbeddedAssets_lofiObj3Embed_),8,8),
            "lofiObj4": generateData(new (EmbeddedAssets_lofiObj4Embed_),8,8),
            "lofiObj5": generateData(new (EmbeddedAssets_lofiObj5Embed_),8,8),
            "lofiObj6": generateData(new (EmbeddedAssets_lofiObj6Embed_),8,8),
            "lofiObjBig": generateData(new (EmbeddedAssets_lofiObjBigEmbed_),16,16),
            "lofiObj40x40": generateData(new (EmbeddedAssets_lofiObj40x40Embed_),40,40),
            "lofiProjs": generateData(new (EmbeddedAssets_lofiProjsEmbed_),8,8),
            "lofiProjsBig": generateData(new (EmbeddedAssets_lofiProjsBigEmbed_),16,16),
            "lofiParts": generateData(new (EmbeddedAssets_lofiPartsEmbed_),8,8),
            "stars": generateData(new (EmbeddedAssets_starsEmbed_),5,5),
            "textile4x4": generateData(new (EmbeddedAssets_textile4x4Embed_),4,4),
            "textile5x5": generateData(new (EmbeddedAssets_textile5x5Embed_),5,5),
            "textile9x9": generateData(new (EmbeddedAssets_textile9x9Embed_),9,9),
            "textile10x10": generateData(new (EmbeddedAssets_textile10x10Embed_),10,10),
            "inner_mask": generateData(new (EmbeddedAssets_innerMaskEmbed_),4,4),
            "sides_mask": generateData(new (EmbeddedAssets_sidesMaskEmbed_),4,4),
            "outer_mask": generateData(new (EmbeddedAssets_outerMaskEmbed_),4,4),
            "innerP1_mask": generateData(new (EmbeddedAssets_innerP1MaskEmbed_),4,4),
            "innerP2_mask": generateData(new (EmbeddedAssets_innerP2MaskEmbed_),4,4),
            "invisible": generateData(new BitmapData(8,8,true,0), 8, 8),
            "cursorsEmbed": generateData(new (EmbeddedAssets_cursorsEmbed_), 32, 32)
        };

        public static const animatedAssetsDict:Object = {
             "chars8x8rBeach": generateAData(new (EmbeddedAssets_chars8x8rBeachEmbed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8dBeach": generateAData(new (EmbeddedAssets_chars8x8dBeachEmbed_), null,8,8,56,8,AnimatedChar.DOWN),
             "chars8x8rLow1": generateAData(new (EmbeddedAssets_chars8x8rLow1Embed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8rLow2": generateAData(new (EmbeddedAssets_chars8x8rLow2Embed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8rMid": generateAData(new (EmbeddedAssets_chars8x8rMidEmbed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8rMid2": generateAData(new (EmbeddedAssets_chars8x8rMid2Embed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8rHigh": generateAData(new (EmbeddedAssets_chars8x8rHighEmbed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8rHero1": generateAData(new (EmbeddedAssets_chars8x8rHero1Embed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8rHero2": generateAData(new (EmbeddedAssets_chars8x8rHero2Embed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars8x8dHero1": generateAData(new (EmbeddedAssets_chars8x8dHero1Embed_), null,8,8,56,8,AnimatedChar.DOWN),
             "chars16x16dMountains1": generateAData(new (EmbeddedAssets_chars16x16dMountains1Embed_), null,16,16,112,16,AnimatedChar.DOWN),
             "chars16x16dMountains2": generateAData(new (EmbeddedAssets_chars16x16dMountains2Embed_), null,16,16,112,16,AnimatedChar.DOWN),
             "chars8x8dEncounters": generateAData(new (EmbeddedAssets_chars8x8dEncountersEmbed_), null,8,8,56,8,AnimatedChar.DOWN),
             "chars8x8rEncounters": generateAData(new (EmbeddedAssets_chars8x8rEncountersEmbed_), null,8,8,56,8,AnimatedChar.RIGHT),
             "chars16x8dEncounters": generateAData(new (EmbeddedAssets_chars16x8dEncountersEmbed_), null,16,8,112,8,AnimatedChar.DOWN),
             "chars16x8rEncounters": generateAData(new (EmbeddedAssets_chars16x8rEncountersEmbed_), null,16,8,112,8,AnimatedChar.DOWN),
             "chars16x16dEncounters": generateAData(new (EmbeddedAssets_chars16x16dEncountersEmbed_), null,16,16,112,16,AnimatedChar.DOWN),
             "chars16x16dEncounters2": generateAData(new (EmbeddedAssets_chars16x16dEncounters2Embed_), null,16,16,112,16,AnimatedChar.DOWN),
             "chars16x16rEncounters": generateAData(new (EmbeddedAssets_chars16x16rEncountersEmbed_), null,16,16,112,16,AnimatedChar.RIGHT),
             "players": generateAData(new (EmbeddedAssets_playersEmbed_), new (EmbeddedAssets_playersMaskEmbed_),8,8,56,24,AnimatedChar.RIGHT),
             "playerskins": generateAData(new (EmbeddedAssets_playersSkinsEmbed_), new (EmbeddedAssets_playersSkinsMaskEmbed_),8,8,56,24,AnimatedChar.RIGHT),
             "chars8x8rPets1": generateAData(new (EmbeddedAssets_chars8x8rPets1Embed_), new (EmbeddedAssets_chars8x8rPets1MaskEmbed_),8,8,56,8,AnimatedChar.RIGHT)
        };

        public static var particlesEmbed:Class = EmbeddedAssets_particlesEmbed;
        private static var monsterTank1Embed_:Class = EmbeddedAssets_monsterTank1Embed_;
        private static var monsterTank2Embed_:Class = EmbeddedAssets_monsterTank2Embed_;
        private static var monsterTank3Embed_:Class = EmbeddedAssets_monsterTank3Embed_;
        private static var monsterTank4Embed_:Class = EmbeddedAssets_monsterTank4Embed_;
        private static var labTankEmbed_:Class = EmbeddedAssets_labTankEmbed_;
        private static var teslaEmbed_:Class = EmbeddedAssets_teslaEmbed_;
        private static var gasEmitter_:Class = EmbeddedAssets_gasEmitter_;
        private static var cloningVatEmbed_:Class = EmbeddedAssets_cloningVatEmbed_;
        private static var crateEmbed_:Class = EmbeddedAssets_crateEmbed_;
        private static var cubeEmbed_:Class = EmbeddedAssets_cubeEmbed_;
        private static var bigcubeEmbed_:Class = EmbeddedAssets_bigcubeEmbed_;
        private static var icosahedronEmbed_:Class = EmbeddedAssets_icosahedronEmbed_;
        private static var octahedronEmbed_:Class = EmbeddedAssets_octahedronEmbed_;
        private static var pyramidEmbed_:Class = EmbeddedAssets_pyramidEmbed_;
        private static var tetrahedronEmbed_:Class = EmbeddedAssets_tetrahedronEmbed_;
        private static var dodecahedronEmbed_:Class = EmbeddedAssets_dodecahedronEmbed_;
        private static var pillarEmbed_:Class = EmbeddedAssets_pillarEmbed_;
        private static var brokenPillarEmbed_:Class = EmbeddedAssets_brokenPillarEmbed_;
        private static var towerEmbed_:Class = EmbeddedAssets_towerEmbed_;
        private static var obeliskEmbed_:Class = EmbeddedAssets_obeliskEmbed_;
        private static var tableEmbed_:Class = EmbeddedAssets_tableEmbed_;
        private static var tableEdgeEmbed_:Class = EmbeddedAssets_tableEdgeEmbed_;
        private static var signEmbed_:Class = EmbeddedAssets_signEmbed_;
        private static var webEmbed_:Class = EmbeddedAssets_webEmbed_;
        private static var candyColBrokenEmbed_:Class = EmbeddedAssets_candyColBrokenEmbed_;
        private static var candyColWholeEmbed_:Class = EmbeddedAssets_candyColWholeEmbed_;
        private static var candyDoughnut1Embed_:Class = EmbeddedAssets_candyDoughnut1Embed_;
        private static var candyDoughnut2Embed_:Class = EmbeddedAssets_candyDoughnut2Embed_;
        private static var candyDoughnut3Embed_:Class = EmbeddedAssets_candyDoughnut3Embed_;
        private static var candyDoughnut4Embed_:Class = EmbeddedAssets_candyDoughnut4Embed_;
        private static var newGateEmbed_:Class = EmbeddedAssets_newGateEmbed_;
        private static var newGateEntryEmbed_:Class = EmbeddedAssets_newGateEntryEmbed_;
        private static var newGateEntry2Embed_:Class = EmbeddedAssets_newGateEntry2Embed_;
        private static var newGateEnd1Embed_:Class = EmbeddedAssets_newGateEnd1Embed_;
        private static var newGateEnd2Embed_:Class = EmbeddedAssets_newGateEnd2Embed_;
        private static var newMonument1Embed_:Class = EmbeddedAssets_newMonument1Embed_;
        private static var newMonument2Embed_:Class = EmbeddedAssets_newMonument2Embed_;
        private static var newMonument3Embed_:Class = EmbeddedAssets_newMonument3Embed_;
        private static var largeMonument1Embed_:Class = EmbeddedAssets_largeMonument1Embed_;
        private static var largeMonument2Embed_:Class = EmbeddedAssets_largeMonument2Embed_;
        private static var largeMonument3Embed_:Class = EmbeddedAssets_largeMonument3Embed_;
        private static var jackoEmbed_:Class = EmbeddedAssets_jackoEmbed_;

        public static var models_:Object = {
            "Monster Tank1":new monsterTank1Embed_(),
            "Monster Tank2":new monsterTank2Embed_(),
            "Monster Tank3":new monsterTank3Embed_(),
            "Monster Tank4":new monsterTank4Embed_(),
            "GasEmitter":new gasEmitter_(),
            "Lab Tank":new labTankEmbed_(),
            "Tesla":new teslaEmbed_(),
            "CloningVat":new cloningVatEmbed_(),
            "Crate":new crateEmbed_(),
            "Cube":new cubeEmbed_(),
            "Big Cube":new bigcubeEmbed_(),
            "Ico":new icosahedronEmbed_(),
            "Octa":new octahedronEmbed_(),
            "Pyramid":new pyramidEmbed_(),
            "Tetra":new tetrahedronEmbed_(),
            "Dodec":new dodecahedronEmbed_(),
            "Pillar":new pillarEmbed_(),
             "Broken Pillar":new brokenPillarEmbed_(),
            "Tower":new towerEmbed_(),
            "Obelisk":new obeliskEmbed_(),
            "Table":new tableEmbed_(),
            "Table Edge":new tableEdgeEmbed_(),
            "Sign":new signEmbed_(),
            "Web":new webEmbed_(),
            "Candy Col Broken":new candyColBrokenEmbed_(),
            "Candy Col Whole":new candyColWholeEmbed_(),
            "Candy Doughnut 1":new candyDoughnut1Embed_(),
            "Candy Doughnut 2":new candyDoughnut2Embed_(),
            "Candy Doughnut 3":new candyDoughnut3Embed_(),
            "Candy Doughnut 4":new candyDoughnut4Embed_(),
            "Gate":new newGateEmbed_(),
            "Gate Entry":new newGateEntryEmbed_(),
            "Gate Entry 2":new newGateEntry2Embed_(),
            "Gate End 1":new newGateEnd1Embed_(),
            "Gate End 2":new newGateEnd2Embed_(),
            "Monument 1":new newMonument1Embed_(),
            "Monument 2":new newMonument2Embed_(),
            "Monument 3":new newMonument3Embed_(),
            "Large Monument 1":new largeMonument1Embed_(),
            "Large Monument 2":new largeMonument2Embed_(),
            "Large Monument 3":new largeMonument3Embed_(),
            "Jacko":new jackoEmbed_()
        };

        public function EmbeddedAssets() {
            super();
        }
    }
}
