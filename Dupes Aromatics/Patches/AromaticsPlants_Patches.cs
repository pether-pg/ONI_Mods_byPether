using HarmonyLib;
using KMod;
using ProcGen;
using System;
using System.Collections.Generic;
using System.Linq;
using TUNING;
using Dupes_Aromatics.Plants;

namespace Dupes_Aromatics.Patches
{
    public class AromaticsPlants_Patches
    {
        //public static Dictionary<string, CuisinePlantsTuning.CropsTuning> CropsDictionary;
        public const float CyclesForGrowth = 4f;


        [HarmonyPatch(typeof(EntityConfigManager), "LoadGeneratedEntities")]
        public static class EntityConfigManager_LoadGeneratedEntities_Patch
        {
            public static void Prefix()
            {
                //=========================================================================> SPINOSA <===============================
                //====[ SPINOSA ROSE ]===================
                RegisterStrings.MakePlantProductStrings(Crop_SpinosaRoseConfig.Id, Crop_SpinosaRoseConfig.Name, Crop_SpinosaRoseConfig.Description);

                //====[ SPINOSA HIPS ]===================
                RegisterStrings.MakeFoodStrings(Crop_SpinosaHipsConfig.Id, Crop_SpinosaHipsConfig.Name, Crop_SpinosaHipsConfig.Description);

                //====[ SPINOSA SEED ]===================
                RegisterStrings.MakeSeedStrings(Plant_SpinosaConfig.SeedId, STRINGS.SEEDS.SPINOSA.SEED_NAME, STRINGS.SEEDS.SPINOSA.SEED_DESC);

                //====[ BLOOMING SPINOSA ]===============
                RegisterStrings.MakePlantSpeciesStrings(Plant_SpinosaConfig.Id, STRINGS.PLANTS.SPINOSA.NAME, STRINGS.PLANTS.SPINOSA.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaRoseConfig.Id, 100f, 1, true));

                //====[ FRUITING SPINOSA ]===============
                RegisterStrings.MakePlantSpeciesStrings(Plant_SuperSpinosaConfig.Id, STRINGS.PLANTS.SUPERSPINOSA.NAME, STRINGS.PLANTS.SUPERSPINOSA.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaHipsConfig.Id, 100f, 1, true));

                //=========================================================================> DUSK LAVENDER <========================
                //====[ DUSKBLOOM ]===================
                RegisterStrings.MakePlantProductStrings(Crop_DuskbloomConfig.Id, Crop_DuskbloomConfig.Name, Crop_DuskbloomConfig.Description);

                //====[ DUSKBERRY ]===================
                RegisterStrings.MakeFoodStrings(Crop_DuskberryConfig.Id, Crop_DuskberryConfig.Name, Crop_DuskberryConfig.Description);

                //====[ DUSK SEED ]===================
                RegisterStrings.MakeSeedStrings(Plant_DuskLavenderConfig.SeedId, STRINGS.SEEDS.DUSKLAVENDER.SEED_NAME, STRINGS.SEEDS.DUSKLAVENDER.SEED_DESC);

                //====[ DUSKBLOOM LAVENDER ]==========
                RegisterStrings.MakePlantSpeciesStrings(Plant_DuskLavenderConfig.Id, STRINGS.PLANTS.DUSKLAVENDER.NAME, STRINGS.PLANTS.DUSKLAVENDER.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskbloomConfig.Id, 100f, 1, true));

                //====[ DUSKBERRY LAVENDER ]==========
                RegisterStrings.MakePlantSpeciesStrings(Plant_SuperDuskLavenderConfig.Id, STRINGS.PLANTS.SUPERDUSKLAVENDER.NAME, STRINGS.PLANTS.SUPERDUSKLAVENDER.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskberryConfig.Id, 100f, 1, true));

                //=========================================================================> RIMED MALLOW <========================
                //====[ RIMED COTTON BOLL ]===========
                RegisterStrings.MakePlantProductStrings(Crop_CottonBollConfig.ID, Crop_CottonBollConfig.Name, Crop_CottonBollConfig.Description);

                //====[ ICED MALLOW SEED ]============
                RegisterStrings.MakeSeedStrings(Plant_RimedMallowConfig.SeedId, STRINGS.SEEDS.RIMEDMALLOW.SEED_NAME, STRINGS.SEEDS.RIMEDMALLOW.SEED_DESC);

                //====[ RIMED MALLOW ]================
                RegisterStrings.MakePlantSpeciesStrings(Plant_RimedMallowConfig.Id, STRINGS.PLANTS.RIMEDMALLOW.NAME, STRINGS.PLANTS.RIMEDMALLOW.DESC);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_CottonBollConfig.ID, 100f, 1, true));
            }
        }
    }
}
