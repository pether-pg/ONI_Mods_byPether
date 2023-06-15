using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Faces
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public static class Db_Initialize_Accessories_Patch
        {
            public static void Postfix(Db __instance)
            {
                ModdedAccesories.Register(__instance.Accessories, __instance.AccessorySlots);
            }
        }

        [HarmonyPatch(typeof(Faces))]
        [HarmonyPatch(MethodType.Constructor)]
        public static class Faces_Constructor_Patch
        {
            public static void Postfix(Faces __instance)
            {
                ModdedFaces.Register(__instance);
            }
        }

        [HarmonyPatch(typeof(Expressions))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ResourceSet) })]
        public static class Expressions_Constructor_Patch
        {
            public static void Postfix(Expressions __instance)
            {
                ModdedExpressions.Register(__instance);
            }
        }
    }
}
