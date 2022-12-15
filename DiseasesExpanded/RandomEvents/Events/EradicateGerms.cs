using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class EradicateGerms : RandomDiseaseEvent
    {
        public EradicateGerms(int weight = 1)
        {
            ID = nameof(EradicateGerms);
            GeneralName = "Eradicate Germs";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.None;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    foreach (int cell in ONITwitchLib.Utils.GridUtil.ActiveSimCells())
                        if (Grid.DiseaseCount[cell] > 0)
                            SimMessages.ModifyDiseaseOnCell(cell, Grid.DiseaseIdx[cell], -Grid.DiseaseCount[cell]);
                });
        }
    }
}
