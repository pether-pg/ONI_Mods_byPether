using System;

namespace ResearchRequirements
{
    class GVD // = Game Version Dependencies
    {
        private static bool ExpansionActive = false;

        public static int LowTierTechs => ExpansionActive ? 19 : 18;

        public static int MidTierTechs => ExpansionActive ? 38 : 51;

        public static int AdvancedResearchThreshold => ExpansionActive ? 15 : 16;

        public static void VersionAlert(bool expectExpansion)
        {
            if (expectExpansion != ExpansionActive)
                throw new NotSupportedException("Resolve Vanilla/DLC version differences");
        }
    }
}
