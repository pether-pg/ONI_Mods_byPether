using System.Collections.Generic;
using UnityEngine;
using TUNING;

namespace InterplanarInfrastructure
{
    class SolarLenseSateliteConfig : IEntityConfig
    {
        public const string ID = "SolarLenseSatelite";
        public const string StatusItemID = "SolarLenseSateliteStatusItem";
        
        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public GameObject CreatePrefab()
        {
            string name = (string)"Solar Lense Lance";
            string desc = (string)"Uses space illumination to heat up tiles bellow.";
            EffectorValues tieR0_1 = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
            EffectorValues tieR0_2 = TUNING.NOISE_POLLUTION.NOISY.TIER0;
            KAnimFile anim = Assets.GetAnim((HashedString)"solar_panel_kanim");
            EffectorValues decor = tieR0_1;
            EffectorValues noise = tieR0_2;
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name, desc, 400f, anim, "grounded", Grid.SceneLayer.Building, 7, 1, decor, noise);
            placedEntity.AddOrGetDef<CargoLander.Def>().previewTag = "PioneerLander_Preview".ToTag();
            CargoDropperMinion.Def def = placedEntity.AddOrGetDef<CargoDropperMinion.Def>();
            def.kAnimName = "anim_interacts_pioneer_cargo_lander_kanim";
            def.animName = "enter";
            placedEntity.AddOrGet<Prioritizable>();
            Prioritizable.AddRef(placedEntity);
            placedEntity.AddOrGet<Operational>();
            placedEntity.AddOrGet<Deconstructable>().audioSize = "large";
            placedEntity.AddOrGet<Storable>();
            Placeable placeable = placedEntity.AddOrGet<Placeable>();
            placeable.kAnimName = "rocket_pioneer_cargo_lander_kanim";
            placeable.animName = "place";
            placeable.placementRules = new List<Placeable.PlacementRules>()
            {
              Placeable.PlacementRules.VisibleToSpace,
              Placeable.PlacementRules.RestrictToWorld
            };
            EntityTemplates.CreateAndRegisterPreview("PioneerLander_Preview", Assets.GetAnim((HashedString)"rocket_pioneer_cargo_lander_kanim"), "place", ObjectLayer.Building, 7, 1);

            placedEntity.AddComponent<SolarLenseSatelite>();

            return placedEntity;
        }

        public void OnPrefabInit(GameObject inst)
        {
            OccupyArea component = inst.GetComponent<OccupyArea>();
            component.ApplyToCells = false;
            component.objectLayers = new ObjectLayer[1]
            {
                ObjectLayer.Building
            };
        }

        public void OnSpawn(GameObject inst)
        {
        }

        /*
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
            buildingDef.ViewMode = OverlayModes.Light.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.AudioSize = "large";
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery);
            go.AddOrGet<LoopingSounds>();
            Prioritizable.AddRef(go);
            go.AddOrGet<SolarLenseSatelite>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
        */
    }
}
