using KSerialization;
using STRINGS;
using System;
using UnityEngine;
using TodoList;

namespace InterplanarInfrastructure
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class RadiationLenseSatelite : StateMachineComponent<RadiationLenseSatelite.StatesInstance>, IHighEnergyParticleDirection, IProgressBarSideScreen, ISingleSliderControl, ISliderControl
    {
        [MyCmpReq]
        private HighEnergyParticleStorage particleStorage;
        [MyCmpGet]
        private Operational operational;
        private float recentPerSecondConsumptionRate;
        public int minSlider;
        public int maxSlider;
        [Serialize]
        private EightDirection _direction;
        public float minLaunchInterval;
        public float radiationSampleRate;
        [Serialize]
        public float particleThreshold = 50f;
        private EightDirectionController directionController;
        private float launcherTimer;
        private float radiationSampleTimer;
        private MeterController particleController;
        private bool particleVisualPlaying;
        private MeterController progressMeterController;
        [Serialize]
        public Ref<HighEnergyParticlePort> capturedByRef = new Ref<HighEnergyParticlePort>();
        [MyCmpAdd]
        private CopyBuildingSettings copyBuildingSettings;
        private static readonly EventSystem.IntraObjectHandler<RadiationLenseSatelite> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<RadiationLenseSatelite>((System.Action<RadiationLenseSatelite, object>)((component, data) => component.OnCopySettings(data)));

        public float PredictedPerCycleConsumptionRate => (float)Mathf.FloorToInt((float)((double)this.recentPerSecondConsumptionRate * 600.0));

        public EightDirection Direction
        {
            get => this._direction;
            set
            {
                this._direction = value;
                if (this.directionController == null)
                    return;
                this.directionController.SetRotation((float)(45 * EightDirectionUtil.GetDirectionIndex(this._direction)));
                this.directionController.controller.enabled = false;
                this.directionController.controller.enabled = true;
            }
        }

        private void OnCopySettings(object data)
        {
            RadiationLenseSatelite component = ((GameObject)data).GetComponent<RadiationLenseSatelite>();
            if (!((UnityEngine.Object)component != (UnityEngine.Object)null))
                return;
            this.Direction = component.Direction;
            this.particleThreshold = component.particleThreshold;
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.Subscribe<RadiationLenseSatelite>(-905833192, RadiationLenseSatelite.OnCopySettingsDelegate);
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.smi.StartSM();
            this.directionController = new EightDirectionController((KAnimControllerBase)this.GetComponent<KBatchedAnimController>(), "redirector_target", "redirect", EightDirectionController.Offset.Infront);
            this.Direction = EightDirection.Down;
            this.particleController = new MeterController((KAnimControllerBase)this.GetComponent<KBatchedAnimController>(), "orb_target", "orb_off", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, (string[])Array.Empty<string>());
            this.particleController.gameObject.AddOrGet<LoopingSounds>();
            this.progressMeterController = new MeterController((KAnimControllerBase)this.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, (string[])Array.Empty<string>());

            TodoList.Todo.Note("Remove tint when final kanim is provided.");
            KBatchedAnimController kbac = this.gameObject.GetComponent<KBatchedAnimController>();
            if (kbac != null)
                kbac.TintColour = new Color32(0, 255, 0, 255);
        }

        public void OnDestroy()
        {
            smi.ClearSpaceGridSatelite();
            base.OnDestroy();
        }

        public float GetProgressBarMaxValue() => this.particleThreshold;

        public float GetProgressBarFillPercentage() => this.particleStorage.Particles / this.particleThreshold;

        public string GetProgressBarTitleLabel() => (string)UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.PROGRESS_BAR_LABEL;

        public string GetProgressBarLabel()
        {
            int num = Mathf.FloorToInt(this.particleStorage.Particles);
            string str1 = num.ToString();
            num = Mathf.FloorToInt(this.particleThreshold);
            string str2 = num.ToString();
            return str1 + "/" + str2;
        }

        public string GetProgressBarTooltip() => (string)UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.PROGRESS_BAR_TOOLTIP;

        public void LauncherUpdate(float dt)
        {
            Todo.Note("It is a copy from radbolt collector with minimal changes to make it work for space radiation only.");
            Todo.Note("It is possible it can be simplified, but I wanted to leave most code in place in case you needed it later.");

            this.radiationSampleTimer += dt;
            if ((double)this.radiationSampleTimer >= (double)this.radiationSampleRate)
            {
                this.radiationSampleTimer -= this.radiationSampleRate;

                float num1 = 0;
                WorldContainer world = this.gameObject.GetMyWorld();
                if (world != null)
                {
                    num1 = world.cosmicRadiation;
                }

                if ((double)num1 != 0.0 && (double)this.particleStorage.RemainingCapacity() > 0.0)
                {
                    this.recentPerSecondConsumptionRate = num1 / 600f;
                    double num2 = (double)this.particleStorage.Store((float)((double)this.recentPerSecondConsumptionRate * (double)this.radiationSampleRate));
                }
                else
                {
                    this.recentPerSecondConsumptionRate = 0.0f;
                }
            }
            this.progressMeterController.SetPositionPercent(this.GetProgressBarFillPercentage());
            if (!this.particleVisualPlaying && (double)this.particleStorage.Particles > (double)this.particleThreshold / 2.0)
            {
                this.particleController.meterController.Play((HashedString)"orb_pre");
                this.particleController.meterController.Queue((HashedString)"orb_idle", KAnim.PlayMode.Loop);
                this.particleVisualPlaying = true;
            }
            this.launcherTimer += dt;
            if ((double)this.launcherTimer < (double)this.minLaunchInterval || (double)this.particleStorage.Particles < (double)this.particleThreshold)
                return;
            this.launcherTimer = 0.0f;
            int particleOutputCell = Grid.PosToCell(this.gameObject);
            GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab((Tag)"HighEnergyParticle"), Grid.CellToPosCCC(particleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2);
            gameObject.SetActive(true);
            if (!((UnityEngine.Object)gameObject != (UnityEngine.Object)null))
                return;
            HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
            component.payload = this.particleStorage.ConsumeAndGet(this.particleThreshold);
            component.SetDirection(this.Direction);
            this.directionController.PlayAnim("redirect_send");
            this.directionController.controller.Queue((HashedString)"redirect");
            this.particleController.meterController.Play((HashedString)"orb_send");
            this.particleController.meterController.Queue((HashedString)"orb_off");
            this.particleVisualPlaying = false;
        }

        public string SliderTitleKey => "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TITLE";

        public string SliderUnits => (string)UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;

        public int SliderDecimalPlaces(int index) => 0;

        public float GetSliderMin(int index) => (float)this.minSlider;

        public float GetSliderMax(int index) => (float)this.maxSlider;

        public float GetSliderValue(int index) => this.particleThreshold;

        public void SetSliderValue(float value, int index) => this.particleThreshold = value;

        public string GetSliderTooltipKey(int index) => "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP";

        string ISliderControl.GetSliderTooltip() => string.Format((string)Strings.Get("STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP"), (object)this.particleThreshold);

        public class StatesInstance : GameStateMachine<RadiationLenseSatelite.States, RadiationLenseSatelite.StatesInstance, RadiationLenseSatelite, object>.GameInstance
        {
            AxialI DeployLocation = AxialI.ZERO;

            public string GetStatusItemProgress()
            {
                return string.Format("{0}/cycle", this.master.PredictedPerCycleConsumptionRate.ToString());
            }

            public void SetDeployLocation(AxialI location)
            {
                DeployLocation = location;
            }

            public void ClearSpaceGridSatelite()
            {
                for (int i = 0; i < ClusterGrid.Instance.cellContents[DeployLocation].Count; i++)
                {
                    ClusterGridEntity clusterGridEntity = ClusterGrid.Instance.cellContents[DeployLocation][i];
                    if (clusterGridEntity.HasTag(InterplanarInfrastructure_Patches_Deploy.SatelitePrefabId))
                        GameObject.Destroy(clusterGridEntity);
                }
            }

            public StatesInstance(RadiationLenseSatelite smi)
              : base(smi)
            {
            }

        }

        public class States : GameStateMachine<RadiationLenseSatelite.States, RadiationLenseSatelite.StatesInstance, RadiationLenseSatelite>
        {
            public State idle;
            public State absorbing;

            public override void InitializeStates(out StateMachine.BaseState default_state)
            {
                default_state = (StateMachine.BaseState)this.idle;
                this.idle
                    .PlayAnim("on")
                    .Transition(this.absorbing, smi => WorldBorderChecker.IsInTopOfTheWorld(Grid.PosToCell(smi.gameObject.transform.position)));
                this.absorbing
                    .Enter("SetActive(true)", (smi => smi.master.operational.SetActive(true)))
                    .Exit("SetActive(false)", (smi => smi.master.operational.SetActive(false)))
                    .ToggleStatusItem(Db.Get().BuildingStatusItems.Get(RadiationLenseSateliteConfig.StatusItemID), (smi => smi))
                    .Update(((smi, dt) => smi.master.LauncherUpdate(dt)), UpdateRate.SIM_EVERY_TICK)
                    .PlayAnim("working_loop", KAnim.PlayMode.Loop);
            }
        }
    }
}
