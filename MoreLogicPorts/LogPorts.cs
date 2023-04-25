using UnityEngine;
using System;
using System.Collections.Generic;

namespace MoreLogicPorts
{
    class LogPorts
    {
        public static List<Type> ConfigsToAddPorts()
        {
            List<Type> result = new List<Type>()
            {
                typeof(LiquidPumpingStationConfig), // Pitcher Pump
                typeof(BottleEmptierConfig),        // Bottle Emptier
                typeof(BottleEmptierGasConfig),     // Canister Emptier
                typeof(GasBottlerConfig),           // Canister Filler
                typeof(IceCooledFanConfig),         // Ice-E Fan
                typeof(CreatureDeliveryPointConfig) // Critter Drop-Off
            };

            if(!ModInfo.IsCheckpointAutomationActive)
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
                def.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
        }

        public static bool ContainsLogicOperationalPort(BuildingDef def)
        {
            if (def.LogicInputPorts == null || def.LogicInputPorts.Count == 0)
                return false;

            foreach (LogicPorts.Port port in def.LogicInputPorts)
                if (port.id == LogicOperationalController.PORT_ID)
                    return true;

            return false;
        }
    }
}
