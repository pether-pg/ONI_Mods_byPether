﻿using System;
using TUNING;
using UnityEngine;
using STRINGS;
using System.Collections.Generic;
using Klei.AI;

namespace FragrantFlowers
{
    public class Plant_SuperSpinosaConfig : IEntityConfig
    {
        public string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        //===> BASE INFORMATION <=========================================
        public const string ID = "SuperSpinosaPlant";

        //= string.Concat(new string[] { });

        //===> DEFINE THE ANIMATION SETTINGS FOR A SUPER CROP PLANT <=====
        private static StandardCropPlant.AnimSet animSet = new StandardCropPlant.AnimSet
        {
            grow = "super_grow",
            grow_pst = "super_grow_pst",
            idle_full = "super_idle_full",
            wilt_base = "super_wilt",
            harvest = "super_harvest"
        };

        //===> TEMPERATURE SETTINGS <=====================================
        public const float DefaultTemperature = 290.15f;       //  17°C: Normal Temperature
        public const float TemperatureLethalLow = 218.15f;     // -55ºC: Plant will die (Lowest Temp)
        public const float TemperatureWarningLow = 278.15f;    //   5°C: Plant will stop growing (Lowest Temp)
        public const float TemperatureWarningHigh = 303.15f;   //  30°C: Plant will stop growing (Highest Temp)
        public const float TemperatureLethalHigh = 398.15f;    // 125°C: Plant will die (Highest Temp)

        public const float Irrigation = Plant_SpinosaConfig.Irrigation;             // Water Irrigation Needed
        public const float Fertilization = Plant_SpinosaConfig.Fertilization;        // Dirty Fertilization Needed

        public ComplexRecipe Recipe;


        //===> DEFINE THE BASE TEMPLATE <=====================================================================
        public GameObject CreatePrefab()
        {
            GameObject gameObject = Plant_SuperSpinosaConfig.BaseWormPlant(
                ID,
                STRINGS.PLANTS.SUPERSPINOSA.NAME,
                STRINGS.PLANTS.SUPERSPINOSA.DESC,
                Plant_SpinosaConfig.PlantKanim,  // Crop KAnim file.
                DECOR.BONUS.TIER1,  // Decor tier the crop produces around it.
                Crop_SpinosaHipsConfig.ID);  // The produce ID of this crop. 

            gameObject.AddOrGet<SeedProducer>().Configure(
                Plant_SpinosaConfig.SEED_ID,  // It takes the seed definitions from its standard counterpart.
                 SeedProducer.ProductionType.Harvest, // Implies that this Crop will yeild its seed upon harvest.
                 1); // Number of seeds it will produce each time.

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
                2, //Crop height.
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
                TUNING.PLANTS.RADIATION_THRESHOLDS.TIER_5,  // Maxium value of Radiation this Crop can get before stop growing and dying.
                "SuperSpinosaOriginal", // Crop trait id.
                "Fruiting Spinosa Trait"); // Crop trait name.

            //===> SOLID FERTILIZER THIS CROP REQUIRES <============================================================================
            EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
            {
            new PlantElementAbsorber.ConsumeInfo
            {
                tag = SimHashes.Phosphorite.CreateTag(),
                massConsumptionRate = Fertilization
            }
            });

            //===> LIQUID IRRIGATION THIS CROP REQUIRES <===========================================================================
            //EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
            //{
            //new PlantElementAbsorber.ConsumeInfo
            //{
            //    tag = SimHashes.Water.CreateTag(),
            //    massConsumptionRate = Irrigation
            //}
            //});
            gameObject.AddOrGet<StandardCropPlant>();
            gameObject.AddOrGet<LoopingSounds>();

            //===> LIGHT REQUIREMENT <=============================================================================================
            Modifiers component = gameObject.GetComponent<Modifiers>();
            Db.Get().traits.Get(component.initialTraits[0]).Add(new AttributeModifier(Db.Get().PlantAttributes.MinLightLux.Id, 200f, name, false, false, true));

            component.initialAttributes.Add(Db.Get().PlantAttributes.MinLightLux.Id);
            gameObject.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(false);
            gameObject.AddOrGet<BlightVulnerable>();


            return gameObject;
        }

        public void OnPrefabInit(GameObject prefab)
        {
            TransformingPlant transformingPlant = prefab.AddOrGet<TransformingPlant>();
            transformingPlant.SubscribeToTransformEvent(GameHashes.HarvestComplete);
            transformingPlant.transformPlantId = Plant_SpinosaConfig.ID;
            prefab.GetComponent<KAnimControllerBase>().SetSymbolVisiblity("flower", false);
            prefab.AddOrGet<StandardCropPlant>().anims = Plant_SuperSpinosaConfig.animSet;
        }
        public void OnSpawn(GameObject inst)
        {
        }
    }
}