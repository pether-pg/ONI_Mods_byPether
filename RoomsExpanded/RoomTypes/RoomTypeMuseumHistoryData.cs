using System;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeMuseumHistoryData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "HistoryMuseumRoom";
        public static readonly string EffectId = "HistoryMuseumEffectId";


        public RoomTypeMuseumHistoryData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.MUSEUMHISTORY.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.MUSEUMHISTORY.TOOLTIP;
            Effect = string.Format(STRINGS.ROOMS.TYPES.MUSEUMHISTORY.EFFECT, MiscUtils.Percent(Settings.Instance.MuseumHistory.Bonus));
            Catergory = Db.Get().RoomTypeCategories.Recreation;
            ConstraintPrimary = RoomModdedConstraints.PEDESTAL;
            ConstrantsAdditional = new RoomConstraints.Constraint[5]
                                        {
                                        RoomModdedConstraints.FOSSILS,
                                        RoomConstraints.LIGHT,
                                        RoomConstraints.NO_INDUSTRIAL_MACHINERY,
                                        RoomConstraints.MINIMUM_SIZE_32,
                                        RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.MuseumHistory.MaxSize)
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
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.MuseumSortKey + 1);
        }
    }
}
