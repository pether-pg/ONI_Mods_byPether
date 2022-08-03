using HarmonyLib;
using Klei.AI;
using TUNING;
using UnityEngine;

namespace RadiationRebalanced
{
    class RadiationRebalance_Patches_Eater
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public static class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                float radsPerSecond = Settings.Instance.RadiationEater.ConsumedRadsPerCycle / 600;
                float kCalsGranted = 1000 * Settings.Instance.RadiationEater.KCalsGrantedPerCycle;
                float kCalsPerRad = kCalsGranted / Settings.Instance.RadiationEater.ConsumedRadsPerCycle;

                TRAITS.RADIATION_EATER_RECOVERY = -radsPerSecond;
                TRAITS.RADS_TO_CALS = kCalsPerRad;

                Debug.Log($"{ModInfo.Namespace}: TUNING.TRAITS modified: RADIATION_EATER_RECOVERY = {-radsPerSecond}, RADS_TO_CALS = {kCalsPerRad}");
            }
        }

        [HarmonyPatch(typeof(RadiationEater.StatesInstance))]
        [HarmonyPatch("OnEatRads")]
        public static class RadiationEaterStatesInstance_OnEatRads_Patch
        {
            public static void Prefix(RadiationEater.StatesInstance __instance, float radsEaten)
            {
                if (__instance == null || __instance.gameObject == null)
                    return;

                RadiationMonitor.Instance smi = __instance.gameObject.GetSMI<RadiationMonitor.Instance>();
                if (smi == null)
                    return;

                // RadiationRecovery triggers each second, no matter if there was radiation to recover from
                // as a result, RadiationEater creates kCals even without absorbed radiation
                // code below is to mitigate the bug (known issue - it won't register final second of radiation recovery)
                /**/
                float radsAbsorbed = smi.sm.radiationExposure.Get(smi);
                if (radsAbsorbed <= 0)
                    radsEaten = -radsAbsorbed;                

                float delta = Mathf.Abs(radsEaten) * TRAITS.RADS_TO_CALS;
                ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, delta, "Radiation Eating", __instance.gameObject.GetProperName());
                ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, -delta, "Radiation Eating", __instance.gameObject.GetProperName());
            }
        }

        [HarmonyPatch(typeof(MinionStartingStats))]
        [HarmonyPatch("GenerateTraits")]
        public static class MinionStartingStats_GenerateTraits_Patch
        {
            private static bool TraitAdded = false;

            public static void Prefix()
            {
                if (TraitAdded)
                    return;

                for(int i=0; i<DUPLICANTSTATS.GOODTRAITS.Count; i++)
                    if(DUPLICANTSTATS.GOODTRAITS[i].id == "RadiationEater")
                    {
                        DUPLICANTSTATS.TraitVal eater = DUPLICANTSTATS.GOODTRAITS[i];
                        eater.rarity = Settings.Instance.RadiationEater.TraitRarity;
                        DUPLICANTSTATS.GOODTRAITS.RemoveAt(i);
                        DUPLICANTSTATS.GOODTRAITS.Add(eater);
                        TraitAdded = true;
                        break;
                    }

                for(int i=0; i<DUPLICANTSTATS.GOODTRAITS.Count; i++)
                    if(DUPLICANTSTATS.GOODTRAITS[i].id == "GlowStick")
                    {
                        DUPLICANTSTATS.TraitVal eater = DUPLICANTSTATS.GOODTRAITS[i];
                        eater.rarity = Settings.Instance.RadiationEater.TraitRarity;
                        DUPLICANTSTATS.GOODTRAITS.RemoveAt(i);
                        DUPLICANTSTATS.GOODTRAITS.Add(eater);
                        TraitAdded = true;
                        break;
                    }

                TraitAdded = true;

                /*
                for (int i = 0; i < DUPLICANTSTATS.GOODTRAITS.Count; i++)
                    if (DUPLICANTSTATS.GOODTRAITS[i].id == "RadiationEater")
                        Debug.Log($"RadiationEater rarity = {DUPLICANTSTATS.GOODTRAITS[i].rarity}");
                */
            }
        }
    }
}
