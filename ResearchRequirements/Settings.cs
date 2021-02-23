using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResearchRequirements
{
    class Settings
    {
        private static Settings _instance = null;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = JsonSerializer<Settings>.Deserialize();
                if (_instance == null)
                {
                    _instance = new Settings();
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance;
            }
        }

        public float DiffictultyScale = 1f;

        public Dictionary<string, bool> Ignored = new Dictionary<string, bool>()
        {
            {"FarmingTech" , false},
            {"FineDining", false},
            {"FoodRepurposing", false},
            {"FinerDining", false},
            {"Agriculture", false},
            {"Ranching", false},
            {"AnimalControl", false},
            {"ImprovedOxygen", false},
            {"GasPiping", false},
            {"ImprovedGasPiping", false},
            {"PressureManagement", false},
            {"DirectedAirStreams", false},
            {"LiquidFiltering", false},
            {"MedicineI", false},
            {"MedicineII", false},
            {"MedicineIII", false},
            {"MedicineIV", false},
            {"LiquidPiping", false},
            {"ImprovedLiquidPiping", false},
            {"PrecisionPlumbing", false},
            {"SanitationSciences", false},
            {"FlowRedirection", false},
            {"AdvancedFiltration", false},
            {"Distillation", false},
            {"Catalytics", false},
            {"PowerRegulation", false},
            {"AdvancedPowerRegulation", false},
            {"PrettyGoodConductors", false},
            {"RenewableEnergy", false},
            {"Combustion", false},
            {"ImprovedCombustion", false},
            {"InteriorDecor", false},
            {"Artistry", false},
            {"Clothing", false},
            {"Acoustics", false},
            {"FineArt", false},
            {"EnvironmentalAppreciation", false},
            {"Luxury", false},
            {"RefractiveDecor", false},
            {"GlassFurnishings", false},
            {"Screens", false},
            {"RenaissanceArt", false},
            {"Plastics", false},
            {"ValveMiniaturization", false},
            {"Suits", false},
            {"Jobs", false},
            {"AdvancedResearch", false},
            {"NotificationSystems", false},
            {"ArtificialFriends", false},
            {"BasicRefinement", false},
            {"RefinedObjects", false},
            {"Smelting", false},
            {"HighTempForging", false},
            {"TemperatureModulation", false},
            {"HVAC", false},
            {"LiquidTemperature", false},
            {"LogicControl", false},
            {"GenericSensors", false},
            {"LogicCircuits", false},
            {"ParallelAutomation", false},
            {"DupeTrafficControl", false},
            {"SkyDetectors", false},
            {"TravelTubes", false},
            {"SmartStorage", false},
            {"SolidTransport", false},
            {"SolidManagement", false},
            {"BasicRocketry", false},
            {"Jetpacks", false }
        };
    }
}
