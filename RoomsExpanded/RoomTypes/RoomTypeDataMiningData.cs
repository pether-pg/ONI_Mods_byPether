using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeDataMiningData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "DataMiningRoom";

        public RoomTypeDataMiningData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.DATA_MINING.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.DATA_MINING.TOOLTIP;
            Effect = string.Format(STRINGS.ROOMS.TYPES.DATA_MINING.EFFECT, MiscUtils.Percent(Settings.Instance.DataMiningCenter.Bonus));
            Catergory = CreateCategory();
            ConstraintPrimary = RoomModdedConstraints.DATA_MINER;
            ConstrantsAdditional = new RoomConstraints.Constraint[2]
                                            {
                                            RoomModdedConstraints.ROBO_MINER,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.DataMiningCenter.MaxSize)
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
            SortKey = SortingCounter.GetAndIncrement();
        }
    }
}
