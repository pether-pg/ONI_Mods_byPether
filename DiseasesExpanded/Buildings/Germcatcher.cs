using KSerialization;
using UnityEngine;
using System.Collections.Generic;
using Database;

namespace DiseasesExpanded
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class Germcatcher : KMonoBehaviour
    {
        [Serialize]
        public Dictionary<byte, int> GatheredGerms = new Dictionary<byte, int>();

        public const int GatherThreshold = 100000;
        public int EfficiencyDivider = 100;
        public Vector2Int GatherOffset = new Vector2Int(1, 1);

        private byte lastGatheredIdx = byte.MaxValue;
        public static Dictionary<byte, string> SpawnedFlasks;

        public static void InitalizeFlaskDict()
        {
            if (SpawnedFlasks != null)
                return;

            SpawnedFlasks = new Dictionary<byte, string>();

            SpawnedFlasks.Add(GermIdx.FoodPoisoningIdx, FoodGermsFlask.ID);
            SpawnedFlasks.Add(GermIdx.PollenGermsIdx, PollenFlask.ID);
            SpawnedFlasks.Add(GermIdx.SlimelungIdx, SlimelungFlask.ID);
            SpawnedFlasks.Add(GermIdx.ZombieSporesIdx, ZombieSporesFlask.ID);

            if(DlcManager.IsExpansion1Active())
                SpawnedFlasks.Add(GermIdx.RadiationPoisoningIdx, RadiationGermsFlask.ID);

            if (Settings.Instance.FrostPox.IncludeDisease)
                SpawnedFlasks.Add(GermIdx.FrostShardsIdx, FrostShardsFlask.ID);
            if (Settings.Instance.MooFlu.IncludeDisease)
                SpawnedFlasks.Add(GermIdx.GassyGermsIdx, FrostShardsFlask.ID);
            if (Settings.Instance.AlienGoo.IncludeDisease)
                SpawnedFlasks.Add(GermIdx.AlienGermsIdx, AlienGermFlask.ID);
            if (Settings.Instance.MutatingVirus.IncludeDisease)
                SpawnedFlasks.Add(GermIdx.MutatingGermsIdx, MutatingGermFlask.ID);
            if (DlcManager.IsExpansion1Active() && Settings.Instance.BogInsects.IncludeDisease)
                SpawnedFlasks.Add(GermIdx.BogInsectsIdx, BogBugsFlask.ID);
            if (DlcManager.IsExpansion1Active() && Settings.Instance.HungerGerms.IncludeDisease)
                SpawnedFlasks.Add(GermIdx.HungerGermsIdx, HungermsFlask.ID);
        }

        public void GatherGerms(float dt)
        {
            int cell = Grid.OffsetCell(Grid.PosToCell(this.gameObject), GatherOffset.x, GatherOffset.y);
            byte idx = Grid.DiseaseIdx[cell];
            if (idx == byte.MaxValue)
                return;
            lastGatheredIdx = idx;

            int count = (int)(dt * Grid.DiseaseCount[cell] / EfficiencyDivider);

            AddGerms(idx, count);
            TintWaterSymbol();
        }

        public void TintWaterSymbol()
        {
            if (this.gameObject == null)
                return;

            if (lastGatheredIdx == GermIdx.Invalid)
                return;

            Color32 color = GlobalAssets.Instance.colorSet.GetColorByName(Db.Get().Diseases[lastGatheredIdx].overlayColourName);

            KBatchedAnimController kbac = this.gameObject.GetComponent<KBatchedAnimController>();
            if (kbac != null)
                kbac.SetSymbolTint("water", color);
        }

        public void AddGerms(byte idx, int count)
        {
            if (GatheredGerms == null)
                GatheredGerms = new Dictionary<byte, int>();
            if (!GatheredGerms.ContainsKey(idx))
                GatheredGerms.Add(idx, 0);
            GatheredGerms[idx] += count;

            if (GatheredGerms[idx] >= GatherThreshold)
                SpawnFlask(idx);
        }
        private void SpawnFlask(byte idx)
        {
            GatheredGerms[idx] = 0;

            string id = string.Empty;
            if (SpawnedFlasks.ContainsKey(idx))
                id = SpawnedFlasks[idx];
            else
                id = UnspecifiedFlask.ID;

            if (!string.IsNullOrEmpty(id))
            {
                GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(id), this.transform.GetPosition() + new Vector3(-0.2f, 1.0f, 0), Grid.SceneLayer.Ore);
                if (gameObject != null)
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
            if (GatheredGerms == null)
                return maxIdx;
            foreach (byte idx in GatheredGerms.Keys)
                if (GatheredGerms[idx] > max)
                {
                    max = GatheredGerms[idx];
                    maxIdx = idx;
                }
            return maxIdx;
        }

        public int GetHighestGermCount()
        {
            if (GatheredGerms == null)
                return 0;

            byte idx = GetHighestGermIdx();
            if (GatheredGerms.ContainsKey(idx))
                return GatheredGerms[idx];
            return 0;
        }

        public string GetHighestGermName()
        {
            return GermIdx.GetGermName(GetHighestGermIdx());
        }

        public byte GetCurrentGermIdx()
        {
            return lastGatheredIdx;
        }

        public int GetCurrentGermCount()
        {
            if (GatheredGerms == null)
                return 0;

            byte idx = GetCurrentGermIdx();
            if (GatheredGerms.ContainsKey(idx))
                return GatheredGerms[idx];
            return 0;
        }
        public string GetCurrentGermName()
        {
            return GermIdx.GetGermName(GetCurrentGermIdx());
        }
    }
}
