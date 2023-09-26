using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class SlimyPollutedOxygen : RandomDiseaseEvent
    {
        public SlimyPollutedOxygen(int weight = 1)
        {
            ID = nameof(SlimyPollutedOxygen);
            Group = Helpers.MAKE_THINGS_GERMY_GROUP;
            GeneralName = STRINGS.RANDOM_EVENTS.SLIMY_POLLUTED_OXYGEN.NAME;
            AppearanceWeight = weight;
            DangerLevel = Helpers.EstimateGermDanger(GermIdx.SlimelungIdx);

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    int germsToAdd = 100000;
                    foreach (int cell in ONITwitchLib.Utils.GridUtil.ActiveSimCells())
                        if (Grid.Element[cell].id == SimHashes.ContaminatedOxygen)
                            SimMessages.ModifyDiseaseOnCell(cell, GermIdx.SlimelungIdx, germsToAdd);

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.SLIMY_POLLUTED_OXYGEN.TOAST);
                });
        }
    }
}
