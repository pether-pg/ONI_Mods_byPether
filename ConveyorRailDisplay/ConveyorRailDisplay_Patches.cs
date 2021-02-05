using Harmony;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace ConveyorRailDisplay
{
    public class ConveyorRailDisplay_Patches
    {
        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
            }
        }

        public static bool IsLayerOn = false;

        public static Dictionary<SaveLoadRoot, float> InitialZValues = new Dictionary<SaveLoadRoot, float>();

        public static void RefreshKbacForLayerTarget(SaveLoadRoot layerTarget)
        {
            KBatchedAnimController kbac = layerTarget.GetComponent<KBatchedAnimController>();
            if ((UnityEngine.Object)kbac == (UnityEngine.Object)null)
                return;

            kbac.enabled = false;
            kbac.enabled = true;
        }

        public static void RestoreInitialZAxis(SaveLoadRoot layerTarget)
        {
            if (layerTarget == null)
                return;

            if (!InitialZValues.ContainsKey(layerTarget))
                return;

            Vector3 position = layerTarget.transform.GetPosition();
            position.z = InitialZValues[layerTarget];

            layerTarget.transform.SetPosition(position);
            RefreshKbacForLayerTarget(layerTarget);
        }

        public static void ModifyVectorZAxis(SaveLoadRoot layerTarget)
        {
            if (layerTarget == null)
                return;

            SolidConduit component = layerTarget.GetComponent<SolidConduit>();
            if (!(UnityEngine.Object)component != (UnityEngine.Object)null)
                return;

            Vector3 position = layerTarget.transform.GetPosition();
            float desired = Grid.GetLayerZ(Grid.SceneLayer.Move);

            if (position.z != desired)
            {
                if (!InitialZValues.ContainsKey(layerTarget))
                    InitialZValues.Add(layerTarget, position.z);

                position.z = desired;
                layerTarget.transform.SetPosition(position);
                RefreshKbacForLayerTarget(layerTarget);
            }
        }

        [HarmonyPatch(typeof(OverlayModes.SolidConveyor))]
        [HarmonyPatch("Disable")]
        public class SolidConveyor_Disable_Patch
        {
            public static void Prefix()
            {
                foreach(SaveLoadRoot layerTarget in InitialZValues.Keys)
                    if (!((UnityEngine.Object)layerTarget == (UnityEngine.Object)null))
                        RestoreInitialZAxis(layerTarget);

                InitialZValues.Clear();
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
                    if (!((UnityEngine.Object) layerTarget == (UnityEngine.Object) null))
                        ModifyVectorZAxis(layerTarget);
            }
        } 

        [HarmonyPatch(typeof(Grid))]
        [HarmonyPatch("GetLayerZ")]
        public class Grid_GetLayerZ_Patch
        {
            public static void Postfix(Grid.SceneLayer layer, ref float __result)
            {
                __result = (IsLayerOn && layer == Grid.SceneLayer.SolidConduitContents) ?
                            Grid.GetLayerZ(Grid.SceneLayer.Front)
                            : __result;
            }
        }        
    }
}
