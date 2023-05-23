using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod.Sidequests
{
    abstract class Sidequest
    {
        public float QuestWeight = 1;
        public MinionIdentity RequestingDupe;
        public string Name;

        public bool IsRunning = false;
        public bool IsTutorial = false;
        public bool IsFinished = false;
        public string ID { get; protected set; }

        public abstract bool QuestRequirement();

        public abstract bool MinionIdentityRequirement(MinionIdentity mi);

        public virtual bool CanBeStarted()
        {
            if (IsRunning || IsFinished)
                return false;

            if (!QuestRequirement())
                return false;

            foreach (MinionIdentity mi in Components.MinionIdentities)
                if (MinionIdentityRequirement(mi))
                    return true;

            return false;
        }

        public virtual void StartForDuplicant(MinionIdentity mi)
        {
            RequestingDupe = mi;
            IsRunning = true;
        }

        public virtual void Update(float dt)
        {

        }

        public abstract QuestStatus QuickStatusCheck();

        public virtual void CompleteQuest()
        {
            IsRunning = false;
            IsFinished = true;
        }

        public virtual void FailQuest()
        {
            IsRunning = false;
            IsFinished = true;
        }
    }
}
