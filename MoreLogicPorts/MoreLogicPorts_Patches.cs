using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System;
using System.Reflection;

namespace MoreLogicPorts
{
    public class MoreLogicPorts_Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            static bool Patched = false;

            public static void Prefix()
            {
                if (Patched)
                    return;


                Harmony harmony = new Harmony("pether-pg.MoreLogicPorts");
                Debug.Log($"{ModInfo.Namespace}: Starting manual patching");
                
                foreach(Type config in LogPorts.ConfigsToAddPorts())
                {
                    MethodInfo origDef = config.GetMethod("CreateBuildingDef");
                    MethodInfo patchDef = typeof(CreateBuildingDef_Patch).GetMethod("Postfix");

                    MethodInfo origConf = config.GetMethod("ConfigureBuildingTemplate");
                    MethodInfo patchConf = typeof(ConfigureBuildingTemplate_Patch).GetMethod("Postfix");

                    if (origDef != null && patchDef != null && origConf != null && patchConf != null)
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
