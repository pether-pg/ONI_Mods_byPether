using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace Dupes_Aromatics.Plants
{
    public class Plant_SpinosaConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        //===> BASE INFORMATION <=========================================
        public const string ID = "SpinosaPlant";
        public const string SEED_ID = "SpinosaSeed";
        public const string PlantKanim = "plant_spinosa_kanim";
        public const string SeedKanim = "seed_spinosa_kanim";

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
        public const float DefaultTemperature = 290.15f;       //  17°C: Normal Temperature
        public const float TemperatureLethalLow = 218.15f;     // -55ºC: Plant will die (Lowest Temp)
        public const float TemperatureWarningLow = 278.15f;    //   5°C: Plant will stop growing (Lowest Temp)
        public const float TemperatureWarningHigh = 303.15f;   //  30°C: Plant will stop growing (Highest Temp)
        public const float TemperatureLethalHigh = 398.15f;    // 125°C: Plant will die (Highest Temp)

        public const float Irrigation = 0.03f;             // Water Irrigation Needed
        public const float Fertilization = 0.012f;         // Dirty Fertilization Needed

        //public static AromaticsPlantsTuning.CropsTuning tuning;
        public ComplexRecipe Recipe;


        //===> DEFINE THE BASE TEMPLATE <=====================================================================
        public GameObject CreatePrefab()
        {
            GameObject gameObject = Plant_SpinosaConfig.BaseWormPlant(
                ID,
                STRINGS.PLANTS.SPINOSA.NAME,
                STRINGS.PLANTS.SPINOSA.DESC,
                PlantKanim,  // Crop KAnim file.
                DECOR.BONUS.TIER2,  // Decor tier the crop produces around it.
                Crop_SpinosaRoseConfig.ID);  // The produce ID of this crop. 

            //===> BASE SETTINGS FOR THE CROP SEED <=======================================================
            List<Tag> additionalTags = new List<Tag>();
            additionalTags.Add(GameTags.CropSeed);
            Tag replantGroundTag = new Tag();

            EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(
                gameObject,
                SeedProducer.ProductionType.Harvest, //Implies the seed will be produced upon harvest.
                SEED_ID,
                STRINGS.SEEDS.SPINOSA.SEED_NAME,
                STRINGS.SEEDS.SPINOSA.SEED_DESC,
                Assets.GetAnim(SeedKanim), //The Crop seed KAnim
                "object",
                1, //Number of seeds produced each time.
                additionalTags,
                SingleEntityReceptacle.ReceptacleDirection.Top, //The orientation which the seed requires the planter box to be pointing.
                replantGroundTag,
                2,
                STRINGS.PLANTS.SPINOSA.DOMESTICATED_DESC,
                EntityTemplates.CollisionShape.CIRCLE,
                0.2f,
                0.2f,
                null,
                "",
                false),
                "SpinosaPlant_preview",
                Assets.GetAnim(PlantKanim),
                "place",
                1, // Preview Crop width
                3); // Preview Crop Height

            return gameObject;
        }

        //===> ENTITY SETTINGS <===========================================================================
        public static GameObject BaseWormPlant(string id, string name, string desc, string animFile, EffectorValues decor, string cropID)
        {
            GameObject gameObject = EntityTemplates.CreatePlacedEntity(
                id,
                name,
                desc,
                1f, // Specify the entity mass in kg.
                Assets.GetAnim(animFile),
                "idle_empty",
                Grid.SceneLayer.BuildingBack,  // The layer which this crop will be placed in game.
                1, //Crop width.
                3, //Crop height.
                decor,
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
                cropID,
                true, // Implies this Crop can be drowned by liquids.
                true, // Implies this Crop can receive Micro Fertilizer buff in the agricultural room.
                true, // Implies this Crop requires a solid ground to grow.
                true, // Implies this Crop will grow old and eventualy yeilds a produce.
                2400f, // Max age this Crop can grow, or the time it require for it to complete its growth.
                0f, // Minium Radiation required by this Crop.
                9800f, // Maxium value of Radiation this Crop can get before stop growing and dying.
                "SpinozaOriginal", // Crop trait id.
                "Spinoza Original"); // Crop trait name.

            //===> SOLID FERTILIZER THIS CROP REQUIRES <============================================================================
            EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
            {
            new PlantElementAbsorber.ConsumeInfo
            {
                tag = SimHashes.Dirt.CreateTag(),
                massConsumptionRate = Fertilization
            }
            });

            //===> LIQUID IRRIGATION THIS CROP REQUIRES <===========================================================================
            EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
            {
            new PlantElementAbsorber.ConsumeInfo
            {
                tag = SimHashes.Water.CreateTag(),
                massConsumptionRate = Irrigation
            }
            });
            gameObject.AddOrGet<StandardCropPlant>();
            gameObject.AddOrGet<LoopingSounds>();

            //===> DISEASE OR GERMS THIS CROP RELEASES <===========================================================================
            DiseaseDropper.Def def = gameObject.AddOrGetDef<DiseaseDropper.Def>();
            def.diseaseIdx = Db.Get().Diseases.GetIndex(RoseScent.ID);
            def.emitFrequency = 1f;
            def.averageEmitPerSecond = 1000;
            def.singleEmitQuantity = 1000000;
            gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = RoseScent.ID;

            //===> LIGHT REQUIREMENT <=============================================================================================
            Modifiers component = gameObject.GetComponent<Modifiers>();
            Db.Get().traits.Get(component.initialTraits[0]).Add(new AttributeModifier(Db.Get().PlantAttributes.MinLightLux.Id, 200f, name, false, false, true));

            component.initialAttributes.Add(Db.Get().PlantAttributes.MinLightLux.Id);
            gameObject.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(false);
            gameObject.AddOrGet<BlightVulnerable>();


            return gameObject;
        }

        //===> CROP SWAP WITH DIVERGENT CRITTER TOUCH <===========================================================================
        public void OnPrefabInit(GameObject prefab)
        {
            TransformingPlant transformingPlant = prefab.AddOrGet<TransformingPlant>();
            transformingPlant.transformPlantId = Plant_SuperSpinosaConfig.ID;
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
            prefab.AddOrGet<StandardCropPlant>().anims = Plant_SpinosaConfig.animSet;
        }

        public void OnSpawn(GameObject inst)
        {
        }
    }
}