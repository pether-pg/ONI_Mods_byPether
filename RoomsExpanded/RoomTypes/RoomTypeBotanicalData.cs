using System;
using STRINGS;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class RoomTypeBotanicalData : RoomTypeAbstractData
    {
        public static readonly string RoomId = "BotanicalRoom";

        public static List<string> DecorativeNames = new List<string>() { 
            "PrickleGrass",     // from vanilla - Bristle Briar
            "LeafyPlant",       // from vanilla - Mirth Leaf
            "CactusPlant",      // from vanilla - Jumping Joya
            "BulbPlant",        // from vanilla - Buddy Bud
            "EvilFlower",       // from vanilla - Sporechid
            "Cylindrica",       // from DLC - Bliss Burst
            "WineCups" ,        // from DLC - Mellow Mallow
            "ToePlant",         // from DLC - Tranquil Toes
            "FrostBlossom",     // from Omni Flora
            "IcyShroom",        // from Omni Flora
            "MyrthRose",        // from Omni Flora
            "PricklyLotus",     // from Omni Flora
            "RustFern",         // from Omni Flora
            "ShlurpCoral",      // from Omni Flora
            "SporeLamp",        // from Omni Flora
            "Tropicalgae",      // from Omni Flora
            "Fervine"           // from Fervine 
        };

        private static int requiredDecorative = 4;
        private static int requiredPlants = 8;

        public RoomTypeBotanicalData()
        {
            Id = RoomId;
            Name = STRINGS.ROOMS.TYPES.BOTANICAL.NAME;
            Tooltip = STRINGS.ROOMS.TYPES.BOTANICAL.TOOLTIP;
            Effect = STRINGS.ROOMS.TYPES.BOTANICAL.EFFECT;
            Catergory = Db.Get().RoomTypeCategories.Park;
            ConstraintPrimary = RoomConstraints.PARK_BUILDING;

            ConstrantsAdditional = new RoomConstraints.Constraint[5]
                                {
                                new RoomConstraints.Constraint((Func<KPrefabID, bool>) null,
                                                                (Func<Room, bool>)(room =>
                                                                {
                                                                    List<string> names = new List<string>();
                                                                    foreach (var plant in room.cavity.plants)
                                                                    {
                                                                        if(plant == null) continue;
                                                                        if(!DecorativeNames.Contains(plant.name)) continue;
                                                                        if(!names.Contains(plant.name))
                                                                            names.Add(plant.name);
                                                                    }
                                                                    return names.Count >= requiredDecorative;
                                                                }),
                                                                name: string.Format(STRINGS.ROOMS.CRITERIA.DECORPLANTS.NAME, requiredDecorative) ,
                                                                description: string.Format(STRINGS.ROOMS.CRITERIA.DECORPLANTS.DESCRIPTION, requiredDecorative)),
                                new RoomConstraints.Constraint((Func<KPrefabID, bool>) null,
                                                                (Func<Room, bool>)(room => room.cavity.plants.Count >= requiredPlants),
                                                                name: string.Format(STRINGS.ROOMS.CRITERIA.PLANTCOUNT.NAME, requiredPlants) ,
                                                                description: string.Format(STRINGS.ROOMS.CRITERIA.PLANTCOUNT.DESCRIPTION, requiredPlants)),
                                new RoomConstraints.Constraint((Func<KPrefabID, bool>) null,
                                                                (Func<Room, bool>)(room =>
                                                                {
                                                                    foreach (KPrefabID plant in room.cavity.plants)
                                                                    {
                                                                        if ((UnityEngine.Object) plant != (UnityEngine.Object) null)
                                                                        {
                                                                            BasicForagePlantPlanted component1 = plant.GetComponent<BasicForagePlantPlanted>();
                                                                            ReceptacleMonitor component2 = plant.GetComponent<ReceptacleMonitor>();
                                                                            if ((UnityEngine.Object) component2 != (UnityEngine.Object) null && !component2.Replanted)
                                                                                return false;
                                                                            else if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
                                                                                return false;
                                                                        }
                                                                    }
                                                                    return true;
                                                                }),
                                                                name: STRINGS.ROOMS.CRITERIA.NOWILDPLANTS.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.NOWILDPLANTS.DESCRIPTION),
                                RoomConstraints.MINIMUM_SIZE_32,
                                RoomConstraintTags.GetMaxSizeConstraint(Settings.Instance.Botanical.MaxSize)
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
            Effects = new string[1] { "RoomNatureReserve" };
            SortKey = SortingCounter.GetAndIncrement(SortingCounter.ParkSortKey + 1);
        }
    }
}
