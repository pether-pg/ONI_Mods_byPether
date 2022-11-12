using System.Collections.Generic;
using UnityEngine;
using TUNING;

namespace ExoticCuisine
{
    class MantleerPlntConfig : IEntityConfig
    {
        public const float FERTILIZATION_RATE = 0.006666667f;
        public const string ID = "MantleerPlnt";
        public const string SEED_ID = "MantleerPlntSeed";
        public const string PLANT_KANIM = "mantleer_kanim";
        public const string SEED_KANIM = "mantleerSeed_kanim";
        public const string FRUIT_KANIM = "mantleerFruit_kanim";
        public const float CROP_DURATION = 45f; //in seconds
        public const int WIDTH = 1;
        public const int HEIGHT = 2;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            string name1 = (string)STRINGS.PLANTS.MANTLEER.NAME;
            string desc1 = (string)STRINGS.PLANTS.MANTLEER.DESC;
            KAnimFile anim1 = Assets.GetAnim(PLANT_KANIM);
            EffectorValues decor = TUNING.DECOR.BONUS.TIER1;
            EffectorValues noise = new EffectorValues();
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name1, desc1, 1f, anim1, "idle_empty", Grid.SceneLayer.BuildingFront, WIDTH, HEIGHT, decor, noise);

            SimHashes[] safeElements = new SimHashes[2]
            {
                SimHashes.CarbonDioxide,
                SimHashes.Oxygen
            };

            EntityTemplates.ExtendEntityToBasicPlant(placedEntity, 228.15f, 278.15f, 308.15f, safe_elements: safeElements, crop_id: MantleerPlntFruitConfig.ID, max_radiation: 4600f, baseTraitId: "MushroomPlantOriginal", baseTraitName: ((string)STRINGS.PLANTS.DRAGONPLANT.NAME));
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
            string name2 = (string)STRINGS.SEEDS.MANTLEER.NAME;
            string desc2 = (string)STRINGS.SEEDS.MANTLEER.DESC;
            KAnimFile anim2 = Assets.GetAnim(SEED_KANIM);
            List<Tag> additionalTags = new List<Tag>() { GameTags.CropSeed };
            Tag replantGroundTag = new Tag();
            string domesticateddesc = (string)STRINGS.PLANTS.MANTLEER.DOMESTICATEDDESC;
            EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(plant, SeedProducer.ProductionType.Harvest, SEED_ID, name2, desc2, anim2, numberOfSeeds: 0, additionalTags: additionalTags, replantGroundTag: replantGroundTag, sortOrder: 3, domesticatedDescription: domesticateddesc, width: 0.33f, height: 0.33f), "MushroomPlant_preview", Assets.GetAnim(PLANT_KANIM), "place", WIDTH, HEIGHT);
            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", TUNING.NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", TUNING.NOISE_POLLUTION.CREATURES.TIER3);
            return placedEntity;
        }

        public void OnPrefabInit(GameObject inst)
        {
            Light2D light2D = inst.AddOrGet<Light2D>();
            light2D.overlayColour = LIGHT2D.FLOORLAMP_OVERLAYCOLOR;
            light2D.Color = LIGHT2D.FLOORLAMP_COLOR;
            light2D.Range = 4f;
            light2D.Angle = 0.0f;
            light2D.Direction = LIGHT2D.FLOORLAMP_DIRECTION;
            light2D.Offset = new Vector2(0.4f, 1.1f);
            light2D.shape = LightShape.Circle;
            light2D.drawOverlay = true;

            inst.AddOrGetDef<MantleerLightControler.Def>();
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}
