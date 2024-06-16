using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeMissionControlRoomData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "MissionControlRoom";

        public RoomTypeMissionControlRoomData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.MISSIONCONTROL.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.MISSIONCONTROL.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.MISSIONCONTROL.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Science;
            ConstraintPrimary = RoomModdedConstraints.SPACE_BUILDING;
            ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                            {
                                            RoomModdedConstraints.TRANSPARENT_CEILING,
                                            RoomConstraints.IS_BACKWALLED,
                                            RoomConstraints.MINIMUM_SIZE_12,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Bathroom.MaxSize)
                                            };

            RoomDetails = new RoomDetails.Detail[2]
                            {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.BUILDING_COUNT.NAME, (object) room.buildings.Count)))
                            };

            Priority = 0;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.LaboratorySortKey);
        }
    }
}
