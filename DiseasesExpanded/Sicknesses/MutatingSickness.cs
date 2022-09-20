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
        private const float breathPerLvl = -0.15f;
        private const float staminaPerLvl = -0.01f;
        private const float attrPerLvl = -1f;

        public static Effect CreateRelatedEffect(float time)
        {
            Effect effect = new Effect(EFFECT_ID, STRINGS.EFFECTS.MUTATEDSYMPTOMS.NAME, STRINGS.EFFECTS.MUTATEDSYMPTOMS.DESC, time, true, false, true);
            effect.SelfModifiers = new List<AttributeModifier>();

            int strLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Stress);
            int calLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Calories);
            int brtLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Breathing);
            int exhLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Exhaustion);
            int attLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Attributes);

            if (strLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("StressDelta", stressPerLvl * strLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
            if (brtLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("BreathDelta", breathPerLvl * brtLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
            if (exhLvl > 0)
                effect.SelfModifiers.Add(new AttributeModifier("StaminaDelta", staminaPerLvl * exhLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));

            if (calLvl > 0)
            {
                effect.SelfModifiers.Add(new AttributeModifier("CaloriesDelta", calPerLvl * calLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier("BladderDelta", bladderPerLvl * calLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
            }

            if (attLvl > 0)
            {
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Athletics.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Strength.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Digging.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Construction.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Art.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Caring.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Learning.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Machinery.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Cooking.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Botanist.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
                effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.Ranching.Id, attrPerLvl * attLvl, (string)STRINGS.DISEASES.MUTATINGSICKNESS.NAME));
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
            this.AddSicknessComponent((Sickness.SicknessComponent)new PeriodicEmoteSickness(Db.Get().Emotes.Minion.Sick, 10f));
            this.AddSicknessComponent((Sickness.SicknessComponent)new MutatingSickness.MutatingSicknessComponent());
        }

        public class MutatingSicknessComponent : Sickness.SicknessComponent
        {
            public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
            {
                MutatingSickness.MutatingSicknessComponent.StatesInstance statesInstance = new MutatingSickness.MutatingSicknessComponent.StatesInstance(diseaseInstance);
                statesInstance.StartSM();

                Debug.Log($"{ModInfo.Namespace}: {go.name} infected with Mutated Disease: {MutationData.Instance.GetMutationsCode()}");

                float time = diseaseInstance.GetInfectedTimeRemaining();
                Effect effect = CreateRelatedEffect(time);
                Effects effects = go.GetComponent<Effects>();
                if (effects != null && !effects.HasEffect(effect.Id))
                    effects.Add(effect, true);

                return (object)statesInstance;
            }

            public override void OnCure(GameObject go, object instance_data)
            {
                ((StateMachine.Instance)instance_data).StopSM("Cured");

                if (go == null) 
                    return;
                Effects effects = go.GetComponent<Effects>();
                if (effects != null && effects.HasEffect(EFFECT_ID))
                    effects.Remove(EFFECT_ID);
            }

            public class StatesInstance : GameStateMachine<MutatingSickness.MutatingSicknessComponent.States, MutatingSickness.MutatingSicknessComponent.StatesInstance, SicknessInstance, object>.GameInstance
            {
                public float lastCoughTime;

                public StatesInstance(SicknessInstance master)
                  : base(master)
                {
                }

                public Reactable GetReactable() 
                {
                    Emote cough = Db.Get().Emotes.Minion.Cough;
                    SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(this.master.gameObject, (HashedString)"SlimeLungCough", Db.Get().ChoreTypes.Cough, localCooldown: 0.0f);
                    selfEmoteReactable.SetEmote(cough);
                    selfEmoteReactable.RegisterEmoteStepCallbacks((HashedString)"react", (System.Action<GameObject>)null, new System.Action<GameObject>(this.Coughing));
                    return (Reactable)selfEmoteReactable;
                }

                private void Coughing(GameObject infected)
                {
                    int dmgLvl = MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Att_Damage);
                    if(dmgLvl > 0)
                    {
                        float damage = 0.1f * dmgLvl * dmgLvl;
                        if (Settings.Instance.RebalanceForDiseasesRestored)
                            damage *= 2;
                        infected.GetComponent<Health>()?.Damage(damage);
                        PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, STRINGS.DISEASES.MUTATINGSICKNESS.POPFXTEXT, infected.transform);
                    }

                    byte idx = Db.Get().Diseases.GetIndex((HashedString)MutatingGerms.ID);
                    int coughLvl = 1 + MutationData.Instance.GetMutationLevel(MutationVectors.Vectors.Res_Coughing);
                    int count = 1000 * coughLvl * coughLvl;
                    Equippable equippable = this.master.gameObject.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
                    if (equippable != null)
                    {
                        AmountInstance amountInstance = Db.Get().Amounts.Temperature.Lookup(infected);
                        equippable.GetComponent<Storage>().AddGasChunk(SimHashes.CarbonDioxide, 0.1f, amountInstance.value, idx, count, false);
                    }
                    else
                        SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(infected.transform.position), idx, count);

                    this.sm.coughFinished.Trigger(this);
                }

                //private void FinishedCoughing(GameObject cougher) => this.sm.coughFinished.Trigger(this);
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
