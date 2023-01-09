using System.Collections.Generic;
using UnityEngine;

namespace ExoticCuisine
{
    class SpiralciclePlantConfig : IEntityConfig
    {
        public const float FERTILIZATION_RATE = 0.006666667f;
        public const string ID = "SpiralciclePlant";
        public const string SEED_ID = "SpiralciclePlantSeed";
        public const string PLANT_KANIM = "spiralcicle_kanim";
        public const string SEED_KANIM = "spiralcicleSeed_kanim";
        public const string FRUIT_KANIM = "spiralcicleFruit_kanim";
        public const float CROP_DURATION = 45f; //in seconds
        public const int WIDTH = 1;
        public const int HEIGHT = 1;

        private static string CROP_ID = SpiralcicleFruitConfig.ID;
        private static string name = (string)STRINGS.PLANTS.SPIRALCICLE.NAME;
        private static string desc = (string)STRINGS.PLANTS.SPIRALCICLE.DESC;
        private static string seedName = (string)STRINGS.SEEDS.SPIRALCICLE.NAME;
        private static string seedDesc = (string)STRINGS.SEEDS.SPIRALCICLE.DESC;
        private static string domesticateddesc = (string)STRINGS.PLANTS.SPIRALCICLE.DOMESTICATEDDESC;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            KAnimFile anim1 = Assets.GetAnim(PLANT_KANIM);
            EffectorValues decor = TUNING.DECOR.BONUS.TIER1;
            EffectorValues noise = new EffectorValues();
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name, desc, 1f, anim1, "idle_empty", Grid.SceneLayer.BuildingFront, WIDTH, HEIGHT, decor, noise, additionalTags: new List<Tag>() { GameTags.Hanging });

            EntityTemplates.MakeHangingOffsets(placedEntity, WIDTH, HEIGHT);

            SimHashes[] safeElements = new SimHashes[2]
            {
                SimHashes.CarbonDioxide,
                SimHashes.Oxygen
            };

            EntityTemplates.ExtendEntityToBasicPlant(placedEntity, 228.15f, 278.15f, 308.15f, safe_elements: safeElements, crop_id: CROP_ID, max_radiation: 4600f, baseTraitId: "MushroomPlantOriginal", baseTraitName: name);
            EntityTemplates.ExtendPlantToFertilizable(placedEntity, new PlantElementAbsorber.ConsumeInfo[1]
            {
                new PlantElementAbsorber.ConsumeInfo()
                {
                tag = SimHashes.Sand.CreateTag(),
                massConsumptionRate = 0.006666667f
                }
            });

            placedEntity.GetComponent<UprootedMonitor>().monitorCells = new CellOffset[] { new CellOffset(0, 1) };

            placedEntity.AddOrGet<StandardCropPlant>();
            GameObject plant = placedEntity;
            KAnimFile anim2 = Assets.GetAnim(SEED_KANIM);
            List<Tag> additionalTags = new List<Tag>() { GameTags.CropSeed };
            Tag replantGroundTag = new Tag();
            
            EntityTemplates.MakeHangingOffsets(
                EntityTemplates.CreateAndRegisterPreviewForPlant(
                    EntityTemplates.CreateAndRegisterSeedForPlant(
                        plant, 
                        SeedProducer.ProductionType.Harvest, 
                        SEED_ID, 
                        seedName, 
                        seedDesc, 
                        anim2, 
                        numberOfSeeds: 0, 
                        additionalTags: additionalTags,
                        planterDirection: SingleEntityReceptacle.ReceptacleDirection.Bottom,
                        replantGroundTag: replantGroundTag, 
                        sortOrder: 3,
                        domesticatedDescription: domesticateddesc, 
                        width: 0.33f, 
                        height: 0.33f
                    ), 
                    "MushroomPlant_preview", 
                    Assets.GetAnim(PLANT_KANIM), 
                    "place", 
                    WIDTH, 
                    HEIGHT
                ), 
                WIDTH, 
                HEIGHT
            );
            
            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", TUNING.NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", TUNING.NOISE_POLLUTION.CREATURES.TIER3);
            return placedEntity;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
