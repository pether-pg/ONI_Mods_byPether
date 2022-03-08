using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace CombinedConduitDisplay
{
    class CombinedConduitDisplay_Patches_LogicWires
    {
        [HarmonyPatch(typeof(LogicSwitchConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicSwitch_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicDuplicantSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicDuplicantSensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicPressureSensorGasConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicPressureSensorGas_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicPressureSensorLiquidConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicPressureSensorLiquid_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicTemperatureSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicTemperatureSensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicWattageSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicWattageSensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicTimeOfDaySensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicTimeOfDaySensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicTimerSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicTimerSensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicCritterCountSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicCritterCountSensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicDiseaseSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicDiseaseSensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicElementSensorGasConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicElementSensorGas_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicElementSensorLiquidConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicElementSensorLiquid_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicRadiationSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicRadiationSensor_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicHEPSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicHEPSensor_ConfigureBuildingTemplate_Patch
            {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicCounterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicCounter_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicCounter))]
        [HarmonyPatch("OnSpawn")]
        public class LogicCounter_OnSpawn_Patch
        {
            public static void Postfix(LogicCounter __instance)
            {
                MeterController meter = Traverse.Create(__instance).Field("meter").GetValue<MeterController>();
                if (meter == null)
                    return;
                Vector3 position = meter.gameObject.transform.position;
                position.z = Grid.GetLayerZ(Grid.SceneLayer.LogicGates) - 0.1f;
                meter.gameObject.transform.SetPosition(position);

                __instance.FindOrAddComponent<FollowParentsZ>();
            }
        }

        [HarmonyPatch(typeof(LogicAlarmConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicAlarm_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicHammerConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicHammer_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicInterasteroidSenderConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicInterasteroidSender_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicInterasteroidReceiverConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicInterasteroidReceiver_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicRibbonReaderConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicRibbonReader_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(LogicRibbonWriterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class LogicRibbonWriter_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(FloorSwitchConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class FloorSwitch_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }
    }
}
