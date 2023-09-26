using System;
using Klei.AI;

namespace DiseasesExpanded.RandomEvents.Events
{
    class NightOfTheLivingDead : RandomDiseaseEvent
    {
        public NightOfTheLivingDead(int weight = 1)
        {
            ID = nameof(NightOfTheLivingDead);
            Group = Helpers.DIRECT_INFECT_GROUP;
            GeneralName = STRINGS.RANDOM_EVENTS.NIGHT_OF_THE_LIVING_DEAD.NAME;
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Deadly;

            Condition = new Func<object, bool>(data => GameClock.Instance.GetCycle() > 500);

            Event = new Action<object>(
                data =>
                {
                    foreach(MinionIdentity mi in Components.MinionIdentities)
                    {
                        Sicknesses sicknesses = mi.gameObject.GetSicknesses();
                        if (sicknesses == null)
                            continue;

                        sicknesses.Infect(new SicknessExposureInfo(ZombieSickness.ID, GeneralName));
                    }
                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.NIGHT_OF_THE_LIVING_DEAD.TOAST);
                });
        }
    }
}
