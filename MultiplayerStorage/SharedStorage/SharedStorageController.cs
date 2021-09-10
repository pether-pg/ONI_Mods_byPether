using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerStorage
{
    class SharedStorageController : GameStateMachine<SharedStorageController, SharedStorageController.Instance>
    {
        public GameStateMachine<SharedStorageController, SharedStorageController.Instance, IStateMachineTarget, object>.State off;
        public GameStateMachine<SharedStorageController, SharedStorageController.Instance, IStateMachineTarget, object>.State on;
        public GameStateMachine<SharedStorageController, SharedStorageController.Instance, IStateMachineTarget, object>.State working;

        public override void InitializeStates(out StateMachine.BaseState default_state)
        {
            default_state = (StateMachine.BaseState)this.off;
            this.root.EventTransition(GameHashes.OnStorageInteracted, this.working);
            this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (StateMachine<SharedStorageController, SharedStorageController.Instance, IStateMachineTarget, object>.Transition.ConditionCallback)(smi => smi.GetComponent<Operational>().IsOperational));
            this.on.PlayAnim("on", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.off, (StateMachine<SharedStorageController, SharedStorageController.Instance, IStateMachineTarget, object>.Transition.ConditionCallback)(smi => !smi.GetComponent<Operational>().IsOperational));
            this.working.PlayAnim("working").OnAnimQueueComplete(this.off);
        }

        public class Def : StateMachine.BaseDef
        {
        }

        public new class Instance : GameStateMachine<SharedStorageController, SharedStorageController.Instance, IStateMachineTarget, object>.GameInstance
        {
            public Instance(IStateMachineTarget master, SharedStorageController.Def def)
              : base(master)
            {
            }
        }
    }

}
