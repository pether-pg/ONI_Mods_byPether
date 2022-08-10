using System;

namespace ResearchRequirements
{
    class GVD // = Game Version Dependencies
    {
        public static int LowTierTechs => DlcManager.IsExpansion1Active() ? 19 : 18;

        public static int MidTierTechs => DlcManager.IsExpansion1Active() ? 38 : 51;

        public static int AdvancedResearchThreshold => DlcManager.IsExpansion1Active() ? 15 : 16;
    }
}
