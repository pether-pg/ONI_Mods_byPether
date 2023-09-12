using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System.Collections;
using System.Collections.Generic;

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

        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("OnSpawn")]
        public class Game_OnPrefabInit_Patch
        {
            public static void Postfix()
            {
                if (Settings.Instance.PurgeMapFromDisabledGerms)
                    GameScheduler.Instance.Schedule("Purge the Map", 0.2f, obj => ThisEntireCityMustBePurged());
            }

            private static void ThisEntireCityMustBePurged()
            {
                PurgeMap();
                Debug.Log($"{ModInfo.Namespace}: Map Purged!");
            }

            private static void PurgeMap()
            {
                int germCount = Db.Get().Diseases.Count;

                for (int x = 0; x < Grid.WidthInCells; x++)
                    for (int y = 0; y < Grid.HeightInCells; y++)
                    {
                        int cell = Grid.XYToCell(x, y);
                        if (Grid.DiseaseIdx[cell] != byte.MaxValue && Grid.DiseaseIdx[cell] >= germCount)
                        {
                            SimMessages.ConsumeDisease(cell, 1.0f, int.MaxValue, 0);
                        }
                    }
            }
        }
    }
}
