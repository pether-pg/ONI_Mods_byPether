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
            EradicateCmps<Pickupable>(Components.Pickupables);
            EradicateCmps<BuildingComplete>(Components.BuildingCompletes);

            int repeat = 5;
            for (int i = 0; i < repeat; i++)
            {
                ClearAllCells();
                yield return new WaitForSeconds(0.2f);
            }
        }

        private void ClearAllCells()
        {
            int overkill = 5;
            foreach (int cell in ONITwitchLib.Utils.GridUtil.ActiveSimCells())
                if (Grid.DiseaseCount[cell] > 0)
                    SimMessages.ModifyDiseaseOnCell(cell, Grid.DiseaseIdx[cell], -1 * overkill * Grid.DiseaseCount[cell]);
        }

        private static void EradicateCmps<T>(object toPurge) where T : KMonoBehaviour
        {
            Components.Cmps<T> cmps = toPurge as Components.Cmps<T>;
            if (cmps == null)
                return;

            int germCount = Db.Get().Diseases.Count;
            byte abaIdx = Db.Get().Diseases.GetIndex(AbandonedGerms.ID);

            foreach (T cmp in cmps)
            {
                PrimaryElement prime;
                if (cmp.TryGetComponent<PrimaryElement>(out prime) == false)
                    continue;

                prime.AddDisease(prime.DiseaseIdx, -prime.DiseaseCount, nameof(EradicateGerms));
            }
        }
    }
}
