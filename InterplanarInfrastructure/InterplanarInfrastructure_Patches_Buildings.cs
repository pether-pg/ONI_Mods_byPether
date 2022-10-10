using HarmonyLib;
using UnityEngine;
using TodoList;

namespace InterplanarInfrastructure
{
    public class InterplanarInfrastructure_Patches_Buildings
	{		
		[HarmonyPatch(typeof(TemporalTearConfig))]
		[HarmonyPatch("CreatePrefab")]
		public static class TemporalTearConfig_CreatePrefab_Patch
		{
			public static void Postfix(GameObject __result)
			{
				Todo.Note("I couldn't add ClusterDestinationSelector to ClusterGridEntity to make Dyson Sphere work and select destination");
				Todo.Note("As a result I had to abandon this idea. Feel free to revive it if you can make it work");
				//__result.AddComponent<DysonSphere>();
				//__result.AddComponent<ClusterDestinationSelector>();
			}
		}

		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				ModUtil.AddBuildingToPlanScreen("HEP", RadiationLaserConfig.ID);

				SelectModuleSideScreen.moduleButtonSortOrder.Add(RadiationSateliteModuleConfig.ID);
				SelectModuleSideScreen.moduleButtonSortOrder.Add(SolarLenseModuleConfig.ID);
			}
		}

		[HarmonyPatch(typeof(Database.Techs))]
		[HarmonyPatch("Init")]
		public static class Techs_Init_Patch
		{
			public static void Postfix(Database.Techs __instance)
			{
				Todo.Note("Final unlocking techs may be different");

				__instance.Get("HighPressureForging").unlockedItemIDs.Add(SolarLenseSateliteConfig.ID);
				__instance.Get("NuclearPropulsion").unlockedItemIDs.Add(RadiationLaserConfig.ID);
				__instance.Get("NuclearPropulsion").unlockedItemIDs.Add(SolarLenseSateliteConfig.ID);
				__instance.Get("NuclearPropulsion").unlockedItemIDs.Add(RadiationLenseSateliteConfig.ID);
				__instance.Get("NuclearPropulsion").unlockedItemIDs.Add(RadiationSateliteModuleConfig.ID);
			}
		}



		[HarmonyPatch(typeof(Db))]
		[HarmonyPatch("Initialize")]
		public static class Db_Initialize_Patch
        {
			public static void Postfix()
			{
				Todo.Note("I assume you have/use existing methods for adding strings.");

				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{RadiationLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.NAME", "Collecting Space Radiation: ");
				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{RadiationLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.TOOLTIP", "Collecting Space Radiation: ");

				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{SolarLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.NAME", "Focusing current illumination: ");
				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{SolarLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.TOOLTIP", "Focusing current illumination: ");

				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{SolarLenseModuleConfig.ID.ToUpperInvariant()}.NAME", "Solar Lense Satelite Module");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{SolarLenseModuleConfig.ID.ToUpperInvariant()}.DESC", "Allows to deploy Solar Lense Satelite on the orbit");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{SolarLenseModuleConfig.ID.ToUpperInvariant()}.EFFECT", "Allows to deploy Solar Lense Satelite on the orbit");

				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationSateliteModuleConfig.ID.ToUpperInvariant()}.NAME", "Radiation Lense Satelite Module");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationSateliteModuleConfig.ID.ToUpperInvariant()}.DESC", "Allows to deploy Radiation Lense Satelite on the orbit");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationSateliteModuleConfig.ID.ToUpperInvariant()}.EFFECT", "Allows to deploy Radiation Lense Satelite on the orbit");
				
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationLenseSateliteConfig.ID.ToUpperInvariant()}.NAME", "Radiation Lense Satelite");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationLenseSateliteConfig.ID.ToUpperInvariant()}.DESC", "Converts Space Radiation to Radbolts");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationLenseSateliteConfig.ID.ToUpperInvariant()}.EFFECT", "Converts Space Radiation to Radbolts");

				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationLaserConfig.ID.ToUpperInvariant()}.NAME", "Radiation Laser");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationLaserConfig.ID.ToUpperInvariant()}.DESC", "Increases Space Radiation on another Asteroid");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{RadiationLaserConfig.ID.ToUpperInvariant()}.EFFECT", "Increases Space Radiation on another Asteroid");

				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{SolarLenseSateliteConfig.ID.ToUpperInvariant()}.NAME", "Solar Lance Lense");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{SolarLenseSateliteConfig.ID.ToUpperInvariant()}.DESC", "Heats the surface up using solar light");
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{SolarLenseSateliteConfig.ID.ToUpperInvariant()}.EFFECT", "Heats the surface up using solar light");

				StatusItem radStatusItem = new StatusItem(RadiationLenseSateliteConfig.StatusItemID, "BUILDINGS", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
				radStatusItem.SetResolveStringCallback((str, data) => str += ((RadiationLenseSatelite.StatesInstance)data).GetStatusItemProgress());
				Db.Get().BuildingStatusItems.Add(radStatusItem);

				StatusItem statusItem = new StatusItem(SolarLenseSateliteConfig.StatusItemID, "BUILDINGS", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
				statusItem.SetResolveStringCallback((str, data) => str += ((SolarLenseSatelite.StatesInstance)data).GetStatusItemProgress());
				Db.Get().BuildingStatusItems.Add(statusItem);
			}
        }
	}
}
