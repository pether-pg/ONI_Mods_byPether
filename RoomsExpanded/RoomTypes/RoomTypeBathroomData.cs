using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeBathroomData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "BathroomRoom";
        public static readonly string BathroomTagName = "Shower";

        public RoomTypeBathroomData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.SHOWERROOM.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.SHOWERROOM.TOOLTIP;
            Effect = string.Format(STRINGS.ROOMS.TYPES.SHOWERROOM.EFFECT, MiscUtils.Percent(Settings.Instance.Bathroom.Bonus));
            Catergory = Db.Get().RoomTypeCategories.Bathroom;
            ConstraintPrimary = RoomModdedConstraints.BATHROOM;
            ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                            {
                                            RoomConstraints.DECORATIVE_ITEM,
                                            RoomConstraints.NO_INDUSTRIAL_MACHINERY,
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
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.PlumbedBathroomSortKey);
        }
    }
}
