using UnityEngine;
using KSerialization;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class RustSicknessHistory : KMonoBehaviour
    {
        public int LastFateRoll = 0;

        public List<int> InfectionsOnStages = new List<int>() { 0, 0, 0, 0 };
        public List<int> CuresOnStages = new List<int>() { 0, 0, 0, 0 };

        private List<int> InfectionPenalties    = new List<int>() { 5, 10, 15, 20 };
        private List<int> CureBonuses           = new List<int>() { 0, 5, 10, 15 };
        private int PenaltyPerCycleAge = 1;

        private float thresholdPerStage = 100;
        private const float MIN_CHANCE = 5;
        private const float MAX_CHANCE = 95;

        public float GetAgeAffinity()
        {
            float age = 0;
            MinionIdentity mi = this.gameObject.GetComponent<MinionIdentity>();
            if (mi != null)
                age = (int)(GameClock.Instance.GetCycle() - mi.arrivalTime) * PenaltyPerCycleAge;
            return age;
        }

        public float GetHistoricAffinity()
        {
            float historic = 0;
            for (int stage = 0; stage <= 3; stage++)
                historic += InfectionsOnStages[stage] * InfectionPenalties[stage] - CuresOnStages[stage] * CureBonuses[stage];
            return historic;
        }

        public float GetTotalAffinity()
        {
            return GetAgeAffinity() + GetHistoricAffinity();
        }

        public float GetChanceForStage(int stage)
        {
            if (stage == 1)
                return 200;
            float chance = GetTotalAffinity() - stage * thresholdPerStage;
            return Mathf.Clamp(chance, MIN_CHANCE, MAX_CHANCE);
        }

        public float GetDeathChance()
        {
            return GetChanceForStage(4);
        }

        public List<float> GetRelativeChances(List<int> stages)
        {
            List<float> result = new List<float>();
            float sum = 0;
            foreach (int stage in stages)
                sum += GetChanceForStage(stage);

            foreach (int stage in stages)
                result.Add(100 * GetChanceForStage(stage) / sum);

            return result;
        }

        public int GetRandomizedIndex(List<float> chances, float rollD100)
        {
            float chanceSum = 0;
            for (int i = 0; i < chances.Count; i++)
                chanceSum += chances[i];

            float scaleTo100 = 100.0f / chanceSum;
            for (int i = 0; i < chances.Count; i++)
                chances[i] *= scaleTo100;

            for (int i = 0; i < chances.Count; i++)
                if (rollD100 < chances[i])
                    return i;
                else
                    rollD100 -= chances[i];

            return chances.Count - 1; // should never reach this line
        }
    }
}
