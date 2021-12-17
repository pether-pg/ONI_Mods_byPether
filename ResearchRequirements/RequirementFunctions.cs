using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ResearchRequirements
{
    public class RequirementFunctions
    {
        public struct GasExposure
        {
            public float OxygenMass;
            public float OtherMass;

            public GasExposure(float oxygen, float other)
            {
                OxygenMass = oxygen;
                OtherMass = other;
            }

            public static GasExposure operator +(GasExposure a, GasExposure b) => new GasExposure(a.OxygenMass + b.OxygenMass, a.OtherMass + b.OtherMass);
        }

        public static GasExposure GetElementExposition(int cell, int xWidth, int yHeight)
        {
            GasExposure exp;
            exp.OxygenMass = 0;
            exp.OtherMass = 0;
            Element oxygen = ElementLoader.GetElement(GameTags.Oxygen);

            for (int x = 0; x  <xWidth; x++)
                for(int y = 0; y < yHeight; y++)
                {
                    int offsetCell = Grid.OffsetCell(cell, x, y);
                    if (Grid.Element[offsetCell] == oxygen)
                        exp.OxygenMass += Grid.Mass[offsetCell];
                    else if(Grid.Element[offsetCell].IsGas)
                        exp.OtherMass += Grid.Mass[offsetCell];
                }

            return exp;
        }

        public static float NonOxygenExposure()
        {
            GasExposure exp;
            exp.OtherMass = 0;
            exp.OxygenMass = 0;

            foreach (IUsable toilet in Components.Toilets)
                exp = exp + GetElementExposition(Grid.PosToCell((KMonoBehaviour)toilet), 2, 3);

            foreach (Sleepable sleepable in Components.Sleepables)
                if (sleepable.name == "BedComplete")
                    exp = exp + GetElementExposition(Grid.PosToCell((KMonoBehaviour)sleepable), 2, 2);
                else
                    exp = exp + GetElementExposition(Grid.PosToCell((KMonoBehaviour)sleepable), 4, 2);

            if (exp.OtherMass + exp.OxygenMass <= 0)
                return 0;
            return 100 * (exp.OtherMass / (exp.OtherMass + exp.OxygenMass));
        }

        private static Dictionary<Tag, float> StoredGases = new Dictionary<Tag, float>();
        private static Dictionary<Tag, float> StoredLiquids = new Dictionary<Tag, float>();
        private static float TotalHEPs;

        public static void InitalizeDictionaries()
        {
            TotalHEPs = 0;
            StoredGases = new Dictionary<Tag, float>();
            StoredLiquids = new Dictionary<Tag, float>();

            if (!TechRequirements.Instance.GetGameTech("Plastics").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.CrudeOil).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("ValveMiniaturization").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.Petroleum).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("LiquidFiltering").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.SaltWater).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("LiquidFiltering").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.Brine).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("Distillation").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag, 0);

            if (!TechRequirements.Instance.GetGameTech("Catalytics").IsComplete())
                StoredGases.Add(ElementLoader.FindElementByHash(SimHashes.CarbonDioxide).tag, 0);
            if (!TechRequirements.Instance.GetGameTech("RenewableEnergy").IsComplete())
                StoredGases.Add(ElementLoader.FindElementByHash(SimHashes.Steam).tag, 0);

            //DLC Techs:
            if (DlcManager.IsExpansion1Active() && !TechRequirements.Instance.GetGameTech("CryoFuelPropulsion").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.LiquidHydrogen).tag, 0);
            if (DlcManager.IsExpansion1Active() && !TechRequirements.Instance.GetGameTech("HydrocarbonPropulsion").IsComplete())
                StoredLiquids.Add(ElementLoader.FindElementByHash(SimHashes.LiquidOxygen).tag, 0);
        }

        private static void PopulateDictionary(BuildingComplete building, ref Dictionary<Tag, float> dictionary)
        {
            Reservoir res = building.gameObject.GetComponent<Reservoir>();
            if (res != null)
            {
                Storage storage = Traverse.Create(res).Field("storage").GetValue<Storage>();
                if (storage != null)
                {
                    List<Tag> keys = dictionary.Keys.ToList();
                    foreach (Tag key in keys)
                        dictionary[key] += storage.GetMassAvailable(key);
                }
            }
        }

        private static void AddTotalHEPs(BuildingComplete building)
        {
            HighEnergyParticleStorage storage = building.gameObject.GetComponent<HighEnergyParticleStorage>();
            if (storage != null)
                TotalHEPs += storage.Particles;
        }

        public static void CountResourcesInReservoirs()
        {
            InitalizeDictionaries();

            foreach (BuildingComplete building in Components.BuildingCompletes)
            {
                if (building.gameObject.name == "GasReservoirComplete" && StoredGases.Keys.Count > 0)
                    PopulateDictionary(building, ref StoredGases);
                else if (building.gameObject.name == "LiquidReservoirComplete" && StoredLiquids.Keys.Count > 0)
                    PopulateDictionary(building, ref StoredLiquids);
                else if (building.gameObject.name == "HEPBatteryComplete")
                    AddTotalHEPs(building);
            }
        }

        public static float StoredGas(SimHashes hash)
        {
            Tag tag = ElementLoader.FindElementByHash(hash).tag;

            if (!StoredGases.ContainsKey(tag))
            {
                Debug.Log($"Research Requirements: Stored tag not tracked: {tag}");
                return 0;
            }
            return StoredGases[tag];
        }

        public static float StoredLiquid(SimHashes hash)
        {
            Tag tag = ElementLoader.FindElementByHash(hash).tag;

            if (!StoredLiquids.ContainsKey(tag))
            {
                Debug.Log($"Research Requirements: Stored tag not tracked: {tag}");
                return 0;
            }
            return StoredLiquids[tag];
        }

        public static int LoudDupes()
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
            {
                if (resume.gameObject.GetComponent<Snorer>() != null
                    || resume.gameObject.GetComponent<Flatulence>() != null)
                    count++;
            }
            return count;
        }

        public static int AnalysedPlanets()
        {
            int count = 0;
            foreach (SpaceDestination destination in SpacecraftManager.instance.destinations)
                if (destination.AnalysisState() == SpacecraftManager.DestinationAnalysisState.Complete)
                    count++;
            return count;
        }

        public static int HomeSweetHome()
        {
            ColonyAchievementTracker tracker = SaveGame.Instance.GetComponent<ColonyAchievementTracker>();
            if (tracker == null)
                return 0;
            ColonyAchievementStatus status = tracker.achievements.Where(a => a.Key == Db.Get().ColonyAchievements.Thriving.Id).First().Value;
            if (status.success && !status.failed)
                return 1;
            return 0;
        }

        public static int NumberOfMonuments()
        {
            int count = 0;
            foreach (MonumentPart monumentPart in Components.MonumentParts)
                if (monumentPart.part == MonumentPart.Part.Top && monumentPart.IsMonumentCompleted())
                    count++;
            return count;
        }

        public static int DuplicantsWithEffect(string effectName)
        {
            int count = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                GameObject go = identity.gameObject;
                if (go == null)
                    continue;
                Klei.AI.Effects AIeffects = go.GetComponent<Klei.AI.Effects>();
                if (AIeffects == null)
                    continue;
                if (AIeffects.HasEffect(effectName))
                    count++;
            }
            return count;
        }

        public static float PercentOfDupesWithEffect(string effectName)
        {
            int count = DuplicantsWithEffect(effectName);
            return 100.0f * count / Components.MinionIdentities.Count;
        }

        public static int SickDuplicants(string diseaseId)
        {
            int count = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                MinionModifiers modifiers = identity.GetComponent<MinionModifiers>();
                if (modifiers == null)
                    continue;

                foreach (Klei.AI.SicknessInstance sickness in modifiers.sicknesses.ModifierList)
                {
                    if (sickness.Sickness.Id == diseaseId)
                        count++;
                }

            }
            return count;
        }

        public static float MaximumAttribute(string attributeName)
        {
            float max = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                MinionModifiers modifiers = identity.GetComponent<MinionModifiers>();
                if (modifiers == null)
                    continue;
                Klei.AI.AttributeInstance attributeInstance = modifiers.attributes.AttributeTable.Where(p => p.Name == attributeName).FirstOrDefault();
                if (attributeInstance == null)
                {
                    continue;
                }

                float value = attributeInstance.GetTotalValue();
                if (value > max)
                    max = value;
            }
            return max;
        }

        public static int DuplicantsWithInterest(string skillId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
                if (resume.HasSkillAptitude(Db.Get().Skills.Get(skillId)))
                    count++;
            return count;
        }

        public static int DuplicantsWithSkill(string skillId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
                if (resume.HasMasteredSkill(skillId))
                    count++;
            return count;
        }

        public static int NamedCritters(string name)
        {
            int count = 0;
            foreach (object brain in Components.Brains)
            {
                CreatureBrain cmp = brain as CreatureBrain;
                if ((UnityEngine.Object)cmp != (UnityEngine.Object)null && cmp.name.Contains(name))
                    count++;
            }
            return count;
        }

        public static float Resources(SimHashes hash)
        {
            Tag tag = ElementLoader.FindElementByHash(hash).tag;
            return Resources(tag);
        }

        public static float Resources(Tag tag)
        {
            GVD.VersionAlert(false);
            // Works for DLC
            /* */
            if(ClusterManager.Instance.GetAllWorldsAccessibleAmounts().ContainsKey(tag))
                return ClusterManager.Instance.GetAllWorldsAccessibleAmounts()[tag];
            return 0;
            // Works for vanilla 
            /*
            return WorldInventory.Instance.GetTotalAmount(tag);*/
        }

        public static float DailyReport_Average(ReportManager.ReportType entryType)
        {
            float total = DailyReport_Positive(entryType);
            return total * 100 / 600 / Components.MinionResumes.Count;
        }

        public static float DailyReport_Net(ReportManager.ReportType entryType)
        {
            float yesterday = ReportManager.Instance.YesterdaysReport == null ? 0 : ReportManager.Instance.YesterdaysReport.GetEntry(entryType).Net;
            float today = ReportManager.Instance.TodaysReport == null ? 0 : ReportManager.Instance.TodaysReport.GetEntry(entryType).Net;

            return Math.Max(yesterday, today);
        }

        public static float DailyReport_Positive(ReportManager.ReportType entryType)
        {
            float yesterday = ReportManager.Instance.YesterdaysReport == null ? 0 : ReportManager.Instance.YesterdaysReport.GetEntry(entryType).Positive;
            float today = ReportManager.Instance.TodaysReport == null ? 0 : ReportManager.Instance.TodaysReport.GetEntry(entryType).Positive;

            return Math.Max(yesterday, today);
        }

        public static float DailyReport_Negative(ReportManager.ReportType entryType)
        {
            float yesterday = ReportManager.Instance.YesterdaysReport == null ? 0 : Math.Abs(ReportManager.Instance.YesterdaysReport.GetEntry(entryType).Negative);
            float today = ReportManager.Instance.TodaysReport == null ? 0 : Math.Abs(ReportManager.Instance.TodaysReport.GetEntry(entryType).Negative);

            return Math.Max(yesterday, today);
        }

        public static int DomesticatedCritters()
        {
            int count = 0;
            foreach (object brain in Components.Brains)
            {
                CreatureBrain cmp = brain as CreatureBrain;
                if ((UnityEngine.Object)cmp != (UnityEngine.Object)null)
                    if (!cmp.HasTag(GameTags.Creatures.Wild))
                        count++;
            }
            return count;
        }

        public static int NonManualGeneratorsCount()
        {
            int count = 0;
            foreach (Generator generator in Components.Generators)
                if (!generator.ToString().Contains("ManualGenerator"))
                    count++;
            return count;
        }

        public static float NonManualGenertorsPercent()
        {
            int nonManual = 0;
            int gymGenerators = 0;
            foreach (Generator generator in Components.Generators)
                if (!generator.ToString().Contains("ManualGenerator"))
                {
                    nonManual++;
                }
                else
                {
                    CavityInfo info = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(generator));
                    if (info == null || info.room == null || info.room.roomType == null)
                        continue;
                    if (info.room.roomType.Id == "GymRoom") // use room Id from Rooms Expanded
                        gymGenerators++;
                }

            if (gymGenerators == Components.Generators.Count)
                return 0;
            return 100.0f * nonManual / (Components.Generators.Count - gymGenerators);
        }

        public static int DuplicantsWithTrait(string traitId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
            {
                Klei.AI.Traits traits = resume.gameObject.GetComponent<Klei.AI.Traits>();
                if (traits != null)
                {
                    if (traits.HasTrait(traitId))
                        count++;
                }
            }
            return count;
        }

        // DLC specific functions

        public static int PilotWithTrait(string traitId)
        {
            int count = 0;
            foreach (MinionResume resume in Components.MinionResumes)
                if (resume.HasMasteredSkill("RocketPiloting1"))
                {
                    Klei.AI.Traits traits = resume.gameObject.GetComponent<Klei.AI.Traits>();
                    if (traits != null)
                    {
                        if (traits.HasTrait(traitId))
                            count++;
                    }
                }
            return count;
        }

        public static int WorldsWithBeds()
        {
            GVD.VersionAlert(false);
            // Vanilla - default return
            //return 0;
            // DLC only code
            /**/
            List<int> UniqueWorldIds = new List<int>();
            foreach (Sleepable sleepable in Components.Sleepables)
                if (!UniqueWorldIds.Contains(sleepable.GetMyWorldId()) && !sleepable.GetMyWorld().IsModuleInterior)
                    UniqueWorldIds.Add(sleepable.GetMyWorldId());

            foreach (int i in UniqueWorldIds)
                Debug.Log($"Unique sleepable world: {i}");

            return UniqueWorldIds.Count;
        }

        public static int RevealedSpaceHexes(int radiusMin, int radiusMax)
        {
            GVD.VersionAlert(false);
            // Vanilla - default return
            //return 0;
            // DLC only code
            /**/
            int count = 0;
            ClusterFogOfWarManager.Instance smi = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
            if (smi == null)
                return 0;

            List<AxialI> ColonisedWorlds = new List<AxialI>();
            foreach (Sleepable sleepable in Components.Sleepables)
                if (!ColonisedWorlds.Contains(sleepable.GetMyWorldLocation()))
                    ColonisedWorlds.Add(sleepable.GetMyWorldLocation());

            List<AxialI> HexesMinRad = new List<AxialI>();
            List<AxialI> HexesMaxRad = new List<AxialI>();
            List<AxialI> SearchedHexes = new List<AxialI>();

            foreach(AxialI world in ColonisedWorlds)
            {
                foreach (AxialI hex in AxialUtil.GetAllPointsWithinRadius(world, radiusMin))
                    if (!HexesMinRad.Contains(hex))
                        HexesMinRad.Add(hex);

                foreach (AxialI hex in AxialUtil.GetAllPointsWithinRadius(world, radiusMax))
                    if (!HexesMaxRad.Contains(hex))
                        HexesMaxRad.Add(hex);
            }
            foreach (AxialI hex in HexesMaxRad)
                if (!HexesMinRad.Contains(hex))
                    SearchedHexes.Add(hex);

            foreach (AxialI hex in SearchedHexes)
                if (smi.IsLocationRevealed(hex))
                    count++;

            return count;
        }

        public static int MaxColonyDistance()
        {
            int max = 0;
            foreach(Telepad telepad1 in Components.Telepads)
                foreach(Telepad telepad2 in Components.Telepads)
                {
                    int distance = AxialUtil.GetDistance(telepad1.GetMyWorldLocation(), telepad2.GetMyWorldLocation());
                    if (distance > max)
                        max = distance;
                }

            return max;
        }

        public static int DuplicantsWithMorale(int morale)
        {
            int count = 0;
            foreach (MinionIdentity identity in Components.MinionIdentities)
            {
                Klei.AI.AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup((Component)identity.gameObject.GetComponent<MinionModifiers>());
                if (attributeInstance == null)
                    continue;

                float value = attributeInstance.GetTotalValue();
                if (value >= morale)
                    count++;
            }
            return count;
        }

        public static float StoredHEPs()
        {
            return TotalHEPs;
        }

        public static int MutatedSeeds()
        {
            int count = 0;

            foreach(PlantableSeed seed in Components.PlantableSeeds)
            {
                MutantPlant mutant = seed.gameObject.GetComponent<MutantPlant>();
                if (mutant != null && !mutant.IsOriginal)
                    count++;
            }

            return count;
        }
    }
}
