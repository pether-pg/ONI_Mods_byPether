using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DiseasesExpanded
{
    class NanobotReplicatorConfig : IBuildingConfig
    {
        public const string ID = "NanobotReplicator";
        public const string StatusItemID = "NanobotReplicatorItem";

        public override BuildingDef CreateBuildingDef()
        {
            float[] construction_mass = new float[2] { 10_000f, 1f };
            string[] nanobotMaterial = new string[2] { SimHashes.Steel.ToString(), NanobotBottleConfig.BOTTLED_NANOBOTS_TAG.ToString() }; 
            EffectorValues tieR3_2 = TUNING.NOISE_POLLUTION.NOISY.TIER3;
            EffectorValues tieR1 = TUNING.BUILDINGS.DECOR.PENALTY.TIER1;
            EffectorValues noise = tieR3_2;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 1, 2, Kanims.NanobotReplicator, 30, 30f, construction_mass, nanobotMaterial, 800f, BuildLocationRule.OnFloor, tieR1, noise);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 60f;
            buildingDef.ExhaustKilowattsWhenActive = 0.5f;
            buildingDef.SelfHeatKilowattsWhenActive = 1f;
            buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 1));
            buildingDef.ViewMode = OverlayModes.Disease.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.Breakable = true;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
            go.AddOrGetDef<NanobotReplicator.Def>();
            go.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = MedicalNanobots.ID;
        }
    }
}