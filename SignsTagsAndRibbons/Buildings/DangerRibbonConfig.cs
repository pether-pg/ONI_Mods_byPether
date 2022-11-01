using TUNING;
using UnityEngine;

namespace SignsTagsAndRibbons
{
    public class DangerRibbonConfig : IBuildingConfig
    {
        public const string ID = "DangerRibbon";

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
        }

        public override BuildingDef CreateBuildingDef()
        {
            float[] singleArray1 = new float[] { 10f };
            string[] textArray1 = new string[] { "BuildableRaw" };
            EffectorValues nONE = NOISE_POLLUTION.NONE;
            BuildingDef def1 = BuildingTemplates.CreateBuildingDef(ID, 1, 1, "tag_dangerribbon_kanim", 30, 30f, singleArray1, textArray1, 1600f, BuildLocationRule.Anywhere, nONE, TUNING.NOISE_POLLUTION.NONE, 0.2f);
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
            GeneratedBuildings.RemoveLoopingSounds(go);
        }
    }
}
