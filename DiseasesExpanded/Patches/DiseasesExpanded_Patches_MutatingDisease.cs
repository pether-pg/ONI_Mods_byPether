using HarmonyLib;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_MutatingDisease
    {
        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("DestroyInstances")]
        public class Game_DestroyInstances_Patch
        {
            public static void Prefix()
            {
                MutationData.Clear();
            }
        }

        [HarmonyPatch(typeof(SaveGame))]
        [HarmonyPatch("OnPrefabInit")]
        public class SaveGame_OnPrefabInit_Patch
        {
            public static void Postfix(SaveGame __instance)
            {
                __instance.gameObject.AddComponent<MutationData>();
            }
        }

        [HarmonyPatch(typeof(Sickness))]
        [HarmonyPatch("Cure")]
        public class Sickness_Cure_Patch
        {
            public static void Postfix(GameObject go)
            {
                MutationData.Instance.IncreaseMutationProgress(go);
            }
        }


    }
}
