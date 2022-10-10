using System;
using System.Collections.Generic;
using UnityEngine;
using TUNING;
using TodoList;

namespace InterplanarInfrastructure
{
    class RadiationLaserConfig : IBuildingConfig
    {
        public const string ID = "RadiationLaser";

        public override string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public override BuildingDef CreateBuildingDef()
        {
            Todo.Note("Adjust building materials and cost");
            Todo.Note("Provide final kanim");
            float[] tieR4 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
            string[] allMetals = MATERIALS.ALL_METALS;
            EffectorValues tieR5 = TUNING.NOISE_POLLUTION.NOISY.TIER5;
            EffectorValues none = TUNING.BUILDINGS.DECOR.NONE;
            EffectorValues noise = tieR5;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 5, 6, "rail_gun_kanim", 250, 30f, tieR4, allMetals, 1600f, BuildLocationRule.OnFloor, none, noise);
            buildingDef.Floodable = false;
            buildingDef.Overheatable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.BaseTimeUntilRepair = 400f;
            buildingDef.DefaultAnimState = "off";
            buildingDef.RequiresPowerInput = true;
            buildingDef.PowerInputOffset = new CellOffset(-2, 0);
            buildingDef.EnergyConsumptionWhenActive = 240f;
            buildingDef.ViewMode = OverlayModes.Power.ID;
            buildingDef.ExhaustKilowattsWhenActive = 0.5f;
            buildingDef.SelfHeatKilowattsWhenActive = 2f;
            buildingDef.UseHighEnergyParticleInputPort = true;
            buildingDef.HighEnergyParticleInputOffset = new CellOffset(-2, 1);
            buildingDef.LogicInputPorts = new List<LogicPorts.Port>()
            {
              LogicPorts.Port.InputPort(RailGun.PORT_ID, new CellOffset(-2, 2), (string) STRINGS.BUILDINGS.PREFABS.RAILGUN.LOGIC_PORT, (string) STRINGS.BUILDINGS.PREFABS.RAILGUN.LOGIC_PORT_ACTIVE, (string) STRINGS.BUILDINGS.PREFABS.RAILGUN.LOGIC_PORT_INACTIVE)
            };
            buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
            {
              LogicPorts.Port.OutputPort((HashedString) "HEP_STORAGE", new CellOffset(2, 0), (string) STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE, (string) STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_ACTIVE, (string) STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_INACTIVE)
            };

            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            RadiationLaser radiationLaser = go.AddOrGet<RadiationLaser>();
            go.AddOrGet<LoopingSounds>();
            ClusterDestinationSelector destinationSelector = go.AddOrGet<ClusterDestinationSelector>();
            destinationSelector.assignable = true;
            destinationSelector.requireAsteroidDestination = true;
            HighEnergyParticleStorage energyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
            energyParticleStorage.capacity = 1000f;
            energyParticleStorage.autoStore = true;
            energyParticleStorage.showInUI = false;
            energyParticleStorage.PORT_ID = "HEP_STORAGE";
            energyParticleStorage.showCapacityStatusItem = true;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}
