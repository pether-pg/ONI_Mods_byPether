using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded
{
    class RandomExtensions
    {
        public static T WeightedRandom<T>(Dictionary<T, float> optionsWithWeights)
        {
            if (optionsWithWeights == null || optionsWithWeights.Count == 0)
                return default(T);

            float weightSum = 0;
            foreach (float weight in optionsWithWeights.Values)
                weightSum += weight;

            float rand = UnityEngine.Random.Range(0, weightSum);

            foreach (T option in optionsWithWeights.Keys)
                if (rand <= optionsWithWeights[option])
                    return option;
                else
                    rand -= optionsWithWeights[option];

            // this line should never be reached
            return default(T);
        }

        public static T WeightedRandom<T>(Dictionary<T, float> optionsWithWeights, List<T> optionsMask)
        {
            Dictionary<T, float> optionsCopy = new Dictionary<T, float>();
            foreach (T opt in optionsMask)
                optionsCopy.Add(opt, optionsWithWeights[opt]);
            return WeightedRandom<T>(optionsCopy);
        }
    }
}
