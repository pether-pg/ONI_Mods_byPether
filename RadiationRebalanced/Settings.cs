using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadiationRebalanced
{
    class Settings
    {
        private static Settings _instance = null;

        public static Settings Instance
        {
            get
            {
                //if (_instance == null)
                //    _instance = JsonSerializer<Settings>.Deserialize();
                if (_instance == null)
                {
                    _instance = new Settings();
                    JsonSerializer<Settings>.Serialize(_instance);
                }
                return _instance;
            }
            set { _instance = value; }
        }

        public RadiationEaterSettings RadiationEater = new RadiationEaterSettings(true, 600, 0.5f, 1, true, true);
        public GlowStickSettings GlowStick = new GlowStickSettings(new EmitterSettings(null, null, null, null, null, null, null), 1);
        public MutationSettings PlantMutations = new MutationSettings(10, 100, true);

        public string EmitterNote = "For each setting, use null to keep the original value unchanged. For emitType use one of the numbers: null, 0 (Constant), 1 (Pulsing), 2 (PulsingAveraged), 3 (SimplePulse)";
        public EmitterSettings BeeHiveIdle = new EmitterSettings(null, null, null, 400, 1, null, RadiationEmitter.RadiationEmitterType.Constant);
        public EmitterSettings BeeHiveEating = new EmitterSettings(null, null, null, 4000, 1, null, RadiationEmitter.RadiationEmitterType.Constant);
        public EmitterSettings Shinebug = new EmitterSettings(null, null, null, 400, null, null, RadiationEmitter.RadiationEmitterType.Constant);
        public EmitterSettings WheezewortGrowing = new EmitterSettings(null, null, null, null, null, null, null);
        public EmitterSettings Beeta = new EmitterSettings(null, null, null, null, null, null, null);
        public EmitterSettings Beetiny = new EmitterSettings(null, null, null, null, null, null, null);
        public EmitterSettings RadiationLamp = new EmitterSettings(null, null, null, null, null, null, null);
        public EmitterSettings NuclearRocketEngine = new EmitterSettings(null, null, null, null, null, null, null);
        public EmitterSettings ResearchReactor = new EmitterSettings(null, null, null, 400, null, null, null);

        public class EmitterSettings
        {
            public bool? radiusProportionalToRads;
            public short? emitRadiusX;
            public short? emitRadiusY;
            public float? emitRads;
            public float? emitRate;
            public float? emitSpeed;
            public RadiationEmitter.RadiationEmitterType? emitType;

            public EmitterSettings(bool? a_radiusProportionalToRads = null, short? a_emitRadiusX = null, short? a_emitRadiusY = null, 
                float? a_emitRads = null, float? a_emitRate = null, float? a_emitSpeed = null, RadiationEmitter.RadiationEmitterType? a_emitType = null)
            {
                this.radiusProportionalToRads = a_radiusProportionalToRads;
                this.emitRadiusX = a_emitRadiusX;
                this.emitRadiusY = a_emitRadiusY;
                this.emitRads = a_emitRads;
                this.emitRate = a_emitRate;
                this.emitSpeed = a_emitSpeed;
                this.emitType = a_emitType;
            }

            public void ApplySetting(RadiationEmitter emitter)
            {
                if (emitter == null || this.IsAllNull())
                    return;

                if(this.radiusProportionalToRads.HasValue)
                    emitter.radiusProportionalToRads = this.radiusProportionalToRads.Value;
                
                if(this.emitRadiusX.HasValue)
                    emitter.emitRadiusX = this.emitRadiusX.Value;
                
                if(this.emitRadiusY.HasValue)
                    emitter.emitRadiusY = this.emitRadiusY.Value;

                if (this.emitRads.HasValue)
                    emitter.emitRads = this.emitRads.Value;
                
                if (this.emitRate.HasValue)
                    emitter.emitRate = this.emitRate.Value;
                
                if (this.emitSpeed.HasValue)
                    emitter.emitSpeed = this.emitSpeed.Value;
                
                if (this.emitType.HasValue)
                    emitter.emitType = this.emitType.Value;
            }

            public bool IsAllNull()
            {
                return !(this.radiusProportionalToRads.HasValue 
                        || this.emitRadiusX.HasValue 
                        || this.emitRadiusY.HasValue 
                        || this.emitRads.HasValue 
                        || this.emitRate.HasValue 
                        || this.emitSpeed.HasValue 
                        || this.emitType.HasValue);
            }
        }

        public class RadiationEaterSettings
        {
            public string RadiationEaterNote = "The game readjusts kCal value based on difficulty level. Trait rarity accepts values from 1 (common) up to 5 (legendary)";
            public bool ApplyContinousEffect;
            public float ConsumedRadsPerCycle;
            public float DailyKCalFulfillment;
            public int TraitRarity;
            public bool ShowInUI;
            public bool TriggerFloatingText;

            public RadiationEaterSettings(bool apply, float rads, float kcals, int rarity, bool ui, bool text)
            {
                ApplyContinousEffect = apply;
                ConsumedRadsPerCycle = rads;
                DailyKCalFulfillment = kcals;
                TraitRarity = rarity;
                ShowInUI = ui;
                TriggerFloatingText = text;
            }
        }

        public class GlowStickSettings
        {
            public int TraitRarity;
            public EmitterSettings Emitter;

            public GlowStickSettings(EmitterSettings emitter, int rarity)
            {
                Emitter = emitter;
                TraitRarity = rarity;
            }
        }

        public class MutationSettings
        {
            public float MutationChanceMultiplier;
            public float AmbientRadiationRequirement;
            public bool MutatedPlantsDropSeed;

            public MutationSettings(float mutation, float ambientRads, bool drop)
            {
                MutationChanceMultiplier = mutation;
                MutatedPlantsDropSeed = drop;
                AmbientRadiationRequirement = ambientRads;
            }
        }
    }
}
