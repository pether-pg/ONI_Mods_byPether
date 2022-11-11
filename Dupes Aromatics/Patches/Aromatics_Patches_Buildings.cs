using HarmonyLib;

namespace Dupes_Aromatics
{
    class Aromatics_Patches_Buildings
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
	}
}
