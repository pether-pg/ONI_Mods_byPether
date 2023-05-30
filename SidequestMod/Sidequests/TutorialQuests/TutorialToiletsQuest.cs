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
            ID = nameof(TutorialToiletsQuest);
            Name = STRINGS.SIDEQUESTS.TUTORIAL_TOILETS.NAME;
            StartingText = STRINGS.SIDEQUESTS.TUTORIAL_TOILETS.STARTING_TEXT;
            PassingText = STRINGS.SIDEQUESTS.TUTORIAL_TOILETS.PASSING_TEXT;
            TimeDuration = float.PositiveInfinity;
        }

        public override QuestStatus QuickStatusCheck()
        {
            if (Components.Toilets.Count > 0)
                return QuestStatus.COMPLETED;
            return QuestStatus.ONGOING;
        }
    }
}
