using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Frost
    {
        [HarmonyPatch(typeof(ColdBreather.States))]
        [HarmonyPatch("InitializeStates")]
        public static class ColdBreatherStates_InitializeStates_Patch
        {
            public static void Postfix(ColdBreather.States __instance)
            {
                __instance.dead.ToggleTag(GameTags.PreventEmittingDisease);
                __instance.alive.wilting.ToggleTag(GameTags.PreventEmittingDisease);
            }
        }

        [HarmonyPatch(typeof(ColdBreatherConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class ColdBreatherConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.FrostPox.IncludeDisease)
                    return;

                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)FrostShards.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = FrostShards.ID;
            }
        }

        [HarmonyPatch(typeof(SaunaWorkable))]
        [HarmonyPatch("OnCompleteWork")]
        public static class SaunaWorkables_OnCompleteWork_Patch
        {
            public static void Postfix(WorkerBase worker)
            {
                Klei.AI.Sicknesses sicknesses = worker.GetSicknesses();
                string curedSickness = FrostSickness.ID;
                SicknessInstance sicknessInstance = sicknesses.Get(curedSickness);
                if (sicknessInstance != null)
                {
                    Game.Instance.savedInfo.curedDisease = true;
                    sicknessInstance.Cure();
                }
            }
        }

        [HarmonyPatch(typeof(HotTubWorkable))]
        [HarmonyPatch("OnCompleteWork")]
        public static class HotTubWorkable_OnCompleteWork_Patch
        {
            public static void Postfix(WorkerBase worker)
            {
                Klei.AI.Sicknesses sicknesses = worker.GetSicknesses();
                string curedSickness = FrostSickness.ID;
                SicknessInstance sicknessInstance = sicknesses.Get(curedSickness);
                if (sicknessInstance != null)
                {
                    Game.Instance.savedInfo.curedDisease = true;
                    sicknessInstance.Cure();
                }
            }
        }
    }
}
