using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded.RandomEvents.Events
{
    class MiraculousRecovery : RandomDiseaseEvent
    {
        public MiraculousRecovery(int weight = 1)
        {
            ID = nameof(MiraculousRecovery);
            GeneralName = "Miraculous Recovery";
            DangerLevel = ONITwitchLib.Danger.None;
            AppearanceWeight = weight;

            Condition = new System.Func<object, bool>(data => SickDuplicants().Count > 0);

            Event = new System.Action<object>(
                data =>
                {
                    foreach(MinionIdentity mi in SickDuplicants())
                    {
                        if (mi.gameObject == null)
                            continue;

                        Sicknesses sicknesses = mi.gameObject.GetSicknesses();
                        if (sicknesses != null)
                            while (sicknesses.Count > 0)
                                sicknesses[0].Cure();

                        Effects effects = mi.GetComponent<Effects>(); 
                        if (effects != null)
                            if (effects.HasEffect(AlienSickness.ASSIMILATION_EFFECT_ID))
                                effects.Remove(AlienSickness.ASSIMILATION_EFFECT_ID);

                    }
                });

        }

        public static List<MinionIdentity> SickDuplicants()
        {
            List<MinionIdentity> result = new List<MinionIdentity>();

            foreach(MinionIdentity mi in Components.MinionIdentities)
            {
                if (mi.gameObject == null)
                    continue;

                Sicknesses sicknesses = mi.gameObject.GetSicknesses();
                if(sicknesses != null && sicknesses.Count > 0)
                {
                    result.Add(mi);
                    continue;
                }

                Effects effects = mi.GetComponent<Effects>();
                if (effects != null && effects.HasEffect(AlienSickness.ASSIMILATION_EFFECT_ID))
                {
                    result.Add(mi);
                    continue;
                }
            }

            return result;
        }
    }
}
