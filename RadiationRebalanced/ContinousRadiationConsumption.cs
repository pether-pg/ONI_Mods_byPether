using System;
using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace RadiationRebalanced
{
    class ContinousRadiationConsumption : KMonoBehaviour, ISim1000ms
    {
        RadiationMonitor.Instance smi = null;
        GameObject eatingDuplicant = null;

        const string EffectID = "ContinousRadiationConsumptionEffect";
        const float EffectTimeSeconds = 25f;
        const float DailyKCalConsumed = 1666.67f;

        private void EnsureInitalized()
        {
            if (eatingDuplicant == null)
                eatingDuplicant = this.gameObject;

            if(smi == null && eatingDuplicant != null)
                smi = eatingDuplicant.GetSMI<RadiationMonitor.Instance>();
        }

        private bool HasEffect()
        {
            if (eatingDuplicant == null)
                return false;
            Effects effects = eatingDuplicant.GetComponent<Effects>();
            return (effects != null && effects.HasEffect(EffectID));
        }

        private bool CanConsume()
        {
            if (smi == null)
                return false;
            return smi.sm.radiationExposure.Get(smi) > 1;
        }

        private float RadsPerConsume()
        {
            return Settings.Instance.RadiationEater.ConsumedRadsPerCycle / ConsumesPerCycle();
        }

        private float ConsumesPerCycle()
        {
            return 600 / EffectTimeSeconds;
        }

        private Effect GetEffect(float scale)
        {
            float kCalsGranted = scale * DailyKCalConsumed * Settings.Instance.RadiationEater.DailyKCalFulfillment;
            bool showUi = Settings.Instance.RadiationEater.ShowInUI;
            Effect effect = new Effect(EffectID, "ConsumedRads", "ConsumedRads - Desc", EffectTimeSeconds, showUi, false, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("CaloriesDelta", kCalsGranted, "ConsumedRads effect"));

            return effect;
        }

        private void ConsumeAndGrantEffect()
        {
            if (smi == null || eatingDuplicant == null)
                return;

            float absorbedRads = smi.sm.radiationExposure.Get(smi);
            float consumedRads = Math.Min(absorbedRads, RadsPerConsume());
            eatingDuplicant.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id).ApplyDelta(-consumedRads);
            if (consumedRads < 1.0f)
                return;

            if(Settings.Instance.RadiationEater.TriggerFloatingText)
                PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, Mathf.FloorToInt(consumedRads).ToString() + STRINGS.UI.UNITSUFFIXES.RADIATION.RADS, smi.master.transform);

            Effects effects = eatingDuplicant.GetComponent<Effects>();
            float scale = consumedRads / RadsPerConsume();
            if (scale < 0.01f)
                return;

            Effect effect = GetEffect(scale);
            effects.Add(effect, true);
        }

        public void Sim1000ms(float dt)
        {
            EnsureInitalized();

            if (!HasEffect() && CanConsume())
                ConsumeAndGrantEffect();
        }
    }
}
