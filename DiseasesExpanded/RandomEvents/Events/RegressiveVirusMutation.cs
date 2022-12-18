using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class RegressiveVirusMutation : RandomDiseaseEvent
    {
        public RegressiveVirusMutation(int weight = 1)
        {
            ID = nameof(RegressiveVirusMutation);
            GeneralName = "Regressive Mutation";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.None;
            Condition = new Func<object, bool>(data => MutationData.Instance.GetCompletionPercent() > 0);

            Event = new Action<object>(
                data =>
                {
                    MutationData.Instance.BulkModifyMutation(MutationVectors.GetAttackVectors(), -1);
                    MutationData.Instance.BulkModifyMutation(MutationVectors.GetResilianceVectors(), -1);
                    MutationData.Instance.Mutate();

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "We observed slightly less dangerous variant of the Virus.");
                });
        }
    }
}
