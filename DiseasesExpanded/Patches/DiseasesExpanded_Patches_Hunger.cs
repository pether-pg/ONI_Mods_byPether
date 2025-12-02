using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Hunger
    {
        [HarmonyPatch(typeof(SapTreeConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class SapTreeConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.HungerGerms.IncludeDisease)
                    return;

                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)HungerGerms.ID);
                def.emitFrequency = 10f;
                def.averageEmitPerSecond = 100000;
                def.singleEmitQuantity = 1000000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = HungerGerms.ID;
            }
        }

        [HarmonyPatch(typeof(CritterTrapPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class CritterTrapPlantConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.HungerGerms.IncludeDisease)
                    return;

                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)HungerGerms.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = HungerGerms.ID;
            }
        }

        [HarmonyPatch(typeof(FlyTrapPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class FlyTrapPlantConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.HungerGerms.IncludeDisease)
                    return;

                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)HungerGerms.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = HungerGerms.ID;
            }
        }

        [HarmonyPatch(typeof(EntityTemplates))]
        [HarmonyPatch("ExtendEntityToWildCreature")]
        [HarmonyPatch(new Type[] { typeof(GameObject), typeof(int), typeof(bool) })]
        public static class EntityTemplates_ExtendEntityToWildCreature_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if(!__result.HasTag(GameTags.Creatures.Swimmer))
                    __result.AddOrGet<CritterSicknessMonitor>();
            }
        }

        [HarmonyPatch(typeof(RanchStation.Instance))]
        [HarmonyPatch(nameof(RanchStation.Instance.RanchCreature))]
        public static class RanchStationInstance_DoPostConfigureComplete_Patch
        {
            public static void Prefix(RanchStation.Instance __instance)
            {
                RanchedStates.Instance activeRanchable = Traverse.Create(__instance).Field("activeRanchable").GetValue<RanchedStates.Instance>() ;
                if (activeRanchable == null)
                    return;

                GameObject creature_go = activeRanchable.gameObject;
                Effects effects = creature_go.GetComponent<Effects>();
                if (effects == null || !effects.HasEffect(HungerSickness.CRITTER_EFFECT_ID))
                    return;

                RanchStation.Instance targetRanchStation = creature_go.GetSMI<RanchableMonitor.Instance>().TargetRanchStation;
                RancherChore.RancherChoreStates.Instance smi = targetRanchStation.GetSMI<RancherChore.RancherChoreStates.Instance>();
                float medicineLvl = targetRanchStation.GetSMI<RancherChore.RancherChoreStates.Instance>().sm.rancher.Get(smi).GetAttributes().Get(Db.Get().Attributes.Caring.Id).GetTotalValue();
                float reductionScale = 1.0f + medicineLvl * 0.1f;
                float constantScale = 2.0f;

                effects.Get(HungerSickness.CRITTER_EFFECT_ID).timeRemaining -= 600 * reductionScale * constantScale;
            }
        }
    }
}
