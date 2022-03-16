using TUNING;
using UnityEngine;

namespace DiseasesExpanded
{
    class InsectAllergies
    {
        public const string ID = nameof(InsectAllergies);
        public const string ProtectionID = "HistamineSuppression";
        public const float BogSicknessDamageModifier = 4;
        public const float BeetaStingDamageModifier = 4;

        public static DUPLICANTSTATS.TraitVal GetTrait()
        {
            return new DUPLICANTSTATS.TraitVal()
            {
                id = InsectAllergies.ID,
                statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
                rarity = DUPLICANTSTATS.RARITY_COMMON,
                dlcId = DlcManager.EXPANSION1_ID
            };
        }

        public static bool HasAffectingTrait(GameObject duplicant)
        {
            if (duplicant == null)
                return false;

            Klei.AI.Traits traits = duplicant.GetComponent<Klei.AI.Traits>();
            bool hasTrait = (traits != null && traits.HasTrait(InsectAllergies.ID));

            Klei.AI.Effects effects = duplicant.GetComponent<Klei.AI.Effects>();
            bool hasProtection = (effects != null && effects.HasEffect(InsectAllergies.ProtectionID));

            return (hasTrait && !hasProtection);
        }
    }
}
