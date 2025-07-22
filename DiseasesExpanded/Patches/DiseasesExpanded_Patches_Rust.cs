using System;
using HarmonyLib;
using UnityEngine;

namespace DiseasesExpanded.Patches
{
    class DiseasesExpanded_Patches_Rust
    {
        [HarmonyPatch(typeof(BionicMinionConfig))]
        [HarmonyPatch(nameof(BionicMinionConfig.CreatePrefab))]
        public static class BionicMinionConfig_CreatePrefab_Patch
        {
            public static void Postfix(GameObject __result)
            {
                __result.AddOrGet<RustSicknessHistory>();
            }
        }

        [HarmonyPatch(typeof(PuftBleachstoneConfig))]
        [HarmonyPatch(nameof(PuftBleachstoneConfig.CreatePuftBleachstone))]
        public static class PuftBleachstoneConfig_CreatePuftBleachstone_Patch
        {
            public static void Postfix(GameObject __result)
            {
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)RustGerms.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = RustGerms.ID;
            }
        }

        [HarmonyPatch(typeof(BasePuftConfig))]
        [HarmonyPatch(nameof(BasePuftConfig.SetupDiet))]
        [HarmonyPatch(new Type[] { typeof(GameObject), typeof(Tag), typeof(Tag), typeof(float), typeof(float), typeof(string), typeof(float), typeof(float) })]
        public static class BasePuftConfig_SetupDiet_Patch
        {
            public static void Prefix(Tag producedTag, ref string diseaseId, ref float diseasePerKgProduced)
            {
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

                if (producedTag == SimHashes.BleachStone.CreateTag())
                {
                    diseaseId = RustGerms.ID;
                    diseasePerKgProduced = 100000;
                }
            }
        }

        [HarmonyPatch(typeof(BasePrehistoricPacuConfig))]
        [HarmonyPatch(nameof(BasePrehistoricPacuConfig.CreatePrefab))]
        public static class BasePrehistoricPacuConfig_CreatePuftBleachstone_Patch
        {
            public static void Postfix(GameObject __result)
            {
                Diet.Info[] original = __result.AddOrGetDef<SolidConsumerMonitor.Def>().diet.infos;
                Diet.Info[] modified = new Diet.Info[original.Length];
                for (int i = 0; i < original.Length; i++)
                {
                    string germId = RustGerms.ID;
                    int germCount = 100000;
                    modified[i] = new Diet.Info(original[i].consumedTags, 
                                                original[i].producedElement, 
                                                original[i].caloriesPerKg, 
                                                original[i].producedConversionRate, 
                                                germId, 
                                                germCount, 
                                                original[i].produceSolidTile, 
                                                original[i].foodType, 
                                                original[i].emmitDiseaseOnCell, 
                                                original[i].eatAnims);
                }
                Diet diet = new Diet(modified);
                __result.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
                __result.AddOrGetDef<CreatureCalorieMonitor.Def>().diet = diet;
            }
        }
    }
}
