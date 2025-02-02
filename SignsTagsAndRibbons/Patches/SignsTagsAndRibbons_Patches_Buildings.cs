using HarmonyLib;

namespace SignsTagsAndRibbons
{
    class SignsTagsAndRibbons_Patches_Buildings
    {
        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                BasicModUtils.MakeBuildingStrings(DangerRibbonConfig.ID, STRINGS.BUILDINGS.DANGERRIBBON.NAME, STRINGS.BUILDINGS.DANGERRIBBON.DESC, STRINGS.BUILDINGS.DANGERRIBBON.EFFECT);
                BasicModUtils.MakeBuildingStrings(DangerRibbonCornerConfig.ID, STRINGS.BUILDINGS.DANGERCORNER.NAME, STRINGS.BUILDINGS.DANGERCORNER.DESC, STRINGS.BUILDINGS.DANGERCORNER.EFFECT);
                BasicModUtils.MakeBuildingStrings(MeterScaleConfig.ID, STRINGS.BUILDINGS.METERSCALE.NAME, STRINGS.BUILDINGS.METERSCALE.DESC, STRINGS.BUILDINGS.METERSCALE.EFFECT);
                BasicModUtils.MakeBuildingStrings(InfoTagConfig.ID, STRINGS.BUILDINGS.INFO.NAME, STRINGS.BUILDINGS.INFO.DESC, STRINGS.BUILDINGS.INFO.EFFECT);
                BasicModUtils.MakeBuildingStrings(SystemTagConfig.ID, STRINGS.BUILDINGS.SYSTEM.NAME, STRINGS.BUILDINGS.SYSTEM.DESC, STRINGS.BUILDINGS.SYSTEM.EFFECT);
                BasicModUtils.MakeBuildingStrings(UtilityTagConfig.ID, STRINGS.BUILDINGS.UTILITY.NAME, STRINGS.BUILDINGS.UTILITY.DESC, STRINGS.BUILDINGS.UTILITY.EFFECT);
                BasicModUtils.MakeBuildingStrings(AlertTagConfig.ID, STRINGS.BUILDINGS.ALERT.NAME, STRINGS.BUILDINGS.ALERT.DESC, STRINGS.BUILDINGS.ALERT.EFFECT);
                BasicModUtils.MakeBuildingStrings(SolidTagConfig.ID, STRINGS.BUILDINGS.SOLID.NAME, STRINGS.BUILDINGS.SOLID.DESC, STRINGS.BUILDINGS.SOLID.EFFECT);
                BasicModUtils.MakeBuildingStrings(LiquidTagConfig.ID, STRINGS.BUILDINGS.LIQUID.NAME, STRINGS.BUILDINGS.LIQUID.DESC, STRINGS.BUILDINGS.LIQUID.EFFECT);
                BasicModUtils.MakeBuildingStrings(GasTagConfig.ID, STRINGS.BUILDINGS.GAS.NAME, STRINGS.BUILDINGS.GAS.DESC, STRINGS.BUILDINGS.GAS.EFFECT);
                BasicModUtils.MakeBuildingStrings(NumbersTagConfig.ID, STRINGS.BUILDINGS.NUMBER.NAME, STRINGS.BUILDINGS.NUMBER.DESC, STRINGS.BUILDINGS.NUMBER.EFFECT);
                BasicModUtils.MakeBuildingStrings(LetterTagConfig.ID, STRINGS.BUILDINGS.LETTER.NAME, STRINGS.BUILDINGS.LETTER.DESC, STRINGS.BUILDINGS.LETTER.EFFECT);
                BasicModUtils.MakeBuildingStrings(GeyserTagConfig.ID, STRINGS.BUILDINGS.GEYSER.NAME, STRINGS.BUILDINGS.GEYSER.DESC, STRINGS.BUILDINGS.GEYSER.EFFECT);
                BasicModUtils.MakeBuildingStrings(LocationTagConfig.ID, STRINGS.BUILDINGS.LOCATION.NAME, STRINGS.BUILDINGS.LOCATION.DESC, STRINGS.BUILDINGS.LOCATION.EFFECT);
                BasicModUtils.MakeBuildingStrings(SmallElementTagConfig.ID, STRINGS.BUILDINGS.SMALL_EMELENT.NAME, STRINGS.BUILDINGS.SMALL_EMELENT.DESC, STRINGS.BUILDINGS.SMALL_EMELENT.EFFECT);

                BasicModUtils.MakeSideScreenStrings(SignSideScreen.SCREEN_TITLE_KEY, STRINGS.SIDESCREEN.TITLE);

                const string categoryId = "SignsTagsRibbonsSubcategory";
                BasicModUtils.MakeBuildMenuSubcatagory(categoryId, STRINGS.BUILDINGS.MENU_SUBCATEGORY.NAME);

                ModUtil.AddBuildingToPlanScreen("Utilities", DangerRibbonConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", DangerRibbonCornerConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", MeterScaleConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", InfoTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", SystemTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", UtilityTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", AlertTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", SolidTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", LiquidTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", GasTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", NumbersTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", LetterTagConfig.ID, categoryId);
                // ModUtil.AddBuildingToPlanScreen("Utilities", GeyserTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", LocationTagConfig.ID, categoryId);
                ModUtil.AddBuildingToPlanScreen("Utilities", SmallElementTagConfig.ID, categoryId);
            }
        }
    }
}
