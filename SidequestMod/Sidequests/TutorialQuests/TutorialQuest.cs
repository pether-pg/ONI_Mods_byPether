using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod.Sidequests.TutorialQuests
{
    class TutorialQuest : Sidequest
    {
        public TutorialQuest()
        {
            base.IsTutorial = true;
            base.QuestWeight = 100;
        }

        public override bool MinionIdentityRequirement(MinionIdentity mi)
        {
            return true;
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
