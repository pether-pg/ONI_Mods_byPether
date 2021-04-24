using KSerialization;
using STRINGS;
using UnityEngine;

namespace IlluminationSensor
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class LogicIlluminationSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
    {
        private HandleVector<int>.Handle structureTemperature;
        [Serialize]
        public float thresholdIllumination = 0;
        [Serialize]
        public bool activateOnBrighterThan;
        [Serialize]
        public float minLight;
        public float maxLight = 100000f;
        private float averageIllumination;
        private bool wasOn;
        [MyCmpAdd]
        private CopyBuildingSettings copyBuildingSettings; // even if never used, this is required to coppy settings
        private static readonly EventSystem.IntraObjectHandler<LogicIlluminationSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicIlluminationSensor>((System.Action<LogicIlluminationSensor, object>)((component, data) => component.OnCopySettings(data)));

        public float StructureTemperature => GameComps.StructureTemperatures.GetPayload(this.structureTemperature).Temperature;

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.Subscribe<LogicIlluminationSensor>(-905833192, LogicIlluminationSensor.OnCopySettingsDelegate);
        }

        private void OnCopySettings(object data)
        {
            LogicIlluminationSensor component = ((GameObject)data).GetComponent<LogicIlluminationSensor>();
            if (!((UnityEngine.Object)component != (UnityEngine.Object)null))
                return;
            this.Threshold = component.Threshold;
            this.ActivateAboveThreshold = component.ActivateAboveThreshold;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.structureTemperature = GameComps.StructureTemperatures.GetHandle(this.gameObject);
            this.OnToggle += new System.Action<bool>(this.OnSwitchToggled);
            this.UpdateVisualState(true);
            this.UpdateLogicCircuit();
            this.wasOn = this.switchedOn;
        }

        public void Sim200ms(float dt)
        {
            int cell = Grid.PosToCell((KMonoBehaviour)this);
            this.averageIllumination = Grid.LightIntensity[cell];

            if (this.activateOnBrighterThan)
            {
                if (((double)this.averageIllumination <= (double)this.thresholdIllumination || this.IsSwitchedOn) && ((double)this.averageIllumination > (double)this.thresholdIllumination || !this.IsSwitchedOn))
                    return;
                this.Toggle();
            }
            else
            {
                if (((double)this.averageIllumination < (double)this.thresholdIllumination || !this.IsSwitchedOn) && ((double)this.averageIllumination >= (double)this.thresholdIllumination || this.IsSwitchedOn))
                    return;
                this.Toggle();
            }
        }

        public float GetIllumination() => this.averageIllumination;

        private void OnSwitchToggled(bool toggled_on)
        {
            this.UpdateVisualState();
            this.UpdateLogicCircuit();
        }

        private void UpdateLogicCircuit() => this.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);

        private void UpdateVisualState(bool force = false)
        {
            if (!(this.wasOn != this.switchedOn | force))
                return;
            this.wasOn = this.switchedOn;
            KBatchedAnimController component = this.GetComponent<KBatchedAnimController>();
            component.Play((HashedString)(this.switchedOn ? "on_pre" : "on_pst"));
            component.Queue((HashedString)(this.switchedOn ? "on" : "off"));
        }

        protected override void UpdateSwitchStatus()
        {
            StatusItem status_item = this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive;
            this.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, status_item);
        }

        public float Threshold
        {
            get => this.thresholdIllumination;
            set => this.thresholdIllumination = value;
            
        }

        public bool ActivateAboveThreshold
        {
            get => this.activateOnBrighterThan;
            set
            {
                this.activateOnBrighterThan = value;
            }
        }

        public float CurrentValue => this.GetIllumination();

        public float RangeMin => this.minLight;

        public float RangeMax => this.maxLight;

        public float GetRangeMinInputField() => this.RangeMin;

        public float GetRangeMaxInputField() => this.RangeMax;

        public LocString Title => STRINGS.ILLUMINATIONSENSOR.NAME;

        public LocString ThresholdValueName => UI.UNITSUFFIXES.LIGHT.LUX;

        public string AboveToolTip => STRINGS.ILLUMINATIONSENSOR.TOOLTIP_PATTERN_ABOVE;

        public string BelowToolTip => STRINGS.ILLUMINATIONSENSOR.TOOLTIP_PATTERN_BELOW;

        public string Format(float value, bool units) => ((int)value).ToString();

        public float ProcessedSliderValue(float input) => Mathf.Round(input);

        public float ProcessedInputValue(float input) => input;

        public LocString ThresholdValueUnits() => UI.UNITSUFFIXES.LIGHT.LUX;

        public ThresholdScreenLayoutType LayoutType => ThresholdScreenLayoutType.SliderBar;

        public int IncrementScale => 100;

        public NonLinearSlider.Range[] GetRanges => new NonLinearSlider.Range[2]
        {
            new NonLinearSlider.Range(50f, 2000),
            new NonLinearSlider.Range(50f, maxLight)
        };
    }
}
