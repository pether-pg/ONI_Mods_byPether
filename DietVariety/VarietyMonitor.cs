using UnityEngine;
using KSerialization;
using System.Collections.Generic;
using Klei.AI;

namespace DietVariety
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class VarietyMonitor : KMonoBehaviour
    {
        public const string EFFECT_ID = "DietVarietyEffect";

        protected override void OnSpawn()
        {
            base.OnSpawn();

            this.gameObject.Subscribe((int)GameHashes.EatCompleteEater, new System.Action<object>(OnEatComplete));

            InitalizeEffect();
        }

        private void OnEatComplete(object data)
        {
            Edible edible = (Edible)data;
            if (edible == null)
                return;

            string id = edible.FoodID;
            PastMealsEaten.Instance.RegisterNewMeal(this.gameObject, id);
            RefreshEffect();
        }

        private void InitalizeEffect()
        {
            Effects effects = this.gameObject.GetComponent<Effects>();
            if (effects == null)
                return;

            if (!effects.HasEffect(EFFECT_ID))
                effects.Add(GetEffect(), true);
        }

        public void RefreshEffect()
        {
            Effects effects = this.gameObject.GetComponent<Effects>();
            if (effects == null)
                return;

            effects.Remove(EFFECT_ID);
            effects.Add(GetEffect(), true);
        }

        private int GetUniqueCount()
        {
            return PastMealsEaten.Instance.GetUniqueMealsCount(this.gameObject);
        }

        public Effect GetEffect()
        {
            int uniqueCount = GetUniqueCount();
            float duration = 0;
            float moraleBonus = Settings.Instance.MoralePerFoodType * uniqueCount - Settings.Instance.MinFoodTypesRequired * Settings.Instance.MoralePerFoodType;
            string desc = string.Format(STRINGS.EFFECTS.VARIED_DIET.DESC, uniqueCount, Settings.Instance.MaxMealsCounted);
            
            Effect effect = new Effect(EFFECT_ID, STRINGS.EFFECTS.VARIED_DIET.NAME, desc, duration, true, false, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(Db.Get().Attributes.QualityOfLife.Id, moraleBonus, STRINGS.EFFECTS.VARIED_DIET.NAME));
            
            return effect;
        }
    }
}
