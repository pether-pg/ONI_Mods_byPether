using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod.Sidequests.InterestQuest
{
    class BaseInterestQuest : Sidequest
    {
        public string RelatedInterest;

        public BaseInterestQuest(string interest) : base()
        {
            RelatedInterest = interest;
        }

        public override bool MinionIdentityRequirement(MinionIdentity mi)
        {
            return Requirements.HasInterest(mi, RelatedInterest);
        }

        public override bool QuestRequirement()
        {
            return true;
        }

        public override QuestStatus QuickStatusCheck()
        {
            return QuestStatus.ONGOING;
        }
    }
}
