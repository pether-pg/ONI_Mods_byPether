using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace FragrantFlowers
{
    public class Plant_RimedMallowConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        //===> BASE INFORMATION <=========================================
        public const string ID = "RimedMallowPlant";
        public const string SEED_ID = "IceMallowSeed";
        public const string PlantKanim = "plant_rimedmallow_kanim";
        public const string SeedKanim = "seed_rimedmallow_kanim";
        public const int WIDTH = 1;
        public const int HEIGHT = 3;

        //===> DEFINE THE ANIMATION SETTINGS FOR A STANDARD CROP PLANT <=
        private static StandardCropPlant.AnimSet animSet = new StandardCropPlant.AnimSet
        {
            grow = "basic_grow",
            grow_pst = "basic_grow_pst",
            idle_full = "basic_idle_full",
            wilt_base = "basic_wilt",
            harvest = "basic_harvest"
        };

        //===> TEMPERATURE SETTINGS <=====================================
        public const float DefaultTemperature = 253.15f;       // -20°C: Normal Temperature
        public const float TemperatureLethalLow = 118.15f;     //-155ºC: Plant will die (Lowest Temp)
        public const float TemperatureWarningLow = 183.15f;    // -60°C: Plant will stop growing (Lowest Temp)
        public const float TemperatureWarningHigh = 273.15f;   //   0°C: Plant will stop growing (Highest Temp)
        public const float TemperatureLethalHigh = 298.15f;    //  25°C: Plant will die (Highest Temp)

        public const float Fertilization = 1 / 100f;         // Ice Fertilization Needed

        public ComplexRecipe Recipe;

        //===> DEFINE THE BASE TEMPLATE <=====================================================================
        public GameObject CreatePrefab()
        {

            float mass = 2f;
            EffectorValues tier = DECOR.BONUS.TIER1;
            GameObject gameObject = EntityTemplates.CreatePlacedEntity(
                ID,
                STRINGS.PLANTS.RIMEDMALLOW.NAME,
                STRINGS.PLANTS.RIMEDMALLOW.DESC,
                mass,
                Assets.GetAnim(PlantKanim),
                "idle_empty",
                Grid.SceneLayer.BuildingFront,
                WIDTH,
                HEIGHT,
                tier,
                default(EffectorValues),
                SimHashes.Creature,
                new List<Tag> { GameTags.Hanging },
                253.15f
                );

            EntityTemplates.MakeHangingOffsets(gameObject, WIDTH, HEIGHT);
            EntityTemplates.ExtendEntityToBasicPlant(
                gameObject,
                TemperatureLethalLow,
                TemperatureWarningLow,
                TemperatureWarningHigh,
                TemperatureLethalHigh,
                null,
                true,
                0f,
                0.15f,
                Crop_CottonBollConfig.ID,
                true,
                true,
                true,
                true,
                2400f,
                0f,
                9800f,
                "RimedMallowOriginal",
                "Rimed Mallow Original"
                );

            EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
            {
                new PlantElementAbsorber.ConsumeInfo
                {
                    tag = SimHashes.Ice.CreateTag(),
                    massConsumptionRate = Fertilization
                }
            });
            gameObject.GetComponent<UprootedMonitor>().monitorCells = new CellOffset[] { new CellOffset(0, 1) };
            gameObject.AddOrGet<StandardCropPlant>();

            EntityTemplates.MakeHangingOffsets(EntityTemplates.CreateAndRegisterPreviewForPlant(
                EntityTemplates.CreateAndRegisterSeedForPlant(
                    gameObject,
                    SeedProducer.ProductionType.Harvest,
                    SEED_ID,
                    STRINGS.SEEDS.RIMEDMALLOW.SEED_NAME,
                    STRINGS.SEEDS.RIMEDMALLOW.SEED_DESC,
                    Assets.GetAnim(SeedKanim),
                    "object",
                    1,
                    new List<Tag> { GameTags.CropSeed },
                    SingleEntityReceptacle.ReceptacleDirection.Bottom,
                    default(Tag),
                    4,
                    STRINGS.PLANTS.RIMEDMALLOW.DOMESTICATED_DESC,
                    EntityTemplates.CollisionShape.CIRCLE,
                    0.3f,
                    0.3f,
                    null,
                    "",
                    false
                    ),
                    "RimedMallowPlant_preview",
                    Assets.GetAnim(PlantKanim),
                    "place",
                    WIDTH,
                    HEIGHT),
                WIDTH,
                HEIGHT);

            //===> DISEASE OR GERMS THIS CROP RELEASES <===========================================================================
            DiseaseDropper.Def def = gameObject.AddOrGetDef<DiseaseDropper.Def>();
            def.diseaseIdx = Db.Get().Diseases.GetIndex(MallowScent.ID);
            def.emitFrequency = 10f;
            def.averageEmitPerSecond = 1000;
            def.singleEmitQuantity = 100000;
            gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = MallowScent.ID;

            return gameObject;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

    }
}
