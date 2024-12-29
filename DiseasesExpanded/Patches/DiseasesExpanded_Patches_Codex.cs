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
        [HarmonyPatch(typeof(CodexEntryGenerator), "GenerateCategoryEntry")]
        public class CodexEntryGenerator_GenerateCategoryEntry_Patch
        {
            public static void Prefix(string id, Dictionary<string, CodexEntry> entries)
            {
                if (id != "HOME")
                    return;

                entries.Add(CodexCache.FormatLinkID("MEDICINE"),
                    CodexEntryGenerator.GenerateCategoryEntry("MEDICINE",
                    UI.FormatAsLink(Strings.Get("STRINGS.MISC.TAGS.MEDICALSUPPLIES"), "MEDICINE"),
                    GeneratePillsEntries(), Def.GetUISprite("Apothecary").first
                ));
            }
        }

        private static void FixNameLinks(GameObject __result)
        {
            string id = __result.PrefabID().ToString();
            string name = __result.GetProperName();
            TagManager.Create(id, UI.FormatAsLink(name, id));
            __result.AddOrGet<KSelectable>().SetName(UI.FormatAsLink(name, id));
        }

        public static Dictionary<string, CodexEntry> GeneratePillsEntries()
        {
            List<string> hideInCodex = new List<string>()
            {
                IntermediateRadPillConfig.ID,
            };

            List<GameObject> prefabsWithComponent = Assets.GetPrefabsWithComponent<MedicinalPill>();
            Dictionary<string, CodexEntry> res = new Dictionary<string, CodexEntry>();

            foreach (GameObject go in prefabsWithComponent)
            {
                string id = go.PrefabID().ToString();

                if (hideInCodex.Contains(id))
                    continue;

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

                string desc = string.Empty;
                StringEntry strEnt;

                if (Strings.TryGet($"STRINGS.ITEMS.PILLS.{id.ToUpperInvariant()}.DESC", out strEnt))
                    desc = strEnt;
                else
                {
                    var infoDesc = go.GetComponent<InfoDescription>();
                    if (infoDesc != null)
                        desc = infoDesc.description;
                }

                List<ICodexWidget> content = new List<ICodexWidget>
                    {
                        new CodexText(desc, CodexTextStyle.Body)
                    };

                var pillDescriptors = go.GetComponent<MedicinalPill>().GetDescriptors(go);
                if (pillDescriptors.Count > 0)
                {
                    content.Add(new CodexText((string)CODEX.HEADERS.EQUIPMENTEFFECTS, CodexTextStyle.Subtitle));
                    foreach (Descriptor descriptor in pillDescriptors)
                        content.Add(new CodexTextWithTooltip("    " + descriptor.text, descriptor.tooltipText));
                    content.Add(new CodexSpacer());
                }

                ContentContainer contentContainer = new ContentContainer(content, ContentContainer.ContentLayout.Vertical);
                contentContainerList.Add(contentContainer);

                CodexEntry entry = new CodexEntry("MEDICINE", contentContainerList, go.GetProperName());
                entry.icon = first;
                entry.parentId = "MEDICINE";

                entry.contentMadeAndUsed.Add(new CodexEntry_MadeAndUsed() { tag = id });

                CodexCache.AddEntry(id, entry);
                res.Add(id, entry);
            }

            return res;
        }
    }
}
