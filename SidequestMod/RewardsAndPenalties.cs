using Klei.AI;
using System.Collections.Generic;

namespace SidequestMod
{
    class RewardsAndPenalties
    {
        public const string SMALL_REWARD_ID = nameof(SMALL_REWARD_ID);
        public const string MEDIUM_REWARD_ID = nameof(MEDIUM_REWARD_ID);
        public const string BIG_REWARD_ID = nameof(BIG_REWARD_ID);
        public const string SMALL_PENALTY_ID = nameof(SMALL_PENALTY_ID);
        public const string MEDIUM_PENALTY_ID = nameof(MEDIUM_PENALTY_ID);
        public const string BIG_PENALTY_ID = nameof(BIG_PENALTY_ID);

        private const float CYCLE = 600f;
        private const float STRESS_PER_S = 100 / 600.0f;
        private const string STRESS_MODIFIER = "StressDelta";
        private const string MORALE_MODIFIER = "QualityOfLife";

        public static Effect GetSmallRewardEffect()
        {
            Effect effect = new Effect(SMALL_REWARD_ID, STRINGS.EFFECTS.SMALL_HAPPINESS.NAME, STRINGS.EFFECTS.SMALL_HAPPINESS.DESC, CYCLE / 2, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(STRESS_MODIFIER, -1 * STRESS_PER_S / 2));
            effect.SelfModifiers.Add(new AttributeModifier(MORALE_MODIFIER, 1));
            return effect;
        }

        public static Effect GetMediumRewardEffect()
        {
            Effect effect = new Effect(MEDIUM_REWARD_ID, STRINGS.EFFECTS.MEDIUM_HAPPINESS.NAME, STRINGS.EFFECTS.MEDIUM_HAPPINESS.DESC, CYCLE, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(STRESS_MODIFIER, -1 * STRESS_PER_S));
            effect.SelfModifiers.Add(new AttributeModifier(MORALE_MODIFIER, 2));
            return effect;
        }

        public static Effect GetBigRewardEffect()
        {
            Effect effect = new Effect(BIG_REWARD_ID, STRINGS.EFFECTS.BIG_HAPPINESS.NAME, STRINGS.EFFECTS.BIG_HAPPINESS.DESC, CYCLE * 2, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(STRESS_MODIFIER, -1 * STRESS_PER_S * 2));
            effect.SelfModifiers.Add(new AttributeModifier(MORALE_MODIFIER, 4));
            return effect;
        }

        public static Effect GetSmallPenaltyEffect()
        {
            Effect effect = new Effect(SMALL_PENALTY_ID, STRINGS.EFFECTS.SMALL_SADNESS.NAME, STRINGS.EFFECTS.SMALL_SADNESS.DESC, CYCLE / 2, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(STRESS_MODIFIER, STRESS_PER_S / 2));
            effect.SelfModifiers.Add(new AttributeModifier(MORALE_MODIFIER, -1));
            return effect;
        }

        public static Effect GetMediumPenaltyEffect()
        {
            Effect effect = new Effect(MEDIUM_PENALTY_ID, STRINGS.EFFECTS.MEDIUM_SADNESS.NAME, STRINGS.EFFECTS.MEDIUM_SADNESS.DESC, CYCLE, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(STRESS_MODIFIER, STRESS_PER_S));
            effect.SelfModifiers.Add(new AttributeModifier(MORALE_MODIFIER, -2));
            return effect;
        }

        public static Effect GetBigPenaltyEffect()
        {
            Effect effect = new Effect(BIG_PENALTY_ID, STRINGS.EFFECTS.BIG_SADNESS.NAME, STRINGS.EFFECTS.BIG_SADNESS.DESC, CYCLE * 2, true, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier(STRESS_MODIFIER, STRESS_PER_S * 2));
            effect.SelfModifiers.Add(new AttributeModifier(MORALE_MODIFIER, -4));
            return effect;
        }

        public static bool HasEffect(MinionIdentity mi, string effectId)
        {
            Effects eff = mi.GetComponent<Effects>();
            if (eff == null)
                return false;
            return eff.HasEffect(effectId);
        }

        public static bool HasTrait(MinionIdentity mi, string traitId)
        {
            Traits traits = mi.GetComponent<Traits>();
            if (traits == null)
                return false;
            return traits.HasTrait(traitId);
        }

        public static void ApplyEffect(MinionIdentity mi, string effectId)
        {
            Effects eff = mi.GetComponent<Effects>();
            if (eff != null)
                eff.Add(effectId, true);
        }

        public static void ApplyEffect(MinionIdentity mi, Effect effect)
        {
            Effects eff = mi.GetComponent<Effects>();
            if (eff != null)
                eff.Add(effect, true);
        }

        public static void GrantTrait(MinionIdentity mi, string traitID)
        {

            Trait trait = Db.Get().traits.TryGet(traitID);
            GrantTrait(mi, trait);
        }

        public static void GrantTrait(MinionIdentity mi, Trait trait)
        {
            Traits traits = mi.GetComponent<Traits>();
            if (traits != null && trait != null)
                traits.Add(trait);
        }
    }
}
