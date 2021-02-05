using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterplanarAutomation
{
    class RadioTowerStates : GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>
    {
        public GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State off;
        public RadioTowerStates.OnStates on;

        public override void InitializeStates(out StateMachine.BaseState default_state)
        {
            default_state = (StateMachine.BaseState)this.off;
            this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, (GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State)this.on, (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.Transition.ConditionCallback)(smi => smi.GetComponent<Operational>().IsOperational));
            this.on.DefaultState(this.on.pre).Enter("ToggleActive", (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State.Callback)(smi => smi.GetComponent<Operational>().SetActive(true))).Exit("ToggleActive", (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State.Callback)(smi => smi.GetComponent<Operational>().SetActive(false)));
            this.on.pre.PlayAnim("on_pre").OnAnimQueueComplete(this.on.loop);
            this.on.loop.PlayAnim("on", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on.pst, (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.Transition.ConditionCallback)(smi => !smi.GetComponent<Operational>().IsOperational)).TagTransition(GameTags.Detecting, (GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State)this.on.working);
            this.on.pst.PlayAnim("on_pst").OnAnimQueueComplete(this.off);
            this.on.working.DefaultState(this.on.working.pre).Enter("ToggleActive", (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State.Callback)(smi =>
            {
                smi.GetComponent<Operational>().SetActive(true);
            })).Exit("ToggleActive", (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State.Callback)(smi =>
            {
                smi.GetComponent<Operational>().SetActive(false);
            }));
            this.on.working.pre.PlayAnim("detect_pre").OnAnimQueueComplete(this.on.working.loop);
            this.on.working.loop.PlayAnim("detect_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on.working.pst, (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.Transition.ConditionCallback)(smi => !smi.GetComponent<Operational>().IsOperational)).EventTransition(GameHashes.ActiveChanged, this.on.working.pst, (StateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.Transition.ConditionCallback)(smi => !smi.GetComponent<Operational>().IsActive)).TagTransition(GameTags.Detecting, this.on.working.pst, true);
            this.on.working.pst.PlayAnim("detect_pst").OnAnimQueueComplete(this.on.loop);
        }

        public class Def : StateMachine.BaseDef
        {
        }

        public class OnStates : GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State
        {
            public GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State pre;
            public GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State loop;
            public RadioTowerStates.WorkingStates working;
            public GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State pst;
        }

        public class WorkingStates : GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State
        {
            public GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State pre;
            public GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State loop;
            public GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.State pst;
        }


        public new class Instance : GameStateMachine<RadioTowerStates, RadioTowerStates.Instance, IStateMachineTarget, RadioTowerStates.Def>.GameInstance
        {
            public bool ShowWorkingStatus;

            public Instance(IStateMachineTarget master, RadioTowerStates.Def def)
              : base(master, def)
            {
            }

            public override void StartSM()
            {
                base.StartSM();
            }

            public override void StopSM(string reason)
            {
                base.StopSM(reason);
            }
        }
    }
}
