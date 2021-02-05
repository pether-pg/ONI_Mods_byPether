using System.Collections.Generic;
using UnityEngine;
using TUNING;

namespace IlluminationSensor
{
    public class LogicIlluminationSensorConfig : IBuildingConfig
    {
        private static string GreenSignal = (string)STRINGS.UI.FormatAsAutomationState("Green Signal", STRINGS.UI.AutomationState.Active);
        private static string RedSignal = (string)STRINGS.UI.FormatAsAutomationState("Red Signal", STRINGS.UI.AutomationState.Standby);
        private static string IlluminationKey = STRINGS.UI.PRE_KEYWORD + "Illumination" + STRINGS.UI.PST_KEYWORD;
        private static string IlluminationLink = STRINGS.UI.FormatAsLink("Illumination", "Light");

        private static string LOGIC_PORT = "Surrounding " + IlluminationLink;
        private static string LOGIC_PORT_ACTIVE = "Sends a " + GreenSignal + " if " + IlluminationLink + " is within the selected range";
        private static string LOGIC_PORT_INACTIVE = "Otherwise, sends a " + RedSignal;

        public static string ID = "LogicIlluminationSensor";
        public static string Name = "Illumination Sensor";
        public static string Description = "Illumination sensors can turn additional light sources on when the surrounding is too dim.";
        public static string Efect = "Sends a " + GreenSignal + " or a " + RedSignal + " when " + IlluminationLink + " enters the chosen range.";
        public static string TooltipPatternAbove = "Will send a " + GreenSignal + " if the " + IlluminationKey + " is above <b>{0} Lux</b>";
        public static string TooltipPatternBelow = "Will send a " + GreenSignal + " if the " + IlluminationKey + " is below <b>{0} Lux</b>";


        public override BuildingDef CreateBuildingDef()
        {
            string id = LogicIlluminationSensorConfig.ID;
            float[] tieR0_1 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
            string[] materials = MATERIALS.REFINED_METALS;
            EffectorValues none = TUNING.NOISE_POLLUTION.NONE;
            EffectorValues tieR0_2 = TUNING.BUILDINGS.DECOR.PENALTY.TIER0;
            EffectorValues noise = none;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, 1, 1, "luxsensor_kanim", 30, 30f, tieR0_1, materials, 1600f, BuildLocationRule.Anywhere, tieR0_2, noise);
            buildingDef.Overheatable = false;
            buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.ViewMode = OverlayModes.Logic.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            buildingDef.AlwaysOperational = true;
            buildingDef.LogicOutputPorts = new List<LogicPorts.Port>();
            buildingDef.LogicOutputPorts.Add(LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), LOGIC_PORT, LOGIC_PORT_ACTIVE, LOGIC_PORT_INACTIVE, true));
            SoundEventVolumeCache.instance.AddVolume("luxsensor_kanim", "on", TUNING.NOISE_POLLUTION.NOISY.TIER3);
            SoundEventVolumeCache.instance.AddVolume("luxsensor_kanim", "off", TUNING.NOISE_POLLUTION.NOISY.TIER3);
            GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicIlluminationSensorConfig.ID);
            return buildingDef;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            LogicIlluminationSensor illuminationSensor = go.AddOrGet<LogicIlluminationSensor>();
            illuminationSensor.manuallyControlled = false;
            go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits);
        }
    }
}
