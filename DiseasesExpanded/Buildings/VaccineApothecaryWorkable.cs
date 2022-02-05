using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded
{
    class VaccineApothecaryWorkable : ComplexFabricatorWorkable
    {
        protected override void OnSpawn()
        {
            EnableRadiationEmitter(false);
            base.OnSpawn();
        }

        protected override void OnStartWork(Worker worker)
        {
            EnableRadiationEmitter(true);
            base.OnStartWork(worker);
        }

        protected override void OnStopWork(Worker worker)
        {
            EnableRadiationEmitter(false);
            base.OnStopWork(worker);
        }

        protected override void OnCompleteWork(Worker worker)
        {
            EnableRadiationEmitter(false);
            base.OnCompleteWork(worker);
        }

        private void EnableRadiationEmitter(bool enable)
        {
            RadiationEmitter emitter = this.gameObject.GetComponent<RadiationEmitter>();
            if (emitter != null)
            {
                emitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
                emitter.SetEmitting(enable);
            }
        }
    }
}
