using Klei.AI;

namespace SidequestMod.Sidequests.TutorialQuests
{
    class TutorialClinicQuest : TutorialQuest
    {
        public TutorialClinicQuest() : base()
        {
            Name = nameof(TutorialClinicQuest);
            ID = nameof(TutorialClinicQuest);
        }

        public override QuestStatus QuickStatusCheck()
        {
            if (Components.Clinics.Count >= 1)
                return QuestStatus.COMPLETED;
            return QuestStatus.ONGOING;
        }
    }
}
