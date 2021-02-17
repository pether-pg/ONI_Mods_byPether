using Harmony;
using Database;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Kitchen
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.Kitchen.IncludeRoom)
                __instance.Add(RoomTypes_AllModded.KitchenRoom);
        }

        private static void ModifyKitchenResultUnits(ref List<GameObject> __result, ref CookingStation __instance)
        {
            if (!Settings.Instance.Kitchen.IncludeRoom) return;

            if (RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeKitchenData.RoomId)
                && Settings.Instance.Kitchen.Bonus.HasValue)
                foreach (GameObject go in __result)
                {
                    go.GetComponent<PrimaryElement>().Units *= (1 + Settings.Instance.Kitchen.Bonus.Value);
                }
        }

        [HarmonyPatch(typeof(CookingStation))]
        [HarmonyPatch("SpawnOrderProduct")]
        public static class CookingStation_SpawnOrderProduct_Patch
        {
            public static void Postfix(ref List<GameObject> __result, ref CookingStation __instance)
            {
                if (!Settings.Instance.Kitchen.IncludeRoom) return;
                ModifyKitchenResultUnits(ref __result, ref __instance);
            }
        }

        [HarmonyPatch(typeof(GourmetCookingStation))]
        [HarmonyPatch("SpawnOrderProduct")]
        public static class GourmetCookingStation_SpawnOrderProduct_Patch
        {
            public static void Postfix(ref List<GameObject> __result, ref CookingStation __instance)
            {
                if (!Settings.Instance.Kitchen.IncludeRoom) return;
                ModifyKitchenResultUnits(ref __result, ref __instance);
            }
        }

        [HarmonyPatch(typeof(MicrobeMusher))]
        [HarmonyPatch("SpawnOrderProduct")]
        public static class MicrobeMusher_SpawnOrderProduct_Patch
        {
            public static void Postfix(ref List<GameObject> __result, ref CookingStation __instance)
            {
                if (!Settings.Instance.Kitchen.IncludeRoom) return;
                ModifyKitchenResultUnits(ref __result, ref __instance);
            }
        }


        [HarmonyPatch(typeof(CookingStationConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class CookingStationConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchen.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.KitchenBuildingTag);
            }
        }

        [HarmonyPatch(typeof(GourmetCookingStationConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class GourmetCookingStationConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchen.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.KitchenBuildingTag);
            }
        }

        [HarmonyPatch(typeof(MicrobeMusherConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class MicrobeMusherConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchen.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.KitchenBuildingTag);
            }
        }

        [HarmonyPatch(typeof(RefrigeratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class RefrigeratorConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchen.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.RefrigeratorTag);
            }
        }
    }
}
