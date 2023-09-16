using HarmonyLib;
using UnityEngine;

namespace GridCellHelper
{
    public class Patches
    {
        [HarmonyPatch(typeof(HeadquartersConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public class HeadquartersConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<CursorLocationEffect>();
            }
        }

        [HarmonyPatch(typeof(MinionConfig))]
        [HarmonyPatch("CreatePrefab")]
        public class MinionConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                //__result.AddOrGet<CursorLocationEffect>();
            }
        }
    }
}
