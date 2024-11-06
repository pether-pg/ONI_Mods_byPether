using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Medicine
    {
        [HarmonyPatch(typeof(MedicinalPillWorkable))]
        [HarmonyPatch("OnCompleteWork")]
        public static class MedicinalPillWorkable_OnCompleteWork_Patch
        {
            public static void Postfix(MedicinalPillWorkable __instance, Worker worker)
            {
                if (__instance.pill.info.effect != TestSampleConfig.EFFECT_ID)
                    return;

                TestSampleConfig.OnEatComplete(worker.gameObject);
            }
        }

        [HarmonyPatch(typeof(MedicinalPillWorkable))]
        [HarmonyPatch("CanBeTakenBy")]
        public static class MedicinalPillWorkable_CanBeTakenBy_Patch
        {
            public static void Postfix(MedicinalPillWorkable __instance, GameObject consumer, ref bool __result)
            {
                if (__instance.pill.info.effect != TestSampleConfig.EFFECT_ID)
                    return;

                if (!HasGermInfection(consumer))
                    __result = false;
            }

            public static bool HasGermInfection(GameObject worker)
            {
                Sicknesses sicknesses = worker.gameObject.GetSicknesses();
                if (sicknesses == null)
                    return false;

                foreach (SicknessInstance si in sicknesses)
                    foreach (ExposureType et in TUNING.GERM_EXPOSURE.TYPES)
                        if (et.sickness_id == si.Sickness.id && !string.IsNullOrEmpty(et.germ_id))
                            return true;

                RadiationMonitor.Instance smi = worker.GetSMI<RadiationMonitor.Instance>();

                if (smi != null && smi.sm.isSick.Get(smi))
                    return true;

                return false;
            }
        }

        [HarmonyPatch(typeof(Immigration), "ConfigureCarePackages")]
        public static class Immigration_ConfigureCarePackages_Patch
        {
            public static Dictionary<string, float> Printables = new Dictionary<string, float>()
            {
                { HappyPillConfig.ID, 5 },
                { TestSampleConfig.ID, 5 },
                { AntihistamineBoosterConfig.ID, 3 },
                { SunburnCureConfig.ID, 3 },
                { GasCureConfig.ID, 3 },
                { AlienSicknessCureConfig.ID, 1 },
                { SerumSuperConfig.ID, 1 },
                { SerumTummyConfig.ID, 1 },
                { SerumDeepBreathConfig.ID, 1 },
                { NanobotBottleConfig.ID, 1 }
            };

            public static Dictionary<string, float> DlcPrintables = new Dictionary<string, float>()
            {
                { MudMaskConfig.ID, 5 },
                { SerumYummyConfig.ID, 1 },
                { RadShotConfig.ID, 1 },
                { SapShotConfig.ID, 1 }
            };

            public static void Postfix(ref Immigration __instance)
            {
                Traverse traverse = Traverse.Create(__instance).Field("carePackages");
                List<CarePackageInfo> list = traverse.GetValue<List<CarePackageInfo>>();
                foreach (string id in Printables.Keys)
                    list.Add(new CarePackageInfo(id, Printables[id], () => DiscoveredResources.Instance.IsDiscovered(id)));
                if(DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID))
                    foreach (string id in DlcPrintables.Keys)
                        list.Add(new CarePackageInfo(id, DlcPrintables[id], () => DiscoveredResources.Instance.IsDiscovered(id)));
                traverse.SetValue(list);
            }
        }
    }
}
