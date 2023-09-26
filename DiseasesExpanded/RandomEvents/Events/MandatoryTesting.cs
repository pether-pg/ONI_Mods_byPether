using System;
using Klei.AI;

namespace DiseasesExpanded.RandomEvents.Events
{
    class MandatoryTesting : RandomDiseaseEvent
    {
        public MandatoryTesting(int weight = 1)
        {
            ID = nameof(MandatoryTesting);
            GeneralName = STRINGS.RANDOM_EVENTS.MANDATORY_TESTING.NAME;
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.None;

            Condition = new Func<object, bool>(
                data => 
                {
                    foreach(MinionIdentity mi in Components.MinionIdentities)
                    {
                        Sicknesses sicknesses = mi.gameObject.GetSicknesses();
                        if (sicknesses == null)
                            continue;

                        if (sicknesses.Count > 0)
                            return true;
                    }
                    return false; 
                });

            Event = new Action<object>(
                data =>
                {
                    foreach (MinionIdentity mi in Components.MinionIdentities)
                        TestSampleConfig.OnEatComplete(mi.gameObject);

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.MANDATORY_TESTING.TOAST);
                });
        }
    }
}
