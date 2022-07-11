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
            Catergory = CreateCategory();
            ConstraintPrimary = RoomModdedConstraints.FISH_FEEDER;
            ConstrantsAdditional = new RoomConstraints.Constraint[4]
                                        {
                                            RoomModdedConstraints.FISH_RELEASE,
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
