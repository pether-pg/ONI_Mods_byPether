using System;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypePrivateRoomData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "PrivateRoom";
        public static readonly string BasicEffectId = "PrivateEffectId";
        public static readonly string LuxuryEffectId = "PrivateLuxuryEffectId";

        public RoomTypePrivateRoomData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.PRIVATEROOM.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.PRIVATEROOM.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.PRIVATEROOM.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Sleep;
            ConstraintPrimary = new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraints.ConstraintTags.Bed)
                                                                                                || bc.HasTag(RoomConstraints.ConstraintTags.LuxuryBed)),
                                                                                                (Func<Room, bool>)null,
                                                                                                name: STRINGS.ROOMS.CRITERIA.ANYBED.NAME,
                                                                                                description: STRINGS.ROOMS.CRITERIA.ANYBED.DESCRIPTION);
            ConstrantsAdditional = new RoomConstraints.Constraint[6]
                                    {
                                    new RoomConstraints.Constraint(
                                        (Func<KPrefabID, bool>)null,
                                        (Func<Room, bool>) (room =>
                                                            {
                                                                int count = 0;
                                                                if(room != null)
                                                                    foreach(KPrefabID building in room.buildings)
                                                                        if(building != null)
                                                                        {
                                                                            Bed bed = building.GetComponent<Bed>();
                                                                            if(bed != null)
                                                                                count ++;
                                                                        }
                                                                return count == 1;
                                                            }),
                                        name: STRINGS.ROOMS.CRITERIA.ONLYONEBED.NAME,
                                        description: STRINGS.ROOMS.CRITERIA.ONLYONEBED.DESCRIPTION),

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
