using System.Collections.Generic;
using UnityEngine;
using TodoList;

namespace InterplanarInfrastructure
{
    class SolarLenseSateliteConfig : IEntityConfig
    {
        public const string ID = "SolarLenseSatelite";
        public const string StatusItemID = "SolarLenseSateliteStatusItem";

        private string note = Todo.Note("Use final kanim here");
        public const string PlacableKAnim = "solar_panel_kanim";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public GameObject CreatePrefab()
        {
            string name = (string)"Solar Lense Lance";
            string desc = (string)"Uses space illumination to heat up tiles bellow.";
            EffectorValues tieR0_1 = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
            EffectorValues tieR0_2 = TUNING.NOISE_POLLUTION.NOISY.TIER0;
            Todo.Note("Needs new kanim. The kanim should have height 2 more than the building size (so 3 in case of 1) to make it appear above the end of asteroid.");
            Todo.Note("I imagine the building as Stargazer-like glass dome on metal ring hovering in space. Additionally some solar beam would be nice to indicate how the building works");
            KAnimFile anim = Assets.GetAnim((HashedString)"solar_panel_kanim");
            EffectorValues decor = tieR0_1;
            EffectorValues noise = tieR0_2;
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name, desc, 400f, anim, "grounded", Grid.SceneLayer.Building, 7, 1, decor, noise);
            placedEntity.AddOrGetDef<CargoLander.Def>().previewTag = PlacableKAnim.ToTag();
            CargoDropperMinion.Def def = placedEntity.AddOrGetDef<CargoDropperMinion.Def>();
            def.kAnimName = PlacableKAnim;
            def.animName = "enter";
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
            // LogicPorts with empty outputPorts list is required not to crash LogicBroadcastReceiver. 
            // The building does not use ports, instead it is remotely enabled/disabled by signal available in LogicBroadcastReceiver
            inst.AddOrGet<LogicPorts>().outputPorts = new List<ILogicUIElement>();
            inst.AddOrGet<LogicBroadcastReceiver>();
        }
    }
}
