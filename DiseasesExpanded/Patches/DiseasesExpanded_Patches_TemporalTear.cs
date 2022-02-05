using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_TemporalTear
    {
        [HarmonyPatch(typeof(TemporalTearConfig))]
        [HarmonyPatch("OnSpawn")]
        public static class TemporalTearConfig_OnSpawn_Patch
        {
            public static void Postfix(GameObject inst)
            {
                //inst.AddOrGet<TemporalDiseaseSpreader>();
            }
        }       
    }
}
