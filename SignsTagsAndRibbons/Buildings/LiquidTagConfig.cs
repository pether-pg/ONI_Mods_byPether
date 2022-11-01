using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace SignsTagsAndRibbons
{
    public class LiquidTagConfig : IBuildingConfig
    {
        public const string ID = "LiquidTag";

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
        }

        public override BuildingDef CreateBuildingDef()
        {
            float[] singleArray1 = new float[] { 10f };
            string[] textArray1 = new string[] { "BuildableRaw" };

            EffectorValues decor = NOISE_POLLUTION.NONE;
            BuildingDef def = BuildingTemplates.CreateBuildingDef(ID, 2, 2, "liquid_tag_kanim", 30, 10f, singleArray1, textArray1, 1600f, BuildLocationRule.Anywhere, decor, TUNING.NOISE_POLLUTION.NONE, 0.2f);
            def.Floodable = false;
            def.SceneLayer = Grid.SceneLayer.InteriorWall;
            def.Overheatable = false;
            def.AudioCategory = "Metal";
            def.BaseTimeUntilRepair = -1f;
            def.DefaultAnimState = "off";
            return def;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            SelectableSign selectable = go.AddOrGet<SelectableSign>();
            selectable.AnimationNames = new List<string>()
            {
                "off", "art_a", "art_b", "art_c","art_d","art_e","art_f","art_g","art_h","art_i","art_j","art_l","art_m","art_n","art_o",
            };
        }
    }
}
