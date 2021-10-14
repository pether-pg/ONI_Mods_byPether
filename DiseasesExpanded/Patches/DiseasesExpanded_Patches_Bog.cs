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
                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)BogInsects.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = BogInsects.ID;
            }
        }

        [HarmonyPatch(typeof(ModifierSet))]
        [HarmonyPatch("LoadTraits")]
        public static class ModifierSet_LoadTraits_Patch
        {
            public static void Prefix()
            {
                TUNING.TRAITS.TRAIT_CREATORS.Add(TraitUtil.CreateNamedTrait(InsectAllergies.ID, (string)STRINGS.TRAITS.INSECTALLERGIES.NAME, (string)STRINGS.TRAITS.INSECTALLERGIES.NAME));
            }
        }

        [HarmonyPatch(typeof(MinionStartingStats))]
        [HarmonyPatch("GenerateTraits")]
        public static class MinionStartingStats_GenerateTraits_Patch
        {
            private static bool TraitAdded = false;

            public static void Prefix()
            {
                if(!TraitAdded)
                {
                    DUPLICANTSTATS.BADTRAITS.Add(InsectAllergies.GetTrait());
                    TraitAdded = true;
                }
            }
        }
    }
}
