using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace ConveyorRailDisplay
{
    public class ConveyorRailDisplay_Patches
    {
        public static bool IsLayerOn = false;

        public static void TryPatchAll(Harmony harmony)
        {
            bool canPatch = true;

            MethodInfo patched1 = ManualPatching.GetMethodInfo(typeof(OverlayModes.SolidConveyor), "Disable");
            MethodInfo prefix1 = ManualPatching.GetMethodInfo(typeof(SolidConveyor_Disable_Patch), "Prefix");
            canPatch &= patched1 != null && prefix1 != null;

            MethodInfo patched2 = ManualPatching.GetMethodInfo(typeof(OverlayModes.SolidConveyor), "Update");
            MethodInfo postfix2 = ManualPatching.GetMethodInfo(typeof(SolidConveyor_Update_Patch), "Postfix");
            canPatch &= patched2 != null && postfix2 != null;

            MethodInfo commonTagPostfix = ManualPatching.GetMethodInfo(typeof(SolidConduitCommonConfig_DoPostConfigureComplete_Patch), "Postfix");
            canPatch &= commonTagPostfix != null;

            List<MethodInfo> SolidBuildingsPatchedMethods = new List<MethodInfo>();
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidConduitInboxConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidVentConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidConduitBridgeConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidTransferArmConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidFilterConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidConduitTemperatureSensorConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidConduitElementSensorConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidConduitDiseaseSensorConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidLogicValveConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidLogicValveConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidLimitValveConfig), "DoPostConfigureComplete"));
            SolidBuildingsPatchedMethods.Add(ManualPatching.GetMethodInfo(typeof(SolidConduitOutboxConfig), "DoPostConfigureComplete"));

            foreach (MethodInfo mi in SolidBuildingsPatchedMethods)
                canPatch &= mi != null;

            if (!canPatch)
            {
                Debug.Log($"{ModInfo.Namespace}: This mod is unable to patch specified methods, at least one method is null... The mod will not work.");
                return;
            }

            ManualPatching.ManualPatch(harmony, patched1, prefix1, null);
            ManualPatching.ManualPatch(harmony, patched2, null, postfix2);
            foreach(MethodInfo mi in SolidBuildingsPatchedMethods)
                ManualPatching.ManualPatch(harmony, mi, null, commonTagPostfix);

            Debug.Log($"{ModInfo.Namespace}: Manual patching finished, the mod should work correctly now.");
        }

        public class SolidConveyor_Disable_Patch
        {
            public static void Prefix()
            {
                foreach(SaveLoadRoot layerTarget in ZAxis.InitialZValues.Keys)
                    if (!((UnityEngine.Object)layerTarget == (UnityEngine.Object)null))
                        ZAxis.RestoreInitialZAxis(layerTarget);

                ZAxis.InitialZValues.Clear();
                IsLayerOn = false;
            }
        }

        public class SolidConveyor_Update_Patch
        {
            public static void Postfix(ref OverlayModes.SolidConveyor __instance)
            {
                IsLayerOn = true;
                HashSet<SaveLoadRoot> layerTargets = Traverse.Create(__instance).Field("layerTargets")?.GetValue<HashSet<SaveLoadRoot>>();
                if (layerTargets == null)
                    return;

                foreach (SaveLoadRoot layerTarget in layerTargets)
                    if (!((UnityEngine.Object) layerTarget == (UnityEngine.Object) null))
                        ZAxis.ModifyVectorZAxis(layerTarget);
            }
        }

        public class SolidConduitCommonConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                ZAxis.ForceBehindTag(go);
            }
        }
    }
}
