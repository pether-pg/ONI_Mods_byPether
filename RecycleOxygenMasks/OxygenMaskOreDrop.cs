using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecycleOxygenMasks
{
    class OxygenMaskOreDrop : KMonoBehaviour
    {
        public void OnDestroy()
        {
            if (this.gameObject == null) return;
            int cell = Grid.PosToCell(this.gameObject.transform.GetPosition());
            PrimaryElement primaryElement = this.gameObject.GetComponent<PrimaryElement>();

            if (primaryElement == null) return;
            Element element = primaryElement.Element;
            float temperature = primaryElement.Temperature;
            float mass = primaryElement.Mass;
            byte disease_idx = primaryElement.DiseaseIdx;
            int disease_count = primaryElement.DiseaseCount;

            if (element.substance == null) return;
            element.substance.SpawnResource(Grid.CellToPosCCC(cell, Grid.SceneLayer.Ore), mass, temperature, disease_idx, disease_count);
        }
    }
}
