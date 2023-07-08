using TUNING;
using UnityEngine;

namespace DiseasesExpanded
{
    class NotWashingHands
    {
        public static string ID = nameof(NotWashingHands);

        public static DUPLICANTSTATS.TraitVal GetTrait()
        {
            return new DUPLICANTSTATS.TraitVal()
            {
                id = ID,
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
            bool hasTrait = (traits != null && traits.HasTrait(ID));

            return hasTrait;
        }
    }
}
