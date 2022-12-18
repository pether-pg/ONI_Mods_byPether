using UnityEngine;

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
                SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(this.gameObject), def.diseaseIdx, def.singleEmitQuantity);

        }
    }
}
