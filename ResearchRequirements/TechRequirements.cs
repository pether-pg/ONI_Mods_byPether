using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using Harmony;

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

            ReqDict.Add("FarmingTech", new TechReq(STRINGS.REQUIREMENTS.FARMING_TECH, 1, () => RequirementFunctions.DuplicantsWithSkill("Farming1")));
            ReqDict.Add("FineDining", new TechReq(STRINGS.REQUIREMENTS.FINE_DINING, 1, () => RequirementFunctions.DuplicantsWithSkill("Cooking1")));
            ReqDict.Add("FoodRepurposing", new TechReq()); // No need to add anything here, the machine is a quest of its own.
            ReqDict.Add("FinerDining", new TechReq(STRINGS.REQUIREMENTS.FINER_DINING, 15, () => RequirementFunctions.MaximumAttribute("Cuisine"), max: 20));
            ReqDict.Add("Agriculture", new TechReq(STRINGS.REQUIREMENTS.AGRICULTURE, 2, () => RequirementFunctions.DuplicantsWithSkill("Farming2")));
            ReqDict.Add("Ranching", new TechReq(STRINGS.REQUIREMENTS.RANCHING, 1, () => RequirementFunctions.DuplicantsWithSkill("Ranching1")));
            ReqDict.Add("AnimalControl", new TechReq(STRINGS.REQUIREMENTS.ANIMAL_CONTROL, 20, () => RequirementFunctions.DomesticatedCritters()));
            ReqDict.Add("ImprovedOxygen", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_OXYGEN, 450, () => RequirementFunctions.DailyReport_Negative(ReportManager.ReportType.OxygenCreated)));
            ReqDict.Add("GasPiping", new TechReq(STRINGS.REQUIREMENTS.GAS_PIPING, 450, () => RequirementFunctions.DailyReport_Positive(ReportManager.ReportType.OxygenCreated)));
            ReqDict.Add("ImprovedGasPiping", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_GAS_PIPING, 25, () => RequirementFunctions.PercentOfDupesWithEffect("PoppedEarDrums"), max: 100));
            ReqDict.Add("PressureManagement", new TechReq());
            ReqDict.Add("DirectedAirStreams", new TechReq(STRINGS.REQUIREMENTS.DIRECTED_AIR_STREAMS, 50, () => RequirementFunctions.NonOxygenExposure(), max: 100));
            ReqDict.Add("LiquidFiltering", new TechReq(STRINGS.REQUIREMENTS.LIQUID_FILTERING, 5000, () => RequirementFunctions.StoredLiquid(SimHashes.SaltWater) + RequirementFunctions.StoredLiquid(SimHashes.Brine)));
            ReqDict.Add("MedicineI", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_I, 1, () => RequirementFunctions.SickDuplicants("FoodSickness") + RequirementFunctions.SickDuplicants("Allergies")));
            ReqDict.Add("MedicineII", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_II, 1, () => RequirementFunctions.SickDuplicants("SlimeSickness")));
            ReqDict.Add("MedicineIII", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_III, 1, () => RequirementFunctions.DuplicantsWithSkill("Medicine3")));
            ReqDict.Add("MedicineIV", new TechReq(STRINGS.REQUIREMENTS.MEDICINE_IV, 1, () => RequirementFunctions.SickDuplicants("ZombieSickness")));
            ReqDict.Add("LiquidPiping", new TechReq(STRINGS.REQUIREMENTS.LIQUID_PIPING, 1, () => RequirementFunctions.DuplicantsWithInterest("Basekeeping1")));
            ReqDict.Add("ImprovedLiquidPiping", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_LIQUID_PIPING, 2, () => RequirementFunctions.DuplicantsWithSkill("Basekeeping2")));
            ReqDict.Add("PrecisionPlumbing", new TechReq(STRINGS.REQUIREMENTS.PRECISION_PLUMBING, 1, () => GameClock.Instance.GetTimeSinceStartOfCycle() <= 2 * 25 ? 1 : 0, continuous: true, max: 1));
            ReqDict.Add("SanitationSciences", new TechReq(STRINGS.REQUIREMENTS.SANITATION_SCIENCES, 3, () => RequirementFunctions.NamedCritters("Glom")));
            ReqDict.Add("FlowRedirection", new TechReq(STRINGS.REQUIREMENTS.FLOW_REDIRECTION, 15, () => RequirementFunctions.MaximumAttribute("Athletics"), max: 20));
            ReqDict.Add("AdvancedFiltration", new TechReq());
            ReqDict.Add("Distillation", new TechReq(STRINGS.REQUIREMENTS.DISTILLATION, 5000, () => RequirementFunctions.StoredLiquid(SimHashes.DirtyWater)));
            ReqDict.Add("Catalytics", new TechReq(STRINGS.REQUIREMENTS.CATALYTICS, 600, () => RequirementFunctions.StoredGas(SimHashes.CarbonDioxide)));
            ReqDict.Add("PowerRegulation", new TechReq(STRINGS.REQUIREMENTS.POWER_REGULATION, 10, () => Components.Batteries.Count));
            ReqDict.Add("AdvancedPowerRegulation", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_POWER_REGULATION, 5, () => RequirementFunctions.NonManualGeneratorsCount()));
            ReqDict.Add("PrettyGoodConductors", new TechReq(STRINGS.REQUIREMENTS.PRETTY_GOOD_CONDUCTORS, 10, () => RequirementFunctions.NonManualGeneratorsCount()));
            ReqDict.Add("RenewableEnergy", new TechReq(STRINGS.REQUIREMENTS.RENEWABLE_ENERGY, 600, () => RequirementFunctions.StoredGas(SimHashes.Steam)));
            ReqDict.Add("Combustion", new TechReq(STRINGS.REQUIREMENTS.COMBUSTION, 200, () => RequirementFunctions.DailyReport_Negative(ReportManager.ReportType.EnergyCreated) / 1000));
            ReqDict.Add("ImprovedCombustion", new TechReq(STRINGS.REQUIREMENTS.IMPROVED_COMBUSTION, 100, () => GameClock.Instance.GetCycle()));
            ReqDict.Add("InteriorDecor", new TechReq(STRINGS.REQUIREMENTS.INTERIOR_DECOR, 1, () => GameClock.Instance.IsNighttime() ? 1 : 0, continuous: true, max: 1));
            ReqDict.Add("Artistry", new TechReq(STRINGS.REQUIREMENTS.ARTISTRY, 1, () => RequirementFunctions.DuplicantsWithSkill("Arting1")));
            ReqDict.Add("Clothing", new TechReq(STRINGS.REQUIREMENTS.CLOTHING, 50, () => RequirementFunctions.Resources(GameTags.BuildingFiber)));
            ReqDict.Add("Acoustics", new TechReq(STRINGS.REQUIREMENTS.ACOUSTICS, 1, () => RequirementFunctions.LoudDupes()));
            ReqDict.Add("FineArt", new TechReq(STRINGS.REQUIREMENTS.FINE_ART, 2, () => RequirementFunctions.DuplicantsWithSkill("Arting2")));
            ReqDict.Add("EnvironmentalAppreciation", new TechReq(STRINGS.REQUIREMENTS.ENVIRONMENTAL_APPRECIATION, 1, () => RequirementFunctions.SickDuplicants("SunburnSickness")));
            ReqDict.Add("Luxury", new TechReq(STRINGS.REQUIREMENTS.LUXURY, 10000, () => RequirementFunctions.Resources(SimHashes.Polypropylene)));
            ReqDict.Add("RefractiveDecor", new TechReq(STRINGS.REQUIREMENTS.REFRACTIVE_DECOR, 3, () => RequirementFunctions.DuplicantsWithSkill("Arting3")));
            ReqDict.Add("GlassFurnishings", new TechReq(STRINGS.REQUIREMENTS.GLASS_FURNISHINGS, 10000, () => RequirementFunctions.Resources(SimHashes.Glass)));
            ReqDict.Add("Screens", new TechReq()); // no need to add anything here, this is a building for cosmetics only.
            ReqDict.Add("RenaissanceArt", new TechReq(STRINGS.REQUIREMENTS.RENAISSANCE_ART, 15, () => RequirementFunctions.MaximumAttribute("Creativity"), max: 20));
            ReqDict.Add("Plastics", new TechReq(STRINGS.REQUIREMENTS.PLASTICS, 5000, () => RequirementFunctions.StoredLiquid(SimHashes.CrudeOil)));
            ReqDict.Add("ValveMiniaturization", new TechReq(STRINGS.REQUIREMENTS.VALVE_MINIATURIZATION, 5000, () => RequirementFunctions.StoredLiquid(SimHashes.Petroleum)));
            ReqDict.Add("Suits", new TechReq(STRINGS.REQUIREMENTS.SUITS, 3, () => RequirementFunctions.DuplicantsWithSkill("Suits1")));
            ReqDict.Add("Jobs", new TechReq(STRINGS.REQUIREMENTS.JOBS, 3, () => Components.MinionResumes.Count - 3));
            ReqDict.Add("AdvancedResearch", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_RESEARCH, GVD.AdvancedResearchThreshold, () => Db.Get().Techs.resources.Where(p => p.IsComplete()).Count(), max: GVD.LowTierTechs - 1));
            ReqDict.Add("NotificationSystems", new TechReq()); // no need to add anything here, unlocked buildings do not affect gameplay
            ReqDict.Add("ArtificialFriends", new TechReq(STRINGS.REQUIREMENTS.ARTIFICIAL_FRIENDS, 1, () => RequirementFunctions.DuplicantsWithSkill("Technicals1")));
            ReqDict.Add("BasicRefinement", new TechReq(STRINGS.REQUIREMENTS.BASIC_REFINEMENT, 1, () => RequirementFunctions.DuplicantsWithSkill("Mining1")));
            ReqDict.Add("RefinedObjects", new TechReq(STRINGS.REQUIREMENTS.REFINED_OBJECTS, 200, () => Components.Ladders.Count));
            ReqDict.Add("Smelting", new TechReq(STRINGS.REQUIREMENTS.SMELTING, 5000, () => RequirementFunctions.Resources(GameTags.RefinedMetal)));
            ReqDict.Add("HighTempForging", new TechReq(STRINGS.REQUIREMENTS.HIGH_TEMP_FORGING, 5000, () => RequirementFunctions.Resources(SimHashes.Steel)));
            ReqDict.Add("TemperatureModulation", new TechReq(STRINGS.REQUIREMENTS.TEMPERATURE_MODULATION, 20, () => RequirementFunctions.PercentOfDupesWithEffect("ColdAir") + RequirementFunctions.PercentOfDupesWithEffect("WarmAir"), max: 100));
            ReqDict.Add("HVAC", new TechReq(STRINGS.REQUIREMENTS.HVAC, 1000, () => RequirementFunctions.Resources(SimHashes.Ice)));
            ReqDict.Add("LiquidTemperature", new TechReq(STRINGS.REQUIREMENTS.LIQUID_TEMPERATURE, 1, () => RequirementFunctions.SickDuplicants("ColdSickness") + RequirementFunctions.SickDuplicants("HeatSickness")));
            ReqDict.Add("LogicControl", new TechReq(STRINGS.REQUIREMENTS.LOGIC_CONTROL, 50, () => RequirementFunctions.DailyReport_Negative(ReportManager.ReportType.EnergyWasted) / 1000));
            ReqDict.Add("GenericSensors", new TechReq(STRINGS.REQUIREMENTS.GENERIC_SENSORS, 1, () => RequirementFunctions.DuplicantsWithInterest("Technicals2")));
            ReqDict.Add("LogicCircuits", new TechReq(STRINGS.REQUIREMENTS.LOGIC_CIRCUITS, 1, () => RequirementFunctions.DuplicantsWithSkill("Researching2")));
            ReqDict.Add("ParallelAutomation", new TechReq(STRINGS.REQUIREMENTS.PARALLEL_AUTOMATION, 4, () => Components.ResearchCenters.Count));
            ReqDict.Add("DupeTrafficControl", new TechReq(STRINGS.REQUIREMENTS.DUPE_TRAFFIC_CONTROL, 1, () => RequirementFunctions.DuplicantsWithSkill("Researching3")));
            ReqDict.Add("Multiplexing", new TechReq());
            ReqDict.Add("SkyDetectors", new TechReq(STRINGS.REQUIREMENTS.SKY_DETECTORS, 1, () => RequirementFunctions.HomeSweetHome(), max: 1));
            ReqDict.Add("TravelTubes", new TechReq(STRINGS.REQUIREMENTS.TRAVEL_TUBES, 66, () => RequirementFunctions.DailyReport_Average(ReportManager.ReportType.TravelTime)));
            ReqDict.Add("SmartStorage", new TechReq(STRINGS.REQUIREMENTS.SMART_STORAGE, 2, () => RequirementFunctions.DuplicantsWithSkill("Hauling2")));
            ReqDict.Add("SolidTransport", new TechReq(STRINGS.REQUIREMENTS.SOLID_TRANSPORT, 15, () => RequirementFunctions.MaximumAttribute("Strength")));
            ReqDict.Add("SolidManagement", new TechReq());
            ReqDict.Add("BasicRocketry", new TechReq(STRINGS.REQUIREMENTS.BASIC_ROCKETRY, 4, () => RequirementFunctions.AnalysedPlanets(), max: 30));
            ReqDict.Add("Jetpacks", new TechReq(STRINGS.REQUIREMENTS.JETPACKS, 20, () => RequirementFunctions.NamedCritters("Oilfloater")));

            // DLC techs
            ReqDict.Add("PortableGasses", new TechReq(STRINGS.REQUIREMENTS.PORTABLE_GASES, 4, () => RequirementFunctions.DuplicantsWithEffect("ContaminatedLungs")));
            ReqDict.Add("LiquidDistribution", new TechReq(STRINGS.REQUIREMENTS.LIQUID_DISTRIBUTION, 1, () => RequirementFunctions.PilotWithTrait("SmallBladder")));
            ReqDict.Add("SpacePower", new TechReq(STRINGS.REQUIREMENTS.SPACE_POWER, 1, () => RequirementFunctions.PilotWithTrait("NightLight")));
            ReqDict.Add("HydrocarbonPropulsion", new TechReq(STRINGS.REQUIREMENTS.HYDROCARBON_PROPULSION, 5000, () => RequirementFunctions.StoredLiquid(SimHashes.LiquidOxygen)));
            ReqDict.Add("CryoFuelPropulsion", new TechReq(STRINGS.REQUIREMENTS.CRYO_FUEL_PROPULSION, 5000, () => RequirementFunctions.StoredLiquid(SimHashes.LiquidHydrogen)));
            ReqDict.Add("SpaceProgram", new TechReq(STRINGS.REQUIREMENTS.SPACE_PROGRAM, 1, () => RequirementFunctions.DuplicantsWithSkill("RocketPiloting1")));
            ReqDict.Add("CrashPlan", new TechReq(STRINGS.REQUIREMENTS.CRASH_PLAN, 3, () => RequirementFunctions.WorldsWithBeds()));
            ReqDict.Add("DurableLifeSupport", new TechReq(STRINGS.REQUIREMENTS.DURABLE_LIFE_SUPPORT, 10, () => RequirementFunctions.DuplicantsWithSkill("RocketPiloting1")));
            ReqDict.Add("RoboticTools", new TechReq(STRINGS.REQUIREMENTS.ROBOTIC_TOOLS, 3, () => RequirementFunctions.DuplicantsWithSkill("Mining3")));
            ReqDict.Add("GasDistribution", new TechReq(STRINGS.REQUIREMENTS.GAS_DISTRIBUTION, 1, () => RequirementFunctions.PilotWithTrait("MouthBreather")));
            ReqDict.Add("AdvancedScanners", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_SCANNERS, 3, () => RequirementFunctions.RevealedSpaceHexes(3, 6)));
            ReqDict.Add("SolidDistribution", new TechReq(STRINGS.REQUIREMENTS.SOLID_DISTRIBUTION, 1, () => RequirementFunctions.PilotWithTrait("CalorieBurner")));

            // DLC Nuclear
            ReqDict.Add("NuclearResearch", new TechReq(STRINGS.REQUIREMENTS.NUCLEAR_RESEARCH, 50, () => Db.Get().Techs.resources.Where(p => p.IsComplete()).Count(), max: GVD.LowTierTechs + GVD.MidTierTechs - 1));
            ReqDict.Add("RadiationProtection", new TechReq(STRINGS.REQUIREMENTS.RADIATION_PROTECTION, 1, () => RequirementFunctions.DuplicantsWithEffect("RadiationExposureMinor") + RequirementFunctions.DuplicantsWithEffect("RadiationExposureMajor") + RequirementFunctions.DuplicantsWithEffect("RadiationExposureExtreme")));
            ReqDict.Add("Monuments", new TechReq(STRINGS.REQUIREMENTS.MONUMENTS, 12, () => RequirementFunctions.DuplicantsWithMorale(16)));
            //ReqDict.Add("AdvancedSanitation", new TechReq(STRINGS.REQUIREMENTS.ADVANCED_SANITATION, 1, () => 0)); // no idea yet... :(
            ReqDict.Add("NuclearRefinement", new TechReq(STRINGS.REQUIREMENTS.NUCLEAR_REFINEMENT, 2, () => RequirementFunctions.DuplicantsWithSkill("Mining4")));
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
