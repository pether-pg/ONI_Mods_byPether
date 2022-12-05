using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class HungryPet : RandomDiseaseEvent
    {
        public HungryPet(int weight = 1)
        {
            ID = nameof(HungryPet);
            GeneralName = "Hungry Pet";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Medium;

            Condition = new Func<object, bool>(data => FindTamedCritters().Count > 0);

            Event = new Action<object>(
                data =>
                {
                    List<GameObject> possibles = FindTamedCritters();
                    possibles.Shuffle();

                    foreach(GameObject go in possibles)
                    {
                        if (go == null)
                            continue;

                        CritterSicknessMonitor monitor = go.GetComponent<CritterSicknessMonitor>();
                        if (monitor == null)
                            continue;

                        monitor.Infect();
                        break;
                    }

                });
        }

        private List<GameObject> FindTamedCritters()
        {
            List<GameObject> result = new List<GameObject>();

            foreach (Brain brain in Components.Brains)
            {
                if (brain == null || brain.gameObject == null)
                    continue;

                WildnessMonitor.Instance smi = brain.gameObject.GetSMI<WildnessMonitor.Instance>();
                if (smi == null || smi.wildness.value > 0)
                    continue;

                result.Add(brain.gameObject);
            }

            return result;
        }
    }
}
