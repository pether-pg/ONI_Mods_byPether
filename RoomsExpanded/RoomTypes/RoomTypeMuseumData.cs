using System;
using STRINGS;

namespace RoomsExpanded
{
    class RoomTypeMuseumData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "MuseumRoom";
        public static readonly string EffectId = "MuseumEffectId";

        private static readonly int requiredMasterpieces = 6;

        public RoomTypeMuseumData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.MUSEUM.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.MUSEUM.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.MUSEUM.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Hospital;
            ConstraintPrimary = new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraintTags.ItemPedestalTag)),
                                                                (Func<Room, bool>)null,
                                                                name: STRINGS.ROOMS.CRITERIA.PEDESTAL.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.PEDESTAL.DESCRIPTION);
            ConstrantsAdditional = new RoomConstraints.Constraint[5]
                                        {
                                        new RoomConstraints.Constraint((Func<KPrefabID, bool>)null,
                                                                        (Func<Room, bool>) (room => 
                                                                        {
                                                                            int count = 0;
                                                                            foreach(KPrefabID building in room.buildings)
                                                                            {
                                                                                Artable art = building.GetComponent<Artable>();
                                                                                if(art == null)
                                                                                    continue;
                                                                                if(art.CurrentStatus == Artable.Status.Great)
                                                                                    count ++;
                                                                            }
                                                                            return count >= requiredMasterpieces; 
                                                                        }),
                                                                        name: string.Format(STRINGS.ROOMS.CRITERIA.MASTERPIECES.NAME, requiredMasterpieces),
                                                                        description: string.Format(STRINGS.ROOMS.CRITERIA.MASTERPIECES.DESCRIPTION, requiredMasterpieces)),
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
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.CreaturePenSortKey + 1);
        }
    }
}
