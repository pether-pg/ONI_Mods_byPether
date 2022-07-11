using System;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeMuseumData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "MuseumRoom";
        public static readonly string EffectId = "MuseumEffectId";


        public RoomTypeMuseumData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.MUSEUM.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.MUSEUM.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.MUSEUM.EFFECT;
            Catergory = RoomTypeCategories_AllModded.GetCategory(RoomId);
            ConstraintPrimary = RoomModdedConstraints.PEDESTAL;
            ConstrantsAdditional = new RoomConstraints.Constraint[5]
                                        {
                                        RoomModdedConstraints.MASTERPIECES,
                                        RoomConstraints.LIGHT,
                                        RoomConstraints.NO_INDUSTRIAL_MACHINERY,
                                        RoomConstraints.MINIMUM_SIZE_32,
                                        RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Museum.MaxSize)
                                        };

            RoomDetails = new RoomDetails.Detail[2]
                                {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.BUILDING_COUNT.NAME, (object) room.buildings.Count)))
                                };

            Priority = 1;
            Upgrades = new RoomType[] { RoomTypes_AllModded.HistoryMuseum, RoomTypes_AllModded.MuseumSpace };
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.MuseumSortKey);
        }
    }
}
