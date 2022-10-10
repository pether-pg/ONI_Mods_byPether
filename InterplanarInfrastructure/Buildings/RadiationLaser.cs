using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InterplanarInfrastructure
{
    class RadiationLaser : StateMachineComponent<RadiationLaser.StatesInstance>
    {
        [MyCmpGet]
        private Operational operational;
        private HighEnergyParticleStorage particleStorage;
        private ClusterDestinationSelector destinationSelector;
        private bool hasLogicWire;
        private bool isLogicActive;
        private static StatusItem infoStatusItemLogic;
        private static readonly EventSystem.IntraObjectHandler<RadiationLaser> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<RadiationLaser>((System.Action<RadiationLaser, object>)((component, data) => component.OnLogicValueChanged(data)));

        public bool AllowLaunchingFromLogic
        {
            get
            {
                if (!this.hasLogicWire)
                    return true;
                return this.hasLogicWire && this.isLogicActive;
            }
        }


        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.destinationSelector = this.GetComponent<ClusterDestinationSelector>();
            this.particleStorage = this.GetComponent<HighEnergyParticleStorage>();

            this.smi.StartSM();
            this.CheckLogicWireState();
            this.Subscribe<RadiationLaser>(-801688580, RadiationLaser.OnLogicValueChangedDelegate);

            KBatchedAnimController kbac = this.gameObject.GetComponent<KBatchedAnimController>();
            if (kbac != null)
                kbac.TintColour = new Color32(0, 255, 0, 255);
        }
        private LogicCircuitNetwork GetNetwork() => Game.Instance.logicCircuitManager.GetNetworkForCell(this.GetComponent<LogicPorts>().GetPortCell(RailGun.PORT_ID));

        private void CheckLogicWireState()
        {
            LogicCircuitNetwork network = this.GetNetwork();
            this.hasLogicWire = network != null;
            this.isLogicActive = LogicCircuitNetwork.IsBitActive(0, network != null ? network.OutputValue : 1);
            this.smi.sm.allowedFromLogic.Set(this.AllowLaunchingFromLogic, this.smi);
            //this.GetComponent<KSelectable>().ToggleStatusItem(RadiationLaser.infoStatusItemLogic, network != null, (object)this);
        }

        private void OnLogicValueChanged(object data)
        {
            if (!(((LogicValueChanged)data).portID == RailGun.PORT_ID))
                return;
            this.CheckLogicWireState();
        }

        public class StatesInstance : GameStateMachine<RadiationLaser.States, RadiationLaser.StatesInstance, RadiationLaser, object>.GameInstance
        {
            public const int INVALID_PATH_LENGTH = -1;
            public const float LASER_TIME = 10; // 10s is a time of EffectPrefabs.Instance.OpenTemporalTearBeam
            public const float EFFICIENCY = 0.2f;

            public LaserBeam laserBeam;
            public float TimeSinceFire = 0;

            private List<AxialI> m_cachedPath;
            private Dictionary<WorldContainer, int> ModifiedSpaceRadiations;

            public StatesInstance(RadiationLaser smi)
              : base(smi)
            {
            }
            
            public float LaserTimeInCycles()
            {
                return LASER_TIME / 600.0f;
            }

            public int RadiationDelta()
            {
                float rad = EnergyCost() * EFFICIENCY / LaserTimeInCycles();
                return (int)rad;
            }

            public bool MayTurnOn()
            {
                return this.HasEnergy() && this.IsDestinationReachable() && this.master.operational.IsOperational && this.sm.allowedFromLogic.Get(this); ;
            }

            public bool CanCeaseFire(float dt)
            {
                TimeSinceFire += dt;
                return TimeSinceFire >= LASER_TIME;
            }

            public void ClearAllRadiations()
            {
                TimeSinceFire = 0;
                if (laserBeam != null)
                    laserBeam.ClearAllRadiations();
            }

            public void FIRE()
            {
                UpdatePath();
                if (PathLength() == INVALID_PATH_LENGTH)
                    return;

                CreateBeamFX();
                laserBeam = new LaserBeam(m_cachedPath, RadiationDelta());
                laserBeam.ModifyRadiationOfPath();
                this.master.particleStorage.ConsumeAndGet(EnergyCost());
            }

            public int PathLength()
            {
                if (this.m_cachedPath == null)
                    this.UpdatePath();
                if (this.m_cachedPath == null)
                    return INVALID_PATH_LENGTH;
                int count = this.m_cachedPath.Count;
                return count;
            }

            public bool IsDestinationReachable(bool forceRefresh = false)
            {
                if (forceRefresh)
                    this.UpdatePath();
                return this.smi.master.destinationSelector.GetDestinationWorld() != this.smi.master.GetMyWorldId() && this.PathLength() != INVALID_PATH_LENGTH;
            }

            public void UpdatePath() => this.m_cachedPath = ClusterGrid.Instance.GetPath(this.gameObject.GetMyWorldLocation(), this.smi.master.destinationSelector.GetDestination(), this.smi.master.destinationSelector);

            public bool HasEnergy() => (double)this.smi.master.particleStorage.Particles >= (double)this.EnergyCost();
            
            public float EnergyCost() => 500;

            public void Log(string msg)
            {
                Debug.Log($"InterplanarInfrastructure: {msg}");
                PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, msg, this.gameObject.transform);
            }

            public void CreateBeamFX()
            {
                Vector3 position = this.gameObject.transform.position;
                position.y += 3.25f;
                Quaternion rotation = Quaternion.Euler(-90f, 90f, 0.0f);
                Util.KInstantiate(EffectPrefabs.Instance.OpenTemporalTearBeam, position, rotation, this.gameObject);
            }
        }

        public class States : GameStateMachine<RadiationLaser.States, RadiationLaser.StatesInstance, RadiationLaser>
        {
            public State off;
            public OnStates on;
            public State laserOn;
            public BoolParameter allowedFromLogic;

            public override void InitializeStates(out StateMachine.BaseState default_state)
            {
                default_state = (StateMachine.BaseState)this.off;
                this.off
                    .Enter(smi => smi.Log("State: off"))
                    .PlayAnim("off")
                    .EventTransition(GameHashes.OnParticleStorageChanged, this.on, (smi => smi.MayTurnOn()))
                    .EventTransition(GameHashes.ClusterDestinationChanged, this.on, (smi => smi.MayTurnOn()))
                    .EventTransition(GameHashes.OperationalChanged, this.on, (smi => smi.MayTurnOn()))
                    .ParamTransition<bool>(this.allowedFromLogic, this.on, ((smi, p) => smi.MayTurnOn()));
                this.on
                    .Enter(smi => smi.Log("State: on"))
                    .PlayAnim("on")
                    .DefaultState(this.on.power_on)
                    .EventTransition(GameHashes.OperationalChanged, this.on.power_off, smi => !smi.master.operational.IsOperational)
                    .EventTransition(GameHashes.ClusterDestinationChanged, this.on.power_off, smi => !smi.IsDestinationReachable())
                    .EventTransition(GameHashes.OnParticleStorageChanged, this.on.power_off, (smi => !smi.MayTurnOn()))
                    .ParamTransition<bool>(this.allowedFromLogic, this.on.power_off, ((smi, p) => !p));
                this.on.power_on
                    .Enter(smi => smi.Log("State: on.power_on"))
                    .PlayAnim("power_on")
                    .OnAnimQueueComplete(this.on.wait);
                this.on.wait
                    .Enter(smi => smi.Log("State: on.wait"))
                    .UpdateTransition(this.laserOn, (smi, dt) => smi.MayTurnOn());
                this.on.power_off
                    .Enter(smi => smi.Log("State: on.power_off"))
                    .PlayAnim("power_off")
                    .OnAnimQueueComplete(this.off);
                this.laserOn
                    .Enter(smi => smi.Log($"State: LASER!! Path len = {smi.PathLength()}"))
                    .Enter(smi => smi.FIRE())
                    .UpdateTransition(this.on.power_off, (smi, dt)=> smi.CanCeaseFire(dt))
                    .Exit(smi => smi.ClearAllRadiations());
            }

            public class OnStates : GameStateMachine<RadiationLaser.States, RadiationLaser.StatesInstance, RadiationLaser, object>.State
            {
                public State power_on;
                public State wait;
                public State power_off;
            }
        }
    }
}
