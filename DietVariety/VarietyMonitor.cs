using UnityEngine;
using System.Collections.Generic;
using Klei.AI;

namespace DietVariety
{
    class VarietyMonitor : KMonoBehaviour
    {
        public Dictionary<string, int> TimeSinceAte;

        public const string EFFECT_ID = "DietVarietyEffect";

        protected override void OnSpawn()
        {
            base.OnSpawn();

            TimeSinceAte = new Dictionary<string, int>();
            this.gameObject.Subscribe((int)GameHashes.EatCompleteEater, new System.Action<object>(OnEatComplete));
        }

        public int GetVarietyCost(string FoodId)
        {
            if (!TimeSinceAte.ContainsKey(FoodId))
                return 0;
            return Settings.Instance.MaxMealsCounted - TimeSinceAte[FoodId];
        }

        private void OnEatComplete(object data)
        {
            Edible edible = (Edible)data;
            if (edible == null)
                return;

            UpdateFoodDiary(edible.FoodID);
            RefreshEffect();
        }

        private void UpdateFoodDiary(string LastEatenFoodId)
        {
            if (!TimeSinceAte.ContainsKey(LastEatenFoodId))
                TimeSinceAte.Add(LastEatenFoodId, 0);
            else TimeSinceAte[LastEatenFoodId] = 0;

            List<string> keysToIncrease = new List<string>();
            foreach (string key in TimeSinceAte.Keys)
                keysToIncrease.Add(key);
            foreach (string key in keysToIncrease)
                if(TimeSinceAte[key] < Settings.Instance.MaxMealsCounted)
                    TimeSinceAte[key] += 1;
        }

        private void RefreshEffect()
        {
            Effects effects = this.gameObject.GetComponent<Effects>();
            if (effects == null)
                return;

            effects.Remove(EFFECT_ID);
            effects.Add(GetEffect(), true);
        }

        private int GetUniqueCount()
        {
            int count = 0;
            foreach (int value in TimeSinceAte.Values)
                if (value < Settings.Instance.MaxMealsCounted)
                    count++;

            return count;
        }

        public Effect GetEffect()
        {
            int uniqueCount = GetUniqueCount();
            float duration = 600 * 10;
            float moraleBonus = Settings.Instance.StartingMorale + Settings.Instance.MoralePerFoodType * uniqueCount;
            string desc = string.Format(STRINGS.EFFECTS.VARIED_DIET.DESC, uniqueCount, Settings.Instance.MaxMealsCounted);
            Effect effect = new Effect(EFFECT_ID, STRINGS.EFFECTS.VARIED_DIET.NAME, desc, duration, true, false, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("QualityOfLife", moraleBonus, STRINGS.EFFECTS.VARIED_DIET.NAME));
            return effect;
        }
    }
}
