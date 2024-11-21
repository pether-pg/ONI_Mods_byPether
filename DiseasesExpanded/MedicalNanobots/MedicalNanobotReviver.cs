using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded
{
    class MedicalNanobotReviver : KMonoBehaviour, ISim1000ms
    {
        public void Sim1000ms(float dt)
        {
            PrimaryElement prime = this.gameObject.GetComponent<PrimaryElement>();
            if (prime == null)
                return;
            int count = prime.DiseaseCount;
            int delta = (prime.DiseaseIdx == GermIdx.MedicalNanobotsIdx) ? CalculateDeltaGerms(count) : NanobotBottleConfig.SPAWNED_BOTS_COUNT;
            prime.AddDisease(GermIdx.MedicalNanobotsIdx, delta, "Nanobot Revival");
        }
        
        private int CalculateDeltaGerms(int currentGerms)
        {
            int delta = NanobotBottleConfig.SPAWNED_BOTS_COUNT - currentGerms;
            return delta;
        }
    }
}
