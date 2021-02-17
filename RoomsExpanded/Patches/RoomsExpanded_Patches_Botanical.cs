using Database;
using Harmony;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Botanical
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.Botanical.IncludeRoom)
                __instance.Add(RoomTypes_AllModded.Botanical);
        }
    }
}
