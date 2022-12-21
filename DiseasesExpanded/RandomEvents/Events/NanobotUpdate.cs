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
            GeneralName = "Nanobot Update";
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


                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "New update to Nanobot software got rushed for release! " + (malicious ? "Sadly, it may contain some bugs..." : ""));
                });
        }

        private IEnumerator WaitToRestore()
        {
            float time = Mathf.Max(60, GameClock.Instance.GetCycle() / 5);
            yield return new WaitForSeconds(time);
            MedicalNanobots.MaliciousOverride = false;
            MedicalNanobotsData.Instance.UpdateAll();
            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "Hotfix released: Malicious issues of Nanobot software got fixed.");
        }
    }
}
