using System;
using System.Collections.Generic;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeNurseryData : RoomTypeAbstractData
    {

        public static readonly string RoomId = "NurseryRoom";
        public static readonly string PlanterBoxTagName = "PlanterBox";

        public RoomTypeNurseryData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.NURSERY.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.NURSERY.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.NURSERY.EFFECT;
            Catergory = CreateCategory();
            ConstraintPrimary = RoomModdedConstraints.PLANTER_BOX;
            ConstrantsAdditional = new RoomConstraints.Constraint[4] { 
                                            RoomModdedConstraints.UNIQUE_PLANTS,
                                            RoomConstraints.LIGHT,
                                            RoomConstraints.MINIMUM_SIZE_12,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Nursery.MaxSize)
                                        };

            RoomDetails = new RoomDetails.Detail[2]
                            {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.PLANT_COUNT.NAME, (object) room.plants.Count)))
                            };

            Priority = 0;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.FarmSortKey - 1);
        }
    }
}
