using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Bog
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = BogInsects.ID,
                sickness_id = BogSickness.ID,
                exposure_threshold = 1,
                excluded_traits = new List<string>() { },
                base_resistance = 2,
                excluded_effects = new List<string>()
                    {
                      BogSickness.RECOVERY_ID
                    }
            };
        }

        [HarmonyPatch(typeof(SwampHarvestPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class SwampHarvestPlantConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
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
