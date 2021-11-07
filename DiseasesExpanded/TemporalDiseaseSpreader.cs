using UnityEngine;
using System;

namespace DiseasesExpanded
{
    class TemporalDiseaseSpreader : KMonoBehaviour
    {
        protected override void OnSpawn()
        {
            base.OnSpawn();
            GameClock.Instance.Subscribe((int)GameHashes.NewDay, this.OnNewDay);
        }

        protected override void OnCleanUp()
        {
            GameClock.Instance.Unsubscribe((int)GameHashes.NewDay, this.OnNewDay);
            base.OnCleanUp();
        }

        public void OnNewDay(object data)
        {
            foreach(MinionIdentity minion in Components.MinionIdentities)
                if (GetMinionInfectionChance(minion) > UnityEngine.Random.Range(0f, 100f))
                    InfectMinion(minion);
        }

        public int GetDistanceToMinion(MinionIdentity minion)
        {
            AxialI hexMinion = minion.GetMyWorldLocation();
            AxialI hexTear = ClusterManager.Instance.GetComponent<ClusterPOIManager>().GetTemporalTear().GetMyWorldLocation();
            return AxialUtil.GetDistance(hexMinion, hexTear);
        }

        public float GetMinionInfectionChance(MinionIdentity minion)
        {
            float baseChance = 100;
            if (IsMinionWearingLeadSuit(minion))
                baseChance /= 2;
            if (Settings.Instance.RebalanceForDiseasesRestored)
                baseChance *= 4;

            return baseChance / GetDistanceToMinion(minion);
        }

        public bool IsMinionWearingLeadSuit(MinionIdentity minion)
        {
            foreach (AssignableSlotInstance slot in minion.GetEquipment().Slots)
            {
                Equippable assignable = slot.assignable as Equippable;
                if (assignable != null && assignable.GetComponent<KPrefabID>().name == LeadSuitConfig.ID)
                    return true;
            }
            return false;
        }

        public void InfectMinion(MinionIdentity minion)
        {
            Debug.Log($"{ModInfo.Namespace}: Duplicant {minion.name} infected");
        }
    }
}
