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

        [HarmonyPatch(typeof(EntombedItemManager))]
        [HarmonyPatch("OnDeserialized")]
        public class EntombedItemManager_OnDeserialized_Patch
        {
            public static void Prefix(EntombedItemManager __instance)
            {
                if (!Settings.Instance.PurgeMapFromDisabledGerms)
                    return;

                int germCount = Db.Get().Diseases.Count;

                List<byte> diseaseIndices = Traverse.Create(__instance).Field("diseaseIndices").GetValue<List<byte>>();
                List<int> diseaseCounts = Traverse.Create(__instance).Field("diseaseCounts").GetValue<List<int>>();

                if (diseaseIndices == null || diseaseCounts == null)
                    return;

                for(int i=0; i< diseaseIndices.Count; i++)
                {
                    if(diseaseIndices[i] >= germCount && diseaseIndices[i] != byte.MaxValue)
                    {
                        diseaseIndices[i] = byte.MaxValue;
                        diseaseCounts[i] = 0;
                    }
                }

                Debug.Log($"{ModInfo.Namespace}: Entombed Items Purged!");
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
                PurgeCmps<Edible>(Components.Edibles);
                PurgeCmps<Pickupable>(Components.Pickupables);
                PurgeCmps<BuildingComplete>(Components.BuildingCompletes);

                Debug.Log($"{ModInfo.Namespace}: This cntire city just got purged!");
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
                Debug.Log($"{ModInfo.Namespace}: Map Purged!");
            }

            private static void PurgeCmps<T>(object toPurge) where T : KMonoBehaviour
            {
                Components.Cmps<T> cmps = toPurge as Components.Cmps<T>;
                if (cmps == null)
                    return;

                int germCount = Db.Get().Diseases.Count;
                byte abaIdx = Db.Get().Diseases.GetIndex(AbandonedGerms.ID);

                foreach(T cmp in cmps)
                {
                    PrimaryElement prime = cmp.GetComponent<PrimaryElement>();
                    if (prime == null)
                        continue;

                    if (prime.DiseaseIdx >= germCount && prime.DiseaseIdx != byte.MaxValue)
                        prime.AddDisease(abaIdx, 100000, "Overwrite disabled germs");
                }

                Debug.Log($"{ModInfo.Namespace}: Purged Cmps<{typeof(T)}>");
            }
        }
    }
}
