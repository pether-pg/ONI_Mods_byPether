using System;
using STRINGS;

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

        public static RoomConstraints.Constraint GetMaxSizeConstraint(int maxSize)
        {
            return new RoomConstraints.Constraint((Func<KPrefabID, bool>)null, (Func<Room, bool>)(room => room.cavity.numCells <= maxSize), 
                name: string.Format((string)ROOMS.CRITERIA.MAXIMUM_SIZE.NAME, (object)maxSize.ToString()), 
                description: string.Format((string)ROOMS.CRITERIA.MAXIMUM_SIZE.DESCRIPTION, (object)maxSize.ToString()));
        }
    }
}
