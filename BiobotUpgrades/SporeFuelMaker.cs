using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Klei.AI;

namespace BiobotUpgrades
{
    class SporeFuelMaker : KMonoBehaviour, ISim1000ms
    {
        const float MAX_EFFICIENCY = 1.0f;
        const int SPORES_TO_SUSTAIN_PER_SECOND = 10000;
        const int MAX_SPORES_CLEARED_PER_SECOND = (int)(MAX_EFFICIENCY * SPORES_TO_SUSTAIN_PER_SECOND);
        const float BATTERY_DEPLETION_RATE = 30f;
        const string EFFECT_ID = "BiobotRechargeEffect";

        public void Sim1000ms(float dt)
        {
            Effects effects = this.gameObject.GetComponent<Effects>();
            if (!effects.HasEffect(EFFECT_ID))
                effects.Add(CreateEffect(), true);

            int cell = CellAboveMyCell();
            int sporeCount = HowManyZombieSpores(cell);
            int sporesToClean = Math.Min(sporeCount, MAX_SPORES_CLEARED_PER_SECOND);
            SimMessages.ConsumeDisease(cell, 1.0f, sporesToClean, 0);

            float percentOfRequiredSpores = sporesToClean / SPORES_TO_SUSTAIN_PER_SECOND;
            AddToBattery(percentOfRequiredSpores * BATTERY_DEPLETION_RATE);
        }

        private Effect CreateEffect ()
        {
            Effect effect = new Effect(EFFECT_ID, EFFECT_ID, EFFECT_ID, 0, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Amounts.InternalBioBattery.deltaAttribute.Id, 20));
            return effect;
        }

        private int CellAboveMyCell()
        {
            int cell = Grid.PosToCell(this.gameObject);
            return Grid.CellAbove(cell);
        }

        private int HowManyZombieSpores(int cell)
        {
            if(Grid.DiseaseIdx[cell] == Db.Get().Diseases.GetIndex(Db.Get().Diseases.ZombieSpores.id))
                return Grid.DiseaseCount[cell];
            return 0;
        }

        private void AddToBattery(float amount)
        {
            RobotBatteryMonitor.Instance battery = this.gameObject.GetSMI<RobotBatteryMonitor.Instance>();
            battery.amountInstance.value += amount;
        }
    }
}
