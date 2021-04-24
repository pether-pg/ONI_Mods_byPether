using Harmony;
using UnityEngine;
using System.Collections.Generic;
using STRINGS;
using TUNING;

namespace IlluminationSensor
{
    public class IlluminationSensor_Patches
    {
		public static class Mod_OnLoad
        {
            public static void OnLoad()
			{
				//Debug.Log("IlluminationSensor: Loaded version of the mod for Vanilla buid 460672. Last update: 24.04.2021");
			}
        }

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

				ModUtil.AddBuildingToPlanScreen("Automation", LogicIlluminationSensorConfig.ID);
			}
		}

		
		[HarmonyPatch(typeof(Db))]
		[HarmonyPatch("Initialize")]
		public static class Db_Initialize_Patch
		{
			public static void Prefix()
			{
				// for vanilla version
				BasicModUtils.AddToTech("GenericSensors", LogicIlluminationSensorConfig.ID);
			}
		}

		/*
		[HarmonyPatch(typeof(Database.Techs))]
		[HarmonyPatch("Init")]
		public static class Techs_Init_Patch
		{
			public static void Postfix(Database.Techs __instance)
			{
				// for dlc version
				Tech tech = __instance.TryGet("GenericSensors");
				tech.unlockedItemIDs.Add(LogicIlluminationSensorConfig.ID);
			}
		}*/
	}
}
