﻿using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Medicine
    {
        [HarmonyPatch(typeof(MedicinalPillWorkable))]
        [HarmonyPatch("OnCompleteWork")]
        public static class MedicinalPillWorkable_OnCompleteWork_Patch
        {
            public static void Postfix(MedicinalPillWorkable __instance, Worker worker)
            {
                if (__instance.pill.info.effect != TestSampleConfig.EFFECT_ID)
                    return;

                TestSampleConfig.OnEatComplete(worker.gameObject);
            }
        }

        [HarmonyPatch(typeof(MedicinalPillWorkable))]
        [HarmonyPatch("CanBeTakenBy")]
        public static class MedicinalPillWorkable_CanBeTakenBy_Patch
        {
            public static void Postfix(MedicinalPillWorkable __instance, GameObject consumer, ref bool __result)
            {
                if (__instance.pill.info.effect != TestSampleConfig.EFFECT_ID)
                    return;

                if (!HasGermInfection(consumer))
                    __result = false;
            }

            public static bool HasGermInfection(GameObject worker)
            {
                Sicknesses sicknesses = worker.gameObject.GetSicknesses();
                if (sicknesses == null)
                    return false;

                foreach (SicknessInstance si in sicknesses)
                    foreach (ExposureType et in TUNING.GERM_EXPOSURE.TYPES)
                        if (et.sickness_id == si.Sickness.id && !string.IsNullOrEmpty(et.germ_id))
                            return true;

                return false;
            }
        }

        [HarmonyPatch(typeof(Immigration), "ConfigureCarePackages")]
        public static class Immigration_ConfigureCarePackages_Patch
        {
            public static Dictionary<string, float> Printables = new Dictionary<string, float>()
            {
                { HappyPillConfig.ID, 5 },
                { TestSampleConfig.ID, 5 },
                { AntihistamineBoosterConfig.ID, 3 },
                { SunburnCureConfig.ID, 3 },
                { GasCureConfig.ID, 3 },
                { AlienSicknessCureConfig.ID, 1 },
                { SerumSuperConfig.ID, 1 },
                { SerumTummyConfig.ID, 1 },
                { SerumDeepBreathConfig.ID, 1 },
            };

            public static Dictionary<string, float> DlcPrintables = new Dictionary<string, float>()
            {
                { MudMaskConfig.ID, 5 },
                { SerumYummyConfig.ID, 1 },
                { RadShotConfig.ID, 1 },
                { SapShotConfig.ID, 1 }
            };

            public static void Postfix(ref Immigration __instance)
            {
                Traverse traverse = Traverse.Create(__instance).Field("carePackages");
                List<CarePackageInfo> list = traverse.GetValue<CarePackageInfo[]>().ToList<CarePackageInfo>();
                foreach (string id in Printables.Keys)
                    list.Add(new CarePackageInfo(id, Printables[id], () => DiscoveredResources.Instance.IsDiscovered(id)));
                if (DlcManager.IsExpansion1Active())
                    foreach (string id in DlcPrintables.Keys)
                        list.Add(new CarePackageInfo(id, DlcPrintables[id], () => DiscoveredResources.Instance.IsDiscovered(id)));
                traverse.SetValue(list.ToArray());
            }
        }
        [HarmonyPatch(typeof(CodexCache), "CollectYAMLEntries")]
        public class CodexEntryGenerator_GenerateSomeEntries_Patch
        {
            public static void Postfix()
            {
                var r = CodexEntryGenerator.GenerateCategoryEntry("MEDICINE", Strings.Get("STRINGS.UI.CODEX.CATEGORYNAMES.MEDICINE"),
                    GeneratePillsEntries(),
                    Def.GetUISprite("Apothecary").first
                );
                var temp = CodexCache.FindEntry("HOME");
                (temp as CategoryEntry)?.entriesInCategory.Add(r);
                CodexEntryGenerator.PopulateCategoryEntries(new List<CategoryEntry>() { r });
            }
        }

        private static void FixNameLinks(GameObject __result)
        {
            string id = __result.PrefabID().ToString();
            string name = __result.GetProperName();
            TagManager.Create(id, UI.FormatAsLink(name, id));
            __result.AddOrGet<KSelectable>().SetName(UI.FormatAsLink(name, id));
        }

        public static void GenerateTitleContainers(string title, string subtitls, List<ContentContainer> contentContainerList)
        {
            contentContainerList.Add(new ContentContainer(new List<ICodexWidget>()
                        {
                            new CodexText(title, CodexTextStyle.Title),
                            new CodexText() {stringKey = subtitls,
                            style = CodexTextStyle.Subtitle },
                            new CodexDividerLine()
                            }, ContentContainer.ContentLayout.Vertical));
        }

        public static void GenerateRecipesContainers(Tag tag, List<ContentContainer> contentContainerList)
        {
            List<ICodexWidget> content3 = new List<ICodexWidget>();
            List<ICodexWidget> content4 = new List<ICodexWidget>();
            foreach (ComplexRecipe recipe in ComplexRecipeManager.Get().recipes)
            {
                if (((IEnumerable<ComplexRecipe.RecipeElement>)recipe.ingredients).Any(i => i.material == tag))
                    content3.Add(new CodexRecipePanel(recipe));
                if (((IEnumerable<ComplexRecipe.RecipeElement>)recipe.results).Any(i => i.material == tag))
                    content4.Add(new CodexRecipePanel(recipe, true));
            }
            ContentContainer contents1 = new ContentContainer(content3, ContentContainer.ContentLayout.Vertical);
            ContentContainer contents2 = new ContentContainer(content4, ContentContainer.ContentLayout.Vertical);
            if (content3.Count > 0)
            {
                contentContainerList.Add(new ContentContainer(new List<ICodexWidget>()
                            {
                            new CodexSpacer(),
                            new CodexCollapsibleHeader(CODEX.HEADERS.ELEMENTCONSUMEDBY, contents1)
                            }, ContentContainer.ContentLayout.Vertical));
                contentContainerList.Add(contents1);
            }
            if (content4.Count > 0)
            {
                contentContainerList.Add(new ContentContainer(new List<ICodexWidget>()
                            {
                            new CodexSpacer(),
                            new CodexCollapsibleHeader(CODEX.HEADERS.ELEMENTPRODUCEDBY, contents2)
                            }, ContentContainer.ContentLayout.Vertical));
                contentContainerList.Add(contents2);
            }
        }

        public static Dictionary<string, CodexEntry> GeneratePillsEntries()
        {
            List<string> hideInCodex = new List<string>() { 
                //HungermsVaccineConfig.ID, 
                IntermediateRadPillConfig.ID,
                //AntihistamineBoosterConfig.ID
            };

            List<GameObject> prefabsWithComponent = Assets.GetPrefabsWithComponent<MedicinalPill>();
            Dictionary<string, CodexEntry> res = new Dictionary<string, CodexEntry>();

            foreach (GameObject go in prefabsWithComponent)
            {
                //MedicinalPill component = go.GetComponent<MedicinalPill>();

                if (!hideInCodex.Contains(go.PrefabID().ToString()))
                {
                    FixNameLinks(go);
                    List<ContentContainer> contentContainerList = new List<ContentContainer>
                    {
                        new ContentContainer(new List<ICodexWidget>()
                        {
                            new CodexText(go.GetProperName(), CodexTextStyle.Title),
                            new CodexDividerLine()
                            }, ContentContainer.ContentLayout.Vertical)
                    };

                    Sprite first = Def.GetUISprite(go).first;
                    CodexEntryGenerator.GenerateImageContainers(first, contentContainerList);
                    List<ICodexWidget> content = new List<ICodexWidget>();
                    Tag tag = go.PrefabID();
                    content.Add(new CodexText()
                    {
                        stringKey = $"STRINGS.ITEMS.PILLS.{tag.ToString().ToUpper()}.DESC",
                        style = CodexTextStyle.Body
                    });
                    ContentContainer contentContainer = new ContentContainer(content, ContentContainer.ContentLayout.Vertical);
                    contentContainerList.Add(contentContainer);

                    CodexEntry entry = new CodexEntry("MEDICINE", contentContainerList, go.GetProperName());
                    entry.icon = first;

                    GenerateRecipesContainers(tag, contentContainerList);

                    entry.parentId = "MEDICINE";
                    //entry.id = tag.ToString();

                    CodexCache.AddEntry(tag.ToString(), entry);
                    res.Add(tag.ToString(), entry);

                }
            }
            return res;
        }
    }
}
