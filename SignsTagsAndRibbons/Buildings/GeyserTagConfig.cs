using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace SignsTagsAndRibbons
{
    public class GeyserTagConfig : IBuildingConfig
    {
        public const string ID = "GeyserTag";

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
        }

        public override BuildingDef CreateBuildingDef()
        {
            float[] singleArray1 = new float[] { 10f };
            string[] textArray1 = new string[] { "BuildableRaw" };

            EffectorValues decor = NOISE_POLLUTION.NONE;
            BuildingDef def = BuildingTemplates.CreateBuildingDef(ID, 2, 2, "geyser_tag_kanim", 30, 10f, singleArray1, textArray1, 1600f, BuildLocationRule.Anywhere, decor, TUNING.NOISE_POLLUTION.NONE, 0.2f);
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
                "art_hot_water",
                "art_cool_slush",
                "art_cont_pol_water",
                "art_hot_salt_water",
                "art_cool_brine",
                "art_liquid_sulfur",
                "art_liquid_co2",
                "art_oil_fissure",
                "art_oil_reservoir",
                "art_cont_pol_oxygen",
                "art_hot_pol_oxygen",
                "art_hot_co2",
                "art_hot_hydrogen",
                "art_hot_chlorine",
                "art_hot_methane",
                "art_cool_steam",
                "art_hot_steam",
                "art_minor_volcano",
                "art_big_volcano",
                "art_copper_volcano",
                "art_iron_volcano",
                "art_gold_volcano",
                "art_aluminum_volcano",
                "art_cobalt_volcano",
                "art_tungsten_volcano",
                "art_niobium_volcano"
            };
        }
    }
}
