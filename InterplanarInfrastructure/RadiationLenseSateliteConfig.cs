using System;
using System.Collections.Generic;
using UnityEngine;
using TUNING;

namespace InterplanarInfrastructure
{
    class RadiationLenseSateliteConfig : IBuildingConfig
    {
        public const string ID = "RadiationLenseSatelite";
        public const string StatusItemID = "RadiationLenseSateliteStatusItem";
        public override string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public override BuildingDef CreateBuildingDef()
        {
            float[] tieR3 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
            string[] glasses = MATERIALS.GLASSES;
            EffectorValues tieR5 = NOISE_POLLUTION.NOISY.TIER5;
            EffectorValues tieR2 = BUILDINGS.DECOR.PENALTY.TIER2;
            EffectorValues noise = tieR5;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 7, 1, "solar_panel_kanim", 100, 120f, tieR3, glasses, 2400f, BuildLocationRule.Anywhere, tieR2, noise);
            buildingDef.BuildLocationRule = BuildLocationRule.Anywhere;
            buildingDef.HitPoints = 10;
            buildingDef.UseHighEnergyParticleOutputPort = true;
            buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 0);
            buildingDef.ViewMode = OverlayModes.Radiation.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.AudioSize = "large";
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery);
            go.AddOrGet<LoopingSounds>();
            Prioritizable.AddRef(go);
            go.AddOrGet<HighEnergyParticleStorage>().capacity = 500f;
            RadiationLenseSatelite energyParticleSpawner = go.AddOrGet<RadiationLenseSatelite>();
            energyParticleSpawner.minLaunchInterval = 2f;
            energyParticleSpawner.radiationSampleRate = 0.2f;
            energyParticleSpawner.minSlider = 50;
            energyParticleSpawner.maxSlider = 500;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<Repairable>().expectedRepairTime = 52.5f;
        }
    }
}
