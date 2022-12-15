using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System;
using System.Linq;
using System.Collections.Generic;
using Database;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_MissionControl
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!Settings.Instance.MissionControl.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.MissionControlRoom);
        }

        [HarmonyPatch(typeof(MissionControlClusterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class MissionControlClusterConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                if (!Settings.Instance.MissionControl.IncludeRoom) 
                    return;

                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.SpaceBuildingTag);

                RoomTracker roomTracker = go.GetComponent<RoomTracker>();
                if (roomTracker != null)
                    roomTracker.requiredRoomType = RoomTypes_AllModded.MissionControlRoom.Id;
            }
        }

        [HarmonyPatch(typeof(MissionControlConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class MissionControlConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(ref GameObject go)
            {
                if (!Settings.Instance.MissionControl.IncludeRoom) 
                    return;

                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.SpaceBuildingTag); 

                RoomTracker roomTracker = go.GetComponent<RoomTracker>();
                if (roomTracker != null)
                    roomTracker.requiredRoomType = RoomTypes_AllModded.MissionControlRoom.Id;
            }
        }
    }
}
