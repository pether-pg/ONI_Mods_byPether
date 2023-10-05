using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_DiseaseDroppers
    {
        [HarmonyPatch(typeof(EvilFlowerConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class EvilFlowerConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = ZombieSpores.ID;
            }
        }

        [HarmonyPatch(typeof(PrickleFlowerConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class PrickleFlowerConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = PollenGerms.ID;
            }
        }

        [HarmonyPatch(typeof(BulbPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class BulbPlantConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = PollenGerms.ID;
            }
        }
    }
}
