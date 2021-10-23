using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace CombinedConduitDisplay
{
    public class CombinedConduitDisplay_Patches_SolidConduit
    {
        public static bool IsLayerOn = false;

        [HarmonyPatch(typeof(OverlayModes.SolidConveyor))]
        [HarmonyPatch("Disable")]
        public class SolidConveyor_Disable_Patch
        {
            public static void Prefix()
            {
                foreach (SaveLoadRoot layerTarget in ZAxis.InitialZValues.Keys)
                    if (!((UnityEngine.Object)layerTarget == (UnityEngine.Object)null))
                        ZAxis.RestoreInitialZAxis(layerTarget);

                ZAxis.InitialZValues.Clear();
                IsLayerOn = false;
            }
        }

        [HarmonyPatch(typeof(OverlayModes.SolidConveyor))]
        [HarmonyPatch("Update")]
        public class SolidConveyor_Update_Patch
        {
            public static void Postfix(ref OverlayModes.SolidConveyor __instance)
            {
                IsLayerOn = true;
                HashSet<SaveLoadRoot> layerTargets = Traverse.Create(__instance).Field("layerTargets")?.GetValue<HashSet<SaveLoadRoot>>();
                if (layerTargets == null)
                    return;

                foreach (SaveLoadRoot layerTarget in layerTargets)
                    if (!((UnityEngine.Object)layerTarget == (UnityEngine.Object)null))
                        ZAxis.ModifyVectorZAxis(layerTarget);
            }
        }

        [HarmonyPatch(typeof(SolidConduitInboxConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidConduitInboxConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidVentConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidVentConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidConduitBridgeConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidConduitBridgeConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidTransferArmConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidTransferArmConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidFilterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidFilterConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidConduitTemperatureSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidConduitTemperatureSensorConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidConduitElementSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidConduitElementSensorConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidConduitDiseaseSensorConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidConduitDiseaseSensorConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidLogicValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidLogicValveConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidConduitOutboxConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidConduitOutboxConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }

        [HarmonyPatch(typeof(SolidLimitValveConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public class SolidLimitValveConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }
    }
}
