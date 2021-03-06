using System;
using STRINGS;

namespace RoomsExpanded
{
    public class RoomTypeKitchenData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "KitchenRoom";

        public RoomTypeKitchenData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.KITCHEN.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.KITCHEN.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.KITCHEN.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Agricultural;
            ConstraintPrimary = new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraintTags.KitchenBuildingTag)), 
                                                                                            (Func<Room, bool>)null, 
                                                                                            name: STRINGS.ROOMS.CRITERIA.COOKING.NAME, 
                                                                                            description: STRINGS.ROOMS.CRITERIA.COOKING.DESCRIPTION);
            ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                        {
                                        new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.GetComponent<Refrigerator>() != null),
                                                                                            (Func<Room, bool>)null,
                                                                                            name: STRINGS.ROOMS.CRITERIA.FRIDGE.NAME,
                                                                                            description: STRINGS.ROOMS.CRITERIA.FRIDGE.DESCRIPTION),
                                        RoomConstraints.DECORATIVE_ITEM,
                                        RoomConstraints.MINIMUM_SIZE_12,
                                        RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Kitchen.MaxSize)
                                        };

            RoomDetails = new RoomDetails.Detail[2]
                                {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.BUILDING_COUNT.NAME, (object) room.buildings.Count)))
                                };

            Priority = 1;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.CreaturePenSortKey + 1);
        }
    }
}
