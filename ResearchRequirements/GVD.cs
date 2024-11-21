using System;

namespace ResearchRequirements
{
    class GVD // = Game Version Dependencies
    {
        public static int LowTierTechs => DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID) ? 19 : 18;

        public static int MidTierTechs => DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID) ? 38 : 51;

        public static int AdvancedResearchThreshold => DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID) ? 15 : 16;
    }
}
