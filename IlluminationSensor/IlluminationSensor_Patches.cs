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
			}
        }

		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				BasicModUtils.MakeStrings(LogicIlluminationSensorConfig.ID, 
										LogicIlluminationSensorConfig.Name, 
										LogicIlluminationSensorConfig.Description, 
										LogicIlluminationSensorConfig.Efect);

				ModUtil.AddBuildingToPlanScreen("Automation", LogicIlluminationSensorConfig.ID);
			}
		}

		[HarmonyPatch(typeof(Db))]
		[HarmonyPatch("Initialize")]
		public static class Db_Initialize_Patch
		{
			public static void Prefix()
			{
				BasicModUtils.AddToTech("GenericSensors", LogicIlluminationSensorConfig.ID);
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
