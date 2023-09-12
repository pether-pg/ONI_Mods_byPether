using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded.Patches
{
    class DiseasesExpanded_Patches_PrimaryElement
    {
        [HarmonyPatch(typeof(PrimaryElement))]
        [HarmonyPatch(nameof(PrimaryElement.AddDisease))]
        public static class PrimaryElement_AddDisease_Patch
        {
            public static void Prefix(ref byte disease_idx)
            {
                if (disease_idx >= Db.Get().Diseases.Count)
                    disease_idx = byte.MaxValue;
            }
        }
    }
}
