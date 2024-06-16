using System;
using STRINGS;

namespace RoomsExpanded
{
    public class RoomTypeKitchenetteData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "KitchenetteRoom";

        public RoomTypeKitchenetteData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.KITCHENETTE.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.KITCHENETTE.TOOLTIP;
            Effect = string.Format(STRINGS.ROOMS.TYPES.KITCHENETTE.EFFECT, MiscUtils.Percent(Settings.Instance.Kitchenette.Bonus));
            Catergory = Db.Get().RoomTypeCategories.Food;
            ConstraintPrimary = RoomModdedConstraints.COOKING_STATION;
            ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                        {
                                        RoomConstraints.REFRIGERATOR,
                                        RoomConstraints.DECORATIVE_ITEM,
                                        RoomConstraints.MINIMUM_SIZE_12,
                                        RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Kitchenette.MaxSize)
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
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.KitchenSortKey);
        }
    }
}
