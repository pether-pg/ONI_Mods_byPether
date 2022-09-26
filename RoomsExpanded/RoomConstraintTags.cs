using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    public class RoomConstraintTags
    {
        //public static Tag ResearchStationTag = RoomConstraints.ConstraintTags.ResearchStation; // already present in game
        public static Tag KitchenBuildingTag = nameof(KitchenBuildingTag).ToString().ToTag();
        public static Tag RefrigeratorTag = nameof(RefrigeratorTag).ToString().ToTag();
        public static Tag GravestoneTag = nameof(GravestoneTag).ToString().ToTag();
        public static Tag BathroomTag = nameof(BathroomTag).ToString().ToTag();
        public static Tag WaterCoolerTag = nameof(WaterCoolerTag).ToString().ToTag();
        public static Tag RunningWheelGeneratorTag = nameof(RunningWheelGeneratorTag).ToString().ToTag();
        public static Tag NurseryPlanterBoxTag = nameof(NurseryPlanterBoxTag).ToString().ToTag();
        public static Tag AquariumFeederTag = nameof(AquariumFeederTag).ToString().ToTag();
        public static Tag AquariumReleaseTag = nameof(AquariumReleaseTag).ToString().ToTag();
        public static Tag ItemPedestalTag = nameof(ItemPedestalTag).ToString().ToTag();
        public static Tag FossilBuilding = nameof(FossilBuilding).ToString().ToTag();

        public static RoomConstraints.Constraint GetMaxSizeConstraint(int maxSize)
        {
            if (maxSize == 64)
                return RoomConstraints.MAXIMUM_SIZE_64;
            if (maxSize == 96)
                return RoomConstraints.MAXIMUM_SIZE_96;
            if (maxSize == 120)
                return RoomConstraints.MAXIMUM_SIZE_120;

            return new RoomConstraints.Constraint((Func<KPrefabID, bool>)null, (Func<Room, bool>)(room => room.cavity.numCells <= maxSize), 
                name: string.Format((string)ROOMS.CRITERIA.MAXIMUM_SIZE.NAME, (object)maxSize.ToString()), 
                description: string.Format((string)ROOMS.CRITERIA.MAXIMUM_SIZE.DESCRIPTION, (object)maxSize.ToString()));
        }

        public static RoomConstraints.Constraint GetMinSizeConstraint(int minSize)
        {
            if (minSize == 12)
                return RoomConstraints.MINIMUM_SIZE_12;
            if (minSize == 32)
                return RoomConstraints.MINIMUM_SIZE_32;

            return new RoomConstraints.Constraint((Func<KPrefabID, bool>)null, (Func<Room, bool>)(room => room.cavity.numCells >= minSize),
                name: string.Format((string)ROOMS.CRITERIA.MINIMUM_SIZE.NAME, (object)minSize.ToString()),
                description: string.Format((string)ROOMS.CRITERIA.MINIMUM_SIZE.DESCRIPTION, (object)minSize.ToString()));
        }

        public static void AddStompInConflict(RoomConstraints.Constraint stomping, RoomConstraints.Constraint stomped)
        {
            if (stomping.stomp_in_conflict == null)
                stomping.stomp_in_conflict = new List<RoomConstraints.Constraint>();
            stomping.stomp_in_conflict.Add(stomped);
        }

        public static void AddStompInConflict(RoomType stomping, RoomType stomped)
        {
            if (stomping.primary_constraint.stomp_in_conflict == null)
                stomping.primary_constraint.stomp_in_conflict = new List<RoomConstraints.Constraint>();
            stomping.primary_constraint.stomp_in_conflict.Add(stomped.primary_constraint);
        }

        public static void ResizeRooms(ref Database.RoomTypes __instance)
        {
            for (int i = 0; i < __instance.Count; i++)
            {
                if (__instance[i] == null || __instance[i].additional_constraints == null)
                    continue;

                for (int add = 0; add < __instance[i].additional_constraints.Length; add++)
                {
                    if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_96
                           && __instance[i].Id == __instance.Hospital.Id
                           && Settings.Instance.HospitalUpdate.IncludeRoom)
                        __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.HospitalUpdate.MaxSize);
                    else if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_96
                        && __instance[i].Id == __instance.CreaturePen.Id
                        && Settings.Instance.Agricultural.IncludeRoom)
                        __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Agricultural.MaxSize);

                    if (__instance[i].additional_constraints[add] == RoomConstraints.MINIMUM_SIZE_12)
                        __instance[i].additional_constraints[add] = RoomConstraintTags.GetMinSizeConstraint(Settings.Instance.ResizeMinRoomSize12);
                    else if (__instance[i].additional_constraints[add] == RoomConstraints.MINIMUM_SIZE_32)
                        __instance[i].additional_constraints[add] = RoomConstraintTags.GetMinSizeConstraint(Settings.Instance.ResizeMinRoomSize32);
                    else if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_64)
                        __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.ResizeMaxRoomSize64);
                    else if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_96)
                        __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.ResizeMaxRoomSize96);
                    else if (__instance[i].additional_constraints[add] == RoomConstraints.MAXIMUM_SIZE_120)
                        __instance[i].additional_constraints[add] = RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.ResizeMaxRoomSize120);
                }
            }
        }
    }
}
