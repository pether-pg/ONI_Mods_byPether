using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Hunger
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = HungerGerms.ID,
                sickness_id = HungerSickness.ID,
                exposure_threshold = 1,
                excluded_traits = new List<string>() { },
                base_resistance = 2,
                excluded_effects = new List<string>()
                    {
                      HungerSickness.RECOVERY_ID,
                      MegaFeastConfig.EffectID
                    }
            };
        }

        [HarmonyPatch(typeof(SapTreeConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class SapTreeConfig_CreatePrefab_Patch
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
