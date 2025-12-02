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
        private static void FixNameLinks(GameObject __result)
        {
            string id = __result.PrefabID().ToString();
            string name = __result.GetProperName();
            TagManager.Create(id, UI.FormatAsLink(name, id));
            __result.AddOrGet<KSelectable>().SetName(UI.FormatAsLink(name, id));
        }

        [HarmonyPatch(typeof(CodexCache), "CollectEntries")]
        public class CodexCache_CollectEntries_Patch
        {
            public static void Postfix(string folder, List<CodexEntry> __result)
            {
                if (folder != string.Empty)
                    return;

                CodexEntry temp;

                if (Settings.Instance.AlienGoo.IncludeDisease &&
                    (temp = CreateCodexEntry(AlienSicknessCureConfig.ID, $"STRINGS.CODEX.ADVANCEDCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodexEntry(AllergyVaccineConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodexEntry(AntihistamineBoosterConfig.ID, $"STRINGS.CODEX.ANTIHISTAMINE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.MooFlu.IncludeDisease &&
                    (temp = CreateCodexEntry(GasCureConfig.ID, $"STRINGS.CODEX.BASICCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.MooFlu.IncludeDisease &&
                    (temp = CreateCodexEntry(GassyVaccineConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodexEntry(HappyPillConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.HungerGerms.IncludeDisease &&
                    (temp = CreateCodexEntry(HungermsVaccineConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.BogInsects.IncludeDisease &&
                    (temp = CreateCodexEntry(MudMaskConfig.ID, $"STRINGS.CODEX.BASICCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.MutatingVirus.IncludeDisease &&
                    (temp = CreateCodexEntry(MutatingAntiviralConfig.ID, $"STRINGS.CODEX.BASICCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.HungerGerms.IncludeDisease &&
                    (temp = CreateCodexEntry(RadShotConfig.ID, $"STRINGS.CODEX.ADVANCEDCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.RustDust.IncludeDisease &&
                    (temp = CreateCodexEntry(RustSickness2CureConfig.ID, $"STRINGS.CODEX.ADVANCEDCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.RustDust.IncludeDisease &&
                    (temp = CreateCodexEntry(RustSickness3CureConfig.ID, $"STRINGS.CODEX.ADVANCEDCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.HungerGerms.IncludeDisease &&
                    (temp = CreateCodexEntry(SapShotConfig.ID, $"STRINGS.CODEX.ADVANCEDCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.FrostPox.IncludeDisease &&
                    (temp = CreateCodexEntry(SerumDeepBreathConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.AlienGoo.IncludeDisease &&
                    (temp = CreateCodexEntry(SerumSuperConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.MooFlu.IncludeDisease &&
                    (temp = CreateCodexEntry(SerumTummyConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if (Settings.Instance.BogInsects.IncludeDisease && Settings.Instance.HungerGerms.IncludeDisease &&
                    (temp = CreateCodexEntry(SerumYummyConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodexEntry(SlimelungVaccineConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodexEntry(SunburnCureConfig.ID, $"STRINGS.CODEX.ADVANCEDCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodexEntry(ZombieSporesVaccineConfig.ID, $"STRINGS.CODEX.BASICBOOSTER.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);

                if ((temp = CreateCodexEntry(TestSampleConfig.ID, $"STRINGS.CODEX.BASICCURE.SUBTITLE", "MEDICINES")) != null)
                    __result.Add(temp);
            }
        }

        private static CodexEntry CreateCodexEntry(string id, string subtitle, string category)
        {
            GameObject go = Assets.GetPrefab(id);

            if (go == null)
                return null;

            List<ContentContainer> containers = new List<ContentContainer>
            {
                new ContentContainer(new List<ICodexWidget>()
                    {
                        new CodexText(go.GetProperName(), CodexTextStyle.Title),
                        new CodexText() { stringKey = subtitle, style = CodexTextStyle.Subtitle },
                        new CodexDividerLine()
                    }, ContentContainer.ContentLayout.Vertical)
            };

            Sprite first = Def.GetUISprite(go).first;
            CodexEntryGenerator.GenerateImageContainers(first, containers);

            List<ICodexWidget> content = new List<ICodexWidget>
            {
                new CodexText(go.GetComponent<InfoDescription>()?.description, CodexTextStyle.Body),
            };

            ContentContainer contentContainer = new ContentContainer(content, ContentContainer.ContentLayout.Vertical);
            containers.Add(contentContainer);

            CodexEntry entry = new CodexEntry(category, containers, go.GetProperName());
            entry.icon = first;

            entry.id = id;
            entry.disabled = false;

            entry.contentMadeAndUsed.Add(new CodexEntry_MadeAndUsed() { tag = id });

            return entry;
        }
    }
}
