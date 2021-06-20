using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace SymbioticGerms
{
    public class SymbioticGerms_Patches
    {
        [HarmonyPatch(typeof(CreatureCalorieMonitor.Instance))]
        [HarmonyPatch("Poop")]
        public class CreatureCalorieMonitor_Poop_Patch
        {
            public static void Prefix(CreatureCalorieMonitor.Instance __instance)
            {
                GameObject go = __instance.gameObject;
                if (go == null)
                    return;

                if (go.name.Contains("Oilfloater")) // include modded ones
                    BonusFunctions.AdditionalLiquidPoop(go, __instance, Numbers.IndexZombieSpores, Settings.Instance.MaxSlicksterBonus);
                if (go.name == "PacuAlgae")
                    BonusFunctions.AdditionalSolidPoop(go, __instance, Numbers.IndexFoodPoisoning, Settings.Instance.MaxPacuBonus);
                if (go.name == "DreckoOpulent")
                    BonusFunctions.AdditionalSolidPoop(go, __instance, Numbers.IndexSlimeLung, Settings.Instance.MaxDreckoBonus);
                if (go.name == "PuftCO2")
                    BonusFunctions.AdditionalSolidPoop(go, __instance, Numbers.IndexZombieSpores, Settings.Instance.MaxPuftBonus);
            }
        }

        [HarmonyPatch(typeof(Crop))]
        //[HarmonyPatch("SpawnFruit")] // vanilla
        [HarmonyPatch("SpawnSomeFruit")] // dlc
        public class Crop_SpawnFruit_Patch
        {

            public static void Prefix(Crop __instance, out float __state)
            {
                __state = 0;
                GameObject go = __instance.gameObject;
                if (go == null)
                    return;

                if (go.name == "MushroomPlant")
                    BonusFunctions.SpawnGas(go, Numbers.IndexSlimeLung, Settings.Instance.MaxDuskCupBonus, SimHashes.ContaminatedOxygen);
                if (go.name == "BasicSingleHarvestPlant")
                    BonusFunctions.SpawnAdditionalFood(go, Numbers.IndexFoodPoisoning, Settings.Instance.MaxMealLiceBonus, __instance);
                if (go.name == "SwampHarvestPlant")
                    BonusFunctions.SpawnAdditionalFood(go, Numbers.IndexFoodPoisoning, Settings.Instance.MaxBogBucketBonus, __instance);
                if (go.name == "ColdWheat")
                    BonusFunctions.ChanceForDoubleHarvest(go, Numbers.IndexZombieSpores, Settings.Instance.MaxWheatChance, __instance);
                if (go.name == "BeanPlant")
                    BonusFunctions.ChanceForDoubleHarvest(go, Numbers.IndexZombieSpores, Settings.Instance.MaxBeansChance, __instance);
                if (go.name == "SpiceVine")
                {
                    __state = Numbers.GetGermKillTempDelta(go) * Settings.Instance.MaxPepperTempScale;
                    BonusFunctions.ModifyTemperature(go, __state);
                }    
            }

            public static void Postfix(Crop __instance, float __state)
            {
                GameObject go = __instance.gameObject;
                if (go == null)
                    return;

                if (go.name == "SpiceVine")
                    BonusFunctions.ModifyTemperature(go, -1 * __state);

            }
        }
    }
}
