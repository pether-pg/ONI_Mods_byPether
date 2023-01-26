using HarmonyLib;

namespace FragrantFlowers
{
    class FragrantFlowers_Patches_Buildings
	{
		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				BasicModUtils.MakeBuildingStrings(VaporizerConfig.ID,
										STRINGS.BUILDINGS.VAPORIZER.NAME,
										STRINGS.BUILDINGS.VAPORIZER.DESC,
										STRINGS.BUILDINGS.VAPORIZER.EFFECT);

				ModUtil.AddBuildingToPlanScreen("Medical", VaporizerConfig.ID);
			}
		}

		[HarmonyPatch(typeof(Database.Techs))]
		[HarmonyPatch("Init")]
		public static class Techs_Init_Patch
		{
			public static void Postfix(Database.Techs __instance)
			{
				Tech tech1 = __instance.TryGet("MedicineII");
				if (tech1 != null)
					tech1.unlockedItemIDs.Add(VaporizerConfig.ID);
			}
		}
	}
}
