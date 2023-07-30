using System;
using UnityEngine;
using Klei.AI;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SuddenVirusMutation : RandomDiseaseEvent
    {
        public SuddenVirusMutation(int dangerLvl, int weight = 1)
        {
            ID = GenerateId(nameof(SuddenVirusMutation), dangerLvl);
            GeneralName = "Sudden Mutation";
            NameDetails = "Virus";
            AppearanceWeight = weight;
            
            dangerLvl = Mathf.Clamp(dangerLvl, (int)ONITwitchLib.Danger.None, (int)ONITwitchLib.Danger.Deadly);
            DangerLevel = (ONITwitchLib.Danger)dangerLvl;

            Condition = new Func<object, bool>(
                data => 
                {
                    float currentProgress = MutationData.Instance.GetCompletionPercent();
                    float min = 1.0f * dangerLvl / (int)ONITwitchLib.Danger.Deadly;
                    float max = 1.0f * (dangerLvl + 1) / (int)ONITwitchLib.Danger.Deadly;

                    return min <= currentProgress && currentProgress < max; 
                });

            Event = new Action<object>(
                data =>
                {
                    int randomIdx = UnityEngine.Random.Range(0, Components.MinionIdentities.Count);
                    MinionIdentity mi = Components.MinionIdentities[randomIdx];
                    if (mi == null)
                        return;

                    MutationData.Instance.Mutate(mi.gameObject);                    
                    MutationData.Instance.TryInfectDelayed(mi.gameObject);

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "We observed new mutation strain...");
                });
        }
    }
}
