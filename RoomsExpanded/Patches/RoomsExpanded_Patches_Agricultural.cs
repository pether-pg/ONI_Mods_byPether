using Harmony;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Agricultural
    {
        
        [HarmonyPatch(typeof(FarmStationConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class FarmStationConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Agricultural.IncludeRoom) return;
                RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
                roomTracker.requiredRoomType = Db.Get().RoomTypes.CreaturePen.Id;
            }
        }
        
        [HarmonyPatch(typeof(Tinkerable))]
        [HarmonyPatch("MakeFarmTinkerable")]
        public static class Tinkerable_MakeFarmTinkerable_Patch
        {
            public static void Postfix(GameObject prefab)
            {
                if (!Settings.Instance.Agricultural.IncludeRoom) return;
                RoomTracker roomTracker = prefab.AddOrGet<RoomTracker>();
                roomTracker.requiredRoomType = Db.Get().RoomTypes.CreaturePen.Id;
            }
        }

        [HarmonyPatch(typeof(OverlayModes.Rooms))]
        [HarmonyPatch("GetCustomLegendData")]
        public static class Rooms_GetCustomLegendData_Patch
        {
            public static void Postfix(ref List<LegendEntry> __result)
            {
                if (!Settings.Instance.Agricultural.IncludeRoom) return;
                LegendEntry farm = __result.Find(entry => ((LegendEntry)entry).name.Contains("Farm"));
                if (farm != null)
                    __result.Remove(farm);
            }
        }        
    }
}
