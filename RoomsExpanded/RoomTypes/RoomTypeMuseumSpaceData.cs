using System;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeMuseumSpaceData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "SpaceMuseumRoom";
        public static readonly string EffectId = "SpaceMuseumEffectId";


        public RoomTypeMuseumSpaceData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.MUSEUMSPACE.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.MUSEUMSPACE.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.MUSEUMSPACE.EFFECT;
            Catergory = CreateCategory();
            ConstraintPrimary = RoomModdedConstraints.PEDESTAL;
            ConstrantsAdditional = new RoomConstraints.Constraint[5]
                                        {
                                        RoomModdedConstraints.ARTIFACTS,
                                        RoomConstraints.LIGHT,
                                        RoomConstraints.NO_INDUSTRIAL_MACHINERY,
                                        RoomConstraints.MINIMUM_SIZE_32,
                                        RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.MuseumSpace.MaxSize)
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