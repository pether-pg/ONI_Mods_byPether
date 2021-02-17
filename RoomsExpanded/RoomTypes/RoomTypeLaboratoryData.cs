using System;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeLaboratoryData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "LaboratoryRoom";

        public RoomTypeLaboratoryData()
        {
            
                Id = RoomId;
                Name = STRINGS.ROOMS.TYPES.LABORATORY.NAME;
                Tooltip = STRINGS.ROOMS.TYPES.LABORATORY.TOOLTIP;
                Effect = STRINGS.ROOMS.TYPES.LABORATORY.EFFECT;
                Catergory = Db.Get().RoomTypeCategories.Recreation;
                ConstraintPrimary = RoomConstraints.RESEARCH_STATION;
                ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                            {
                                            RoomConstraints.LIGHT,
                                            RoomConstraints.DECORATIVE_ITEM,
                                            RoomConstraints.MINIMUM_SIZE_12,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Laboratory.MaxSize)
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
                SortKey = SortingCounter.GetAndIncrement(0);  
        }
    }
}
