using System;
using STRINGS;
using UnityEngine;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeAquariumData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "AquariumRoom";

        public RoomTypeAquariumData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.AQUARIUM.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.AQUARIUM.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.AQUARIUM.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Bathroom;
            ConstraintPrimary = new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraintTags.AquariumFeederTag)),
                                                                (Func<Room, bool>)null,
                                                                name: STRINGS.ROOMS.CRITERIA.FISHFEEDER.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.FISHFEEDER.DESCRIPTION);
            ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                        {
                                            new RoomConstraints.Constraint((Func<KPrefabID, bool>)(bc => bc.HasTag(RoomConstraintTags.AquariumReleaseTag)),
                                                                            (Func<Room, bool>)null,
                                                                            name: STRINGS.ROOMS.CRITERIA.FISHRELEASE.NAME,
                                                                            description: STRINGS.ROOMS.CRITERIA.FISHRELEASE.DESCRIPTION),
                                            RoomConstraints.DECORATIVE_ITEM,
                                            RoomConstraints.MINIMUM_SIZE_12,
                                            RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Aquarium.MaxSize)
                                        };

            RoomDetails = new RoomDetails.Detail[2]
                            {
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.SIZE.NAME, (object) room.cavity.numCells))),
                                new RoomDetails.Detail((Func<Room, string>) (room => string.Format((string) ROOMS.DETAILS.CREATURE_COUNT.NAME, (object) (room.cavity.creatures.Count + room.cavity.eggs.Count))))
        };

            Priority = 0;
            Upgrades = null;
            SingleAssignee = false;
            PriorityUse = false;
            Effects = null;
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.RecreationRoom);

        }
    }
}
