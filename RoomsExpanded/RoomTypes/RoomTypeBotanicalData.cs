using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeBotanicalData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "BotanicalRoom";

        public RoomTypeBotanicalData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.BOTANICAL.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.BOTANICAL.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.BOTANICAL.EFFECT;
            Catergory = CreateCategory();
            ConstraintPrimary = RoomConstraints.PARK_BUILDING;

            ConstrantsAdditional = new RoomConstraints.Constraint[5]
                                {
                                RoomModdedConstraints.DECORATIVE_PLANTS,
                                RoomModdedConstraints.BOTANICAL_PLANTS,
                                RoomModdedConstraints.NO_WILD,
                                RoomConstraints.MINIMUM_SIZE_32,
                                RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Botanical.MaxSize)
                                };

            RoomDetails = new RoomDetails.Detail[2]
                                {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.NumCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.BUILDING_COUNT.NAME, (object) room.buildings.Count)))
                                };

            Priority = 1;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = new string[1] { "RoomNatureReserve" };
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.ParkSortKey + 1);
        }
    }
}
