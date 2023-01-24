using System.Collections.Generic;
using UnityEngine;
using TUNING;
namespace Dupes_Aromatics
{
    class VaporizerConfig : IBuildingConfig
    {
        public const string ID = "Vaporizer";
        public override BuildingDef CreateBuildingDef()
        {
            float[] materialMass = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
            string[] materials = MATERIALS.RAW_MINERALS;
            EffectorValues none = NOISE_POLLUTION.NONE;
            EffectorValues tieR0_2 = BUILDINGS.DECOR.PENALTY.TIER0;
            EffectorValues noise = none;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 1, 1, "aromatics_vaporizer_kanim", 30, 30f, materialMass, materials, 1600f, BuildLocationRule.Anywhere, tieR0_2, noise);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 120f;
            buildingDef.Overheatable = false;
            buildingDef.Floodable = false;
            buildingDef.Entombable = true;
            buildingDef.ViewMode = OverlayModes.Disease.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            return buildingDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<Operational>();
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGet<ComplexFabricatorWorkable>();
            go.AddOrGetDef<VaporizerController.Def>();
            AromaticsFabricator af = go.AddOrGet<AromaticsFabricator>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, af);
        }
    }
}
