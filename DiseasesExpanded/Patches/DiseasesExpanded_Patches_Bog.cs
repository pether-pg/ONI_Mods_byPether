using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using Klei.AI;
using TUNING;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Bog
    {
        [HarmonyPatch(typeof(SwampHarvestPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class SwampHarvestPlantConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.BogInsects.IncludeDisease)
                    return;

                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)BogInsects.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = BogInsects.ID;
            }
        }
    }
}
