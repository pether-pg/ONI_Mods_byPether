using Harmony;
using Database;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Bathroom
    {
        [HarmonyPatch(typeof(Shower))]
        [HarmonyPatch("OnWorkTick")]
        public static class Shower_OnWorkTick_Patch
        {
            private static string PlumbedBathroomId = string.Empty;

            public static void Postfix(ref Shower __instance, float dt)
            {
                if (!Settings.Instance.Bathroom.IncludeRoom) return;

                if (string.IsNullOrEmpty(PlumbedBathroomId))
                    PlumbedBathroomId = Db.Get().RoomTypes.PlumbedBathroom.Id;

                if ((RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypeBathroomData.RoomId)
                    || RoomTypes_AllModded.IsInTheRoom(__instance, PlumbedBathroomId))
                    && Settings.Instance.Bathroom.Bonus.HasValue)
                {
                    __instance.WorkTimeRemaining -= dt * Settings.Instance.Bathroom.Bonus.Value;
                }
            }
        }

        [HarmonyPatch(typeof(ShowerConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class ShowerConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Bathroom.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.BathroomTag);
            }
        }
    }
}
