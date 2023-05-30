using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcGen;

namespace SidequestMod.Sidequests.InterestQuest.Piloting
{
    class PilotingInterest_SurfaceBreach_Quest : BaseInterestQuest
    {
        public PilotingInterest_SurfaceBreach_Quest() : base("RocketPiloting1")
        {
            Name = STRINGS.SIDEQUESTS.INTEREST_PILOTING_SURFACE_BREACH.NAME;
            StartingText = STRINGS.SIDEQUESTS.INTEREST_PILOTING_SURFACE_BREACH.STARTING_TEXT;
            PassingText = STRINGS.SIDEQUESTS.INTEREST_PILOTING_SURFACE_BREACH.PASSING_TEXT;
            FailingText = STRINGS.SIDEQUESTS.INTEREST_PILOTING_SURFACE_BREACH.FAILING_TEXT;
        }

        public override bool QuestRequirement()
        {
            return !Game.Instance.savedInfo.discoveredSurface;
        }

        public override bool MinionIdentityRequirement(MinionIdentity mi)
        {
            return base.MinionIdentityRequirement(mi) && !RewardsAndPenalties.HasTrait(mi, nameof(StarryEyed));
        }

        public override QuestStatus QuickStatusCheck()
        {
            if (World.Instance.zoneRenderData.GetSubWorldZoneType(Grid.PosToCell(RequestingDupe.gameObject)) == SubWorld.ZoneType.Space)
                return QuestStatus.COMPLETED;
            if (RemainingTime <= 0)
                return QuestStatus.FAILED;
            return QuestStatus.ONGOING;
        }

        public override void CompleteQuest()
        {
            base.CompleteQuest();
            RewardsAndPenalties.GrantTrait(RequestingDupe, nameof(StarryEyed));
        }

        public override void FailQuest()
        {
            base.FailQuest();
            RewardsAndPenalties.ApplyEffect(RequestingDupe, RewardsAndPenalties.GetBigPenaltyEffect());
        }
    }
}
