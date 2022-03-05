using UnityEngine;
using HarmonyLib;
using Klei.AI;
using System.Linq;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Spindly
    {
        public static void UpdateNarcolepsyTimes()
        {
            TUNING.TRAITS.NARCOLEPSY_INTERVAL_MIN /= 2;
            TUNING.TRAITS.NARCOLEPSY_INTERVAL_MAX /= 2;
            TUNING.TRAITS.NARCOLEPSY_SLEEPDURATION_MIN *= 2;
            TUNING.TRAITS.NARCOLEPSY_SLEEPDURATION_MAX *= 2;
        }


        [HarmonyPatch(typeof(Narcolepsy.States))]
        [HarmonyPatch("InitializeStates")]
        public static class Narcolepsy_InitializeStates_Patch
        {
            public static void Postfix(Narcolepsy.States __instance)
            {
                __instance.sleepy.Enter("Espresso Check", (smi => 
                    {
                        if (HasEsspressoEffect(smi.gameObject))
                            smi.GoTo(__instance.idle);
                    }
                ));
            }

            private static bool HasEsspressoEffect(GameObject go)
            {
                string EffectID = "RecentlyRecDrink"; // from EspressoMachine.cs
                Klei.AI.Effects effects = go.GetComponent<Klei.AI.Effects>();
                return (effects != null && effects.HasEffect(EffectID));
            }
        }


        [HarmonyPatch(typeof(Harvestable))]
        [HarmonyPatch("OnCompleteWork")]
        public static class Harvestable_OnCompleteWork_Patch
        {
            public static void Postfix(Harvestable __instance, Worker worker)
            {
                if (!__instance.gameObject.name.Contains(WormPlantConfig.ID) && false)
                    return;

                if (HasConflictingTraits(worker))
                    return;

                if (SuitWearing.IsWearingAtmoSuit(worker.gameObject) || SuitWearing.IsWearingLeadSuit(worker.gameObject))
                    return;
                
                float randomRoll = UnityEngine.Random.Range(0.0f, 100.0f);
                if (GetInfectionChance(worker) > randomRoll)
                    TryInfect(worker);
            }

            private static bool HasConflictingTraits(Worker worker)
            {
                List<string> conflictingIds = new List<string>() { "Narcolepsy", "NightLight" };
                Traits traits = worker.gameObject.GetComponent<Traits>();
                if (traits != null)
                    foreach (string conflicting in conflictingIds)
                        if (traits.HasTrait(conflicting))
                            return true;

                return false;
            }

            private static float GetInfectionChance(Worker worker)
            {
                float skill = GetBotanicSkillValue(worker);
                float scale = Settings.Instance.RebalanceForDiseasesRestored ? 2 : 4;
                return ((100 / scale ) - skill) * scale;
            }

            private static float GetBotanicSkillValue(Worker worker)
            {
                MinionModifiers modifiers = worker.gameObject.GetComponent<MinionModifiers>();
                if (modifiers == null)
                    return 0;

                AttributeInstance attributeInstance = modifiers.attributes.AttributeTable.Where(p => p.Name == "Agriculture").FirstOrDefault();
                if (attributeInstance == null)
                    return 0;

                float value = attributeInstance.GetTotalValue();
                return value;
            }

            private static void TryInfect(Worker worker)
            {
                Modifiers modifiers = worker.gameObject.GetComponent<Modifiers>();
                if (modifiers == null)
                    return;

                Sicknesses diseases = modifiers.GetSicknesses();
                if (diseases == null)
                    return;

                diseases.Infect(new SicknessExposureInfo(SpindlySickness.ID, STRINGS.DISEASES.SPINDLYCURSE.EXPOSURE_INFO));
            }
        }
    }
}
