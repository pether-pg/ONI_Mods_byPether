using System.Collections.Generic;
using UnityEngine;
using TUNING;

namespace DiseasesExpanded
{
    class ShieldGeneratorConfig : IBuildingConfig
    {
        public const string ID = "ShieldGenerator";
        public const string StatusItemID = "ShieldGeneratorStatusItem";
        public override string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public override BuildingDef CreateBuildingDef()
        {
            float[] materialMass = BUILDINGS.CONSTRUCTION_MASS_KG.TIER7;
            string[] materials = new string[1] { SimHashes.Steel.ToString() };
            EffectorValues none = NOISE_POLLUTION.NONE;
            EffectorValues tieR0_2 = BUILDINGS.DECOR.PENALTY.TIER0;
            EffectorValues noise = none;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 2, 4, "meteor_detector_kanim", 30, 30f, materialMass, materials, 1600f, BuildLocationRule.OnFloor, tieR0_2, noise);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 1200f;
            buildingDef.Overheatable = false;
            buildingDef.Floodable = true;
            buildingDef.Entombable = true;
            buildingDef.ViewMode = OverlayModes.Disease.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            return buildingDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddComponent<ShieldGenerator>();
        }
    }
}
