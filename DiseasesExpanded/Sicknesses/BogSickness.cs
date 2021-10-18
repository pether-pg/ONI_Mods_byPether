using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public class BogSickness : Sickness
    {
        public const string ID = "BogSickness";
        public const string RECOVERY_ID = "BogSicknessRecovery";

        public BogSickness()
            : base(nameof(BogSickness), Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation
            }, 2220f, RECOVERY_ID)
        {
            this.AddSicknessComponent((Sickness.SicknessComponent)new CommonSickEffectSickness());
            this.AddSicknessComponent((Sickness.SicknessComponent)new AnimatedSickness(new HashedString[1]
            {
                (HashedString) "anim_idle_sick_kanim"
            }, Db.Get().Expressions.Sick));
            this.AddSicknessComponent((Sickness.SicknessComponent)new PeriodicEmoteSickness((HashedString)"anim_idle_sick_kanim", new HashedString[3]
            {
                (HashedString) "idle_pre",
                (HashedString) "idle_default",
                (HashedString) "idle_pst"
            }, 50f));
            this.AddSicknessComponent((Sickness.SicknessComponent)new BogSickness.BogSicknessComponent());
        }

        public class BogSicknessComponent : Sickness.SicknessComponent
        {
            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                BogSickness.BogSicknessComponent.StatesInstance statesInstance = new BogSickness.BogSicknessComponent.StatesInstance(diseaseInstance);
                statesInstance.StartSM();
                return (object)statesInstance;
            }

            public override void OnCure(GameObject go, object instance_data) => ((StateMachine.Instance)instance_data).StopSM("Cured");

            public override List<Descriptor> GetSymptoms() => new List<Descriptor>()
            {
                new Descriptor((string) DUPLICANTS.DISEASES.SLIMESICKNESS.COUGH_SYMPTOM, (string) DUPLICANTS.DISEASES.SLIMESICKNESS.COUGH_SYMPTOM_TOOLTIP, Descriptor.DescriptorType.SymptomAidable)
            };

            public class StatesInstance : GameStateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.GameInstance
            {
                public float lastBiteTime;

                public StatesInstance(SicknessInstance master)
                  : base(master)
                {
                }

                public Reactable GetReactable() => (Reactable)new SelfEmoteReactable(this.master.gameObject, (HashedString)"BogBugBite", Db.Get().ChoreTypes.Cough, (HashedString)"anim_irritated_eyes_kanim", min_reactor_time: 0.0f).AddStep(new EmoteReactable.EmoteStep()
                {
                    anim = (HashedString)"irritated_eyes",
                    finishcb = new System.Action<GameObject>(this.GetBitten)
                }).AddStep(new EmoteReactable.EmoteStep()
                {
                    startcb = new System.Action<GameObject>(this.FinishedBitting)
                });

                private void GetBitten(GameObject infected)
                {
                    if (MudMaskConfig.HasEffect(infected))
                        return;

                    float damage = 1f;
                    if (InsectAllergies.HasAffectingTrait(infected))
                        damage *= InsectAllergies.BogSicknessDamageModifier;
                    if (Settings.Instance.RebalanceForDiseasesRestored)
                        damage *= 4;
                    infected.GetComponent<Health>()?.Damage(damage);
                    PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, STRINGS.DISEASES.BOGSICKNESS.POPFXTEXT, infected.transform);
                }

                private void FinishedBitting(GameObject cougher) => this.sm.coughFinished.Trigger(this);
            }

            public class States : GameStateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance>
            {
                public StateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.Signal coughFinished;
                public BogSickness.BogSicknessComponent.States.BreathingStates breathing;
                public GameStateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.State notbreathing;

                public override void InitializeStates(out StateMachine.BaseState default_state)
                {
                    default_state = (StateMachine.BaseState)this.breathing;
                    this.breathing.DefaultState(this.breathing.normal).TagTransition(GameTags.NoOxygen, this.notbreathing);
                    this.breathing.normal.Enter("SetBiteTime", (StateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.State.Callback)(smi =>
                    {
                        if ((double)smi.lastBiteTime >= (double)Time.time)
                            return;
                        smi.lastBiteTime = Time.time;
                    })).Update("Bite", (System.Action<BogSickness.BogSicknessComponent.StatesInstance, float>)((smi, dt) =>
                    {
                        if (smi.master.IsDoctored || (double)Time.time - (double)smi.lastBiteTime <= 14.0)
                            return;
                        smi.GoTo((StateMachine.BaseState)this.breathing.cough);
                    }), UpdateRate.SIM_4000ms);
                    this.breathing.cough.ToggleReactable((Func<BogSickness.BogSicknessComponent.StatesInstance, Reactable>)(smi => smi.GetReactable())).OnSignal(this.coughFinished, this.breathing.normal);
                    this.notbreathing.TagTransition(new Tag[1]
                    {
                        GameTags.NoOxygen
                    }, (GameStateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.State)this.breathing, true);
                }

                public class BreathingStates : GameStateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.State
                {
                    public GameStateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.State normal;
                    public GameStateMachine<BogSickness.BogSicknessComponent.States, BogSickness.BogSicknessComponent.StatesInstance, SicknessInstance, object>.State cough;
                }
            }
        }
    }
}
