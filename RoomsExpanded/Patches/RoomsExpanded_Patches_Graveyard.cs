using Harmony;
using UnityEngine;
using Database;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Graveyard
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.Graveyard.IncludeRoom)
                __instance.Add(RoomTypes_AllModded.GraveyardRoom);
        }

        [HarmonyPatch(typeof(GraveConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class GraveConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.Graveyard.IncludeRoom) return;
                go.GetComponent<KPrefabID>().AddTag(RoomConstraintTags.GravestoneTag);
            }
        }
    }
}
