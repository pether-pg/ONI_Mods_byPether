using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class GreatBogBugMigration : RandomDiseaseEvent
    {
        public GreatBogBugMigration(int weight = 1)
        {
            ID = nameof(GreatBogBugMigration);
            Group = Helpers.MAKE_THINGS_GERMY_GROUP;
            GeneralName = STRINGS.RANDOM_EVENTS.GREAT_BOG_BUG_MIGRATION.NAME;
            NameDetails = Util.StripTextFormatting(STRINGS.GERMS.BOGINSECTS.NAME);
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Medium;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data => 
                {
                    foreach (Crop crop in Components.Crops)
                        SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(crop.gameObject), GermIdx.BogInsectsIdx, 1000000);

                    Game.Instance.StartCoroutine(MigrateAway());

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.GREAT_BOG_BUG_MIGRATION.TOAST);
                });
        }

        private IEnumerator MigrateAway()
        {
            yield return new WaitForSeconds(1800);
            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.GREAT_BOG_BUG_MIGRATION.TOAST_END);

            for(int i=0; i<5; i++)
            {
                ClearAllCells();
                yield return new WaitForSeconds(1);
            }
        }

        private void ClearAllCells()
        {
            int overkill = 5;
            foreach (int cell in ONITwitchLib.Utils.GridUtil.ActiveSimCells())
                if (Grid.DiseaseIdx[cell] == GermIdx.BogInsectsIdx && Grid.DiseaseCount[cell] > 0)
                    SimMessages.ModifyDiseaseOnCell(cell, GermIdx.BogInsectsIdx, -1 * overkill * Grid.DiseaseCount[cell]);
        }
    }
}
