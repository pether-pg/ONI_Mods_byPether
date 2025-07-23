using System;
using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

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
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

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
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

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

        [HarmonyPatch(typeof(UseSolidLubricantChore))]
        [HarmonyPatch(nameof(UseSolidLubricantChore.ConsumeLubricant))]
        public static class UseSolidLubricantChore_ConsumeLubricant_Patch
        {
            public static void Postfix(UseSolidLubricantChore.Instance smi)
            {
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

                Sicknesses sicknesses = smi.gameObject.GetSicknesses();
                if (sicknesses == null)
                    return;

                SicknessInstance sicknessInstance = sicknesses.Get(RustSickness_1.ID);
                if (sicknessInstance != null)
                {
                    Game.Instance.savedInfo.curedDisease = true;
                    sicknessInstance.Cure();
                }
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch(nameof(Db.Initialize))]
        public class Db_Initialize_Patch
        {
            static bool Patched = false;

            public static void Postfix()
            {
                if (!Settings.Instance.RustDust.IncludeDisease)
                    return;

                if (Db_Initialize_Patch.Patched)
                    return;

                Harmony harmony = new Harmony($"pether-pg.{ModInfo.Namespace}");
                Debug.Log($"{ModInfo.Namespace}: Trying to get BionicOilMonitor type...");
                Type oilMonitorType = Type.GetType("BionicOilMonitor, Assembly-CSharp", false);
                if (oilMonitorType == null)
                {
                    Debug.Log($"{ModInfo.Namespace}: Error - BionicOilMonitor type is null...");
                    return;
                }

                var originalWantsOilChange = oilMonitorType.GetMethod(nameof(BionicOilMonitor.WantsOilChange));
                var postfixWantsOilChange = typeof(BionicOilMonitor_WantsOilChange_Patch)?.GetMethod("Postfix");

                if (originalWantsOilChange == null || postfixWantsOilChange == null)
                    Debug.Log($"{ModInfo.Namespace}: Error - unable to patch BionicOilMonitor.WantsOilChange - at least one method is null...");
                else
                {
                    harmony.Patch(originalWantsOilChange, null, new HarmonyMethod(postfixWantsOilChange));
                    Debug.Log($"{ModInfo.Namespace}: BionicOilMonitor_WantsOilChange_Patch - manual patching completed");
                }

                var originalHasDecentAmountOfOil = oilMonitorType.GetMethod(nameof(BionicOilMonitor.HasDecentAmountOfOil));
                var postfixHasDecentAmountOfOil = typeof(BionicOilMonitor_HasDecentAmountOfOil_Patch)?.GetMethod("Postfix");

                if (originalHasDecentAmountOfOil == null || postfixHasDecentAmountOfOil == null)
                    Debug.Log($"{ModInfo.Namespace}: Error - unable to patch BionicOilMonitor.HasDecentAmountOfOil - at least one method is null...");
                else
                {
                    harmony.Patch(originalHasDecentAmountOfOil, null, new HarmonyMethod(postfixHasDecentAmountOfOil));
                    Debug.Log($"{ModInfo.Namespace}: BionicOilMonitor_HasDecentAmountOfOil_Patch - manual patching completed");
                }    

                Db_Initialize_Patch.Patched = true;
            }
        }

        //[HarmonyPatch(typeof(BionicOilMonitor))]
        //[HarmonyPatch(nameof(BionicOilMonitor.WantsOilChange))]
        public static class BionicOilMonitor_WantsOilChange_Patch
        {
            public static void Postfix(BionicOilMonitor.Instance smi, ref bool __result)
            {
                if (SicknessHelper.IsSickWith(smi.gameObject, RustSickness_1.ID))
                    __result = true;
            }
        }

        //[HarmonyPatch(typeof(BionicOilMonitor))]
        //[HarmonyPatch(nameof(BionicOilMonitor.HasDecentAmountOfOil))]
        public static class BionicOilMonitor_HasDecentAmountOfOil_Patch
        {
            public static void Postfix(BionicOilMonitor.Instance smi, ref bool __result)
            {
                if (SicknessHelper.IsSickWith(smi.gameObject, RustSickness_1.ID))
                    __result = false;
            }
        }

    }
}
