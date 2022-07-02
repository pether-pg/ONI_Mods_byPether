using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsExpanded
{
    class RoomModdedConstraints
    {
        private static readonly int requiredMasterpieces = 6;
        private static readonly int requiredFossils = 4;
        private readonly static int requiredUniquePlants = 4;
        private static int requiredDecorativePlants = 4;
        private static int requiredBotanicalPlants = 8;
        private static Tag MorePlantsTag = new Tag("DecorMorePlants");

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

        public static RoomConstraints.Constraint COOKING_STATION = new RoomConstraints.Constraint(
                                                                    bc => bc.HasTag(RoomConstraintTags.KitchenBuildingTag), 
                                                                    null, 
                                                                    name: STRINGS.ROOMS.CRITERIA.COOKING.NAME, 
                                                                    description: STRINGS.ROOMS.CRITERIA.COOKING.DESCRIPTION);

        public static RoomConstraints.Constraint FRIDGE = new RoomConstraints.Constraint(
                                                                bc => bc.GetComponent<Refrigerator>() != null,
                                                                null,
                                                                name: STRINGS.ROOMS.CRITERIA.FRIDGE.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.FRIDGE.DESCRIPTION);

        public static RoomConstraints.Constraint BATHROOM = new RoomConstraints.Constraint(
                                                                bc => bc.HasTag(RoomConstraintTags.BathroomTag) 
                                                                    && !bc.HasTag(RoomConstraints.ConstraintTags.FlushToilet),
                                                                null,
                                                                name: STRINGS.ROOMS.CRITERIA.SHOWER.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.SHOWER.DESCRIPTION);

        public static RoomConstraints.Constraint GRAVESTONE = new RoomConstraints.Constraint(
                                                            bc => bc.HasTag(RoomConstraintTags.GravestoneTag),
                                                            null,
                                                            name: STRINGS.ROOMS.CRITERIA.GRAVE.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.GRAVE.DESCRIPTION);

        public static RoomConstraints.Constraint INDUSTRIAL = new RoomConstraints.Constraint(
                                                            bc => bc.HasTag(RoomConstraints.ConstraintTags.IndustrialMachinery),
                                                            null,
                                                            name: STRINGS.ROOMS.CRITERIA.INDUSTRIAL.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.INDUSTRIAL.DESCRIPTION);

        public static RoomConstraints.Constraint PLANTER_BOX = new RoomConstraints.Constraint(
                                                            bc => bc.HasTag(RoomConstraintTags.NurseryPlanterBoxTag),
                                                            null,
                                                            name: STRINGS.ROOMS.CRITERIA.PLANTERBOX.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.PLANTERBOX.DESCRIPTION);

        public static RoomConstraints.Constraint UNIQUE_PLANTS = new RoomConstraints.Constraint(
                                                            null,
                                                            room =>
                                                            {
                                                                List<string> names = new List<string>();
                                                                foreach (var plant in room.cavity.plants)
                                                                {
                                                                    if (plant == null) continue;
                                                                    if (IsDecorativePlant(plant)) continue;
                                                                    if (!names.Contains(plant.name))
                                                                        names.Add(plant.name);
                                                                }
                                                                return names.Count >= requiredUniquePlants;
                                                            },
                                                            name: string.Format(STRINGS.ROOMS.CRITERIA.SEEDPLANTS.NAME, requiredUniquePlants),
                                                            description: string.Format(STRINGS.ROOMS.CRITERIA.SEEDPLANTS.DESCRIPTION, requiredUniquePlants));

        public static RoomConstraints.Constraint GENETIC_STATION = new RoomConstraints.Constraint(
                                                            bc => bc.GetComponent<GeneticAnalysisStationWorkable>() != null,
                                                            null,
                                                            name: STRINGS.ROOMS.CRITERIA.GENETICSTATION.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.GENETICSTATION.DESCRIPTION);

        public static RoomConstraints.Constraint RADIATION_EMMITER = new RoomConstraints.Constraint(
                                                            bc => bc.GetComponent<RadiationEmitter>() != null,
                                                            null,
                                                            name: STRINGS.ROOMS.CRITERIA.RADIATIONSOURCE.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.RADIATIONSOURCE.DESCRIPTION);

        public static RoomConstraints.Constraint RUNNING_WHEEL = new RoomConstraints.Constraint(
                                                            bc => bc.HasTag(RoomConstraintTags.RunningWheelGeneratorTag), 
                                                            null,
                                                            name: STRINGS.ROOMS.CRITERIA.MANUALGENERATOR.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.MANUALGENERATOR.DESCRIPTION,
                                                            stomp_in_conflict: new List<RoomConstraints.Constraint>() { RoomConstraints.REC_BUILDING });

        public static RoomConstraints.Constraint WATER_COOLER = new RoomConstraints.Constraint(
                                                            bc => bc.HasTag(RoomConstraintTags.WaterCoolerTag),
                                                            null,
                                                            name: STRINGS.ROOMS.CRITERIA.WATERCOOLER.NAME,
                                                            description: STRINGS.ROOMS.CRITERIA.WATERCOOLER.DESCRIPTION);

        public static RoomConstraints.Constraint FISH_FEEDER = new RoomConstraints.Constraint(
                                                                bc => bc.HasTag(RoomConstraintTags.AquariumFeederTag),
                                                                null,
                                                                name: STRINGS.ROOMS.CRITERIA.FISHFEEDER.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.FISHFEEDER.DESCRIPTION);

        public static RoomConstraints.Constraint FISH_RELEASE = new RoomConstraints.Constraint(
                                                                bc => bc.HasTag(RoomConstraintTags.AquariumReleaseTag),
                                                                null,
                                                                name: STRINGS.ROOMS.CRITERIA.FISHRELEASE.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.FISHRELEASE.DESCRIPTION);

        public static RoomConstraints.Constraint ANY_BED = new RoomConstraints.Constraint(
                                                                bc => bc.HasTag(RoomConstraints.ConstraintTags.Bed)
                                                                    || bc.HasTag(RoomConstraints.ConstraintTags.LuxuryBed),
                                                                null,
                                                                name: STRINGS.ROOMS.CRITERIA.ANYBED.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.ANYBED.DESCRIPTION);

        public static RoomConstraints.Constraint ONLY_ONE_BED = new RoomConstraints.Constraint(
                                                                null,
                                                                room =>
                                                                {
                                                                    int count = 0;
                                                                    if (room != null)
                                                                        foreach (KPrefabID building in room.buildings)
                                                                            if (building != null)
                                                                            {
                                                                                Bed bed = building.GetComponent<Bed>();
                                                                                if (bed != null)
                                                                                    count++;
                                                                            }
                                                                    return count == 1;
                                                                },
                                                                name: STRINGS.ROOMS.CRITERIA.ONLYONEBED.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.ONLYONEBED.DESCRIPTION);

        public static RoomConstraints.Constraint DECORATIVE_PLANTS = new RoomConstraints.Constraint(
                                                                null,
                                                                room =>
                                                                {
                                                                    List<string> names = new List<string>();
                                                                    foreach (var plant in room.cavity.plants)
                                                                    {
                                                                        if (plant == null) continue;
                                                                        if (!IsDecorativePlant(plant)) continue;
                                                                        if (!names.Contains(plant.name))
                                                                            names.Add(plant.name);
                                                                    }
                                                                    return names.Count >= requiredDecorativePlants;
                                                                },
                                                                name: string.Format(STRINGS.ROOMS.CRITERIA.DECORPLANTS.NAME, requiredDecorativePlants),
                                                                description: string.Format(STRINGS.ROOMS.CRITERIA.DECORPLANTS.DESCRIPTION, requiredDecorativePlants));

        public static RoomConstraints.Constraint BOTANICAL_PLANTS = new RoomConstraints.Constraint(
                                                                null,
                                                                room => room.cavity.plants.Count >= requiredBotanicalPlants,
                                                                name: string.Format(STRINGS.ROOMS.CRITERIA.PLANTCOUNT.NAME, requiredBotanicalPlants),
                                                                description: string.Format(STRINGS.ROOMS.CRITERIA.PLANTCOUNT.DESCRIPTION, requiredBotanicalPlants));

        public static RoomConstraints.Constraint NO_WILTING = new RoomConstraints.Constraint(
                                                                null,
                                                                room =>
                                                                {
                                                                    foreach (KPrefabID plant in room.cavity.plants)
                                                                    {
                                                                        if ((UnityEngine.Object)plant != (UnityEngine.Object)null)
                                                                        {
                                                                            BasicForagePlantPlanted component1 = plant.GetComponent<BasicForagePlantPlanted>();
                                                                            ReceptacleMonitor component2 = plant.GetComponent<ReceptacleMonitor>();
                                                                            if ((UnityEngine.Object)component2 != (UnityEngine.Object)null && !component2.Replanted)
                                                                                return false;
                                                                            else if ((UnityEngine.Object)component1 != (UnityEngine.Object)null)
                                                                                return false;
                                                                        }
                                                                    }
                                                                    return true;
                                                                },
                                                                name: STRINGS.ROOMS.CRITERIA.NOWILDPLANTS.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.NOWILDPLANTS.DESCRIPTION);

        public static RoomConstraints.Constraint PEDESTAL = new RoomConstraints.Constraint(
                                                                bc => bc.HasTag(RoomConstraintTags.ItemPedestalTag),
                                                                null,
                                                                name: STRINGS.ROOMS.CRITERIA.PEDESTAL.NAME,
                                                                description: STRINGS.ROOMS.CRITERIA.PEDESTAL.DESCRIPTION);

        public static RoomConstraints.Constraint MASTERPIECES = new RoomConstraints.Constraint(
                                                                    null,
                                                                    room =>
                                                                    {
                                                                        int count = 0;
                                                                        if (room != null)
                                                                            foreach (KPrefabID building in room.buildings)
                                                                                if (building != null)
                                                                                {
                                                                                    Artable art = building.GetComponent<Artable>();
                                                                                    if (art == null)
                                                                                        continue;
                                                                                    if (art.CurrentStatus == Artable.Status.Great)
                                                                                        count++;
                                                                                }
                                                                        return count >= requiredMasterpieces;
                                                                    },
                                                                    name: string.Format(STRINGS.ROOMS.CRITERIA.MASTERPIECES.NAME, requiredMasterpieces),
                                                                    description: string.Format(STRINGS.ROOMS.CRITERIA.MASTERPIECES.DESCRIPTION, requiredMasterpieces));

        public static RoomConstraints.Constraint FOSSILS = new RoomConstraints.Constraint(
                                                                null,
                                                                room =>
                                                                {
                                                                    int count = 0;
                                                                    if (room != null)
                                                                        foreach (KPrefabID building in room.buildings)
                                                                            if (building != null)
                                                                                if (building.HasTag(RoomConstraintTags.FossilBuilding))
                                                                                    count++;
                                                                    return count >= requiredFossils;
                                                                },
                                                                name: string.Format(STRINGS.ROOMS.CRITERIA.FOSSILS.NAME, requiredFossils),
                                                                description: string.Format(STRINGS.ROOMS.CRITERIA.FOSSILS.DESCRIPTION, requiredFossils));
        
        private static bool IsDecorativePlant(KPrefabID plant)
        {
            return DecorativeNames.Contains(plant.name) || plant.HasTag(MorePlantsTag);
        }
    }
}
