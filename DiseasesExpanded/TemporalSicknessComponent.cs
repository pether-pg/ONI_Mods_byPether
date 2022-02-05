using STRINGS;
using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class TemporalSicknessComponent : Sickness.SicknessComponent
    {
        public enum AnomalyMode
        {
            None = 0,
            Displacement = 1,
            Reversal = 2,
            Waning = 4
        }

        AnomalyMode Mode = 0;

        public TemporalSicknessComponent(AnomalyMode mode = AnomalyMode.None)
        {
            Mode = mode;
        }

        public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
        {
            TemporalSicknessComponent.StatesInstance statesInstance = new TemporalSicknessComponent.StatesInstance(diseaseInstance);
            statesInstance.anomalyMode = Mode;
            statesInstance.StartSM();
            return (object)statesInstance;
        }

        public override void OnCure(GameObject go, object instance_data) => ((StateMachine.Instance)instance_data).StopSM("Cured");

        public override List<Descriptor> GetSymptoms() => new List<Descriptor>()
            {
                new Descriptor((string) DUPLICANTS.DISEASES.SLIMESICKNESS.COUGH_SYMPTOM, (string) DUPLICANTS.DISEASES.SLIMESICKNESS.COUGH_SYMPTOM_TOOLTIP, Descriptor.DescriptorType.SymptomAidable)
            };

        public class StatesInstance : GameStateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.GameInstance
        {
            public float lastBiteTime;
            public AnomalyMode anomalyMode;

            public StatesInstance(SicknessInstance master)
              : base(master)
            {
            }

            public Reactable GetReactable() => (Reactable)new SelfEmoteReactable(this.master.gameObject, (HashedString)"BogBugBite", Db.Get().ChoreTypes.Cough, (HashedString)"anim_irritated_eyes_kanim", min_reactor_time: 0.0f).AddStep(new EmoteReactable.EmoteStep()
            {
                anim = (HashedString)"irritated_eyes",
                finishcb = new System.Action<GameObject>(this.ApplyAnomaly)
            }).AddStep(new EmoteReactable.EmoteStep()
            {
                startcb = new System.Action<GameObject>(this.FinishedBitting)
            });

            private void ApplyAnomaly(GameObject infected)
            {
                switch(anomalyMode)
                {
                    case AnomalyMode.Displacement:
                        //ApplyDisplacementAnomaly(infected); //does not work
                        break;
                    case AnomalyMode.Reversal:
                        ApplyReversalAnomaly(infected);
                        break;
                    case AnomalyMode.Waning:
                        ApplyWaningAnomaly(infected);
                        break;
                    default:
                        break;
                }
            }

            private void ApplyDisplacementAnomaly(GameObject infected)
            {
                MinionIdentity identity = infected.GetComponent<MinionIdentity>();
                if (identity == null)
                    return;

                int range = 15;
                Vector3 delta = new Vector3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-range, range), 0);

                Vector3 position = identity.gameObject.transform.position;
                Vector3 newPos = position + delta;
                byte worldInit = Grid.WorldIdx[Grid.PosToCell(position)];
                byte worldAfter = Grid.WorldIdx[Grid.PosToCell(newPos)];

                if (worldInit != worldAfter)
                {
                    delta = new Vector3(-delta.x, delta.y, delta.z);
                    newPos = position + delta;
                    worldAfter = Grid.WorldIdx[Grid.PosToCell(newPos)];
                }
                if (worldInit != worldAfter)
                {
                    delta = new Vector3(delta.x, -delta.y, delta.z);
                    newPos = position + delta;
                    worldAfter = Grid.WorldIdx[Grid.PosToCell(newPos)];
                }
                if (worldInit == worldAfter)
                {
                    identity.gameObject.transform.position = newPos;
                    PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, "Displaced!", identity.gameObject.transform);
                }
            }

            private void ApplyReversalAnomaly(GameObject infected)
            {
                Debug.Log("ApplyReversalAnomaly");
            }

            private void ApplyWaningAnomaly(GameObject infected)
            {
                Debug.Log("ApplyWaningAnomaly");
            }

            private void FinishedBitting(GameObject cougher) => this.sm.coughFinished.Trigger(this);
        }

        public class States : GameStateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance>
        {
            public StateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.Signal coughFinished;
            public TemporalSicknessComponent.States.BreathingStates breathing;
            public GameStateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.State notbreathing;

            public override void InitializeStates(out StateMachine.BaseState default_state)
            {
                default_state = (StateMachine.BaseState)this.breathing;
                this.breathing.DefaultState(this.breathing.normal).TagTransition(GameTags.NoOxygen, this.notbreathing);
                this.breathing.normal.Enter("SetBiteTime", (StateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.State.Callback)(smi =>
                {
                    if ((double)smi.lastBiteTime >= (double)Time.time)
                        return;
                    smi.lastBiteTime = Time.time;
                })).Update("Bite", (System.Action<TemporalSicknessComponent.StatesInstance, float>)((smi, dt) =>
                {
                    if (smi.master.IsDoctored || (double)Time.time - (double)smi.lastBiteTime <= 14.0)
                        return;
                    smi.GoTo((StateMachine.BaseState)this.breathing.cough);
                }), UpdateRate.SIM_4000ms);
                this.breathing.cough.ToggleReactable((Func<TemporalSicknessComponent.StatesInstance, Reactable>)(smi => smi.GetReactable())).OnSignal(this.coughFinished, this.breathing.normal);
                this.notbreathing.TagTransition(new Tag[1]
                {
                        GameTags.NoOxygen
                }, (GameStateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.State)this.breathing, true);
            }

            public class BreathingStates : GameStateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.State
            {
                public GameStateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.State normal;
                public GameStateMachine<TemporalSicknessComponent.States, TemporalSicknessComponent.StatesInstance, SicknessInstance, object>.State cough;
            }
        }
    }
}
