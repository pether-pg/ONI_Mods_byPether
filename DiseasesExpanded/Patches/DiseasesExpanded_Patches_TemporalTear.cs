using HarmonyLib;
using UnityEngine;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_TemporalTear
    {

        /*[HarmonyPatch(typeof(TemporalTearConfig))]
        [HarmonyPatch("OnSpawn")]
        public static class TemporalTearConfig_OnSpawn_Patch
        {
            public static void Postfix(GameObject inst)
            {
                Debug.Log($"{ModInfo.Namespace}: TemporalTearConfig_OnSpawn_Patch");
                inst.AddOrGet<NewDayTestBehaviour>();
            }
        }*/
    }
}
