using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class NanobotUpdate : RandomDiseaseEvent
    {
        public NanobotUpdate(bool malicious = false, int weight = 1)
        {
            GeneralName = STRINGS.RANDOM_EVENTS.NANOBOT_UPDATE.NAME;
            NameDetails = malicious ? "Malicious" : "Normal";
            ID = GenerateId(nameof(NanobotUpdate), NameDetails);
            AppearanceWeight = weight;
            DangerLevel = malicious ? ONITwitchLib.Danger.Deadly : ONITwitchLib.Danger.None;

            Condition = new Func<object, bool>(data => malicious ? GameClock.Instance.GetCycle() > 500 : true);

            Event = new Action<object>(
                data =>
                {
                    List<MutationVectors.Vectors> possibles = new List<MutationVectors.Vectors>();
                    possibles.AddRange(MutationVectors.GetAttackVectors());
                    possibles.AddRange(MutationVectors.GetResilianceVectors());
                    possibles.Shuffle();
                    MutationVectors.Vectors vector = possibles[0];

                    if (malicious)
                    {
                        MedicalNanobots.MaliciousOverride = true;
                        SaveGame.Instance.StartCoroutine(WaitToRestore());
                    }

                    MedicalNanobotsData.Instance.IncreaseDevelopment(vector);

                    foreach (Telepad pad in Components.Telepads)
                        SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(pad.gameObject), GermIdx.MedicalNanobotsIdx, (int)MedicalNanobotsData.RECIPE_MASS_LARGE);


                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.NANOBOT_UPDATE.TOAST + (malicious ? (string)STRINGS.RANDOM_EVENTS.NANOBOT_UPDATE.TOAST_MALICIOUS : ""));
                });
        }

        private IEnumerator WaitToRestore()
        {
            float time = Mathf.Max(60, GameClock.Instance.GetCycle() / 5);
            yield return new WaitForSeconds(time);
            MedicalNanobots.MaliciousOverride = false;
            MedicalNanobotsData.Instance.UpdateAll();
            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.NANOBOT_UPDATE.TOAST_END);
        }
    }
}
