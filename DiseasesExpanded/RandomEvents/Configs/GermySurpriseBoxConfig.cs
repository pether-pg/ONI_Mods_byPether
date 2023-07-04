using JetBrains.Annotations;
using ONITwitchLib;
using UnityEngine;
using DiseasesExpanded.RandomEvents.EntityScripts;

namespace DiseasesExpanded.RandomEvents.Configs
{
	// Try to make it similar to https://github.com/asquared31415/ONITwitch/blob/main/ONITwitchCore/Content/Entities/SurpriseBoxConfig.cs
	public class GermySurpriseBoxConfig : IEntityConfig
	{
		public const string ID = "GermySurpriseBox";
		private const string Anim = "germy_surprise_box_kanim";

		public GameObject CreatePrefab()
		{
			var go = EntityTemplates.CreateLooseEntity(
				ID,
				TryGetName(),
				TryGetDesc(),
				100f,
				true,
				Assets.GetAnim(Anim),
				"closed",
				// want it to be below ore
				Grid.SceneLayer.BuildingFront,
				EntityTemplates.CollisionShape.RECTANGLE,
				1f,
				1.1f,
				true
			);

			go.AddOrGet<GermySurpriseBox>();

			return go;
		}

		public static string TryGetName()
		{
			StringEntry OniTwitchName;
			if (Strings.TryGet("STRINGS.ITEMS.ONITWITCH.SURPRISEBOXCONFIG.NAME", out OniTwitchName))
				return OniTwitchName.String;
			return "Surprise Box";
		}

		public static string TryGetDesc()
		{
			StringEntry OniTwitchName;
			if (Strings.TryGet("STRINGS.ITEMS.ONITWITCH.SURPRISEBOXCONFIG.DESC", out OniTwitchName))
				return OniTwitchName.String;
			return "";
		}

		public void OnPrefabInit(GameObject inst)
		{
		}

		public void OnSpawn(GameObject inst)
		{
		}

		public string[] GetDlcIds()
		{
			return DlcManager.AVAILABLE_ALL_VERSIONS;
		}
	}
}
