using TUNING;
using UnityEngine;
using System.Collections.Generic;

namespace SignsTagsAndRibbons
{
    public class LocationTagConfig : IBuildingConfig
    {
        public const string ID = "LocationTag";

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
        }

        public override BuildingDef CreateBuildingDef()
        {
            float[] singleArray1 = new float[] { 10f };
            string[] textArray1 = new string[] { "BuildableRaw" };
            EffectorValues nONE = NOISE_POLLUTION.NONE;
            BuildingDef def1 = BuildingTemplates.CreateBuildingDef(ID, 1, 1, "location_tag_kanim", 30, 30f, singleArray1, textArray1, 1600f, BuildLocationRule.Anywhere, nONE, TUNING.NOISE_POLLUTION.NONE, 0.2f);
            def1.Floodable = false;
            def1.SceneLayer = Grid.SceneLayer.InteriorWall;
            def1.Overheatable = false;
            def1.AudioCategory = "Metal";
            def1.BaseTimeUntilRepair = -1f;
            def1.DefaultAnimState = "off";
            def1.PermittedRotations = PermittedRotations.R360;
            return def1;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            SelectableSign selectable = go.AddOrGet<SelectableSign>();
            selectable.AnimationNames = new List<string>()
            {
                "off", "art_a", "art_b", "art_c", "art_d", "art_e"
            };
        }
    }
}
