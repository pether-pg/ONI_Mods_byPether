using Harmony;
using Database;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Bathroom
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.Bathroom.IncludeRoom)
            {
                __instance.Add(RoomTypes_AllModded.BathroomRoom);

                if (__instance.PlumbedBathroom.primary_constraint.stomp_in_conflict == null)
                    __instance.PlumbedBathroom.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                __instance.PlumbedBathroom.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.BathroomRoom.primary_constraint);

                if (__instance.Hospital.primary_constraint.stomp_in_conflict == null)
                    __instance.Hospital.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                __instance.Hospital.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.BathroomRoom.primary_constraint);
            }
        }

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
                    || RoomTypes_AllModded.IsInTheRoom(__instance, PlumbedBathroomId)
                    || RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypePrivateRoomData.RoomId))
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
