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
            throw new NotImplementedException();
        }

        public override bool QuestRequirement()
        {
            throw new NotImplementedException();
        }

        public override QuestStatus QuickStatusCheck()
        {
            throw new NotImplementedException();
        }
    }
}
