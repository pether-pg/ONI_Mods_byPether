using Klei.AI;
using TUNING;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.Configs

{
    class MorbAlienConfig : IEntityConfig
    {
        public const string ID = "AlienGlom";
        public const string BASE_TRAIT_ID = "GlomBaseTrait";

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public GameObject CreatePrefab()
        {
            string name = "Little Alien";
            string desc = "Cute little and totally harmless alien.";
            EffectorValues tieR0 = TUNING.DECOR.BONUS.TIER0;
            KAnimFile anim = Assets.GetAnim((HashedString)"glom_kanim");
            EffectorValues decor = tieR0;
            EffectorValues noise = new EffectorValues();
            GameObject placedEntity = EntityTemplates.CreatePlacedEntity(ID, name, desc, 25f, anim, "idle_loop", Grid.SceneLayer.Creatures, 1, 1, decor, noise);
            Db.Get().CreateTrait(BASE_TRAIT_ID, name, name, (string)null, false, (ChoreGroup[])null, true, true).Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name));
            KPrefabID component = placedEntity.GetComponent<KPrefabID>();
            component.AddTag(GameTags.Creatures.Walker);
            component.AddTag(GameTags.OriginalCreature);
            component.prefabInitFn += (KPrefabID.PrefabFn)(inst => inst.GetAttributes().Add(Db.Get().Attributes.MaxUnderwaterTravelCost));
            EntityTemplates.ExtendEntityToBasicCreature(placedEntity, FactionManager.FactionID.Pest, BASE_TRAIT_ID, onDeathDropID: "", onDeathDropCount: 0, warningLowTemperature: 293.15f, warningHighTemperature: 393.15f, lethalLowTemperature: 273.15f, lethalHighTemperature: 423.15f);
            placedEntity.AddWeapon(1f, 1f);
            placedEntity.AddOrGet<Trappable>();
            placedEntity.AddOrGetDef<ThreatMonitor.Def>();
            placedEntity.AddOrGetDef<CreatureFallMonitor.Def>();
            
            placedEntity.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = 0;
            placedEntity.AddOrGet<LoopingSounds>();
            placedEntity.GetComponent<LoopingSounds>().updatePosition = true;
            SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_movement_short", NOISE_POLLUTION.CREATURES.TIER2);
            SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_jump", NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_land", NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("glom_kanim", "Morb_expel", NOISE_POLLUTION.CREATURES.TIER4);
            EntityTemplates.CreateAndRegisterBaggedCreature(placedEntity, true, false);
            EntityTemplates.AddCreatureBrain(placedEntity, new ChoreTable.Builder().Add((StateMachine.BaseDef)new DeathStates.Def()).Add((StateMachine.BaseDef)new TrappedStates.Def()).Add((StateMachine.BaseDef)new BaggedStates.Def()).Add((StateMachine.BaseDef)new FallStates.Def()).Add((StateMachine.BaseDef)new StunnedStates.Def()).Add((StateMachine.BaseDef)new DrowningStates.Def()).Add((StateMachine.BaseDef)new DebugGoToStates.Def()).Add((StateMachine.BaseDef)new FleeStates.Def()).Add((StateMachine.BaseDef)new DropElementStates.Def()).Add((StateMachine.BaseDef)new IdleStates.Def()), GameTags.Creatures.Species.GlomSpecies, (string)null);
            return placedEntity;
        }

        public void OnPrefabInit(GameObject prefab)
        {
            GermCarrier carrier = prefab.AddComponent<GermCarrier>();
            carrier.germId = AlienGerms.ID;
        }

        public void OnSpawn(GameObject inst)
        {
            KBatchedAnimController kbac = inst.GetComponent<KBatchedAnimController>();
            if (kbac == null)
                return;
            kbac.TintColour = new Color32(32, 32, 255, 255);
        }
    }
}