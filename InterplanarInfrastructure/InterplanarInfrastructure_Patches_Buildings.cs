using HarmonyLib;
using UnityEngine;
using System;

namespace InterplanarInfrastructure
{
    public class InterplanarInfrastructure_Patches_Buildings
	{
		[HarmonyPatch(typeof(ManualHighEnergyParticleSpawner))]
		[HarmonyPatch("OnSpawn")]
		public static class ManualHighEnergyParticleSpawner_OnSpawn_Patch
		{
			public static void Postfix(ManualHighEnergyParticleSpawner __instance)
			{
				// only for testing

				if (__instance.gameObject == null)
					return;

				WorldContainer world = __instance.gameObject.GetMyWorld();
				if (world == null)
					return;

				world.cosmicRadiation += 1000;
			}
		}

		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
		public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			public static void Prefix()
			{
				ModUtil.AddBuildingToPlanScreen("HEP", RadiationLaserConfig.ID);
				ModUtil.AddBuildingToPlanScreen("HEP", RadiationLenseSateliteConfig.ID);
				ModUtil.AddBuildingToPlanScreen("Rocketry", SolarLenseSateliteConfig.ID);
			}
		}

		[HarmonyPatch(typeof(Database.Techs))]
		[HarmonyPatch("Init")]
		public static class Techs_Init_Patch
		{
			public static void Postfix(Database.Techs __instance)
			{
				__instance.Get("HighPressureForging").unlockedItemIDs.Add(SolarLenseSateliteConfig.ID);
				__instance.Get("NuclearPropulsion").unlockedItemIDs.Add(RadiationLaserConfig.ID);
				__instance.Get("NuclearPropulsion").unlockedItemIDs.Add(RadiationLenseSateliteConfig.ID);
			}
		}



		[HarmonyPatch(typeof(Db))]
		[HarmonyPatch("Initialize")]
		public static class Db_Initialize_Patch
        {
			public static void Postfix()
            {
				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{RadiationLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.NAME", "Collecting Space Radiation: ");
				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{RadiationLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.TOOLTIP", "Collecting Space Radiation: ");

				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{SolarLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.NAME", "Focusing current illumination: ");
				Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{SolarLenseSateliteConfig.StatusItemID.ToUpperInvariant()}.TOOLTIP", "Focusing current illumination: ");

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


		[HarmonyPatch(typeof(BuildingDef))]
		[HarmonyPatch("IsValidPlaceLocation")]
		[HarmonyPatch(new Type[] { typeof(GameObject), typeof(int), typeof(Orientation), typeof(bool), typeof(string) },
			new ArgumentType[] { ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Normal, ArgumentType.Out })]
		public static class CanBuildPatch
		{
			public static void Postfix(BuildingDef __instance, GameObject source_go, int cell, ref bool __result, ref string fail_reason)
			{
				if (__instance.PrefabID != RadiationLenseSateliteConfig.ID && __instance.PrefabID != SolarLenseSateliteConfig.ID)
					return;

				WorldContainer world = source_go.GetMyWorld();
				if (world == null)
					return;

				int currentY = Grid.CellToXY(cell).y;
				int requiredY = world.WorldOffset.y + world.WorldSize.y - __instance.HeightInCells - 2;

				if (currentY != requiredY)
				{
					__result = false;
					fail_reason = $"Build on the space border (move {requiredY - currentY} tiles up)";
				}
			}
		}
	}
}
