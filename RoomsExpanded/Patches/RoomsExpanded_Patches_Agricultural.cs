using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using Database;
using STRINGS;
using System;
using FMODUnity;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_Agricultural
    {
        public static void PrepareModifications()
        {
            if (Settings.Instance.Agricultural.IncludeRoom)
            {
                ROOMS.TYPES.CREATUREPEN.NAME = STRINGS.ROOMS.TYPES.AGRICULTURAL.NAME;
                ROOMS.TYPES.CREATUREPEN.EFFECT = STRINGS.ROOMS.TYPES.AGRICULTURAL.EFFECT;
                ROOMS.TYPES.CREATUREPEN.TOOLTIP = STRINGS.ROOMS.TYPES.AGRICULTURAL.TOOLTIP;
                RoomConstraints.RANCH_STATION = RoomTypeAgriculturalData.MODIFIED_CONSTRAINT;
                if (Settings.Instance.Gym.IncludeRoom)
                {
                    if (RoomConstraints.RANCH_STATION.stomp_in_conflict == null)
                        RoomConstraints.RANCH_STATION.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                    RoomConstraints.RANCH_STATION.stomp_in_conflict.Add(RoomTypes_AllModded.GymRoom.primary_constraint);
                }
            }
        }

        public static void ModifyRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.Agricultural.IncludeRoom)
            {
                for (int i = 0; i < __instance.CreaturePen.additional_constraints.Length; i++)
                    if (__instance.CreaturePen.additional_constraints[i].name.Contains("Maximum size:"))
                        __instance.CreaturePen.additional_constraints[i] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Agricultural.MaxSize);

            }
        }

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

        [HarmonyPatch(typeof(ColonyAchievement))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { 
            typeof(string), 
            typeof(string), 
            typeof(string), 
            typeof(string), 
            typeof(bool), 
            typeof(List<ColonyAchievementRequirement>), 
            typeof(string), 
            typeof(string), 
            typeof(string), 
            typeof(string), 
            typeof(System.Action<KMonoBehaviour>), 
            typeof(EventReference), 
            typeof(string), 
            typeof(string[]),
            typeof(string),
            typeof(string) })]
        public static class ColonyAchievement_Constructor_Patch
        {
            public static void Prefix(string platformAchievementId, ref List<ColonyAchievementRequirement> requirementChecklist)
            {
                if (!Settings.Instance.Agricultural.IncludeRoom) return;
                if (platformAchievementId != "VARIETY_OF_ROOMS") return;

                ColonyAchievementRequirement delete = null;
                foreach(var req in requirementChecklist)
                {
                    RoomType roomType = Traverse.Create(req).Field("roomType").GetValue<RoomType>();
                    if (roomType.Id == "Farm")
                        delete = req;
                }
                if (delete != null)
                {
                    requirementChecklist.Remove(delete);
                    Debug.Log("RoomsExpanded: VARIETY_OF_ROOMS - removed Farm requirement");
                }
            }
        }      
    }
}
