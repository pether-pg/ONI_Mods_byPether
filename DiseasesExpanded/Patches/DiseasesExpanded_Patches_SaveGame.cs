using HarmonyLib;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_SaveGame
    {
        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("DestroyInstances")]
        public class Game_DestroyInstances_Patch
        {
            public static void Prefix()
            {
                MutationData.Clear();
                MedicalNanobotsData.Clear();
                ShieldData.Clear();
            }
        }

        [HarmonyPatch(typeof(SaveGame))]
        [HarmonyPatch("OnPrefabInit")]
        public class SaveGame_OnPrefabInit_Patch
        {
            public static void Postfix(SaveGame __instance)
            {
                __instance.gameObject.AddComponent<MutationData>();
                __instance.gameObject.AddComponent<MedicalNanobotsData>();
                __instance.gameObject.AddComponent<ShieldData>();
            }
        }
    }
}
