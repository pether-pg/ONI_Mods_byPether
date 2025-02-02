using HarmonyLib;
using UnityEngine;
using Database;
using System;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_DataMining
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!DlcManager.IsContentSubscribed(DlcManager.DLC3_ID))
                return;

            if (!Settings.Instance.DataMiningCenter.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.DataMining);
        }
    }

    [HarmonyPatch(typeof(DataMiner))]
    [HarmonyPatch("ComputeWorkProgress")]
    public static class DataMiner_ComputeWorkProgress_Patch
    {
        public static void Prefix(ref float dt)
        {
            if (!Settings.Instance.DataMiningCenter.IncludeRoom)
                return;

            dt *= (1 + Settings.Instance.DataMiningCenter.Bonus);
        }
    }
}
