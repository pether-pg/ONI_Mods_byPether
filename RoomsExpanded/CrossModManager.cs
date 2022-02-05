using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsExpanded
{
    class CrossModManager
    {
        private static Dictionary<string, bool> EnabledMods;

        public const string FossilModId = "DecorPackB";

        public static bool Initalized { get; private set; }

        public static bool IsModEnabled(string staticId)
        {
            if (!Initalized)
                Debug.Log($"{ModInfo.Namespace}: CrossModManager is not initalized while checking if mod \"{staticId}\" is active - will return false");
            return EnabledMods != null && EnabledMods.ContainsKey(staticId) && EnabledMods[staticId];
        }

        public static void Initalize(List<string> enabledMods)
        {
            EnabledMods = new Dictionary<string, bool>();

            EnabledMods.Add(FossilModId, enabledMods.Contains(FossilModId));

            Initalized = true;
        }
    }
}
