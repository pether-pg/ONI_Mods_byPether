using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod.Sidequests.TutorialQuests
{
    class TutorialOxygenQuest : TutorialQuest
    {
        public TutorialOxygenQuest() : base()
        {
            Name = nameof(TutorialOxygenQuest);
            ID = nameof(TutorialOxygenQuest);
        }

        public override QuestStatus QuickStatusCheck()
        {
            if (Tutorial.Instance.oxygenGenerators.Count > 0)
                return QuestStatus.COMPLETED;
            return QuestStatus.ONGOING;
        }

        public override void CompleteQuest()
        {
            RewardsAndPenalties.ApplyEffect(RequestingDupe, RewardsAndPenalties.GetMediumRewardEffect());
            base.CompleteQuest();
        }

        public override bool MinionIdentityRequirement(MinionIdentity mi)
        {
            return Requirements.HasInterest(mi, Db.Get().Skills.Cooking1.Id) ;
        }
    }
}
