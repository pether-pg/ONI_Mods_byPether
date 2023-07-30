using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace MoreLogicPorts
{
    public class MoreLogicPorts_Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            static bool Patched = false;

            public static void Postfix()
            {
                if (Patched)
                    return;

                Harmony harmony = new Harmony($"pether-pg.{ModInfo.Namespace}");
                Debug.Log($"{ModInfo.Namespace}: Starting manual patching");

                MethodInfo patchDef = typeof(CreateBuildingDef_Patch).GetMethod(nameof(CreateBuildingDef_Patch.Postfix));
                MethodInfo patchConf = typeof(ConfigureBuildingTemplate_Patch).GetMethod(nameof(ConfigureBuildingTemplate_Patch.Postfix));
                if(patchDef == null || patchConf == null)
                {
                    Debug.Log($"{ModInfo.Namespace}: Could not find manual patch code, no changes can be applied");
                    return;
                }

                Dictionary<Type, string> ConfigsToPatch = LogPorts.ConfigsToAddPorts();
                foreach (Type config in ConfigsToPatch.Keys)
                {
                    if (!Settings.Instance.CanAddPort(config))
                        continue;

                    MethodInfo origDef = config.GetMethod(LogPorts.BUILDING_DEF_NAME);
                    MethodInfo origConf = config.GetMethod(ConfigsToPatch[config]);

                    if (origDef != null && origConf != null)
                    {
                        harmony.Patch(origDef, null, new HarmonyMethod(patchDef));
                        harmony.Patch(origConf, null, new HarmonyMethod(patchConf));
                    }
                    else
                        Debug.Log($"{ModInfo.Namespace}: Could not get methods to patch for {config}");
                }

                Patched = true;
            }
        }

        public class CreateBuildingDef_Patch
        {
            public static void Postfix(BuildingDef __result)
            {
                LogPorts.AddPortToBiuldingDef(__result);
            }
        }

        public class ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                LogPorts.AddBehaviourToGameObject(go);
            }
        }
    }
}
