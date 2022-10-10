using System;
using System.Collections.Generic;
using UnityEngine;
using TodoList;

namespace InterplanarInfrastructure
{
    class RadiationLenseSateliteConfig : IEntityConfig
    {
        public const string ID = "RadiationLenseSatelite";
        public const string StatusItemID = "RadiationLenseSateliteStatusItem";

        private string note = Todo.Note("Use final kanim here");
        public const string PlacableKAnim = "solar_panel_kanim";
        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public GameObject CreatePrefab()
        {
            string name = (string)"Radiation Lense Satelite";
            string desc = (string)"Collects space radiation and condenses it into radbolts";
            EffectorValues tieR0_1 = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
            EffectorValues tieR0_2 = TUNING.NOISE_POLLUTION.NOISY.TIER0;
            Todo.Note("Needs new kanim. The kanim should have height 2 more than the building size (so 3 in case of 1) to make it appear above the end of asteroid.");
            Todo.Note("I imagine the building as Stargazer-like glass dome on metal ring hovering in space. On top of that some green radbolt collector-like hexes and HEP output port");
            KAnimFile anim = Assets.GetAnim((HashedString)"solar_panel_kanim");
            EffectorValues decor = tieR0_1;
            EffectorValues noise = tieR0_2;
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name, desc, 400f, anim, "grounded", Grid.SceneLayer.Building, 7, 1, decor, noise);
            placedEntity.AddOrGetDef<CargoLander.Def>().previewTag = PlacableKAnim.ToTag();
            CargoDropperMinion.Def def = placedEntity.AddOrGetDef<CargoDropperMinion.Def>();
            def.kAnimName = PlacableKAnim;// "anim_interacts_pioneer_cargo_lander_kanim";
            def.animName = "place";//"enter";
            placedEntity.AddOrGet<Prioritizable>();
            Prioritizable.AddRef(placedEntity);
            placedEntity.AddOrGet<Operational>();
            placedEntity.AddOrGet<Deconstructable>().audioSize = "large";
            placedEntity.AddOrGet<Storable>();
            Placeable placeable = placedEntity.AddOrGet<Placeable>();
            placeable.kAnimName = PlacableKAnim;
            placeable.animName = "place";
            placeable.placementRules = new List<Placeable.PlacementRules>()
            {
              Placeable.PlacementRules.VisibleToSpace,
              Placeable.PlacementRules.RestrictToWorld
            };
            EntityTemplates.CreateAndRegisterPreview(PlacableKAnim, Assets.GetAnim((HashedString)PlacableKAnim), "place", ObjectLayer.Building, 7, 1);


            placedEntity.AddOrGet<HighEnergyParticleStorage>().capacity = 500f;
            RadiationLenseSatelite energyParticleSpawner = placedEntity.AddOrGet<RadiationLenseSatelite>();
            energyParticleSpawner.minLaunchInterval = 2f;
            energyParticleSpawner.radiationSampleRate = 0.2f;
            energyParticleSpawner.minSlider = 50;
            energyParticleSpawner.maxSlider = 500;

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
            Todo.Note("I couldn't make it appear :(");
            HighEnergyParticlePort energyParticlePort = inst.AddOrGet<HighEnergyParticlePort>();
            energyParticlePort.particleOutputOffset = new CellOffset(0, 0);
            energyParticlePort.particleOutputEnabled = true;
            energyParticlePort.particleInputEnabled = false;
        }
    }
}
