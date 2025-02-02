using HarmonyLib;
using UnityEngine;
using Database;
using System;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_BionicUpkeep
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!DlcManager.IsContentSubscribed(DlcManager.DLC3_ID))
                return;

            if (!Settings.Instance.BionicWorkshop.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.BionicUpkeepRoom);

            RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.BionicUpkeepRoom, __instance.Latrine);
            RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.BionicUpkeepRoom, __instance.PlumbedBathroom);
            RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.BionicUpkeepRoom, RoomTypes_AllModded.BathroomRoom);
        }

        [HarmonyPatch(typeof(GunkEmptierConfig))]
        [HarmonyPatch(nameof(GunkEmptierConfig.ConfigureBuildingTemplate))]
        public static class GunkEmptierConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.BionicWorkshop.IncludeRoom)
                    return;

                go.RemoveTag(RoomConstraints.ConstraintTags.FlushToiletType);
            }
        }
    }
}
