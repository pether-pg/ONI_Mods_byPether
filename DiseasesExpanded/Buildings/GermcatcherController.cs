using UnityEngine;
using System.Collections.Generic;
using KSerialization;

namespace DiseasesExpanded
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class GermcatcherController : GameStateMachine<GermcatcherController, GermcatcherController.Instance>, ISaveLoadable
    {
        public GameStateMachine<GermcatcherController, GermcatcherController.Instance, IStateMachineTarget, object>.State off;
        public GameStateMachine<GermcatcherController, GermcatcherController.Instance, IStateMachineTarget, object>.State on;
        public GameStateMachine<GermcatcherController, GermcatcherController.Instance, IStateMachineTarget, object>.State working_pre;
        public GameStateMachine<GermcatcherController, GermcatcherController.Instance, IStateMachineTarget, object>.State working_loop;
        public GameStateMachine<GermcatcherController, GermcatcherController.Instance, IStateMachineTarget, object>.State working_pst;

        private void WorkingLoop(Instance smi, float dt)
        {
            Germcatcher catcher = smi.gameObject.GetComponent<Germcatcher>();
            if (catcher != null)
                catcher.GatherGerms(dt);
        }

        private bool IsPowered(GameObject go)
        {
            if (go == null)
                return false;
            EnergyConsumer ec = go.GetComponent<EnergyConsumer>();
            return ec != null && ec.IsPowered;
        }

        public override void InitializeStates(out StateMachine.BaseState default_state)
        {
            default_state = this.off;
            this.off
                .PlayAnim("off")
                .UpdateTransition(this.on, (smi, dt) => IsPowered(smi.gameObject), UpdateRate.SIM_200ms);
            this.on
                .PlayAnim("on")
                .UpdateTransition(this.off, (smi, dt) => !IsPowered(smi.gameObject), UpdateRate.SIM_200ms)
                .EventTransition(GameHashes.OperationalChanged, this.working_pre, smi => smi.GetComponent<Operational>().IsOperational);
            this.working_pre
                .PlayAnim("working_pre")
                .OnAnimQueueComplete(this.working_loop);
            this.working_loop
                .PlayAnim("working_loop", KAnim.PlayMode.Loop)
                .Update((smi, dt) => { WorkingLoop(smi, dt); }, UpdateRate.SIM_1000ms)
                .ToggleStatusItem(Db.Get().BuildingStatusItems.Get(GermcatcherConfig.StatusItemID), smi => smi)
                .EventTransition(GameHashes.OperationalChanged, this.working_pst, smi => !smi.GetComponent<Operational>().IsOperational);
            this.working_pst
                .PlayAnim("working_pst")
                .OnAnimQueueComplete(this.on);
        }

        public class Def : StateMachine.BaseDef
        {
        }

        [SerializationConfig(MemberSerialization.OptIn)]
        public new class Instance : GameStateMachine<GermcatcherController, GermcatcherController.Instance, IStateMachineTarget, object>.GameInstance, ISaveLoadable
        {
            [Serialize]
            public Dictionary<byte, int> GatheredGerms = new Dictionary<byte, int>();

            public string GetStatusItemProgress()
            {
                Germcatcher catcher = this.gameObject.GetComponent<Germcatcher>();
                if (catcher == null)
                    return string.Empty;

                if (catcher.GetCurrentGermIdx() == GermIdx.Invalid)
                    return string.Empty;

                int percentProgress = 100 * catcher.GetCurrentGermCount() / catcher.GatherThreshold;

                return STRINGS.STATUSITEMS.GATHERING.PROGRESS
                    .Replace("{GERMS}", catcher.GetCurrentGermName())
                    .Replace("{PROGRESS}", percentProgress.ToString());
            }

            public Instance(IStateMachineTarget master, GermcatcherController.Def def)
              : base(master)
            {
            }
        }
    }
}
