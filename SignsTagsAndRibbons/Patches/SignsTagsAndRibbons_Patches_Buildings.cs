using HarmonyLib;

namespace SignsTagsAndRibbons
{
    class SignsTagsAndRibbons_Patches_Buildings
    {
        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            private static void Prefix()
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

                BasicModUtils.MakeSideScreenStrings(SignSideScreen.SCREEN_TITLE_KEY, STRINGS.SIDESCREEN.TITLE);

                ModUtil.AddBuildingToPlanScreen("Utilities", DangerRibbonConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", DangerRibbonCornerConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", MeterScaleConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", InfoTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", SystemTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", UtilityTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", AlertTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", SolidTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", LiquidTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", GasTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", NumbersTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", LetterTagConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Utilities", GeyserTagConfig.ID);
            }
        }
    }
}
