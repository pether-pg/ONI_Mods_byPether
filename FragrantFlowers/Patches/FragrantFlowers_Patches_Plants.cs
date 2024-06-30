using HarmonyLib;
using KMod;
using ProcGen;
using System;
using System.Collections.Generic;
using System.Linq;
using TUNING;
using STRINGS;
using UnityEngine;

namespace FragrantFlowers
{
    public class FragrantFlowers_Patches_Plants
    {
        //public static Dictionary<string, CuisinePlantsTuning.CropsTuning> CropsDictionary;
        public const float CyclesForGrowth = 4f;


        [HarmonyPatch(typeof(EntityConfigManager), "LoadGeneratedEntities")]
        public static class EntityConfigManager_LoadGeneratedEntities_Patch
        {
            public static void Prefix()
            {
                //=========================================================================> SPINOSA <===============================
                //====[ SPINOSA ROSE ]===================
                RegisterStrings.MakePlantProductStrings(Crop_SpinosaRoseConfig.ID, STRINGS.CROPS.SPINOSAROSE.NAME, STRINGS.CROPS.SPINOSAROSE.DESC);
                RegisterStrings.MakeCodexStrings(Crop_SpinosaRoseConfig.ID, STRINGS.CROPS.SPINOSAROSE.NAME, STRINGS.CODEX.PERFUMEINGREDIENTTITLE, STRINGS.CROPS.SPINOSAROSE.DESC);

                //====[ SPINOSA HIPS ]===================
                RegisterStrings.MakeFoodStrings(Crop_SpinosaHipsConfig.ID, STRINGS.CROPS.SPINOSAHIPS.NAME, STRINGS.CROPS.SPINOSAHIPS.DESC);

                //====[ SPINOSA SEED ]===================
                RegisterStrings.MakeSeedStrings(Plant_SpinosaConfig.SEED_ID, STRINGS.SEEDS.SPINOSA.SEED_NAME, STRINGS.SEEDS.SPINOSA.SEED_DESC);

                //====[ BLOOMING SPINOSA ]===============
                RegisterStrings.MakePlantSpeciesStrings(Plant_SpinosaConfig.ID, STRINGS.PLANTS.SPINOSA.NAME, STRINGS.PLANTS.SPINOSA.DESC);
                RegisterStrings.MakeCodexStrings(Plant_SpinosaConfig.ID, STRINGS.PLANTS.SPINOSA.NAME, STRINGS.CODEX.AROMATICPLANTSUBTITLE);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaRoseConfig.ID, Crop_SpinosaRoseConfig.GROW_TIME, 1, true));

                //====[ FRUITING SPINOSA ]===============
                RegisterStrings.MakePlantSpeciesStrings(Plant_SuperSpinosaConfig.ID, STRINGS.PLANTS.SUPERSPINOSA.NAME, STRINGS.PLANTS.SUPERSPINOSA.DESC);
                RegisterStrings.MakeCodexStrings(Plant_SuperSpinosaConfig.ID, STRINGS.PLANTS.SUPERSPINOSA.NAME, STRINGS.CODEX.FOODPLANTSUBTITLE);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_SpinosaHipsConfig.ID, Crop_SpinosaHipsConfig.GROW_TIME, 1, true));

                //=========================================================================> DUSK LAVENDER <========================
                //====[ DUSKBLOOM ]===================
                RegisterStrings.MakePlantProductStrings(Crop_DuskbloomConfig.ID, STRINGS.CROPS.DUSKBLOOM.NAME, STRINGS.CROPS.DUSKBLOOM.DESC);
                RegisterStrings.MakeCodexStrings(Crop_DuskbloomConfig.ID, STRINGS.CROPS.DUSKBLOOM.NAME, STRINGS.CODEX.PERFUMEINGREDIENTTITLE, STRINGS.CROPS.DUSKBLOOM.DESC);

                //====[ DUSKBERRY ]===================
                RegisterStrings.MakeFoodStrings(Crop_DuskberryConfig.ID, STRINGS.CROPS.DUSKBERRY.NAME, STRINGS.CROPS.DUSKBERRY.DESC);

                //====[ DUSK SEED ]===================
                RegisterStrings.MakeSeedStrings(Plant_DuskLavenderConfig.SEED_ID, STRINGS.SEEDS.DUSKLAVENDER.SEED_NAME, STRINGS.SEEDS.DUSKLAVENDER.SEED_DESC);

                //====[ DUSKBLOOM LAVENDER ]==========
                RegisterStrings.MakePlantSpeciesStrings(Plant_DuskLavenderConfig.ID, STRINGS.PLANTS.DUSKLAVENDER.NAME, STRINGS.PLANTS.DUSKLAVENDER.DESC);
                RegisterStrings.MakeCodexStrings(Plant_DuskLavenderConfig.ID, STRINGS.PLANTS.DUSKLAVENDER.NAME, STRINGS.CODEX.AROMATICPLANTSUBTITLE);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskbloomConfig.ID, Crop_DuskbloomConfig.GROW_TIME, 1, true));

                //====[ DUSKBERRY LAVENDER ]==========
                RegisterStrings.MakePlantSpeciesStrings(Plant_SuperDuskLavenderConfig.ID, STRINGS.PLANTS.SUPERDUSKLAVENDER.NAME, STRINGS.PLANTS.SUPERDUSKLAVENDER.DESC);
                RegisterStrings.MakeCodexStrings(Plant_SuperDuskLavenderConfig.ID, STRINGS.PLANTS.SUPERDUSKLAVENDER.NAME, STRINGS.CODEX.FOODPLANTSUBTITLE);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_DuskberryConfig.ID, Crop_DuskberryConfig.GROW_TIME, 1, true));

                //=========================================================================> RIMED MALLOW <========================
                //====[ RIMED COTTON BOLL ]===========
                RegisterStrings.MakePlantProductStrings(Crop_CottonBollConfig.ID, STRINGS.CROPS.COTTONBOLL.NAME, STRINGS.CROPS.COTTONBOLL.DESC);
                RegisterStrings.MakeCodexStrings(Crop_CottonBollConfig.ID, STRINGS.CROPS.COTTONBOLL.NAME, STRINGS.CODEX.PERFUMEINGREDIENTTITLE, STRINGS.CROPS.COTTONBOLL.DESC);

                //====[ ICED MALLOW SEED ]============
                RegisterStrings.MakeSeedStrings(Plant_RimedMallowConfig.SEED_ID, STRINGS.SEEDS.RIMEDMALLOW.SEED_NAME, STRINGS.SEEDS.RIMEDMALLOW.SEED_DESC);

                //====[ RIMED MALLOW ]================
                RegisterStrings.MakePlantSpeciesStrings(Plant_RimedMallowConfig.ID, STRINGS.PLANTS.RIMEDMALLOW.NAME, STRINGS.PLANTS.RIMEDMALLOW.DESC);
                RegisterStrings.MakeCodexStrings(Plant_RimedMallowConfig.ID, STRINGS.PLANTS.RIMEDMALLOW.NAME, STRINGS.CODEX.AROMATICPLANTSUBTITLE);
                CROPS.CROP_TYPES.Add(new Crop.CropVal(Crop_CottonBollConfig.ID, Crop_CottonBollConfig.GROW_TIME, 1, true));


                RegisterStrings.MakeFoodStrings(DuskbunConfig.ID, STRINGS.FOOD.DUSKBUN.NAME, STRINGS.FOOD.DUSKBUN.DESC);
                RegisterStrings.MakeFoodStrings(DuskjamConfig.ID, STRINGS.FOOD.DUSKJAM.NAME, STRINGS.FOOD.DUSKJAM.DESC);
                RegisterStrings.MakeFoodStrings(SpinosaSyrupConfig.ID, STRINGS.FOOD.SPINOSASYRUP.NAME, STRINGS.FOOD.SPINOSASYRUP.DESC);
                RegisterStrings.MakeFoodStrings(SpinosaCakeConfig.ID, STRINGS.FOOD.SPINOSACAKE.NAME, STRINGS.FOOD.SPINOSACAKE.DESC);

                RegisterStrings.MakeCodexStrings(FloralAromaCanConfig.ID, STRINGS.AROMACANS.FLORAL.NAME, STRINGS.CODEX.CONSUMABLETTITLE, STRINGS.AROMACANS.FLORAL.DESC);
                RegisterStrings.MakeCodexStrings(LavenderAromaCanConfig.ID, STRINGS.AROMACANS.LAVENDER.NAME, STRINGS.CODEX.CONSUMABLETTITLE, STRINGS.AROMACANS.LAVENDER.DESC);
                RegisterStrings.MakeCodexStrings(MallowAromaCanConfig.ID, STRINGS.AROMACANS.MALLOW.NAME, STRINGS.CODEX.CONSUMABLETTITLE, STRINGS.AROMACANS.MALLOW.DESC);
                RegisterStrings.MakeCodexStrings(RoseAromaCanConfig.ID, STRINGS.AROMACANS.ROSE.NAME, STRINGS.CODEX.CONSUMABLETTITLE, STRINGS.AROMACANS.ROSE.DESC);
            }
        }

        [HarmonyPatch(typeof(CodexCache), "CollectYAMLEntries")]
        public class CodexEntryGenerator_GenerateSomeEntries_Patch
        {
            public static void Postfix()
            {
                CreateCodex(Crop_CottonBollConfig.ID, "INDUSTRIALINGREDIENTS");
                CreateCodex(Crop_SpinosaRoseConfig.ID, "INDUSTRIALINGREDIENTS");
                CreateCodex(Crop_DuskbloomConfig.ID, "INDUSTRIALINGREDIENTS");

                CreateCodex(FloralAromaCanConfig.ID, "INDUSTRIALINGREDIENTS");
                CreateCodex(LavenderAromaCanConfig.ID, "INDUSTRIALINGREDIENTS");
                CreateCodex(MallowAromaCanConfig.ID, "INDUSTRIALINGREDIENTS");
                CreateCodex(RoseAromaCanConfig.ID, "INDUSTRIALINGREDIENTS");
            }
        }

        [HarmonyPatch(typeof(CodexEntryGenerator), "GeneratePlantEntries")]
        public class CodexEntryGenerator_GeneratePlantEntries_Patch
        {
            public static void Postfix(Dictionary<string, CodexEntry> __result)
            {
                UpdateCodex(Plant_DuskLavenderConfig.ID);
                UpdateCodex(Plant_RimedMallowConfig.ID);
                UpdateCodex(Plant_SpinosaConfig.ID);
                UpdateCodex(Plant_SuperSpinosaConfig.ID);
                UpdateCodex(Plant_SuperDuskLavenderConfig.ID);
            }
        }

        private static void UpdateCodex(string id)
        {
            CodexEntry entry = CodexCache.FindEntry(id.ToUpperInvariant());

            if (entry == null)
                return;

            entry.contentContainers.InsertRange(0, new List<ContentContainer>()
                {
                    new ContentContainer()
                    {
                        contentLayout = ContentContainer.ContentLayout.Vertical,
                        content = new List<ICodexWidget>()
                        {
                        new CodexText() {stringKey = $"STRINGS.CODEX.{id.ToUpperInvariant()}.TITLE",
                            style = CodexTextStyle.Title },
                        new CodexText() {stringKey = $"STRINGS.CODEX.{id.ToUpperInvariant()}.SUBTITLE",
                            style = CodexTextStyle.Subtitle },
                        new CodexDividerLine() {preferredWidth = -1 }
                        }
                    },
                    new ContentContainer()
                    {
                        contentLayout = ContentContainer.ContentLayout.Vertical,
                        content = new List<ICodexWidget>()
                        {
                        new CodexText() { 
                            stringKey = $"STRINGS.CREATURES.SPECIES.{id.ToUpperInvariant()}.DESC",
                            style = CodexTextStyle.Body }
                        }
                    }
                });
        }

        private static void CreateCodex(string id, string category)
        {
            GameObject go = Assets.GetPrefab(id);

            List<ContentContainer> containers = new List<ContentContainer>();

            containers.Add(new ContentContainer(new List<ICodexWidget>()
                        {
                            new CodexText(go.GetProperName(), CodexTextStyle.Title),
                            new CodexText() {stringKey = $"STRINGS.CODEX.{id.ToUpperInvariant()}.SUBTITLE",
                            style = CodexTextStyle.Subtitle },
                            new CodexDividerLine()
                            }, ContentContainer.ContentLayout.Vertical));

            Sprite first = Def.GetUISprite(go).first;
            CodexEntryGenerator.GenerateImageContainers(first, containers);
            List<ICodexWidget> content = new List<ICodexWidget>();
            Tag tag = go.PrefabID();
            content.Add(new CodexText()
            {
                stringKey = $"STRINGS.CODEX.{id.ToUpperInvariant()}.BODY.CONTAINER1",
                style = CodexTextStyle.Body
            });
            ContentContainer contentContainer = new ContentContainer(content, ContentContainer.ContentLayout.Vertical);
            containers.Add(contentContainer);

            CodexEntry entry = new CodexEntry(category, containers, go.GetProperName());
            entry.icon = first;

            #region recipes
            List<ICodexWidget> content3 = new List<ICodexWidget>();
            List<ICodexWidget> content4 = new List<ICodexWidget>();
            foreach (ComplexRecipe recipe in ComplexRecipeManager.Get().recipes)
            {
                if (((IEnumerable<ComplexRecipe.RecipeElement>)recipe.ingredients).Any(i => i.material == id.ToUpperInvariant()))
                    content3.Add(new CodexRecipePanel(recipe));
                if (((IEnumerable<ComplexRecipe.RecipeElement>)recipe.results).Any(i => i.material == id.ToUpperInvariant()))
                    content4.Add(new CodexRecipePanel(recipe, true));
            }
            ContentContainer contents1 = new ContentContainer(content3, ContentContainer.ContentLayout.Vertical);
            ContentContainer contents2 = new ContentContainer(content4, ContentContainer.ContentLayout.Vertical);
            if (content3.Count > 0)
            {
                containers.Add(new ContentContainer(new List<ICodexWidget>()
                {
                    new CodexSpacer(),
                    new CodexCollapsibleHeader((string) CODEX.HEADERS.ELEMENTCONSUMEDBY, contents1)
                }, ContentContainer.ContentLayout.Vertical));
                containers.Add(contents1);
            }
            if (content4.Count > 0)
            {
                containers.Add(new ContentContainer(new List<ICodexWidget>()
                {
                    new CodexSpacer(),
                    new CodexCollapsibleHeader((string) CODEX.HEADERS.ELEMENTPRODUCEDBY, contents2)
                }, ContentContainer.ContentLayout.Vertical));
                containers.Add(contents2);
            }
            #endregion

            string str = tag.ToString();
            entry.id = str;
            entry.disabled = false;
            CodexCache.AddEntry(entry.id, entry);
        }
    }
}
