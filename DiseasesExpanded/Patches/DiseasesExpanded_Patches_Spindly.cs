using UnityEngine;
using HarmonyLib;
using Klei.AI;
using System.Linq;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Spindly
    {
        public const string SPINDLY_PLANTS_EFFECT_ID = "SpindlyPlantEffect";

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
                if (__instance == null || __instance.sleepy == null)
                    return;

                __instance.sleepy.Enter("Espresso Check", (smi => 
                    {
                        if (smi != null && smi.gameObject != null && HasEsspressoEffect(smi.gameObject))
                            smi.GoTo(__instance.idle);
                    }
                ));
            }

            private static bool HasEsspressoEffect(GameObject go)
            {
                if (go == null) return false;
                string EffectID = "RecentlyRecDrink"; // from EspressoMachine.cs
                Effects effects = go.GetComponent<Effects>();
                return (effects != null && effects.HasEffect(EffectID));
            }
        }

        [HarmonyPatch(typeof(WormPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class SapTreeConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.SleepingCurse.IncludeDisease)
                    return;

                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)SpindlyGerms.ID);
                def.emitFrequency = 0;
                def.averageEmitPerSecond = 0;
                def.singleEmitQuantity = 0;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = SpindlyGerms.ID;
            }
        }

        [HarmonyPatch(typeof(Harvestable))]
        [HarmonyPatch("OnCompleteWork")]
        public static class Harvestable_OnCompleteWork_Patch
        {
            public static void Postfix(Harvestable __instance, Worker worker)
            {
                if (!Settings.Instance.SleepingCurse.IncludeDisease)
                    return;

                if (!CausesCurse(__instance))
                    return;

                if (HasConflictingTraits(worker) || IsRecentlyRecovered(worker))
                    return;

                if (SuitWearing.IsWearingAtmoSuit(worker.gameObject) || SuitWearing.IsWearingLeadSuit(worker.gameObject))
                    return;
                
                float randomRoll = UnityEngine.Random.Range(0.0f, 100.0f);
                if (GetInfectionChance(worker) > randomRoll)
                    TryInfect(worker);
            }

            private static bool CausesCurse(Harvestable harvestable)
            {
                if (harvestable == null || harvestable.gameObject == null)
                    return false;

                if (harvestable.gameObject.name.Contains(WormPlantConfig.ID) && !harvestable.gameObject.name.Contains(SuperWormPlantConfig.ID))
                    return true;

                Effects effects = harvestable.gameObject.GetComponent<Effects>();
                if (effects == null)
                    return false;

                if (effects.HasEffect(SPINDLY_PLANTS_EFFECT_ID))
                    return true;

                return false;
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

            private static bool IsRecentlyRecovered(Worker worker)
            {
                if (worker == null || worker.gameObject == null)
                    return false;

                Effects effects = worker.gameObject.GetComponent<Effects>();
                return (effects != null && effects.HasEffect(SpindlySickness.RECOVERY_ID));
            }

            private static float GetInfectionChance(Worker worker)
            {
                float skill = GetBotanicSkillValue(worker);
                float scale = Settings.Instance.RebalanceForDiseasesRestored ? 2 : 4;
                return (100 - (skill * scale)) / 5.0f;
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
