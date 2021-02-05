using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace InterplanarAutomation
{   

    public class RadioTowerConfig : IBuildingConfig
    {
        public static readonly string ID = "InterplanarRadioTower";
        public static readonly string Name = "Interplanar Radio Tower";
        public static readonly string Effect = "Allows to broadcast 4-bit Logic Signals and to receive them on another asteroids. Must be powered and exposed to space.";
        public static readonly string Description = "Radio Towers are mostly used to send images of funny Pipsqueaks and while listening to lofi dupe hop radio best hits.";

        private static readonly string GreenSignal = (string)STRINGS.UI.FormatAsAutomationState("Green Signal", STRINGS.UI.AutomationState.Active);
        private static readonly string RedSignal = (string)STRINGS.UI.FormatAsAutomationState("Red Signal", STRINGS.UI.AutomationState.Standby);

        public static readonly HashedString SnederRadioPortId = (HashedString)"SnederRadioPortId";
        public static readonly string InputDescription = "Transmited Signal";
        public static readonly string InputDescriptionActive = $"Broadcasts {GreenSignal} to be received by Radio Towers.";
        public static readonly string InputDescriptionInactive = $"If {RedSignal} is connected, Radio Tower broadcasts no signal.";

        public static readonly HashedString ReceiverRadioPortId = (HashedString)"ReceiverRadioPortId";
        public static readonly string OutputDescription = "Received Transmission";
        public static readonly string OutputDescriptionActive = $"Outputs {GreenSignal} if any of Radio Towers is broadcasting a signal.";
        public static readonly string OutputDescriptionInactive = $"If no signal was received, outputs {RedSignal}.";

        public override BuildingDef CreateBuildingDef()
        {
            string id = RadioTowerConfig.ID;
            float[] tier6 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER6;
            string[] refinedMetals = MATERIALS.REFINED_METALS;
            EffectorValues none = TUNING.NOISE_POLLUTION.NONE;
            EffectorValues tier4 = TUNING.BUILDINGS.DECOR.PENALTY.TIER4;
            EffectorValues noise = none;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, 2, 4, "meteor_detector_kanim", 30, 30f, tier6, refinedMetals, 1600f, BuildLocationRule.OnFloor, tier4, noise);
            buildingDef.Overheatable = false;
            buildingDef.Floodable = true;
            buildingDef.Entombable = true;
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 1200f;
            buildingDef.ViewMode = OverlayModes.Logic.ID;
            buildingDef.AudioCategory = "Metal";
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            buildingDef.LogicInputPorts = new List<LogicPorts.Port>()
            {
              LogicPorts.Port.RibbonInputPort(SnederRadioPortId, new CellOffset(0, 0), InputDescription, InputDescriptionActive, InputDescriptionInactive)
            };
            buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
            {
              LogicPorts.Port.RibbonOutputPort(ReceiverRadioPortId, new CellOffset(1, 0), OutputDescription, OutputDescriptionActive, OutputDescriptionInactive)
            };
            SoundEventVolumeCache.instance.AddVolume("world_element_sensor_kanim", "PowerSwitch_on", TUNING.NOISE_POLLUTION.NOISY.TIER3);
            SoundEventVolumeCache.instance.AddVolume("world_element_sensor_kanim", "PowerSwitch_off", TUNING.NOISE_POLLUTION.NOISY.TIER3);
            GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, RadioTowerConfig.ID);
            return buildingDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery);
            go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits);
            go.AddOrGetDef<RadioTowerStates.Def>();
            go.AddOrGet<RadioTower>();
        }
    }

}
