using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using STRINGS;
using TUNING;

namespace IlluminationSensor
{
    public class IlluminationSensor_Patches
	{
		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				BasicModUtils.MakeStrings(LogicIlluminationSensorConfig.ID,
										STRINGS.ILLUMINATIONSENSOR.NAME,
										STRINGS.ILLUMINATIONSENSOR.DESCRIPTION,
										STRINGS.ILLUMINATIONSENSOR.EFFECT);

				BasicModUtils.MakeSkinCategoryStrings(IlluminationSensor_Patches_Skins.SUBCATEGORY_ID, STRINGS.KLEI_INVENTORY_SCREEN.SUBCATEGORIES.BUILDING_LIGHT_SENSOR);

				ModUtil.AddBuildingToPlanScreen("Automation", LogicIlluminationSensorConfig.ID);
			}
		}

		[HarmonyPatch(typeof(Database.Techs))]
		[HarmonyPatch("Init")]
		public static class Techs_Init_Patch
		{
			public static void Postfix(Database.Techs __instance)
			{
				Tech tech = __instance.TryGet("GenericSensors");
				tech.unlockedItemIDs.Add(LogicIlluminationSensorConfig.ID);
			}
		}
	}
}
