using UnityEngine;
using System.Collections.Generic;
using KSerialization;

namespace DiseasesExpanded
{
    class ShieldGenerator : StateMachineComponent<ShieldGenerator.SMInstance>
    {
        [Serialize]
        public float ShieldStatus;

        [MyCmpReq]
        protected EnergyConsumer energy;

        [MyCmpReq]
        private Building building;

        [MyCmpReq]
        private LogicOperationalController logic;

        protected override void OnSpawn()
        {
            base.OnSpawn();
            smi.StartSM();
        }

        public class States : GameStateMachine<States, SMInstance, ShieldGenerator>
        {
            public State off;
            public State working_pre;
            public State working_loop;
            public State working_pst;

            public override void InitializeStates(out BaseState default_state)
            {
                default_state = this.off;
                this.off
                    .PlayAnim("off")
                    .ToggleStatusItem(Db.Get().BuildingStatusItems.Get(ShieldGeneratorConfig.StatusItemID), smi => smi)
                    .UpdateTransition(this.working_pre, (smi, dt) => smi.CanWork(), UpdateRate.SIM_200ms);
                this.working_pre
                    .PlayAnim("on_pre")
                    .OnAnimQueueComplete(this.working_loop);
                this.working_loop
                    .PlayAnim("on", KAnim.PlayMode.Loop)
                    .Enter(smi => smi.GetComponent<Operational>().SetActive(true))
                    .ToggleStatusItem(Db.Get().BuildingStatusItems.Get(ShieldGeneratorConfig.StatusItemID), smi => smi)
                    .UpdateTransition(this.working_pst, (smi, dt) => !smi.CanWork(), UpdateRate.SIM_200ms);
                this.working_pst
                    .PlayAnim("on_pst")
                    .Enter(smi => smi.GetComponent<Operational>().SetActive(false))
                    .OnAnimQueueComplete(this.off);
            }
        }

        public class SMInstance : GameStateMachine<States, SMInstance, ShieldGenerator, object>.GameInstance
        {
            public SMInstance(ShieldGenerator master) : base(master) 
            {
            }

            public override void StartSM()
            {
                base.StartSM();
                ShieldData.Instance.Add(this);
            }

            public override void StopSM(string reason)
            {
                ShieldData.Instance.Remove(this);
                base.StopSM(reason);
            }

            public float GetShieldStatus()
            {
                return ShieldData.Instance.GetShieldPercent(master.gameObject.GetMyWorldId());
                //return this.master.ShieldStatus;
            }

            public void SetShieldStatus(float val)
            {
                this.master.ShieldStatus = val;
            }

            public void UpdateActive(float dt, bool works)
            {
                float shieldPerSecond = 0.333f;
                float delta = shieldPerSecond * dt * (works ? 1 : -3);
                float newValue = Mathf.Clamp(GetShieldStatus() + delta, 0, 100.0f);
                SetShieldStatus(newValue);
            }

            public string GetStatusItemProgress()
            {
                return STRINGS.STATUSITEMS.SHIELDPOWERUP.PROGRESS
                       .Replace("{PROGRESS}", ((int)GetShieldStatus()).ToString());
            }

            public bool CanWork()
            {
                return IsPowered() && IsLogicEnabled() && IsSpaceVisible();
            }

            public bool IsPowered()
            {
                return master.energy != null && master.energy.IsPowered;
            }

            public bool IsLogicEnabled()
            {
                return master.logic != null && master.logic.operational != null && master.logic.operational.IsOperational;
            }

            public bool IsSpaceVisible()
            {
                if (master.building == null)
                    return false;

                Extents extents = master.building.GetExtents();
                int scanRadius = 1;
                int cellsClear;
                return Grid.IsRangeExposedToSunlight(Grid.XYToCell(extents.x, extents.y), scanRadius, new CellOffset(0, 0), out cellsClear);
            }
        }
    }
}
