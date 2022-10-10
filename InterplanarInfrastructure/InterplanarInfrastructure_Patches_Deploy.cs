using HarmonyLib;
using UnityEngine;
using System;
using TodoList;

namespace InterplanarInfrastructure
{
    class InterplanarInfrastructure_Patches_Deploy
	{
		private static void Note() => Todo.Note("Use your own prefab for satelites in space.");
		public const string SatelitePrefabId = "ArtifactSpacePOI_GravitasSpaceStation1";


		[HarmonyPatch(typeof(Placeable))]
		[HarmonyPatch("IsValidPlaceLocation")]
		[HarmonyPatch(new Type[] { typeof(int), typeof(string) }, new ArgumentType[] { ArgumentType.Normal, ArgumentType.Out })]
		public static class Placeable_IsValidPlaceLocation_Patch
		{
			public static void Postfix(Placeable __instance, int cell, ref string reason, ref bool __result)
			{
				if (__instance.kAnimName != SolarLenseSateliteConfig.PlacableKAnim
					&& __instance.kAnimName != RadiationLenseSateliteConfig.PlacableKAnim)
					return;

				Todo.Note("This condition is used to check in SMIs if satelites are deployed. If logic here is removed, SMI logic must be updated");
				Todo.Note("The same is true for LogicBroadcastReceiver_IsSpaceVisible_Patch");
				int tilesToTop = WorldBorderChecker.TilesToTheTop(cell, 1);

				if (tilesToTop != 0)
				{
					__result = false;
					reason = $"Place on the space border (move {tilesToTop} tiles up)";
				}
			}
		}

		[HarmonyPatch(typeof(LogicBroadcastReceiver))]
		[HarmonyPatch("IsSpaceVisible")]
		public static class LogicBroadcastReceiver_IsSpaceVisible_Patch
		{
			public static bool Prefix(LogicBroadcastReceiver __instance)
            {
				// IsSpaceVisible() crashes with Null exception when located on space hexmap during rocket flight.
				// This is to make sure the method is called for solar satelite only after deployment

				SolarLenseSatelite satelite = __instance.gameObject.GetComponent<SolarLenseSatelite>();
				if (satelite == null)
					return true;

				if (satelite.smi == null)
					return true;

				return satelite.smi.IsInTopOfTheWorld();
            }
        }

		[HarmonyPatch(typeof(JettisonableCargoModule.StatesInstance))]
		[HarmonyPatch("CanEmptyCargo")]
		public static class JettisonableCargoModule_CanEmptyCargo_Patch
		{
			public static void Postfix(JettisonableCargoModule.StatesInstance __instance, ref bool __result)
			{
				Todo.Note("Instead of patching JettisonableCargoModule you can use custom behaviour");
				if (__instance.def.landerPrefabID != SolarLenseSateliteConfig.ID
					&& __instance.def.landerPrefabID != RadiationLenseSateliteConfig.ID)
					return;

				Clustercraft component = __instance.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
				if (component != null)
					__result &= !IsThereSatelite(component.Location);
			}

			public static bool IsThereSatelite(AxialI location)
            {
				foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.cellContents[location])
					if(clusterGridEntity.HasTag(SatelitePrefabId))
						return true;
				return false;
            }
        }

		[HarmonyPatch(typeof(JettisonableCargoModule.StatesInstance))]
		[HarmonyPatch("FinalDeploy")]
		public static class JettisonableCargoModule_FinalDeploy_Patch
		{
			public static void Prefix(JettisonableCargoModule.StatesInstance __instance, out GameObject __state)
			{
				__state = null;
				Storage landerContainer = Traverse.Create(__instance).Field("landerContainer").GetValue<Storage>();
				if (landerContainer == null)
					return;

				__state = landerContainer.FindFirst(__instance.def.landerPrefabID);
			}

			public static void Postfix(JettisonableCargoModule.StatesInstance __instance, GameObject __state)
			{
				Todo.Note("Instead of patching JettisonableCargoModule you can use custom behaviour");

				if (__instance.def.landerPrefabID != SolarLenseSateliteConfig.ID
					&& __instance.def.landerPrefabID != RadiationLenseSateliteConfig.ID)
					return;

				Clustercraft component = __instance.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
				if(component != null)
				{
					SpawnSatellite(component.Location);

					if (__state == null)
						return;

					// connects deployed space satelite to the building. will allow to clear space satelite on building deconstruct
					__state.GetComponent<SolarLenseSatelite>()?.smi.SetDeployLocation(component.Location);
					__state.GetComponent<RadiationLenseSatelite>()?.smi.SetDeployLocation(component.Location);
				}
			}

			private static void SpawnSatellite(AxialI location)
			{
				Vector3 position = new Vector3(-1f, -1f, 0.0f);
				GameObject sat = Util.KInstantiate(Assets.GetPrefab((Tag)SatelitePrefabId), position);
				sat.GetComponent<ClusterGridEntity>().Location = location;
				sat.SetActive(true);
			}
		}

		/*
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
		}*/
	}
}
