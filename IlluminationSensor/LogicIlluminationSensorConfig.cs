using System.Collections.Generic;
using UnityEngine;
using TUNING;
using STRINGS;

namespace IlluminationSensor
{
    public class LogicIlluminationSensorConfig : IBuildingConfig
    {
        public static string ID = "LogicIlluminationSensor";

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
            buildingDef.LogicOutputPorts.Add(LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), STRINGS.ILLUMINATIONSENSOR.LOGIC_PORT, STRINGS.ILLUMINATIONSENSOR.LOGIC_PORT_ACTIVE, STRINGS.ILLUMINATIONSENSOR.LOGIC_PORT_INACTIVE, true));
            SoundEventVolumeCache.instance.AddVolume("luxsensor_kanim", "on", TUNING.NOISE_POLLUTION.NOISY.TIER3);
            SoundEventVolumeCache.instance.AddVolume("luxsensor_kanim", "off", TUNING.NOISE_POLLUTION.NOISY.TIER3);
            GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicIlluminationSensorConfig.ID);
            buildingDef.Deprecated = true;
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
