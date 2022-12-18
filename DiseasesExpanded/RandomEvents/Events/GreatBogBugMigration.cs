using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class GreatBogBugMigration : RandomDiseaseEvent
    {
        public GreatBogBugMigration(int weight = 1)
        {
            ID = nameof(GreatBogBugMigration);
            GeneralName = "Great Bog Bug Migration";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Medium;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data => 
                {
                    foreach (Crop crop in Components.Crops)
                        SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(crop.gameObject), GermIdx.BogInsectsIdx, 1000000);

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "Researchers proclaim cycle of the Bog Bugs. All dwellings increase population.");
                });
        }
    }
}
