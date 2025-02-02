using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace ResearchRequirements
{
    public class ReqFunc_Misc
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

            foreach (int worldID in ReqFunc_Space.GetAllWorldIds())
                foreach (Sleepable sleepable in Components.NormalBeds.WorldItemsEnumerate(worldID, true))
                    if (sleepable.name == "LuxuryBedComplete")
                        exp = exp + GetElementExposition(Grid.PosToCell((KMonoBehaviour)sleepable), 4, 2);
                    else
                        exp = exp + GetElementExposition(Grid.PosToCell((KMonoBehaviour)sleepable), 2, 2);

            if (exp.OtherMass + exp.OxygenMass <= 0)
                return 0;
            return 100 * (exp.OtherMass / (exp.OtherMass + exp.OxygenMass));
        }

        public static float MinimumNonZeroBreathability(float sampleTime)
        {
            List<int> worldIds = ClusterManager.Instance.GetWorldIDsSorted();
            float minimum = 100;
            foreach(int worldID in worldIds)
            {
                WorldTracker tracker = TrackerTool.Instance.GetWorldTracker<BreathabilityTracker>(worldID);
                if (tracker == null)
                    continue;

                BreathabilityTracker bTracker = (BreathabilityTracker)tracker;
                if (bTracker == null)
                    continue;

                float avg = bTracker.GetAverageValue(sampleTime);
                if (avg != 0 && avg < minimum)
                    minimum = avg;
            }
            return minimum;
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
                if (monumentPart.part == Database.MonumentPartResource.Part.Top && monumentPart.IsMonumentCompleted())
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

        public static int AnalyzedGeysers()
        {
            int count = 0;

            if(ResearchRequirements_Patches_Geysers.Geyser_OnSpawn_Patch.Geysers != null)
                foreach(Geyser g in ResearchRequirements_Patches_Geysers.Geyser_OnSpawn_Patch.Geysers)
                {
                    if (g == null || g.gameObject == null)
                        continue;
                    Studyable study = g.gameObject.GetComponent<Studyable>();
                    if (study != null && study.Studied)
                        count++;
                }

            return count;
        }

        public static int RadboltTravelDistance()
        {
            if(!DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID))
                return 0;
            ColonyAchievementTracker tracker = SaveGame.Instance.GetComponent<ColonyAchievementTracker>();
            if (tracker == null)
                return 0;

            return (int)tracker.radBoltTravelDistance;
        }
    }
}
