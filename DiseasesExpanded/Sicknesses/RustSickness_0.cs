using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class RustSickness_0 : Sickness
    {
        public const float DURATION = 120; // 0.2 cycle
        public const string ID = nameof(RustSickness_0);
        public const string RECOVERY_ID = "RustSicknessRecovery";

        public const float YUCKY_LUNGGS_VALUE = 0.03f;
        public const float BASE_ATTRIBUTE_VALUE = -1f;
        public const float BASE_BATTERIES_VALUE = -0.5f;
        public const float BASE_POWER_VALUE = 50;
        public const float BASE_STRESS_VALUE = 50.0f / 8 / 600; // 50 stress per cycle on highest stage
        public const float BASE_OIL_VALUE = -10.0f / 600; 

        /* Disease progression plan:
         * 
         * Stage 0:
         *      Breathing
         *      
         * Stage 1:
         *      (all previous)
         *      Battery
         *      Attribute (X)
         *      
         * Stage 2:
         *      (all previous)
         *      Power Consumption
         *      Stress
         *      
         * Stage 3:
         *      (all previous)
         *      Risk of Death
         */


        public RustSickness_0()
            : base(ID, SicknessType.Pathogen, Severity.Benign, 0.00025f, new List<Sickness.InfectionVector>() { InfectionVector.Inhalation, InfectionVector.Exposure }, DURATION, RECOVERY_ID)
        {
            this.AddSicknessComponent(new CommonSickEffectSickness());
            this.AddSicknessComponent(new AnimatedSickness(new HashedString[1]
            {
                (HashedString) "anim_idle_sick_kanim"
            }, Db.Get().Expressions.Sick));
            this.AddSicknessComponent((Sickness.SicknessComponent)new AttributeModifierSickness(new AttributeModifier[1]
            {
                new AttributeModifier(Db.Get().Attributes.AirConsumptionRate.Id, YUCKY_LUNGGS_VALUE, (string) STRINGS.DISEASES.RUST_SICKNESS_0.NAME)
            }));
            this.AddSicknessComponent(new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 50f));
            this.AddSicknessComponent(new RustSickness_0_Component());
        }

        public class RustSickness_0_Component : SicknessComponent
        {

            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                RustSicknessHistory rsh = go.GetComponent<RustSicknessHistory>();
                if (rsh != null)
                {
                    rsh.InfectionsOnStages[0]++;
                    rsh.LastFateRoll = Random.Range(0, 100);
                }

                return diseaseInstance;
            }

            public override void OnCure(GameObject go, object instance_data)
            {
                RustSicknessHistory rsh = go.GetComponent<RustSicknessHistory>();
                if (rsh != null)
                {
                    int progresIdx = rsh.GetRandomizedIndex(rsh.GetRelativeChances(new List<int>() { 1, 2, 3 }), rsh.LastFateRoll);
                    switch(progresIdx)
                    {
                        case 0:
                            SicknessHelper.Infect(go, RustSickness_1.ID, STRINGS.DISEASES.RUST_SICKNESS_1.EXPOSURE_INFO);
                            break;
                        case 1:
                            SicknessHelper.Infect(go, RustSickness_2.ID, STRINGS.DISEASES.RUST_SICKNESS_2.EXPOSURE_INFO);
                            break;
                        case 2:
                            SicknessHelper.Infect(go, RustSickness_3.ID, STRINGS.DISEASES.RUST_SICKNESS_3.EXPOSURE_INFO);
                            break;
                        default:
                            SicknessHelper.Infect(go, RustSickness_1.ID, STRINGS.DISEASES.RUST_SICKNESS_1.EXPOSURE_INFO);
                            break;
                    }
                }
            }

            public override List<Descriptor> GetSymptoms(GameObject victim)
            {
                List<float> chances;
                RustSicknessHistory rsh = victim.GetComponent<RustSicknessHistory>();

                if (rsh != null)
                    chances = rsh.GetRelativeChances(new List<int>() { 1, 2, 3 });
                else
                    chances = new List<float>() { 0, 0, 0 };

                return new List<Descriptor>()
                {
                    new Descriptor( STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.NAME.Replace("{CHANCE}", chances[0].ToString("F0")).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_1.NAME),
                                    STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.TOOLTIP.Replace("{CHANCE}", chances[0].ToString("F0")).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_1.NAME),
                                    Descriptor.DescriptorType.Symptom),
                    new Descriptor( STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.NAME.Replace("{CHANCE}", chances[1].ToString("F0")).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_2.NAME),
                                    STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.TOOLTIP.Replace("{CHANCE}", chances[1].ToString("F0")).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_2.NAME),
                                    Descriptor.DescriptorType.Symptom),
                    new Descriptor( STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.NAME.Replace("{CHANCE}", chances[2].ToString("F0")).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_3.NAME),
                                    STRINGS.SYMPTHOMS.DEVELOPMENT_CHANCE.TOOLTIP.Replace("{CHANCE}", chances[2].ToString("F0")).Replace("{SICKNESS}", STRINGS.DISEASES.RUST_SICKNESS_3.NAME),
                                    Descriptor.DescriptorType.Symptom)
                };
            }
        }
    }
}
