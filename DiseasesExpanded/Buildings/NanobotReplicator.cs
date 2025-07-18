using UnityEngine;
using System.Collections.Generic;
using KSerialization;


namespace DiseasesExpanded
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class NanobotReplicator : GameStateMachine<NanobotReplicator, NanobotReplicator.Instance>, ISaveLoadable
    {
        public GameStateMachine<NanobotReplicator, NanobotReplicator.Instance, IStateMachineTarget, object>.State off;
        public GameStateMachine<NanobotReplicator, NanobotReplicator.Instance, IStateMachineTarget, object>.State on;
        public GameStateMachine<NanobotReplicator, NanobotReplicator.Instance, IStateMachineTarget, object>.State working_pre;
        public GameStateMachine<NanobotReplicator, NanobotReplicator.Instance, IStateMachineTarget, object>.State working_loop;
        public GameStateMachine<NanobotReplicator, NanobotReplicator.Instance, IStateMachineTarget, object>.State working_pst;

        private void WorkingLoop(Instance smi, float dt)
        {
            CellOffset cellOffset = new CellOffset(0, 1);
            int cell = Grid.OffsetCell(Grid.PosToCell(smi.gameObject), cellOffset);
            int count = smi.CalculateGermCountPerT(dt);

            SimMessages.ModifyDiseaseOnCell(cell, GermIdx.MedicalNanobotsIdx, count);
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
                .Enter(smi => smi.GetComponent<Operational>().SetActive(true))
                .Update((smi, dt) => { WorkingLoop(smi, dt); }, UpdateRate.SIM_1000ms)
                .ToggleStatusItem(Db.Get().BuildingStatusItems.Get(NanobotReplicatorConfig.StatusItemID), smi => smi)
                .EventTransition(GameHashes.OperationalChanged, this.working_pst, smi => !smi.GetComponent<Operational>().IsOperational);
            this.working_pst
                .PlayAnim("working_pst")
                .Enter(smi => smi.GetComponent<Operational>().SetActive(false))
                .OnAnimQueueComplete(this.on);
        }

        public class Def : StateMachine.BaseDef
        {
        }

        [SerializationConfig(MemberSerialization.OptIn)]
        public new class Instance : GameStateMachine<NanobotReplicator, NanobotReplicator.Instance, IStateMachineTarget, object>.GameInstance, ISaveLoadable
        {
            [Serialize]
            public Dictionary<byte, int> GatheredGerms = new Dictionary<byte, int>();

            public int CalculateGermCountPerT(float dt)
            {
                return (int)(dt * CalculateGermCountPerCycle() / 600);
            }

            public int CalculateGermCountPerCycle()
            {
                int total = NanobotBottleConfig.SPAWNED_BOTS_COUNT;
                float RoiForMax = 20;
                int maxLevel = 16;
                int level = MedicalNanobotsData.Instance.GetDevelopmentLevel(MutationVectors.Vectors.Res_Replication);

                return (int)((1 + level) * total / (RoiForMax * maxLevel));
            }

            public string GetStatusItemProgress()
            {
                return STRINGS.STATUSITEMS.NANOBOT_REPLICATION.PROGRESS
                    .Replace("{COUNT}", CalculateGermCountPerT(1).ToString());
            }

            public Instance(IStateMachineTarget master, NanobotReplicator.Def def)
              : base(master)
            {
            }
        }
    }
}