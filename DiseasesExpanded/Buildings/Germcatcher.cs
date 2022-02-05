using KSerialization;
using UnityEngine;
using System.Collections.Generic;
using Database;

namespace DiseasesExpanded
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class Germcatcher : KMonoBehaviour, ISaveLoadable, ISim1000ms
    {
        [Serialize]
        public Dictionary<byte, int> GatheredGerms;
        public int GatherThreshold = 100000;
        public int EfficiencyDivider = 500;
        private int percentProgress = -1;

        private Dictionary<byte, string> SpawnedFlasks = DlcManager.IsExpansion1Active() ?
            new Dictionary<byte, string>()
            {
                { GermIdx.FoodPoisoningIdx, FoodGermsFlask.ID },
                { GermIdx.PollenGermsIdx, PollenFlask.ID },
                { GermIdx.SlimelungIdx, SlimelungFlask.ID },
                { GermIdx.ZombieSporesIdx, ZombieSporesFlask.ID },
                { GermIdx.RadiationPoisoningIdx, RadiationGermsFlask.ID },
                { GermIdx.BogInsectsIdx, BogBugsFlask.ID },
                { GermIdx.FrostShardsIdx, FrostShardsFlask.ID },
                { GermIdx.GassyGermsIdx, GassyGermFlask.ID },
                { GermIdx.HungerGermsIdx, HungermsFlask.ID }
            }
            :
            new Dictionary<byte, string>()
            {
                { GermIdx.FoodPoisoningIdx, FoodGermsFlask.ID },
                { GermIdx.PollenGermsIdx, PollenFlask.ID },
                { GermIdx.SlimelungIdx, SlimelungFlask.ID },
                { GermIdx.ZombieSporesIdx, ZombieSporesFlask.ID },
                { GermIdx.FrostShardsIdx, FrostShardsFlask.ID },
                { GermIdx.GassyGermsIdx, GassyGermFlask.ID },
                { GermIdx.HungerGermsIdx, HungermsFlask.ID }
            };

        public void Sim1000ms(float dt)
        {
            GatherGerms(dt);
            CompleteCheck();
        }

        private void GatherGerms(float dt)
        {
            if (GatheredGerms == null)
                GatheredGerms = new Dictionary<byte, int>();

            int cell = Grid.PosToCell(this.gameObject);
            byte idx = Grid.DiseaseIdx[cell];
            if (idx == byte.MaxValue)
                return;

            int count = (int)(dt * Grid.DiseaseCount[cell] / EfficiencyDivider);

            if (!GatheredGerms.ContainsKey(idx))
                GatheredGerms.Add(idx, count);
            else
                GatheredGerms[idx] += count;
        }

        private void UpdateStatusItem()
        {
            if (GetHighestGermIdx() == GermIdx.Invalid)
                return;

            StatusItem statusItem = new StatusItem(GermcatcherConfig.ID, "BUILDINGS", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
            string progress = STRINGS.STATUSITEMS.GATHERING.PROGRESS.Replace("{GERMS}", GetHighestGermName()).Replace("{PROGRESS}", percentProgress.ToString());
            statusItem.Name = STRINGS.STATUSITEMS.GATHERING.NAME + progress;
            statusItem.tooltipText = STRINGS.STATUSITEMS.GATHERING.TOOLTIP + progress;
            this.gameObject.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Yield, statusItem);
        }

        private void CompleteCheck()
        {
            if (GetHighestGermIdx() == GermIdx.Invalid)
                return;

            int percent = 100 * GetHighestGermCount() / GatherThreshold;

            if (percent >= 100)
            {
                SpawnFlask(GetHighestGermIdx());
                Util.KDestroyGameObject(this.gameObject);
            }
            else if(percent > percentProgress)
            {
                percentProgress = percent;
                UpdateStatusItem();
            }
        }

        private void SpawnFlask(byte idx)
        {
            string id = string.Empty;
            if (SpawnedFlasks.ContainsKey(idx))
                id = SpawnedFlasks[idx];

            if(!string.IsNullOrEmpty(id))
            {
                GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(id), this.transform.GetPosition() + new Vector3(0, 1, 0), Grid.SceneLayer.Ore);
                if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
                {
                    PrimaryElement element = gameObject.GetComponent<PrimaryElement>();
                    if (element != null)
                        element.AddDisease(idx, GatherThreshold, "Gathered germs");
                    gameObject.SetActive(true);
                }
            }
        }

        public byte GetHighestGermIdx()
        {
            int max = -1;
            byte maxIdx = GermIdx.Invalid;
            foreach (byte idx in GatheredGerms.Keys)
                if(GatheredGerms[idx] > max)
                {
                    max = GatheredGerms[idx];
                    maxIdx = idx;
                }
            return maxIdx;
        }

        public int GetHighestGermCount()
        {
            byte idx = GetHighestGermIdx();
            if(GatheredGerms.ContainsKey(idx))
                return GatheredGerms[idx];
            return 0;
        }

        public string GetHighestGermName()
        {
            return GermIdx.GetGermName(GetHighestGermIdx());
        }
    }
}
