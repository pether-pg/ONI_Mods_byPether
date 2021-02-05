using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterplanarAutomation
{
    class WarpBridgeData
    {
        public static readonly CellOffset LogicPortInOffset = new CellOffset(-1, 1);
        public static readonly CellOffset LogicPortOutOffset = new CellOffset(2, 1);

        private static readonly string GreenSignal = (string)STRINGS.UI.FormatAsAutomationState("Green Signal", STRINGS.UI.AutomationState.Active);
        private static readonly string RedSignal = (string)STRINGS.UI.FormatAsAutomationState("Red Signal", STRINGS.UI.AutomationState.Standby);

        public static readonly string OutId = "LogicRibbonWarpBridgePortOutput";
        public static readonly string OutDescription = "Incomming Signal";
        public static readonly string OutActive = $"Sends a {GreenSignal} if such a signal was sent from another asteroid.";
        public static readonly string OutInactive = $"If no signal was transmited, sends {RedSignal}.";

        public static readonly string InId = "LogicRibbonWarpBridgePortInput";
        public static readonly string InDescription = "Transmited Signal";
        public static readonly string InActive = $"Sends a {GreenSignal} to be received on another asteroid.";
        public static readonly string InInactive = $"If no signal is being transmited, sends a {RedSignal}.";

        public static void AddOutputPort(ref BuildingDef result)
        {
            if (result.LogicOutputPorts == null)
                result.LogicOutputPorts = new List<LogicPorts.Port>();
            result.LogicOutputPorts.Add(LogicPorts.Port.RibbonOutputPort((HashedString)WarpBridgeData.OutId,
                                                                    WarpBridgeData.LogicPortOutOffset,
                                                                    WarpBridgeData.OutDescription,
                                                                    WarpBridgeData.OutActive,
                                                                    WarpBridgeData.OutInactive));

        }

        public static void AddInputPort(ref BuildingDef result)
        {
            if (result.LogicInputPorts == null)
                result.LogicInputPorts = new List<LogicPorts.Port>();
            result.LogicInputPorts.Add(LogicPorts.Port.RibbonInputPort((HashedString)WarpBridgeData.InId,
                                                                    WarpBridgeData.LogicPortInOffset,
                                                                    WarpBridgeData.InDescription,
                                                                    WarpBridgeData.InActive,
                                                                    WarpBridgeData.InInactive));

        }
    }
}
