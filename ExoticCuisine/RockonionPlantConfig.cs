using System.Collections.Generic;
using UnityEngine;

namespace ExoticCuisine
{
    class RockonionPlantConfig : IEntityConfig
    {
        public const float FERTILIZATION_RATE = 0.006666667f;
        public const string ID = "RockonionPlant";
        public const string SEED_ID = "RockonionPlantSeed";
        public const string PLANT_KANIM = "rockonion_kanim";
        public const string SEED_KANIM = "rockonionSeed_kanim";
        public const string FRUIT_KANIM = "rockonionFruit_kanim";
        public const float CROP_DURATION = 45f; //in seconds
        public const int WIDTH = 1;
        public const int HEIGHT = 1;

        private static string CROP_ID = RockonionFruitConfig.ID;
        private static string name = (string)STRINGS.PLANTS.ROCKONION.NAME;
        private static string desc = (string)STRINGS.PLANTS.ROCKONION.DESC;
        private static string seedName = (string)STRINGS.SEEDS.DRAGONPLANT.NAME;
        private static string seedDesc = (string)STRINGS.SEEDS.DRAGONPLANT.DESC;
        private static string domesticateddesc = (string)STRINGS.PLANTS.ROCKONION.DOMESTICATEDDESC;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            KAnimFile anim1 = Assets.GetAnim(PLANT_KANIM);
            EffectorValues decor = TUNING.DECOR.BONUS.TIER1;
            EffectorValues noise = new EffectorValues();
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name, desc, 1f, anim1, "idle_empty", Grid.SceneLayer.BuildingFront, WIDTH, HEIGHT, decor, noise);

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
            placedEntity.AddOrGet<StandardCropPlant>();
            GameObject plant = placedEntity;
            KAnimFile anim2 = Assets.GetAnim(SEED_KANIM);
            List<Tag> additionalTags = new List<Tag>() { GameTags.CropSeed };
            Tag replantGroundTag = new Tag();
            EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(plant, SeedProducer.ProductionType.Harvest, SEED_ID, seedName, seedDesc, anim2, numberOfSeeds: 0, additionalTags: additionalTags, replantGroundTag: replantGroundTag, sortOrder: 3, domesticatedDescription: domesticateddesc, width: 0.33f, height: 0.33f), "MushroomPlant_preview", Assets.GetAnim(PLANT_KANIM), "place", WIDTH, HEIGHT);
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
