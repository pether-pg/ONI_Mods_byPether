using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class MutatingSickness : Sickness
    {
        public const string ID = nameof(MutatingSickness);
        public const string EFFECT_ID = "MutatingSicknessEffect";
        public const string RECOVERY_ID = "MutatingSicknessRecovery";

        private const float stressPerDay = 5;
        private const float stressPerLvl = stressPerDay / 600;
        private const float calPerDay = 1666.682f;
        private const float calPerLvl = -calPerDay / 10;
        private const float bladderPerLvl = 0.05f;
        private const float breathPerLvl = -0.2f;
        private const float staminaPerLvl = -0.01f;
        private const float attrPerLvl = -1f;

        public static Effect CreateRelatedEffect(float time)
        {
            Effect effect = new Effect(EFFECT_ID, ID, ID, time, true, false, true);
            effect.SelfModifiers = new List<AttributeModifier>();

            int strLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Stress);
            int calLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Calories);
            int brtLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Breathing);
            int exhLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Exhaustion);
            int attLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Attributes);

            if (strLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("StressDelta", stressPerLvl * strLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
            if (brtLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("BreathDelta", breathPerLvl * brtLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
            if (exhLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("StaminaDelta", staminaPerLvl * exhLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));

            if (calLvl > 0)
            {
                effect.SelfModifiers.Add(new AttributeModifier("CaloriesDelta", calPerLvl * calLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier("BladderDelta", bladderPerLvl * calLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
            }

            if (attLvl > 0)
            {
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Athletics.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Strength.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Digging.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Construction.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Art.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Caring.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Learning.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Machinery.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Cooking.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Botanist.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Ranching.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGDISEASE.NAME));
            }
            return effect;
        }

        public MutatingSickness()
            : base(ID, Sickness.SicknessType.Pathogen, Sickness.Severity.Minor, 0.00025f, new List<Sickness.InfectionVector>()
            {
                Sickness.InfectionVector.Inhalation,
                Sickness.InfectionVector.Contact,
                Sickness.InfectionVector.Exposure,
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
            this.AddSicknessComponent((Sickness.SicknessComponent)new MutatingSickness.MutatingSicknessComponent());
        }

        public class MutatingSicknessComponent : Sickness.SicknessComponent
        {
            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                BogSickness.BogSicknessComponent.StatesInstance statesInstance = new BogSickness.BogSicknessComponent.StatesInstance(diseaseInstance);
                statesInstance.StartSM();

                Debug.Log($"{ModInfo.Namespace}: {go.name} infected with Mutated Disease: {MutationData.Instance.GetMutationsCode()}");

                float time = diseaseInstance.GetInfectedTimeRemaining();
                Effect effect = CreateRelatedEffect(time);
                Effects effects = go.GetComponent<Effects>();
                if (effects != null && !effects.HasEffect(effect.Id))
                    effects.Add(effect, true);

                return (object)statesInstance;
            }

            public override void OnCure(GameObject go, object instance_data) => ((StateMachine.Instance)instance_data).StopSM("Cured");

            public class StatesInstance : GameStateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.GameInstance
            {
                public float lastCoughTime;

                public StatesInstance(SicknessInstance master)
                  : base(master)
                {
                }

                public Reactable GetReactable() => (Reactable)new SelfEmoteReactable(this.master.gameObject, (HashedString)"CoughingBlood", Db.Get().ChoreTypes.Cough, (HashedString)"anim_slimelungcough_kanim", min_reactor_time: 0.0f).AddStep(new EmoteReactable.EmoteStep()
                {
                    anim = (HashedString)"react",
                    finishcb = new System.Action<GameObject>(this.Coughing)
                }).AddStep(new EmoteReactable.EmoteStep()
                {
                    startcb = new System.Action<GameObject>(this.FinishedCoughing)
                });

                private void Coughing(GameObject infected)
                {
                    int dmgLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Damage);
                    if (dmgLvl == 0)
                        return;

                    float damage = 0.1f * dmgLvl * dmgLvl;
                    if (Settings.Instance.RebalanceForDiseasesRestored)
                        damage *= 2;
                    infected.GetComponent<Health>()?.Damage(damage);
                    PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, STRINGS.DISEASES.MUTATINGDISEASE.POPFXTEXT, infected.transform);

                    byte idx = Db.Get().Diseases.GetIndex((HashedString)MutatingGerms.ID);
                    int coughLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_Coughing);
                    int count = 1000 * coughLvl * coughLvl * coughLvl;
                    SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(infected.transform.position), idx, count);
                }

                private void FinishedCoughing(GameObject cougher) => this.sm.coughFinished.Trigger(this);
            }

            public class States : GameStateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance>
            {
                public StateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.Signal coughFinished;
                public MutatingSickness.MutatingSicknessComponent.States.BreathingStates breathing;
                public GameStateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.State notbreathing;

                public override void InitializeStates(out StateMachine.BaseState default_state)
                {
                    default_state = (StateMachine.BaseState)this.breathing;
                    this.breathing.DefaultState(this.breathing.normal).TagTransition(GameTags.NoOxygen, this.notbreathing);
                    this.breathing.normal.Enter("SetCoughTime", (StateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.State.Callback)(smi =>
                    {
                        if (smi.lastCoughTime >= Time.time)
                            return;
                        smi.lastCoughTime = Time.time;
                    })).Update("Cough", ((smi, dt) =>
                    {
                        float coughInterval = 25 - MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_Coughing);
                        if (smi.master.IsDoctored || Time.time - smi.lastCoughTime <= coughInterval)
                            return;
                        smi.GoTo(this.breathing.cough);
                    }), UpdateRate.SIM_4000ms);
                    this.breathing.cough.ToggleReactable(smi => smi.GetReactable()).OnSignal(this.coughFinished, this.breathing.normal);
                    this.notbreathing.TagTransition(new Tag[1] {GameTags.NoOxygen}, this.breathing, true);
                }

                public class BreathingStates : GameStateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.State
                {
                    public GameStateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.State normal;
                    public GameStateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.State cough;
                }
            }
        }

    }
}
