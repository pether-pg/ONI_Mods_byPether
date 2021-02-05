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
            }
        }

        [HarmonyPatch(typeof(RoomTypes), MethodType.Constructor, new Type[] { typeof(ResourceSet) })]
        public class RoomTypes_Constructor_Patch
        {
            public static void Prefix()
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


            public static void Postfix(ref RoomTypes __instance)
            {
                SortingCounter.Init();

                if (Settings.Instance.Laboratory.IncludeRoom)
                    __instance.Add(RoomTypes_AllModded.LaboratoryRoom);

                if (Settings.Instance.Bathroom.IncludeRoom)
                {
                    __instance.Add(RoomTypes_AllModded.BathroomRoom);

                    if(__instance.PlumbedBathroom.primary_constraint.stomp_in_conflict == null)
                        __instance.PlumbedBathroom.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                    __instance.PlumbedBathroom.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.BathroomRoom.primary_constraint);

                    if (__instance.Hospital.primary_constraint.stomp_in_conflict == null)
                        __instance.Hospital.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                    __instance.Hospital.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.BathroomRoom.primary_constraint);
                }

                if (Settings.Instance.Kitchen.IncludeRoom)
                    __instance.Add(RoomTypes_AllModded.KitchenRoom);

                if (Settings.Instance.Nursery.IncludeRoom)
                {
                    __instance.Add(RoomTypes_AllModded.Nursery);

                    if (__instance.Farm.primary_constraint.stomp_in_conflict == null)
                        __instance.Farm.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                    __instance.Farm.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.Nursery.primary_constraint);

                    if(Settings.Instance.Agricultural.IncludeRoom)
                    {
                        if (__instance.CreaturePen.primary_constraint.stomp_in_conflict == null)
                            __instance.CreaturePen.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                        __instance.CreaturePen.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.Nursery.primary_constraint);
                    }
                }

                if (Settings.Instance.Gym.IncludeRoom)
                {
                    if(Settings.Instance.Laboratory.IncludeRoom)
                    {
                        if (RoomTypes_AllModded.LaboratoryRoom.primary_constraint.stomp_in_conflict == null)
                            RoomTypes_AllModded.LaboratoryRoom.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                        RoomTypes_AllModded.LaboratoryRoom.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.GymRoom.primary_constraint);
                    }

                    if(Settings.Instance.Kitchen.IncludeRoom)
                    {
                        if (RoomTypes_AllModded.KitchenRoom.primary_constraint.stomp_in_conflict == null)
                            RoomTypes_AllModded.KitchenRoom.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                        RoomTypes_AllModded.KitchenRoom.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.GymRoom.primary_constraint);
                    }

                    if (__instance.PowerPlant.primary_constraint.stomp_in_conflict == null)
                        __instance.PowerPlant.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
                    __instance.PowerPlant.primary_constraint.stomp_in_conflict.Add(RoomTypes_AllModded.GymRoom.primary_constraint);

                    __instance.Add(RoomTypes_AllModded.GymRoom);
                }

                if (Settings.Instance.Agricultural.IncludeRoom)
                {
                    for (int i = 0; i < __instance.CreaturePen.additional_constraints.Length; i++)
                        if (__instance.CreaturePen.additional_constraints[i].name.Contains("Maximum size:"))
                            __instance.CreaturePen.additional_constraints[i] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Agricultural.MaxSize);

                }

                if (Settings.Instance.Aquarium.IncludeRoom)
                    __instance.Add(RoomTypes_AllModded.Aquarium);

                if (Settings.Instance.Graveyard.IncludeRoom)
                    __instance.Add(RoomTypes_AllModded.GraveyardRoom);

                if (Settings.Instance.HospitalUpdate.IncludeRoom)
                {
                    for(int i=0; i<__instance.Hospital.additional_constraints.Length; i++)
                    {
                        if (__instance.Hospital.additional_constraints[i] == RoomConstraints.TOILET)
                            __instance.Hospital.additional_constraints[i] = RoomConstraints.ADVANCED_WASH_STATION;
                        if (__instance.Hospital.additional_constraints[i] == RoomConstraints.MESS_STATION_SINGLE)
                            __instance.Hospital.additional_constraints[i] = RoomConstraints.DECORATIVE_ITEM;
                        if (__instance.Hospital.additional_constraints[i] == RoomConstraints.MAXIMUM_SIZE_96)
                            __instance.Hospital.additional_constraints[i] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.HospitalUpdate.MaxSize);

                    }
                }

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
