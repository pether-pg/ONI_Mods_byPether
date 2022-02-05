using HarmonyLib;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Buildings
	{
		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				BasicModUtils.MakeBuildingStrings(GermcatcherConfig.ID,
										STRINGS.BUILDINGS.GERMCATCHER.NAME,
										STRINGS.BUILDINGS.GERMCATCHER.DESC,
										STRINGS.BUILDINGS.GERMCATCHER.EFFECCT);

				BasicModUtils.MakeBuildingStrings(VaccineApothecaryConfig.ID,
										STRINGS.BUILDINGS.VACCINEAPOTHECARY.NAME,
										STRINGS.BUILDINGS.VACCINEAPOTHECARY.DESC,
										STRINGS.BUILDINGS.VACCINEAPOTHECARY.EFFECCT);

				BasicModUtils.MakeStatusItemStrings(GermcatcherConfig.StatusItemID, STRINGS.STATUSITEMS.GATHERING.NAME, STRINGS.STATUSITEMS.GATHERING.TOOLTIP);

				ModUtil.AddBuildingToPlanScreen("Medical", GermcatcherConfig.ID);
				ModUtil.AddBuildingToPlanScreen("Medical", VaccineApothecaryConfig.ID);
			}
		}

		[HarmonyPatch(typeof(Database.Techs))]
		[HarmonyPatch("Init")]
		public static class Techs_Init_Patch
		{
			public static void Postfix(Database.Techs __instance)
			{
				Tech tech1 = __instance.TryGet("MedicineI");
				if (tech1 != null)
				{
					tech1.unlockedItemIDs.Add(GermcatcherConfig.ID);
					tech1.unlockedItemIDs.Add(MedicalResearchDataBank.ID);
				}
				Tech tech2 = __instance.TryGet("MedicineIV");
				if (tech2 != null)
					tech2.unlockedItemIDs.Add(VaccineApothecaryConfig.ID);
			}
		}
	}
}
