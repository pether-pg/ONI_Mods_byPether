using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;

namespace ResearchRequirements
{
    class TechRequirements
    {
        private static TechRequirements _instance = null;

        public static TechRequirements Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TechRequirements();
                return _instance;
            }
        }

        Dictionary<string, TechReq> ReqDict = null;
        Dictionary<string, Tech> TechDict = null;

        public List<string> GetAllTechIds()
        {
            List<string> result = new List<string>();
            foreach (string key in ReqDict.Keys)
                result.Add(key);
            return result;
        }

        public TechReq GetTechReq(string id)
        {
            if (!ReqDict.ContainsKey(id))
                ReqDict.Add(id, new TechReq());
            return ReqDict[id];
        }

        public Tech GetGameTech(TechReq req)
        {
            return GetGameTech(GetTechId(req));
        }

        public Tech GetGameTech(string id)
        {
            if(TechDict == null)
                TechDict = new Dictionary<string, Tech>();
            if(!TechDict.ContainsKey(id))
            {
                Tech tech = Db.Get().Techs.resources.Find(p => p.Id == id);
                if (tech == null)
                    Debug.Log("null tech!");
                TechDict.Add(id, tech);
            }
            return TechDict[id];
        }

        public string GetTechId(TechReq req)
        {
            foreach (string key in ReqDict.Keys)
                if (ReqDict[key] == req)
                    return key;
            return "";
        }

        public bool HasTechReq(string id)
        {
            return ReqDict.ContainsKey(id);
        }

        public bool IsIgnored(string id)
        {
            if (Settings.Instance.Ignored == null || !Settings.Instance.Ignored.ContainsKey(id))
                return false;
            return Settings.Instance.Ignored[id];
        }

        private TechRequirements()
        {
            ReqDict = new Dictionary<string, TechReq>();
            TechDict = new Dictionary<string, Tech>();

            ReqDict.Add("FarmingTech", new TechReq(STRINGS.REQUIREMENTS.FARMING_TECH, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Farming1")));
            ReqDict.Add("FineDining", new TechReq(STRINGS.REQUIREMENTS.FINE_DINING, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Cooking1")));
            ReqDict.Add("FoodRepurposing", new TechReq()); // No need to add anything here, the machine is a quest of its own.
            ReqDict.Add("FinerDining", new TechReq(STRINGS.REQUIREMENTS.FINER_DINING, 15, () => ReqFunc_Dupes.MaximumAttribute("Cuisine"), max: 20));
            ReqDict.Add("Agriculture", new TechReq(STRINGS.REQUIREMENTS.AGRICULTURE, 2, () => ReqFunc_Dupes.DuplicantsWithSkill("Farming2")));
            ReqDict.Add("Ranching", new TechReq(STRINGS.REQUIREMENTS.RANCHING, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Ranching1")));
            ReqDict.Add("AnimalControl", new TechReq(STRINGS.REQUIREMENTS.ANIMAL_CONTROL, 20, () => ReqFunc_Misc.DomesticatedCritters()));
            ReqDict.Add("ImprovedOxygen", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_OXYGEN, 450, () => ReqFunc_Misc.DailyReport_Negative(ReportManager.ReportType.OxygenCreated)));
            ReqDict.Add("GasPiping", new TechReq(STRINGS.REQUIREMENTS.GAS_PIPING, 450, () => ReqFunc_Misc.DailyReport_Positive(ReportManager.ReportType.OxygenCreated)));
            ReqDict.Add("ImprovedGasPiping", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_GAS_PIPING, 25, () => ReqFunc_Dupes.PercentOfDupesWithEffect("PoppedEarDrums"), max: 100));
            ReqDict.Add("PressureManagement", new TechReq());
            ReqDict.Add("DirectedAirStreams", new TechReq(STRINGS.REQUIREMENTS.DIRECTED_AIR_STREAMS, 50, () => 100 - ReqFunc_Misc.MinimumNonZeroBreathability(600), max: 100));
            ReqDict.Add("LiquidFiltering", new TechReq(STRINGS.REQUIREMENTS.LIQUID_FILTERING, 5000, () => ReqFunc_Storage.StoredLiquid(SimHashes.SaltWater) + ReqFunc_Storage.StoredLiquid(SimHashes.Brine)));
            ReqDict.Add("MedicineI", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_I, 2, () => ReqFunc_Dupes.SickDuplicants()));
            ReqDict.Add("MedicineII", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_II, 1, () => ReqFunc_Dupes.SickDuplicants("SlimeSickness")));
            ReqDict.Add("MedicineIII", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_III, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Medicine3")));
            ReqDict.Add("MedicineIV", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_IV, 1, () => ReqFunc_Dupes.SickDuplicants("ZombieSickness")));
            ReqDict.Add("LiquidPiping", new TechReq(STRINGS.REQUIREMENTS.LIQUID_PIPING, 1, () => ReqFunc_Dupes.DuplicantsWithInterest("Basekeeping1")));
            ReqDict.Add("ImprovedLiquidPiping", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_LIQUID_PIPING, 2, () => ReqFunc_Dupes.DuplicantsWithSkill("Basekeeping2")));
            ReqDict.Add("PrecisionPlumbing", new TechReq(STRINGS.REQUIREMENTS.PRECISION_PLUMBING, 1, () => GameClock.Instance.GetTimeSinceStartOfCycle() <= 2 * 25 ? 1 : 0, continuous: true, max: 1));
            ReqDict.Add("SanitationSciences", new TechReq(STRINGS.REQUIREMENTS.SANITATION_SCIENCES, 3, () => ReqFunc_Misc.NamedCritters("Glom")));
            ReqDict.Add("FlowRedirection", new TechReq(STRINGS.REQUIREMENTS.FLOW_REDIRECTION, 15, () => ReqFunc_Dupes.MaximumAttribute("Athletics"), max: 20));
            ReqDict.Add("AdvancedFiltration", new TechReq());
            ReqDict.Add("Distillation", new TechReq(STRINGS.REQUIREMENTS.DISTILLATION, 5000, () => ReqFunc_Storage.StoredLiquid(SimHashes.DirtyWater)));
            ReqDict.Add("Catalytics", new TechReq(STRINGS.REQUIREMENTS.CATALYTICS, 600, () => ReqFunc_Storage.StoredGas(SimHashes.CarbonDioxide)));
            ReqDict.Add("PowerRegulation", new TechReq(STRINGS.REQUIREMENTS.POWER_REGULATION, 10, () => Components.Batteries.Count));
            ReqDict.Add("AdvancedPowerRegulation", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_POWER_REGULATION, 5, () => ReqFunc_Misc.NonManualGeneratorsCount()));
            ReqDict.Add("PrettyGoodConductors", new TechReq(STRINGS.REQUIREMENTS.PRETTY_GOOD_CONDUCTORS, 10, () => ReqFunc_Misc.NonManualGeneratorsCount()));
            ReqDict.Add("RenewableEnergy", new TechReq(STRINGS.REQUIREMENTS.RENEWABLE_ENERGY, 600, () => ReqFunc_Storage.StoredGas(SimHashes.Steam)));
            ReqDict.Add("Combustion", new TechReq(STRINGS.REQUIREMENTS.COMBUSTION, 200, () => ReqFunc_Misc.DailyReport_Negative(ReportManager.ReportType.EnergyCreated) / 1000));
            ReqDict.Add("ImprovedCombustion", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_COMBUSTION, 100, () => GameClock.Instance.GetCycle()));
            ReqDict.Add("InteriorDecor", new TechReq(STRINGS.REQUIREMENTS.INTERIOR_DECOR, 1, () => GameClock.Instance.IsNighttime() ? 1 : 0, continuous: true, max: 1));
            ReqDict.Add("Artistry", new TechReq(STRINGS.REQUIREMENTS.ARTISTRY, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Arting1")));
            ReqDict.Add("Clothing", new TechReq(STRINGS.REQUIREMENTS.CLOTHING, 50, () => ReqFunc_Storage.Resources(GameTags.BuildingFiber)));
            ReqDict.Add("Acoustics", new TechReq(STRINGS.REQUIREMENTS.ACOUSTICS, 1, () => ReqFunc_Dupes.LoudDupes()));
            ReqDict.Add("FineArt", new TechReq(STRINGS.REQUIREMENTS.FINE_ART, 2, () => ReqFunc_Dupes.DuplicantsWithSkill("Arting2")));
            ReqDict.Add("EnvironmentalAppreciation", new TechReq(STRINGS.REQUIREMENTS.ENVIRONMENTAL_APPRECIATION, 1, () => ReqFunc_Dupes.SickDuplicants("SunburnSickness")));
            ReqDict.Add("Luxury", new TechReq(STRINGS.REQUIREMENTS.LUXURY, 10000, () => ReqFunc_Storage.Resources(SimHashes.Polypropylene)));
            ReqDict.Add("RefractiveDecor", new TechReq(STRINGS.REQUIREMENTS.REFRACTIVE_DECOR, 3, () => ReqFunc_Dupes.DuplicantsWithSkill("Arting3")));
            ReqDict.Add("GlassFurnishings", new TechReq(STRINGS.REQUIREMENTS.GLASS_FURNISHINGS, 10000, () => ReqFunc_Storage.Resources(SimHashes.Glass)));
            ReqDict.Add("Screens", new TechReq()); // no need to add anything here, this is a building for cosmetics only.
            ReqDict.Add("RenaissanceArt", new TechReq(STRINGS.REQUIREMENTS.RENAISSANCE_ART, 15, () => ReqFunc_Dupes.MaximumAttribute("Creativity"), max: 20));
            ReqDict.Add("Plastics", new TechReq(STRINGS.REQUIREMENTS.PLASTICS, 5000, () => ReqFunc_Storage.StoredLiquid(SimHashes.CrudeOil)));
            ReqDict.Add("ValveMiniaturization", new TechReq(STRINGS.REQUIREMENTS.VALVE_MINIATURIZATION, 5000, () => ReqFunc_Storage.StoredLiquid(SimHashes.Petroleum)));
            ReqDict.Add("Suits", new TechReq(STRINGS.REQUIREMENTS.SUITS, 3, () => ReqFunc_Dupes.DuplicantsWithSkill("Suits1")));
            ReqDict.Add("Jobs", new TechReq(STRINGS.REQUIREMENTS.JOBS, 3, () => Components.MinionResumes.Count - 3));
            ReqDict.Add("AdvancedResearch", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_RESEARCH, GVD.AdvancedResearchThreshold, () => Db.Get().Techs.resources.Where(p => p.IsComplete()).Count(), max: GVD.LowTierTechs - 1));
            
            ReqDict.Add("ArtificialFriends", new TechReq(STRINGS.REQUIREMENTS.ARTIFICIAL_FRIENDS, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Technicals1")));
            ReqDict.Add("BasicRefinement", new TechReq(STRINGS.REQUIREMENTS.BASIC_REFINEMENT, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Mining1")));
            ReqDict.Add("RefinedObjects", new TechReq(STRINGS.REQUIREMENTS.REFINED_OBJECTS, 200, () => Components.Ladders.Count));
            ReqDict.Add("Smelting", new TechReq(STRINGS.REQUIREMENTS.SMELTING, 5000, () => ReqFunc_Storage.Resources(GameTags.RefinedMetal)));
            ReqDict.Add("HighTempForging", new TechReq(STRINGS.REQUIREMENTS.HIGH_TEMP_FORGING, 5000, () => ReqFunc_Storage.Resources(SimHashes.Steel)));
            ReqDict.Add("TemperatureModulation", new TechReq(STRINGS.REQUIREMENTS.TEMPERATURE_MODULATION, 20, () => ReqFunc_Dupes.PercentOfDupesWithEffect("ColdAir") + ReqFunc_Dupes.PercentOfDupesWithEffect("WarmAir"), max: 100));
            ReqDict.Add("HVAC", new TechReq(STRINGS.REQUIREMENTS.HVAC, 1000, () => ReqFunc_Storage.Resources(SimHashes.Ice)));
            ReqDict.Add("LiquidTemperature", new TechReq(STRINGS.REQUIREMENTS.LIQUID_TEMPERATURE, 3, () => ReqFunc_Misc.AnalyzedGeysers()));
            ReqDict.Add("LogicControl", new TechReq(STRINGS.REQUIREMENTS.LOGIC_CONTROL, 50, () => ReqFunc_Misc.DailyReport_Negative(ReportManager.ReportType.EnergyWasted) / 1000));
            ReqDict.Add("GenericSensors", new TechReq(STRINGS.REQUIREMENTS.GENERIC_SENSORS, 1, () => ReqFunc_Dupes.DuplicantsWithInterest("Technicals2")));
            ReqDict.Add("LogicCircuits", new TechReq(STRINGS.REQUIREMENTS.LOGIC_CIRCUITS, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("Researching2")));
            ReqDict.Add("ParallelAutomation", new TechReq(STRINGS.REQUIREMENTS.PARALLEL_AUTOMATION, 4, () => Components.ResearchCenters.Count));
            ReqDict.Add("DupeTrafficControl", new TechReq(STRINGS.REQUIREMENTS.DUPE_TRAFFIC_CONTROL, 1, () => ReqFunc_Dupes.DuplicantsWithSkill(DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID) ? "Astronomy" : "Researching3")));
            ReqDict.Add("Multiplexing", new TechReq());
            ReqDict.Add("SkyDetectors", new TechReq(STRINGS.REQUIREMENTS.SKY_DETECTORS, 1, () => ReqFunc_Misc.HomeSweetHome(), max: 1));
            ReqDict.Add("TravelTubes", new TechReq(STRINGS.REQUIREMENTS.TRAVEL_TUBES, 66, () => ReqFunc_Misc.DailyReport_Average(ReportManager.ReportType.TravelTime)));
            ReqDict.Add("SmartStorage", new TechReq(STRINGS.REQUIREMENTS.SMART_STORAGE, 2, () => ReqFunc_Dupes.DuplicantsWithSkill("Hauling2")));
            ReqDict.Add("SolidTransport", new TechReq(STRINGS.REQUIREMENTS.SOLID_TRANSPORT, 15, () => ReqFunc_Dupes.MaximumAttribute("Strength")));
            ReqDict.Add("SolidManagement", new TechReq());
            ReqDict.Add("BasicRocketry", new TechReq(STRINGS.REQUIREMENTS.BASIC_ROCKETRY, 4, () => ReqFunc_Space.AnalysedPlanets(), max: 30));
            ReqDict.Add("Jetpacks", new TechReq(STRINGS.REQUIREMENTS.JETPACKS, 20, () => ReqFunc_Misc.NamedCritters("Oilfloater")));
            ReqDict.Add("RoboticTools", new TechReq(STRINGS.REQUIREMENTS.ROBOTIC_TOOLS, 3, () => ReqFunc_Dupes.DuplicantsWithSkill("Mining3")));
            ReqDict.Add("Monuments", new TechReq(STRINGS.REQUIREMENTS.MONUMENTS, 12, () => ReqFunc_Dupes.DuplicantsWithMorale(16)));
            ReqDict.Add("PortableGasses", new TechReq(STRINGS.REQUIREMENTS.PORTABLE_GASES, 4, () => ReqFunc_Dupes.DuplicantsWithEffect("ContaminatedLungs")));

            if (DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID))
            {
                ReqDict.Add("NotificationSystems", new TechReq(STRINGS.REQUIREMENTS.NOTIFICATION_SYSTEMS, 1, () => ReqFunc_Dupes.DuplicantsWithTrait("Loner")));
                ReqDict.Add("AdvancedNuclearResearch", new TechReq());
                ReqDict.Add("NuclearStorage", new TechReq(STRINGS.REQUIREMENTS.NUCLEAR_STORAGE, 10000, () => ReqFunc_Misc.RadboltTravelDistance()));
                ReqDict.Add("SpaceCombustion", new TechReq(STRINGS.REQUIREMENTS.SPACE_COMBUSTION, 5000, () => ReqFunc_Storage.Resources(SimHashes.Sucrose)));
                ReqDict.Add("NuclearPropulsion", new TechReq(STRINGS.REQUIREMENTS.NUCLEAR_PROPULSION, 4000, () => ReqFunc_Storage.StoredHEPs()));
                ReqDict.Add("NuclearRefinement", new TechReq(STRINGS.REQUIREMENTS.NUCLEAR_REFINEMENT, 2, () => ReqFunc_Dupes.DuplicantsWithSkill("Mining4")));
                ReqDict.Add("HighVelocityTransport", new TechReq(STRINGS.REQUIREMENTS.HIGH_VELOCITY_TRANSPORT, 10, () => ReqFunc_Space.MaxColonyDistance()));
                ReqDict.Add("Bioengineering", new TechReq(STRINGS.REQUIREMENTS.BIOENGINEERING, 1, () => ReqFunc_Misc.MutatedSeeds()));
                ReqDict.Add("LiquidDistribution", new TechReq(STRINGS.REQUIREMENTS.LIQUID_DISTRIBUTION, 1, () => ReqFunc_Dupes.PilotWithTrait("SmallBladder")));
                ReqDict.Add("SpacePower", new TechReq(STRINGS.REQUIREMENTS.SPACE_POWER, 1, () => ReqFunc_Dupes.PilotWithTrait("NightLight")));
                ReqDict.Add("BetterHydroCarbonPropulsion", new TechReq(STRINGS.REQUIREMENTS.BETTER_HYDROCARBON_PROPULSION, 5000, () => ReqFunc_Storage.StoredLiquid(SimHashes.LiquidOxygen)));
                ReqDict.Add("CryoFuelPropulsion", new TechReq(STRINGS.REQUIREMENTS.CRYO_FUEL_PROPULSION, 5000, () => ReqFunc_Storage.StoredLiquid(SimHashes.LiquidHydrogen)));
                ReqDict.Add("SpaceProgram", new TechReq(STRINGS.REQUIREMENTS.SPACE_PROGRAM, 1, () => ReqFunc_Dupes.DuplicantsWithSkill("RocketPiloting1")));
                ReqDict.Add("CrashPlan", new TechReq(STRINGS.REQUIREMENTS.CRASH_PLAN, 3, () => ReqFunc_Space.WorldsWithBeds()));
                ReqDict.Add("DurableLifeSupport", new TechReq(STRINGS.REQUIREMENTS.DURABLE_LIFE_SUPPORT, 10, () => ReqFunc_Dupes.DuplicantsWithSkill("RocketPiloting1")));
                ReqDict.Add("GasDistribution", new TechReq(STRINGS.REQUIREMENTS.GAS_DISTRIBUTION, 1, () => ReqFunc_Dupes.PilotWithTrait("MouthBreather")));
                ReqDict.Add("AdvancedScanners", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_SCANNERS, 3, () => ReqFunc_Space.RevealedSpaceHexes(3, 6)));
                ReqDict.Add("SolidDistribution", new TechReq(STRINGS.REQUIREMENTS.SOLID_DISTRIBUTION, 1, () => ReqFunc_Dupes.PilotWithTrait("CalorieBurner")));
                ReqDict.Add("HighPressureForging", new TechReq(STRINGS.REQUIREMENTS.HIGH_PRESSURE_FORGING, 5000, () => ReqFunc_Storage.Resources(SimHashes.RefinedCarbon)));
                ReqDict.Add("HighVelocityDestruction", new TechReq(STRINGS.REQUIREMENTS.HIGH_VELOCITY_DESTRUCTION, 5000, () => ReqFunc_Storage.Resources(SimHashes.Diamond)));
                ReqDict.Add("NuclearResearch", new TechReq(STRINGS.REQUIREMENTS.NUCLEAR_RESEARCH, 50, () => Db.Get().Techs.resources.Where(p => p.IsComplete()).Count(), max: GVD.LowTierTechs + GVD.MidTierTechs - 1));
                ReqDict.Add("RadiationProtection", new TechReq(STRINGS.REQUIREMENTS.RADIATION_PROTECTION, 1, () => ReqFunc_Dupes.DuplicantsWithEffect("RadiationExposureMinor") + ReqFunc_Dupes.DuplicantsWithEffect("RadiationExposureMajor") + ReqFunc_Dupes.DuplicantsWithEffect("RadiationExposureExtreme")));
                //ReqDict.Add("AdvancedSanitation", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_SANITATION, 1, () => 0)); // no idea yet... :(
            }
            else
            {
                ReqDict.Add("NotificationSystems", new TechReq()); // no need to add anything here, unlocked buildings do not affect gameplay
            }

            // DLC techs
        }

        public enum ReqUnlockedStatus
        {
            AllUnlocked,
            RequiredTechsLocked,
            RequirementLocked
        }

        public class TechReq
        {
            Func<float> GetFulfilledAmount;

            public int RequiredAmount;
            public bool ContinuousCheck;

            string DescriptionPattern;
            bool LastReqCheckStatus;

            public TechReq()
            {
                DescriptionPattern = "{0}";
                RequiredAmount = 0;
                GetFulfilledAmount = () => 0;
            }

            public TechReq(string pattern, float required, Func<float> reqFunc, bool continuous = false, int max = -1)
            {
                DescriptionPattern = pattern;
                GetFulfilledAmount = reqFunc;
                ContinuousCheck = continuous;
                if (max != -1)
                {
                    float dif = Settings.Instance.DiffictultyScale;
                    float percent = required / max;
                    required = max - max * (float)Math.Pow(1 - percent, dif);
                }
                else
                    required = required * Settings.Instance.DiffictultyScale;
                RequiredAmount = (int)Math.Ceiling(required);
            }

            public bool GetLastUnlockCheck()
            {
                return LastReqCheckStatus;
            }

            public ReqUnlockedStatus AllowsResearch()
            {
                LastReqCheckStatus = false;
                if (!ReqUnlocked())
                    return ReqUnlockedStatus.RequirementLocked;

                Tech tech = TechRequirements.Instance.GetGameTech(this);
                foreach (Tech t in tech.requiredTech)
                    if (!t.IsComplete() && TechRequirements.Instance.GetTechReq(t.Id).AllowsResearch() != ReqUnlockedStatus.AllUnlocked)
                        return ReqUnlockedStatus.RequiredTechsLocked;

                LastReqCheckStatus = true;
                return ReqUnlockedStatus.AllUnlocked;
            }

            public bool ReqUnlocked()
            {
                if (TechRequirements.Instance.IsIgnored(TechRequirements.Instance.GetTechId(this)))
                    return true;
                return GetFulfilledAmount() >= RequiredAmount;
            }

            public string GetDescription()
            {
                string status = string.Empty; 
                string additional = string.Empty;

                ReqUnlockedStatus unlocked = AllowsResearch();
                UI.AutomationState color = unlocked != ReqUnlockedStatus.RequirementLocked ? UI.AutomationState.Active : UI.AutomationState.Standby;

                if (RequiredAmount == 0)
                {
                    Debug.Log($"No requirement for tech: {Instance.GetGameTech(this).Id}");
                    this.DescriptionPattern = "{0}";
                    status += STRINGS.REQUIREMENTS.NO_REQUIREMENT;
                }
                else if (TechRequirements.Instance.IsIgnored(TechRequirements.Instance.GetTechId(this)))
                {
                    this.DescriptionPattern = "{0}";
                    status += STRINGS.REQUIREMENTS.REQUIREMENT_IGNORED;
                }
                else
                    status += UI.FormatAsAutomationState($"{(int)GetFulfilledAmount()} / {RequiredAmount}", color);

                if (unlocked == ReqUnlockedStatus.RequiredTechsLocked)
                    additional = UI.FormatAsAutomationState(STRINGS.REQUIREMENTS.RESEARCH_PREVIOUS, UI.AutomationState.Standby);

                return string.Format(DescriptionPattern, status) + string.Format(" {0}", additional);
            }
        }
    }
}
