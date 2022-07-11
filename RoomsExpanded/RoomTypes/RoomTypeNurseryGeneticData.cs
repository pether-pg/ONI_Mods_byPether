using System;
using System.Collections.Generic;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeNurseryGeneticData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "GeneticNurseryRoom";

        public RoomTypeNurseryGeneticData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.NURSERYGENETIC.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.NURSERYGENETIC.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.NURSERYGENETIC.EFFECT;
            Catergory = CreateCategory();
            ConstraintPrimary = RoomModdedConstraints.GENETIC_STATION;
            ConstrantsAdditional = new RoomConstraints.Constraint[3] 
                                        {
                                            RoomModdedConstraints.RADIATION_EMMITER,
                                            RoomConstraints.MINIMUM_SIZE_12,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.NurseryGenetic.MaxSize)
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
