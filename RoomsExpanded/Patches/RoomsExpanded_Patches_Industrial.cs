using HarmonyLib;
using Database;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Industrial
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.Industrial.IncludeRoom)
            {
                List<RoomType> upgrades = new List<RoomType>();
                upgrades.Add(__instance.PowerPlant);
                upgrades.Add(__instance.Farm);
                upgrades.Add(__instance.CreaturePen);
                if (Settings.Instance.Laboratory.IncludeRoom)
                    upgrades.Add(RoomTypes_AllModded.LaboratoryRoom);
                if (Settings.Instance.Kitchen.IncludeRoom)
                    upgrades.Add(RoomTypes_AllModded.KitchenRoom);
                if (Settings.Instance.Gym.IncludeRoom)
                    upgrades.Add(RoomTypes_AllModded.GymRoom);

                __instance.Add(RoomTypes_AllModded.IndustrialRoom(upgrades.ToArray()));
            }
        }
    }
}
