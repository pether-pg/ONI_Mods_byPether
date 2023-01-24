using UnityEngine;
using System.Collections.Generic;
using KSerialization;

namespace Dupes_Aromatics
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class VaporizerController : GameStateMachine<VaporizerController, VaporizerController.Instance>, ISaveLoadable
    {
        public GameStateMachine<VaporizerController, VaporizerController.Instance, IStateMachineTarget, object>.State off;
        //public GameStateMachine<VaporizerController, VaporizerController.Instance, IStateMachineTarget, object>.State on;
        public GameStateMachine<VaporizerController, VaporizerController.Instance, IStateMachineTarget, object>.State working_pre;
        public GameStateMachine<VaporizerController, VaporizerController.Instance, IStateMachineTarget, object>.State working_loop;
        public GameStateMachine<VaporizerController, VaporizerController.Instance, IStateMachineTarget, object>.State working_pst;

        public override void InitializeStates(out StateMachine.BaseState default_state)
        {
            default_state = this.off;
            this.off
                .PlayAnim("off")
                .UpdateTransition(this.working_pre, (smi, dt) => IsWorking(smi.gameObject), UpdateRate.SIM_200ms);
            //this.on.PlayAnim("on");
            this.working_pre
                .PlayAnim("working_pre")
                .OnAnimQueueComplete(this.working_loop);
            this.working_loop
                .PlayAnim("working_loop", KAnim.PlayMode.Loop)
                .UpdateTransition(this.working_pst, (smi, dt) => !IsWorking(smi.gameObject), UpdateRate.SIM_200ms);
            this.working_pst
                .PlayAnim("working_pst")
                .OnAnimQueueComplete(this.off);
        }

        private bool IsPowered(GameObject go)
        {
            if (go == null)
                return false;

            EnergyConsumer ec = go.GetComponent<EnergyConsumer>();
            return ec != null && ec.IsPowered;
        }

        private bool IsOperational(GameObject go)
        {
            if (go == null)
                return false;

            Operational oper = go.GetComponent<Operational>();
            return oper != null && oper.IsOperational;
        }

        private bool IsWorking(GameObject go)
        {
            if (go == null)
                return false;

            if (!IsPowered(go) || !IsOperational(go))
                return false;

            AromaticsFabricator af = go.GetComponent<AromaticsFabricator>();
            return af != null && af.CurrentWorkingOrder != null;
        }

        public class Def : StateMachine.BaseDef
        {
        }

        [SerializationConfig(MemberSerialization.OptIn)]
        public new class Instance : GameStateMachine<VaporizerController, VaporizerController.Instance, IStateMachineTarget, object>.GameInstance, ISaveLoadable
        {
            public Instance(IStateMachineTarget master, VaporizerController.Def def)
              : base(master)
            {
            }
        }
    }
}
