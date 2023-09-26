using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.Events
{
    class EradicateGerms : RandomDiseaseEvent
    {
        public EradicateGerms(int weight = 1)
        {
            ID = nameof(EradicateGerms);
            GeneralName = STRINGS.RANDOM_EVENTS.ERADICATE_GERMS.NAME;
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.None;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data =>
                {
                    Game.Instance.StartCoroutine(Eradicate());

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.ERADICATE_GERMS.TOAST);
                });
        }

        private IEnumerator Eradicate()
        {
            int repeat = 5;
            for(int i = 0; i < repeat; i++)
            {
                ClearAllCells();
                yield return new WaitForSeconds(1);
            }
            ClearAllBuildings();
        }


        private void ClearAllCells()
        {
            int overkill = 5;
            foreach (int cell in ONITwitchLib.Utils.GridUtil.ActiveSimCells())
                if (Grid.DiseaseCount[cell] > 0)
                    SimMessages.ModifyDiseaseOnCell(cell, Grid.DiseaseIdx[cell], -1 * overkill * Grid.DiseaseCount[cell]);
        }

        private void ClearAllBuildings()
        {
            foreach(BuildingComplete building in Components.BuildingCompletes)
            {
                if (building == null)
                    continue;

                PrimaryElement prime = building.primaryElement;
                if (prime.DiseaseIdx == GermIdx.Invalid || prime.DiseaseCount <= 0)
                    continue;

                prime.AddDisease(prime.DiseaseIdx, -prime.DiseaseCount, GeneralName);
            }
        }
    }
}
