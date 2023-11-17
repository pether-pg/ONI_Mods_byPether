using UnityEngine;
using System.Collections.Generic;
using KSerialization;
using Klei.AI;

namespace BiobotUpgrades
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class ZombieFuelMakerStates : GameStateMachine<ZombieFuelMakerStates, ZombieFuelMakerStates.Instance>, ISaveLoadable
    {
        const float MAX_EFFICIENCY = 1.0f;
        const int SPORES_TO_SUSTAIN_PER_SECOND = 10000;
        const int MAX_SPORES_CLEARED_PER_SECOND = (int)(MAX_EFFICIENCY * SPORES_TO_SUSTAIN_PER_SECOND);
        const float BATTERY_DEPLETION_RATE = 30f;

        public State off;
        public State recharge_10;
        public State recharge_25;
        public State recharge_50;
        public State recharge_75;
        public State recharge_100;
        public State recharge_150;
        public State recharge_200;

        public override void InitializeStates(out StateMachine.BaseState default_state)
        {
            default_state = this.off;
            this.off
                .ToggleEffect(GetEffectId(0))
                .UpdateTransition(this.recharge_10, (smi, dt) => IsAboveThreshold(smi, 0.10f), UpdateRate.SIM_1000ms);
            this.recharge_10
                .ToggleEffect(GetEffectId(0.10f))
                .Update((smi, dt) => ClearZombieSpores(smi, 0.10f), UpdateRate.RENDER_1000ms)
                .UpdateTransition(this.off, (smi, dt) => IsBelowThreshold(smi, 0.10f), UpdateRate.SIM_1000ms)
                .UpdateTransition(this.recharge_25, (smi, dt) => IsAboveThreshold(smi, 0.25f), UpdateRate.SIM_1000ms);
            this.recharge_25
                .ToggleEffect(GetEffectId(0.25f))
                .Update((smi, dt) => ClearZombieSpores(smi, 0.25f), UpdateRate.RENDER_1000ms)
                .UpdateTransition(this.recharge_10, (smi, dt) => IsBelowThreshold(smi, 0.25f), UpdateRate.SIM_1000ms)
                .UpdateTransition(this.recharge_50, (smi, dt) => IsAboveThreshold(smi, 0.50f), UpdateRate.SIM_1000ms);
            this.recharge_50
                .ToggleEffect(GetEffectId(0.50f))
                .Update((smi, dt) => ClearZombieSpores(smi, 0.50f), UpdateRate.RENDER_1000ms)
                .UpdateTransition(this.recharge_25, (smi, dt) => IsBelowThreshold(smi, 0.50f), UpdateRate.SIM_1000ms)
                .UpdateTransition(this.recharge_75, (smi, dt) => IsAboveThreshold(smi, 0.75f), UpdateRate.SIM_1000ms);
            this.recharge_75
                .ToggleEffect(GetEffectId(0.75f))
                .Update((smi, dt) => ClearZombieSpores(smi, 0.75f), UpdateRate.RENDER_1000ms)
                .UpdateTransition(this.recharge_50, (smi, dt) => IsBelowThreshold(smi, 0.75f), UpdateRate.SIM_1000ms)
                .UpdateTransition(this.recharge_100, (smi, dt) => IsAboveThreshold(smi, 1.00f), UpdateRate.SIM_1000ms);
            this.recharge_100
                .ToggleEffect(GetEffectId(1.00f))
                .Update((smi, dt) => ClearZombieSpores(smi, 1.00f), UpdateRate.RENDER_1000ms)
                .UpdateTransition(this.recharge_75, (smi, dt) => IsBelowThreshold(smi, 1.00f), UpdateRate.SIM_1000ms)
                .UpdateTransition(this.recharge_150, (smi, dt) => IsAboveThreshold(smi, 1.50f), UpdateRate.SIM_1000ms);
            this.recharge_150
                .ToggleEffect(GetEffectId(1.50f))
                .Update((smi, dt) => ClearZombieSpores(smi, 1.50f), UpdateRate.RENDER_1000ms)
                .UpdateTransition(this.recharge_75, (smi, dt) => IsBelowThreshold(smi, 1.50f), UpdateRate.SIM_1000ms)
                .UpdateTransition(this.recharge_150, (smi, dt) => IsAboveThreshold(smi, 2.00f), UpdateRate.SIM_1000ms);
            this.recharge_200
                .ToggleEffect(GetEffectId(2.00f))
                .Update((smi, dt) => ClearZombieSpores(smi, 2.00f), UpdateRate.RENDER_1000ms)
                .UpdateTransition(this.recharge_75, (smi, dt) => IsBelowThreshold(smi, 2.00f), UpdateRate.SIM_1000ms);
        }

        public static string GetEffectId(float threshold)
        {
            string id = string.Format("MorbRoverRechargeEffect_{0}", (int)(threshold * 100));
            return id;
        }

        public static Effect CreateRechargeEffect(float threshold)
        {
            string id = GetEffectId(threshold);
            float value = BATTERY_DEPLETION_RATE * threshold;
            string name = "CreateRechargeEffect name";
            string desc = "CreateRechargeEffect desc";
            Effect effect = new Effect(id, name, desc, 0, true, false, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Amounts.InternalBioBattery.deltaAttribute.Id, value));
            return effect;
        }

        private bool IsAboveThreshold(Instance smi, float threshold)
        {
            int count = HowManyZombieSpores(smi);
            int minimum = (int)(threshold * SPORES_TO_SUSTAIN_PER_SECOND);
            Debug.Log($"IsAboveThreshold? threshold = {threshold}, count = {count}, minimum = {minimum}, result = {count > minimum}");
            return count > minimum;
        }

        private bool IsBelowThreshold(Instance smi, float threshold)
        {
            int count = HowManyZombieSpores(smi);
            int maximum = (int)(threshold * SPORES_TO_SUSTAIN_PER_SECOND);
            Debug.Log($"IsBelowThreshold? threshold = {threshold}, count = {count}, maximum = {maximum}, result = {count > maximum}");
            return count < maximum;
        }

        private int CellAboveMyCell(Instance smi)
        {
            int cell = Grid.PosToCell(smi);
            return Grid.CellAbove(cell);
        }

        private int HowManyZombieSpores(Instance smi)
        {
            int cell = CellAboveMyCell(smi);
            if (Grid.DiseaseIdx[cell] == Db.Get().Diseases.GetIndex(Db.Get().Diseases.ZombieSpores.id))
                return Grid.DiseaseCount[cell];
            return 0;
        }

        private void ClearZombieSpores(Instance smi, float threshold)
        {
            int cell = CellAboveMyCell(smi);
            int max = (int)(SPORES_TO_SUSTAIN_PER_SECOND * threshold);
            SimMessages.ConsumeDisease(cell, 1.0f, max, 0);
        }

        public class Def : StateMachine.BaseDef
        {
        }

        [SerializationConfig(MemberSerialization.OptIn)]
        public new class Instance : GameStateMachine<ZombieFuelMakerStates, ZombieFuelMakerStates.Instance, IStateMachineTarget, object>.GameInstance, ISaveLoadable
        {

            public Instance(IStateMachineTarget master, ZombieFuelMakerStates.Def def)
              : base(master)
            {
            }
        }
    }
}
