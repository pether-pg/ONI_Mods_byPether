using System.Collections.Generic;
using UnityEngine;
using TUNING;

namespace DiseasesExpanded
{
    class GermcatcherConfig : IBuildingConfig
    {
        public const string ID = "Germcatcher";
        public const string StatusItemID = "Germcatcher";

        public override BuildingDef CreateBuildingDef()
        {
            string id = GermcatcherConfig.ID;
            float[] materialMass = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
            string[] materials = MATERIALS.RAW_MINERALS;
            EffectorValues none = NOISE_POLLUTION.NONE;
            EffectorValues tieR0_2 = BUILDINGS.DECOR.PENALTY.TIER0;
            EffectorValues noise = none;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, 1, 2, "relocator_dropoff_kanim", 30, 30f, materialMass, materials, 1600f, BuildLocationRule.OnFloor, tieR0_2, noise);
            buildingDef.Overheatable = false;
            buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.ViewMode = OverlayModes.Disease.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            buildingDef.AlwaysOperational = true;
            return buildingDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<Germcatcher>();
        }
    }
}
