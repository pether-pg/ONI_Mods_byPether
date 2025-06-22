using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Codex
    {

        //[HarmonyPatch(typeof(CodexCache), "CollectYAMLEntries")]
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
                HungermsVaccineConfig.ID, 
                IntermediateRadPillConfig.ID,
                //AntihistamineBoosterConfig.ID
            };

            List<GameObject> prefabsWithComponent = Assets.GetPrefabsWithComponent<MedicinalPill>();
            Dictionary<string, CodexEntry> res = new Dictionary<string, CodexEntry>();

            foreach (GameObject go in prefabsWithComponent)
            {
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
