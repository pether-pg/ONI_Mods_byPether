using HarmonyLib;
using KMod;
using ProcGen;
using System;
using System.Collections.Generic;
using System.Linq;
using TUNING;

namespace Dupes_Aromatics.Plants
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
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{Crop_SpinosaRoseConfig.Id.ToUpperInvariant()}.NAME", Crop_SpinosaRoseConfig.Name);
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{Crop_SpinosaRoseConfig.Id.ToUpperInvariant()}.DESC", Crop_SpinosaRoseConfig.Description);

                //====[ SPINOSA HIPS ]===================
                Strings.Add($"STRINGS.ITEMS.FOOD.{Crop_SpinosaHipsConfig.Id.ToUpperInvariant()}.NAME", Crop_SpinosaHipsConfig.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Crop_SpinosaHipsConfig.Id.ToUpperInvariant()}.DESC", Crop_SpinosaHipsConfig.Description);

                //====[ SPINOSA SEED ]===================
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Plant_SpinosaConfig.SeedId.ToUpperInvariant()}.NAME", Plant_SpinosaConfig.SeedName);
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Plant_SpinosaConfig.SeedId.ToUpperInvariant()}.DESC", Plant_SpinosaConfig.SeedDescription);

                //====[ BLOOMING SPINOSA ]===============
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_SpinosaConfig.Id.ToUpperInvariant()}.NAME", Plant_SpinosaConfig.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_SpinosaConfig.Id.ToUpperInvariant()}.DESC", Plant_SpinosaConfig.Description);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaRoseConfig.Id, 100f, 1, true));

                //====[ FRUITING SPINOSA ]===============
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_SuperSpinosaConfig.Id.ToUpperInvariant()}.NAME", Plant_SuperSpinosaConfig.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_SuperSpinosaConfig.Id.ToUpperInvariant()}.DESC", Plant_SuperSpinosaConfig.Description);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaHipsConfig.Id, 100f, 1, true));

                //=========================================================================> DUSK LAVENDER <========================
                //====[ DUSKBLOOM ]===================
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{Crop_DuskbloomConfig.Id.ToUpperInvariant()}.NAME", Crop_DuskbloomConfig.Name);
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{Crop_DuskbloomConfig.Id.ToUpperInvariant()}.DESC", Crop_DuskbloomConfig.Description);

                //====[ DUSKBERRY ]===================
                Strings.Add($"STRINGS.ITEMS.FOOD.{Crop_DuskberryConfig.Id.ToUpperInvariant()}.NAME", Crop_DuskberryConfig.Name);
                Strings.Add($"STRINGS.ITEMS.FOOD.{Crop_DuskberryConfig.Id.ToUpperInvariant()}.DESC", Crop_DuskberryConfig.Description);

                //====[ DUSK SEED ]===================
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Plant_DuskLavenderConfig.SeedId.ToUpperInvariant()}.NAME", Plant_DuskLavenderConfig.SeedName);
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Plant_DuskLavenderConfig.SeedId.ToUpperInvariant()}.DESC", Plant_DuskLavenderConfig.SeedDescription);

                //====[ DUSKBLOOM LAVENDER ]==========
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_DuskLavenderConfig.Id.ToUpperInvariant()}.NAME", Plant_DuskLavenderConfig.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_DuskLavenderConfig.Id.ToUpperInvariant()}.DESC", Plant_DuskLavenderConfig.Description);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskbloomConfig.Id, 100f, 1, true));

                //====[ DUSKBERRY LAVENDER ]==========
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_SuperDuskLavenderConfig.Id.ToUpperInvariant()}.NAME", Plant_SuperDuskLavenderConfig.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_SuperDuskLavenderConfig.Id.ToUpperInvariant()}.DESC", Plant_SuperDuskLavenderConfig.Description);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskberryConfig.Id, 100f, 1, true));

                //=========================================================================> RIMED MALLOW <========================
                //====[ RIMED COTTON BOLL ]===========
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{Crop_CottonBollConfig.ID.ToUpperInvariant()}.NAME", Crop_CottonBollConfig.Name);
                Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{Crop_CottonBollConfig.ID.ToUpperInvariant()}.DESC", Crop_CottonBollConfig.Description);

                //====[ ICED MALLOW SEED ]============
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Plant_RimedMallowConfig.SeedId.ToUpperInvariant()}.NAME", Plant_RimedMallowConfig.SeedName);
                Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{Plant_RimedMallowConfig.SeedId.ToUpperInvariant()}.DESC", Plant_RimedMallowConfig.SeedDescription);

                //====[ RIMED MALLOW ]================
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_RimedMallowConfig.Id.ToUpperInvariant()}.NAME", Plant_RimedMallowConfig.Name);
                Strings.Add($"STRINGS.CREATURES.SPECIES.{Plant_RimedMallowConfig.Id.ToUpperInvariant()}.DESC", Plant_RimedMallowConfig.Description);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_CottonBollConfig.ID, 100f, 1, true));
            }
        }
    }
}
