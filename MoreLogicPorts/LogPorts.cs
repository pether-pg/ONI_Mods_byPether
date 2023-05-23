using UnityEngine;
using System;
using System.Collections.Generic;

namespace MoreLogicPorts
{
    class LogPorts
    {
        public const string BUILDING_DEF_NAME = nameof(IBuildingConfig.CreateBuildingDef);
        public const string DO_POST_CONFIG_NAME = nameof(IBuildingConfig.DoPostConfigureComplete);
        public const string CONFIG_TEMPLATE_NAME = nameof(IBuildingConfig.ConfigureBuildingTemplate);

        public static Dictionary<Type, string> ConfigsToAddPorts()
        {

            Dictionary<Type, string> Configs = new Dictionary<Type, string>();
            foreach (Type t in AffectedTypes())
                Configs.Add(t, CONFIG_TEMPLATE_NAME);

            if (Configs.ContainsKey(typeof(MassiveHeatSinkConfig)))
                Configs[typeof(MassiveHeatSinkConfig)] = DO_POST_CONFIG_NAME;

            return Configs;
        }

        private static List<Type> AffectedTypes()
        {
            List<Type> result = new List<Type>()
            {
                typeof(FossilDigSiteConfig),        // Lost Specimen
                typeof(LiquidReservoirConfig),      // Liquid Reservoir
                typeof(GasReservoirConfig),         // Gas Reservoir
                typeof(MassiveHeatSinkConfig),      // AETN
                typeof(LiquidPumpingStationConfig), // Pitcher Pump
                typeof(BottleEmptierConfig),        // Bottle Emptier
                typeof(BottleEmptierGasConfig),     // Canister Emptier
                typeof(GasBottlerConfig),           // Canister Filler
                typeof(IceCooledFanConfig),         // Ice-E Fan
                typeof(CreatureDeliveryPointConfig) // Critter Drop-Off
            };

            // Manual Radbolt Generator
            if (DlcManager.IsExpansion1Active())
                result.Add(typeof(ManualHighEnergyParticleSpawnerConfig));

            // Checkpoints
            if (!ModInfo.IsCheckpointAutomationActive)
            {
                result.Add(typeof(JetSuitMarkerConfig));
                result.Add(typeof(LeadSuitMarkerConfig));
                result.Add(typeof(OxygenMaskMarkerConfig));
                result.Add(typeof(SuitMarkerConfig));
            }

            return result;
        }

        public static void AddBehaviourToGameObject(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
        }

        public static void AddPortToBiuldingDef(BuildingDef def)
        {
            if (!ContainsLogicOperationalPort(def))
                def.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(FindSuitablePortOffset(def));
        }

        private static bool ContainsLogicOperationalPort(BuildingDef def)
        {
            if (def.LogicInputPorts == null || def.LogicInputPorts.Count == 0)
                return false;

            foreach (LogicPorts.Port port in def.LogicInputPorts)
                if (port.id == LogicOperationalController.PORT_ID)
                    return true;

            return false;
        }

        private static CellOffset FindSuitablePortOffset(BuildingDef def)
        {
            List<int> has = new List<int>();

            if (def.LogicInputPorts != null)
                foreach (LogicPorts.Port port in def.LogicInputPorts)
                    has.Add(CellOffsetHash(port.cellOffset));

            if (def.LogicOutputPorts != null)
                foreach (LogicPorts.Port port in def.LogicOutputPorts)
                    has.Add(CellOffsetHash(port.cellOffset));

            int maxRange = Math.Max(def.HeightInCells, def.WidthInCells / 2);
            for(int range = 0; range <= maxRange; range++)
                for (int xCheck = range; xCheck >=0; xCheck--)
                    for (int yCheck = range; yCheck >= 0; yCheck--)
                        if (xCheck <= def.WidthInCells / 2 
                            && yCheck <= def.HeightInCells 
                            && !has.Contains(CellOffsetHash(new CellOffset(xCheck, yCheck))))
                            return new CellOffset(xCheck, yCheck);


            // Should never get to this line
            return new CellOffset(0, 0);
        }

        private static int CellOffsetHash(CellOffset offset) => offset.x * 100 + offset.y;
    }
}
