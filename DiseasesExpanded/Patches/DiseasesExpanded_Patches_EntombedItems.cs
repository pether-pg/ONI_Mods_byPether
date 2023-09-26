using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_EntombedItems
    {
        [HarmonyPatch(typeof(SaveLoader))]
        [HarmonyPatch(new Type[] { typeof(string), typeof(bool), typeof(bool) })]
        [HarmonyPatch("Save")]
        public class SaveLoader_Save_Patch
        {
            public static void Prefix()
            {
                PreviousGermIndex.Instance.Save();
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

                PreviousGermIndex.Instance.LogDictionary();
                Dictionary<byte, byte> translationDict = PreviousGermIndex.Instance.GetGermTranslationDict();
                TranslateOldGerms(__instance, translationDict);
                PreviousGermIndex.Instance.UpdateSavedDictionary();
                PreviousGermIndex.Instance.LogDictionary();

                Debug.Log($"{ModInfo.Namespace}: Entombed Items Updated!");
            }

            public static void TranslateOldGerms(EntombedItemManager eim, Dictionary<byte, byte> germTranslation)
            {
                if (germTranslation == null || germTranslation.Count == 0)
                    return;

                List<byte> diseaseIndices = Traverse.Create(eim).Field("diseaseIndices").GetValue<List<byte>>();
                List<int> diseaseCounts = Traverse.Create(eim).Field("diseaseCounts").GetValue<List<int>>();

                if (diseaseIndices == null || diseaseCounts == null)
                    return;

                for (int i = 0; i < diseaseIndices.Count; i++)
                {
                    byte idxToTranslate = diseaseIndices[i];
                    if (!germTranslation.ContainsKey(idxToTranslate))
                        continue;

                    diseaseIndices[i] = germTranslation[idxToTranslate];
                    if(germTranslation[idxToTranslate] == byte.MaxValue)
                        diseaseCounts[i] = 0;

                }
            }
        }
    }
}
