using HarmonyLib;
using UnityEngine;

namespace BiobotUpgrades
{
    public class Patches
    {
        [HarmonyPatch(typeof(MorbRoverConfig))]
        [HarmonyPatch("OnSpawn")]
        public class Db_Initialize_Patch
        {

            public static void Postfix(GameObject inst)
            {
                inst.AddComponent<SporeFuelMaker>();
            }
        }
    }
}
