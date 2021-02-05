using System;
using STRINGS;

namespace RoomsExpanded
{
    public class RoomTypeGraveyardData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "GraveyardRoom";
        public static readonly string GravestoneName = "TastefulMemorial";

        public RoomTypeGraveyardData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.GRAVEYARD.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.GRAVEYARD.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.GRAVEYARD.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Park;
            ConstraintPrimary = new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraintTags.GravestoneTag)), 
                                                            (Func<Room, bool>)null, 
                                                            name: STRINGS.ROOMS.CRITERIA.GRAVE.NAME, 
                                                            description: STRINGS.ROOMS.CRITERIA.GRAVE.DESCRIPTION);
            ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                        {
                                        RoomConstraints.WILDPLANT,
                                        RoomConstraints.NO_INDUSTRIAL_MACHINERY,
                                        RoomConstraints.MINIMUM_SIZE_12,
                                        RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Graveyard.MaxSize)
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
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.ParkSortKey);
        }
    }
}
