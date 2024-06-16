/*using System;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypePrivateRoomData_Obsolete : RoomTypeAbstractData
    {
        public static readonly string RoomId = "PrivateRoom";
        public static readonly string BasicEffectId = "PrivateEffectId";
        public static readonly string LuxuryEffectId = "PrivateLuxuryEffectId";

        public RoomTypePrivateRoomData_Obsolete()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.PRIVATEROOM.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.PRIVATEROOM.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.PRIVATEROOM.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Sleep;
            ConstraintPrimary = RoomModdedConstraints.ANY_BED;
            ConstrantsAdditional = new RoomConstraints.Constraint[6]
                                    {
                                    RoomModdedConstraints.ONLY_ONE_BED,
                                    RoomConstraints.DECORATIVE_ITEM_20,
                                    RoomConstraints.NO_INDUSTRIAL_MACHINERY,
                                    RoomConstraints.CEILING_HEIGHT_4,
                                    RoomConstraints.MINIMUM_SIZE_12,
                                    RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.PrivateBedroom.MaxSize)
                                    };

            RoomDetails = new RoomDetails.Detail[2]
                            {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.BUILDING_COUNT.NAME, (object) room.buildings.Count)))
                            };

            Priority = 2;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.BedroomSortKey + 1);
            
        }
    }
}
*/