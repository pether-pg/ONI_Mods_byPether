using System;
using System.Collections.Generic;
using Klei.AI;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SpaceScream :RandomDiseaseEvent
    {
        public SpaceScream(int weight = 1)
        {
            ID = nameof(SpaceScream);
            Group = Helpers.DIRECT_INFECT_GROUP;
            GeneralName = "In Space No One Can Hear You Scream";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Deadly;

            Condition = new Func<object, bool>(data => DupesInSpace().Count > 0);

            Event = new Action<object>(
                data =>
                {
                    foreach(MinionIdentity mi in DupesInSpace())
                    {
                        if (mi == null)
                            continue;

                        Sicknesses sicknesses = mi.GetSicknesses();
                        if (sicknesses == null)
                            continue;

                        sicknesses.Infect(new SicknessExposureInfo(AlienSickness.ID, GeneralName));
                    }

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "We received strange distress signal from our rocket...");
                });
        }

        public List<MinionIdentity> DupesInSpace()
        {
            List<MinionIdentity> result = new List<MinionIdentity>();

            List<int> FlyingRocketsWorldIds = new List<int>();
            foreach (Clustercraft rocket in Components.Clustercrafts)
                if (rocket.Status == Clustercraft.CraftStatus.InFlight)
                    FlyingRocketsWorldIds.Add(rocket.ModuleInterface.GetInteriorWorld().id);

            foreach (MinionIdentity mi in Components.MinionIdentities)
                if (mi != null && FlyingRocketsWorldIds.Contains(mi.GetMyWorldId()))
                    result.Add(mi);

            return result;
        }
    }
}
