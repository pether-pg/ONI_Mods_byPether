using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace Dupes_Aromatics.Plants
{
    public class Plant_DuskLavenderConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        //===> BASE INFORMATION <=========================================
        public const string Id = "DuskbloomLavender";
        public const string Name = "Duskbloom Lavender";
        public static string Description = string.Concat(new string[] { "A shrub-like plant blooms with a beautiful " + UI.FormatAsLink("Duskbloom", "Duskbloom") + "." });
        public static string DomesticatedDescription = string.Concat(new string[] { "/n/n In domesticated environment this crop requires the use of " + UI.FormatAsLink("Phosphorite", "PHOSPHORITE") + " as fertilization." });
        public const string SeedId = "LavenderSeed";
        public const string SeedName = "Dusk Seed";
        public static string SeedDescription = "The tiny seed of a " + UI.FormatAsLink("Duskbloom Lavender", "DuskbloomLavender") + ".";

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
        public const float DefaultTemperature = 299.15f;       //  26°C: Normal Temperature
        public const float TemperatureLethalLow = 258.15f;     // -15ºC: Plant will die (Lowest Temp)
        public const float TemperatureWarningLow = 288.15f;    //  15°C: Plant will stop growing (Lowest Temp)
        public const float TemperatureWarningHigh = 313.15f;   //  40°C: Plant will stop growing (Highest Temp)
        public const float TemperatureLethalHigh = 333.15f;    //  60°C: Plant will die (Highest Temp)

        public const float Fertilization = 0.014f;         // Phosphorite Fertilization Needed

        //public static AromaticsPlantsTuning.CropsTuning tuning;
        public ComplexRecipe Recipe;

        //===> DEFINE THE BASE TEMPLATE <=====================================================================
        public GameObject CreatePrefab()
        {
            GameObject gameObject = Plant_DuskLavenderConfig.BaseWormPlant(
                Id,
                Name,
                Description,
                "plant_lavender_kanim",  // Crop KAnim file.
                TUNING.DECOR.BONUS.TIER2,  // Decor tier the crop produces around it.
                "Duskbloom");  // The produce ID of this crop. 

            //===> BASE SETTINGS FOR THE CROP SEED <=======================================================
            List<Tag> additionalTags = new List<Tag>();
            additionalTags.Add(GameTags.CropSeed);
            Tag replantGroundTag = new Tag();

            EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(
                gameObject,
                SeedProducer.ProductionType.Harvest, //Implies the seed will be produced upon harvest.
                SeedId,
                SeedName,
                SeedDescription,
                Assets.GetAnim("seed_lavender_kanim"), //The Crop seed KAnim
                "object",
                1, //Number of seeds produced each time.
                additionalTags,
                SingleEntityReceptacle.ReceptacleDirection.Top, //The orientation which the seed requires the planter box to be pointing.
                replantGroundTag,
                2,
                DomesticatedDescription,
                EntityTemplates.CollisionShape.CIRCLE,
                0.2f,
                0.2f,
                null,
                "",
                false),
                "DuskbloomLavender_preview",
                Assets.GetAnim("plant_lavender_kanim"),
                "place",
                1, // Preview Crop width
                3); // Preview Crop Height

            return gameObject;
        }

        //===> ENTITY SETTINGS <===========================================================================
        public static GameObject BaseWormPlant(string id, string name, string desc, string animFile, EffectorValues decor, string cropID)
        {
            GameObject gameObject = EntityTemplates.CreatePlacedEntity(
                Id,
                Name,
                Description,
                1f, // Specify the entity mass in kg.
                Assets.GetAnim("plant_lavender_kanim"),
                "idle_empty",
                Grid.SceneLayer.BuildingBack,  // The layer which this crop will be placed in game.
                1, //Crop width.
                3, //Crop height.
                TUNING.DECOR.BONUS.TIER2,
                default(EffectorValues),
                SimHashes.Creature,
                null,
                DefaultTemperature
                );

            EntityTemplates.ExtendEntityToBasicPlant(
                gameObject,
                TemperatureLethalLow,
                TemperatureWarningLow,
                TemperatureWarningHigh,
                TemperatureLethalHigh,

                //===> SAFE ATMOSPHERE ELEMENTS <===================================================================================
                // Plant will not grow in any element other than the ones here.
                new SimHashes[]{
                SimHashes.Oxygen,
                SimHashes.ContaminatedOxygen,
                SimHashes.CarbonDioxide
                },

                //===> BASE SETTINGS <==============================================================================================
                true, // Implies that this Crop is sensible to Atmospheric Pressure
                0f, // Pressure which this Crop will die
                0.15f, // Pressure which this Crop will stop growing.
                Crop_DuskbloomConfig.Id,
                true, // Implies this Crop can be drowned by liquids.
                true, // Implies this Crop can receive Micro Fertilizer buff in the agricultural room.
                true, // Implies this Crop requires a solid ground to grow.
                true, // Implies this Crop will grow old and eventualy yeilds a produce.
                2400f, // Max age this Crop can grow, or the time it require for it to complete its growth.
                0f, // Minium Radiation required by this Crop.
                9800f, // Maxium value of Radiation this Crop can get before stop growing and dying.
                "LavenderOriginal", // Crop trait id.
                "Lavender Original"); // Crop trait name.

            //===> SOLID FERTILIZER THIS CROP REQUIRES <============================================================================
            EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
            {
            new PlantElementAbsorber.ConsumeInfo
            {
                tag = SimHashes.Phosphorite.CreateTag(),
                massConsumptionRate = Fertilization
            }
            });

            gameObject.AddOrGet<StandardCropPlant>();
            gameObject.AddOrGet<LoopingSounds>();
            gameObject.AddOrGet<BlightVulnerable>();

            //===> DISEASE OR GERMS THIS CROP RELEASES <===========================================================================
            DiseaseDropper.Def def = gameObject.AddOrGetDef<DiseaseDropper.Def>();
            def.diseaseIdx = Db.Get().Diseases.GetIndex(Db.Get().Diseases.PollenGerms.id);
            def.singleEmitQuantity = 1000000;
           
            return gameObject;
        }

        //===> CROP SWAP WITH DIVERGENT CRITTER TOUCH <===========================================================================
        public void OnPrefabInit(GameObject prefab)
        {
            TransformingPlant transformingPlant = prefab.AddOrGet<TransformingPlant>();
            transformingPlant.transformPlantId = Plant_SuperDuskLavenderConfig.Id;
            transformingPlant.SubscribeToTransformEvent(GameHashes.CropTended);
            transformingPlant.useGrowthTimeRatio = true;
            transformingPlant.eventDataCondition = delegate (object data)
            {
                CropTendingStates.CropTendingEventData cropTendingEventData = (CropTendingStates.CropTendingEventData)data;
                if (cropTendingEventData != null)
                {
                    CreatureBrain component = cropTendingEventData.source.GetComponent<CreatureBrain>();
                    if (component != null && component.species == GameTags.Creatures.Species.DivergentSpecies)
                    {
                        return true;
                    }
                }
                return false;
            };
            transformingPlant.fxKAnim = "plant_transform_fx_kanim";
            transformingPlant.fxAnim = "plant_transform";
            prefab.AddOrGet<StandardCropPlant>().anims = Plant_DuskLavenderConfig.animSet;
        }

        public void OnSpawn(GameObject inst)
        {
        }

    }
}
