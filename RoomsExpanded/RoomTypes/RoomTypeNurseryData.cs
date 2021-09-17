using System;
using System.Collections.Generic;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeNurseryData : RoomTypeAbstractData
    {
        private readonly static int requiredNumberOfPlants = 4;

        public static readonly string RoomId = "NurseryRoom";
        public static readonly string PlanterBoxTagName = "PlanterBox";

        public RoomTypeNurseryData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.NURSERY.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.NURSERY.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.NURSERY.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Agricultural;
            ConstraintPrimary = new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraintTags.NurseryPlanterBoxTag)),
                                                            (Func<Room, bool>)null,
                                                            name: STRINGS.ROOMS.CRITERIA.PLANTERBOX.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.PLANTERBOX.DESCRIPTION);

            ConstrantsAdditional = new RoomConstraints.Constraint[4] { 
                                            new RoomConstraints.Constraint((Func<KPrefabID, bool>)null,
                                                            (Func<Room, bool>)(room =>
                                                            {
                                                                List<string> names = new List<string>();
                                                                foreach (var plant in room.cavity.plants)
                                                                {
                                                                    if(plant == null) continue;
                                                                    if(RoomTypeBotanicalData.DecorativeNames.Contains(plant.name)) continue;
                                                                    if(!names.Contains(plant.name))
                                                                        names.Add(plant.name);
                                                                }
                                                                return names.Count >= requiredNumberOfPlants;
                                                            }),
                                                            name: string.Format(STRINGS.ROOMS.CRITERIA.SEEDPLANTS.NAME, requiredNumberOfPlants),
                                                            description: string.Format(STRINGS.ROOMS.CRITERIA.SEEDPLANTS.DESCRIPTION, requiredNumberOfPlants)),
                                            RoomConstraints.LIGHT,
                                            RoomConstraints.MINIMUM_SIZE_12,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Nursery.MaxSize)
                                        };

            RoomDetails = new RoomDetails.Detail[2]
                            {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.PLANT_COUNT.NAME, (object) room.plants.Count)))
                            };

            Priority = 0;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.FarmSortKey - 1);
        }
    }
}
