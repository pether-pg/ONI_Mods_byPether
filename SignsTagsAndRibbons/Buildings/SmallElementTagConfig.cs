using TUNING;
using UnityEngine;
using System.Collections.Generic;

namespace SignsTagsAndRibbons
{
    public class SmallElementTagConfig : IBuildingConfig
    {
        public const string ID = "SmallElementTag";

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
        }

        public override BuildingDef CreateBuildingDef()
        {
            float[] singleArray1 = new float[] { 10f };
            string[] textArray1 = new string[] { "BuildableRaw" };

            EffectorValues decor = NOISE_POLLUTION.NONE;
            BuildingDef def = BuildingTemplates.CreateBuildingDef(ID, 1, 1, "small_element_tags_kanim", 30, 10f, singleArray1, textArray1, 1600f, BuildLocationRule.Anywhere, decor, TUNING.NOISE_POLLUTION.NONE, 0.2f);
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
                "off", "art_brackene", "art_brine_water", "art_carbon_dioxide_gas", "art_carbon_dioxide_liquid", "art_chlorine_gas", "art_chlorine_liquid",
                "art_crude_oil_liquid", "art_ethanol_liquid", "art_fallout_gas", "art_helium_gas", "art_hydrogen_gas", "art_hydrogen_liquid", "art_methane_gas",
                "art_methane_liquid", "art_naphtha", "art_oxygen_gas", "art_oxygen_liquid", "art_petrolleum_liquid", "art_polluted_oxygen_gas", "art_polluted_water",
                "art_propane_gas", "art_propane_liquid", "art_radwaste_liquid", "art_salt_water", "art_sour_gas", "art_steam_gas", "art_super_coolant", "art_visco_gel",
                "art_water"
            };
        }
    }
}
