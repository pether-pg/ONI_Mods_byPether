using Harmony;
using Database;
using System;
using UnityEngine;
using System.Collections.Generic;
using STRINGS;

namespace RoomsExpanded
{
    public class RoomsExpanded_Patches
    {
        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
                GVD.VersionAlert(DlcManager.IsExpansion1Active(), "OnLoad() version check");
                //Debug.Log("RoomsExpanded: Loaded mod. Last mod update: 2021.02.24");
            }
        }

        [HarmonyPatch(typeof(RoomTypes), MethodType.Constructor, new Type[] { typeof(ResourceSet) })]
        public class RoomTypes_Constructor_Patch
        {
            public static void Prefix()
            {
                RoomsExpanded_Patches_Agricultural.PrepareModifications();
            }

            public static void Postfix(ref RoomTypes __instance)
            {
                SortingCounter.Init();

                RoomsExpanded_Patches_Laboratory.AddRoom(ref __instance);
                RoomsExpanded_Patches_Bathroom.AddRoom(ref __instance);
                RoomsExpanded_Patches_Kitchen.AddRoom(ref __instance);
                RoomsExpanded_Patches_Nursery.AddRoom(ref __instance);
                RoomsExpanded_Patches_Gym.AddRoom(ref __instance);
                RoomsExpanded_Patches_Agricultural.ModifyRoom(ref __instance);
                RoomsExpanded_Patches_Aquarium.AddRoom(ref __instance);
                RoomsExpanded_Patches_Botanical.AddRoom(ref __instance);
                RoomsExpanded_Patches_Graveyard.AddRoom(ref __instance);
                RoomsExpanded_Patches_Hospital.UpdateRoom(ref __instance);
                RoomsExpanded_Patches_Industrial.AddRoom(ref __instance);
                RoomsExpanded_Patches_Museum.AddRoom(ref __instance);

                ResizeRooms(ref __instance);
            }

            static void ResizeRooms(ref RoomTypes __instance)
            {
                for (int i = 0; i < __instance.Count; i++)
                {
                    if (__instance[i] == null || __instance[i].additional_constraints == null)
                        continue;

                    for (int add = 0; add < __instance[i].additional_constraints.Length; add++)
                    {
                        if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_64)
                            __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.ResizeMaxRoomSize64);
                        else if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_96)
                            __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.ResizeMaxRoomSize96);
                        else if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_120)
                            __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.ResizeMaxRoomSize120);
                    }
                }
            }
        }

        [HarmonyPatch(typeof(OverlayModes.Rooms))]
        [HarmonyPatch("GetCustomLegendData")]
        public static class Rooms_GetCustomLegendData_Patch
        {
            public static void Postfix(ref List<LegendEntry> __result)
            {
                if (Settings.Instance.HideLegendEffect)
                    foreach (LegendEntry entry in __result)
                        entry.name = entry.name.Split('\n')[0];

                List<RoomType> roomTypeList = new List<RoomType>((IEnumerable<RoomType>)Db.Get().RoomTypes.resources);
                foreach (RoomType roomType in roomTypeList)
                {
                    if (roomType.effects == null && !string.IsNullOrEmpty(roomType.effect))
                    {
                        for (int i = 0; i < __result.Count; i++)
                            if (__result[i].name.Contains(roomType.Name))
                            {
                                string header = (string)ROOMS.EFFECTS.HEADER;
                                __result[i].desc += $"\n\n{header}\n    {roomType.effect}";
                            }
                    }
                }

                if (Settings.Instance.Agricultural.IncludeRoom)
                {
                    LegendEntry farm = __result.Find(entry => ((LegendEntry)entry).name.Contains(ROOMS.TYPES.FARM.NAME));
                    if (farm != null)
                        __result.Remove(farm);
                }
            }
        }
    }
}

