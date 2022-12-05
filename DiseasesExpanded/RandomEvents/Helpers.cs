using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents
{
    class Helpers
    {
        public static ONITwitchLib.Danger EstimateGermDanger(byte idx)
        {
            if (idx == GermIdx.PollenGermsIdx)
                return ONITwitchLib.Danger.None;

            if (idx == GermIdx.FoodPoisoningIdx)
                return ONITwitchLib.Danger.Small;

            if (idx == GermIdx.SlimelungIdx)
                return ONITwitchLib.Danger.Medium;

            if (idx == GermIdx.ZombieSporesIdx)
                return ONITwitchLib.Danger.Extreme;

            if (idx == GermIdx.AlienGermsIdx)
                return ONITwitchLib.Danger.High;

            if (idx == GermIdx.BogInsectsIdx)
                return ONITwitchLib.Danger.Small;

            if (idx == GermIdx.FrostShardsIdx)
                return ONITwitchLib.Danger.Small;

            if (idx == GermIdx.GassyGermsIdx)
                return ONITwitchLib.Danger.Small;

            if (idx == GermIdx.HungerGermsIdx)
                return ONITwitchLib.Danger.Medium;

            if (idx == GermIdx.MedicalNanobotsIdx)
                return ONITwitchLib.Danger.None;

            if (idx == GermIdx.MutatingGermsIdx)
                return ONITwitchLib.Danger.Extreme;

            // Assume the worst by default
            return ONITwitchLib.Danger.Deadly;
        }
    }
}
