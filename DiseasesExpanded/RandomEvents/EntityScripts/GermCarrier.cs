using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DiseasesExpanded.RandomEvents
{
    class GermCarrier : KMonoBehaviour
    {
        public string germId = string.Empty;

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();

            if (string.IsNullOrEmpty(germId))
                return;

            DiseaseDropper.Def def = this.gameObject.AddOrGetDef<DiseaseDropper.Def>();
            def.averageEmitPerSecond = 1000;
            def.singleEmitQuantity = 10000000;
            def.diseaseIdx = Db.Get().Diseases.GetIndex(germId);

            this.gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = germId;
        }

        public void OnDestroy()
        {
            DiseaseDropper.Def def = this.gameObject.GetDef<DiseaseDropper.Def>();
            if (def != null) 
                Game.Instance.StartCoroutine(SpawnPostDeathGerms(Grid.PosToCell(this.gameObject), def.diseaseIdx, def.singleEmitQuantity));
        }

        private IEnumerator SpawnPostDeathGerms(int cell, byte idx, int totalCount)
        {
            int chunks = 5;
            for(int i=0; i<chunks; i++)
            {
                SimMessages.ModifyDiseaseOnCell(cell, idx, totalCount / chunks);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
