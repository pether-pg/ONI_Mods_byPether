using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using STRINGS;
using UnityEngine;

namespace FragrantFlowers
{
    internal class FragrantFlowers_Patches_Codex
    {
        [HarmonyPatch(typeof(CodexCache), "CollectEntries")]
        public class CodexCache_CollectEntries_Patch
        {
            public static void Postfix(string folder, List<CodexEntry> __result)
            {
                if (folder != string.Empty)
                    return;

                string industProdNameTemplate = "STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{0}.NAME";
                string industProdDescTemplate = "STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{0}.DESC";

                string speciesNameTemplate = "STRINGS.CREATURES.SPECIES.{0}.NAME";
                string speciesDescTemplate = "STRINGS.CREATURES.SPECIES.{0}.DESC";

                CodexEntry temp;

                if ((temp = CreateCodex(Crop_CottonBollConfig.ID, industProdNameTemplate, $"STRINGS.CODEX.BASIC_FABRIC.SUBTITLE",
                    industProdDescTemplate, "INDUSTRIALINGREDIENTS")) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(Crop_SpinosaRoseConfig.ID, industProdNameTemplate, $"STRINGS.CODEX.SWAMPLILYFLOWER.SUBTITLE",
                    industProdDescTemplate, "INDUSTRIALINGREDIENTS")) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(Crop_DuskbloomConfig.ID, industProdNameTemplate, $"STRINGS.CODEX.SWAMPLILYFLOWER.SUBTITLE",
                    industProdDescTemplate, "INDUSTRIALINGREDIENTS")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodex(FloralAromaCanConfig.ID, industProdNameTemplate, $"STRINGS.MISC.TAGS.INDUSTRIALPRODUCT",
                    industProdDescTemplate, "INDUSTRIALINGREDIENTS")) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(LavenderAromaCanConfig.ID, industProdNameTemplate, $"STRINGS.MISC.TAGS.INDUSTRIALPRODUCT",
                    industProdDescTemplate, "INDUSTRIALINGREDIENTS")) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(MallowAromaCanConfig.ID, industProdNameTemplate, $"STRINGS.MISC.TAGS.INDUSTRIALPRODUCT",
                    industProdDescTemplate, "INDUSTRIALINGREDIENTS")) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(RoseAromaCanConfig.ID, industProdNameTemplate, $"STRINGS.MISC.TAGS.INDUSTRIALPRODUCT",
                    industProdDescTemplate, "INDUSTRIALINGREDIENTS")) != null)
                    __result.Add(temp);


                if ((temp = CreateCodex(Plant_DuskLavenderConfig.ID, speciesNameTemplate, $"STRINGS.CODEX.DASHASALTVINE.SUBTITLE",
                    speciesDescTemplate, "PLANTS", true)) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(Plant_SpinosaConfig.ID, speciesNameTemplate, $"STRINGS.CODEX.DASHASALTVINE.SUBTITLE",
                    speciesDescTemplate, "PLANTS", true)) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(Plant_RimedMallowConfig.ID, speciesNameTemplate, $"STRINGS.CODEX.THIMBLEREED.SUBTITLE",
                    speciesDescTemplate, "PLANTS", true)) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(Plant_SuperDuskLavenderConfig.ID, speciesNameTemplate, $"STRINGS.CODEX.MEALWOOD.SUBTITLE",
                    speciesDescTemplate, "PLANTS", true)) != null)
                    __result.Add(temp);
                if ((temp = CreateCodex(Plant_SuperSpinosaConfig.ID, speciesNameTemplate, $"STRINGS.CODEX.MEALWOOD.SUBTITLE",
                    speciesDescTemplate, "PLANTS", true)) != null)
                    __result.Add(temp);
            }
        }

        private static CodexEntry CreateCodex(string id, string title, string subtitle, string body,
            string category, bool cutVersion = false)
        {
            GameObject go = Assets.GetPrefab(id);

            if (go == null)
                return null;

            List<ContentContainer> containers = new List<ContentContainer>
            {
                new ContentContainer(new List<ICodexWidget>()
                    {
                        new CodexText() { stringKey = string.Format(title, id.ToUpperInvariant()), style = CodexTextStyle.Title },
                        new CodexText() { stringKey = string.Format(subtitle, id.ToUpperInvariant()), style = CodexTextStyle.Subtitle },
                        new CodexDividerLine()
                    }, ContentContainer.ContentLayout.Vertical)
            };

            Sprite first = Def.GetUISprite(go).first;

            if (!cutVersion)
                CodexEntryGenerator.GenerateImageContainers(first, containers);

            List<ICodexWidget> content = new List<ICodexWidget>
            {
                new CodexText()
                {
                    stringKey = string.Format(body, id.ToUpperInvariant()),
                    style = CodexTextStyle.Body
                }
            };
            ContentContainer contentContainer = new ContentContainer(content, ContentContainer.ContentLayout.Vertical);
            containers.Add(contentContainer);

            CodexEntry entry = new CodexEntry(category, containers, go.GetProperName());

            entry.icon = first;

            entry.id = id;
            entry.disabled = false;

            if (!cutVersion)
                entry.contentMadeAndUsed.Add(new CodexEntry_MadeAndUsed() { tag = id });

            return entry;
        }
    }
}
