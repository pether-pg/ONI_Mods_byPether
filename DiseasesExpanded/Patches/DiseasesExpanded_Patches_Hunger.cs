using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

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
                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)HungerGerms.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = HungerGerms.ID;
            }
        }
    }
}
