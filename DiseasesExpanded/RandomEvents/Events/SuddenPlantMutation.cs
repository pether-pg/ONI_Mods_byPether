using System;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SuddenPlantMutation : RandomDiseaseEvent
    {
        public SuddenPlantMutation(int weight = 1)
        {
            ID = nameof(SuddenPlantMutation);
            GeneralName = STRINGS.RANDOM_EVENTS.SUDDEN_PLANT_MUTATION.NAME;
            NameDetails = "Plant";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Small;

            Condition = new Func<object, bool>(data => DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID));

            Event = new Action<object>(
                data =>
                {
                    int numberOfPlants = GameClock.Instance.GetCycle() / 5;
                    int max = Mathf.Min(100, Components.MutantPlants.Count / 5);
                    numberOfPlants = Mathf.Clamp(numberOfPlants, 0, max);

                    List<int> possibleIdx = new List<int>();
                    for (int i = 0; i < Components.MutantPlants.Count; i++)
                        possibleIdx.Add(i);
                    possibleIdx.Shuffle();

                    for (int i = 0; i < numberOfPlants; i++)
                    {
                        if (possibleIdx.Count == 0)
                            break;

                        int idx = possibleIdx[0];
                        possibleIdx.RemoveAt(0);

                        if (Components.MutantPlants[idx] != null && Components.MutantPlants[idx].GetComponent<SeedProducer>() != null)
                        {
                            Components.MutantPlants[idx].Mutate();
                            Components.MutantPlants[idx].ApplyMutations();
                        }
                    }

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.SUDDEN_PLANT_MUTATION.TOAST);
                });
        }
    }
}
