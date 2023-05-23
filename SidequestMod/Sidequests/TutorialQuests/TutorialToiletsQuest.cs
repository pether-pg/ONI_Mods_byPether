using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod.Sidequests.TutorialQuests
{
    class TutorialToiletsQuest : TutorialQuest
    {
        public TutorialToiletsQuest() : base()
        {
            Name = nameof(TutorialToiletsQuest);
            ID = nameof(TutorialToiletsQuest);
        }

        public override QuestStatus QuickStatusCheck()
        {
            if (Components.Toilets.Count > 0)
                return QuestStatus.COMPLETED;
            return QuestStatus.ONGOING;
        }
    }
}
