using System;
using TUNING;
using UnityEngine;

namespace SignsTagsAndRibbons
{
	public class NumbersTagConfig : IBuildingConfig
	{
        public const string ID = "NumbersTag";

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
		}

		public override BuildingDef CreateBuildingDef()
		{
			float[] construction_mass = new float[]
			{
				10f
			};
			string[] construction_materials = new string[]
			{
				"BuildableRaw"
			};
			EffectorValues none = NOISE_POLLUTION.NONE;
			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 1, 1, "number_tag_kanim", 30, 10f, construction_mass, construction_materials, 1600f, BuildLocationRule.Anywhere, none, NOISE_POLLUTION.NONE, 0.2f);
			buildingDef.Floodable = false;
			buildingDef.SceneLayer = Grid.SceneLayer.InteriorWall;
			buildingDef.Overheatable = false;
			buildingDef.AudioCategory = "Metal";
			buildingDef.BaseTimeUntilRepair = -1f;
			buildingDef.DefaultAnimState = "off";
			return buildingDef;
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			SelectableSign selectable = go.AddOrGet<SelectableSign>();
			selectable.AnimationNames = new System.Collections.Generic.List<string>() 
			{
				"off", "art_a", "art_b", "art_c", "art_d", "art_e", "art_f", "art_g", "art_h", "art_i", "art_j"
			};
		}
	}
}
