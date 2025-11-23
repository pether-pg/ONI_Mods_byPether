using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeBionicUpkeepData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "BionicUpkeepRoom";
        public static readonly string EffectId = "BionicUpkeepEffect";

        public RoomTypeBionicUpkeepData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.BIONIC_UPKEEP.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.BIONIC_UPKEEP.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.BIONIC_UPKEEP.EFFECT;
            Catergory = CreateCategory();
            ConstraintPrimary = RoomConstraints.BIONIC_GUNKEMPTIER;
            ConstrantsAdditional = new RoomConstraints.Constraint[2]
                                            {
                                            RoomConstraints.BIONIC_LUBRICATION,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.BionicWorkshop.MaxSize)
                                            };

            RoomDetails = new RoomDetails.Detail[2]
                            {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.NumCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.BUILDING_COUNT.NAME, (object) room.buildings.Count)))
                            };

            Priority = 0;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = new string[] { EffectId };
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.PlumbedBathroomSortKey);
        }
    }
}
