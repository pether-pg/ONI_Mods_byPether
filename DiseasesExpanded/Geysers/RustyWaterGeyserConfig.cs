using System.Collections.Generic;
using UnityEngine;
using TUNING;
using Klei;

namespace DiseasesExpanded
{
    class RustyWaterGeyserConfig : IEntityConfig, IHasDlcRestrictions
    {
        public const string ID = "RustyWaterGeyser";

        public GameObject CreatePrefab()
        {
            GeyserConfigurator.GeyserType geyserType = RustyWaterGeyser_Data.GetGeyserType();

            GameObject geyser = GeyserGenericConfig.CreateGeyser(
                ID, 
                Kanims.RustyWaterGeyser,
                RustyWaterGeyser_Data.WIDTH,
                RustyWaterGeyser_Data.HEIGHT, 
                STRINGS.GEYSERS.RUSTY_WATER.NAME,
                STRINGS.GEYSERS.RUSTY_WATER.DESC, 
                geyserType.idHash, 
                geyserType.geyserTemperature,
                (string[])null, 
                (string[])null
                );
            
            return geyser;
        }

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }
        public string[] GetDlcIds()
        {
            return null;
        }

        public string[] GetRequiredDlcIds() => DlcManager.EXPANSION1;

        public string[] GetForbiddenDlcIds() => (string[])null;
    }
}
