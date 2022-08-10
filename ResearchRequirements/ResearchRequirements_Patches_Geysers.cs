using HarmonyLib;
using System.Collections.Generic;

namespace ResearchRequirements
{
    class ResearchRequirements_Patches_Geysers
    {
        [HarmonyPatch(typeof(Geyser))]
        [HarmonyPatch("OnSpawn")]
        public class Geyser_OnSpawn_Patch
        {
            public static List<Geyser> Geysers { get; private set; }

            private static void AddGeyser(Geyser geyser)
            {
                if (Geysers == null)
                    Geysers = new List<Geyser>();

                if (!Geysers.Contains(geyser))
                    Geysers.Add(geyser);
            }

            public static void Postfix(Geyser __instance)
            {
                AddGeyser(__instance);
            }
        }
    }
}
