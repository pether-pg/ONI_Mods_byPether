using UnityEngine;

namespace DiseasesExpanded
{
    class UVLampSupport
    {
        // Helping code for cross-mod support: Romen's UV Lamp
        // https://steamcommunity.com/sharedfiles/filedetails/?id=2229356245

        private const int CalculationsPerSecond = 5;
        private const int DefaultHalfLifeTime = 3;

        public static float CalculateRate(float desiredTime, float finalRatio)
        {
            // Romen's formula:
            // final count = starting count * (1 - x) ^ time
            // final / starting = (1 - x) ^ time
            // 
            // eg. half life: 
            // 0.5 = (1 - x) ^ t
            // (1-x) = root t (0.5)
            // x = 1 - root t (0.5)
            // x = 1 - pow 1/t (0.5)
            // 
            // eg. Double life:
            // 2 = (1 - x) ^ t
            // (1-x) = root t (2)
            // x = 1 - root t (2)
            // x = 1 - pow 1/t (2)

            float ticks = desiredTime * CalculationsPerSecond;
            float root = Mathf.Pow(finalRatio, 1.0f / ticks);
            return 1 - root;
        }

        public static float CalculateKillRate(float desiredHalfLife)
        {
            return CalculateRate(desiredHalfLife, 0.5f);
        }

        public static float CalculateGrowthRate(float desiredDoubleTime)
        {
            return CalculateRate(desiredDoubleTime, 2);
        }

        public static float GetUVKillRate(float radiationKillRate)
        {
            if (radiationKillRate > 0)
                return CalculateKillRate(DefaultHalfLifeTime / radiationKillRate);
            else if (radiationKillRate < 0)
                return CalculateGrowthRate(DefaultHalfLifeTime / Mathf.Abs(radiationKillRate));

            return 0; // radiationKillRate == 0
        }
    }
}
