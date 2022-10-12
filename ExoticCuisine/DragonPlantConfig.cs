using System.Collections.Generic;
using UnityEngine;

namespace ExoticCuisine
{
    class DragonPlantConfig : IEntityConfig
    {
        public const float FERTILIZATION_RATE = 0.006666667f;
        public const string ID = "DragonPlant";
        public const string SEED_ID = "DragonPlantSeed";
        public const string PLANT_KANIM = "drakesMouth_kanim";
        public const string SEED_KANIM = "drakesMouthSeed_kanim";
        public const string FRUIT_KANIM = "drakesMouthFruit_kanim";
        public const float CROP_DURATION = 4500f;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            string name1 = (string)STRINGS.PLANTS.DRAGONPLANT.NAME;
            string desc1 = (string)STRINGS.PLANTS.DRAGONPLANT.DESC;
            KAnimFile anim1 = Assets.GetAnim(PLANT_KANIM);
            EffectorValues decor = TUNING.DECOR.BONUS.TIER1;
            EffectorValues noise = new EffectorValues();
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name1, desc1, 1f, anim1, "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, decor, noise);

            SimHashes[] safeElements = new SimHashes[2]
            {
                SimHashes.CarbonDioxide,
                SimHashes.Oxygen
            };

            EntityTemplates.ExtendEntityToBasicPlant(placedEntity, 228.15f, 278.15f, 308.15f, safe_elements: safeElements, crop_id: DragonPlantFruitConfig.ID, max_radiation: 4600f, baseTraitId: "MushroomPlantOriginal", baseTraitName: ((string)STRINGS.PLANTS.DRAGONPLANT.NAME));
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
            string name2 = (string)STRINGS.SEEDS.DRAGONPLANT.NAME;
            string desc2 = (string)STRINGS.SEEDS.DRAGONPLANT.DESC;
            KAnimFile anim2 = Assets.GetAnim(SEED_KANIM);
            List<Tag> additionalTags = new List<Tag>() { GameTags.CropSeed };
            Tag replantGroundTag = new Tag();
            string domesticateddesc = (string)STRINGS.PLANTS.DRAGONPLANT.DOMESTICATEDDESC;
            EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(plant, SeedProducer.ProductionType.Harvest, SEED_ID, name2, desc2, anim2, numberOfSeeds: 0, additionalTags: additionalTags, replantGroundTag: replantGroundTag, sortOrder: 3, domesticatedDescription: domesticateddesc, width: 0.33f, height: 0.33f), "MushroomPlant_preview", Assets.GetAnim(PLANT_KANIM), "place", 1, 2);
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
