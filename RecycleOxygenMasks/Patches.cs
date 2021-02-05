using Harmony;
using UnityEngine;

namespace RecycleOxygenMasks
{
    public class Patches
    {
        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
            }
        }

        [HarmonyPatch(typeof(OxygenMaskConfig))]
        [HarmonyPatch("DoPostConfigure")]
        public class OxygenMaskConfig_DoPostConfigure_Patch
        { 

            public static void Postfix(GameObject go)
            {
                go.AddComponent<OxygenMaskOreDrop>();
            }
        }
    }
}
