using HarmonyLib;
using Database;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Kitchenette
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!Settings.Instance.Kitchenette.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.KitchenetteRoom);
            RoomConstraintTags.AddStompInConflict(__instance.Kitchen, RoomTypes_AllModded.KitchenetteRoom);
        }

        private static void ModifyKitchenResultUnits(ref List<GameObject> __result, ref CookingStation __instance)
        {
            if (!Settings.Instance.Kitchenette.IncludeRoom) return;

            if (RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeKitchenetteData.RoomId) || 
                RoomTypes_AllModded.IsInTheRoom(__instance, Db.Get().RoomTypes.Kitchen.Id))
                foreach (GameObject go in __result)
                {
                    go.GetComponent<PrimaryElement>().Units *= (1 + Settings.Instance.Kitchenette.Bonus);
                }
        }

        [HarmonyPatch(typeof(CookingStation))]
        [HarmonyPatch("SpawnOrderProduct")]
        public static class CookingStation_SpawnOrderProduct_Patch
        {
            public static void Postfix(ref List<GameObject> __result, ref CookingStation __instance)
            {
                if (!Settings.Instance.Kitchenette.IncludeRoom) return;
                ModifyKitchenResultUnits(ref __result, ref __instance);
            }
        }

        [HarmonyPatch(typeof(GourmetCookingStation))]
        [HarmonyPatch("SpawnOrderProduct")]
        public static class GourmetCookingStation_SpawnOrderProduct_Patch
        {
            public static void Postfix(ref List<GameObject> __result, ref CookingStation __instance)
            {
                if (!Settings.Instance.Kitchenette.IncludeRoom) return;
                ModifyKitchenResultUnits(ref __result, ref __instance);
            }
        }

        [HarmonyPatch(typeof(MicrobeMusher))]
        [HarmonyPatch("SpawnOrderProduct")]
        public static class MicrobeMusher_SpawnOrderProduct_Patch
        {
            public static void Postfix(ref List<GameObject> __result, ref CookingStation __instance)
            {
                if (!Settings.Instance.Kitchenette.IncludeRoom) return;
                ModifyKitchenResultUnits(ref __result, ref __instance);
            }
        }


        [HarmonyPatch(typeof(CookingStationConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class CookingStationConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchenette.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.KitchenBuildingTag);
            }
        }

        [HarmonyPatch(typeof(GourmetCookingStationConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class GourmetCookingStationConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchenette.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.KitchenBuildingTag);
            }
        }

        [HarmonyPatch(typeof(MicrobeMusherConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class MicrobeMusherConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchenette.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.KitchenBuildingTag);
            }
        }

        [HarmonyPatch(typeof(RefrigeratorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class RefrigeratorConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Kitchenette.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.RefrigeratorTag);
            }
        }
    }
}
