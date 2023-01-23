using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Twitch
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        [HarmonyAfter(new string[] { "asquared31415.TwitchIntegration" })]
        public static class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                if (Settings.Instance.RandomEvents.EnableTwitchEvents)
                    RandomEvents.AllEvents.RegisterAll();
            }
        }

        [HarmonyPatch(typeof(RadiationEmitter))]
        [HarmonyPatch("OnSpawn")]
        public static class RadiationEmitter_OnSpawn_Patch
        {
            public static Components.Cmps<RadiationEmitter> RadiationEmitters = new Components.Cmps<RadiationEmitter>();

            public static void Postfix(RadiationEmitter __instance)
            {
                if(__instance != null)
                    RadiationEmitters.Add(__instance);
            }
        }

        [HarmonyPatch(typeof(RadiationEmitter))]
        [HarmonyPatch("OnCleanUp")]
        public static class RadiationEmitter_OnCleanUp_Patch
        {
            public static void Postfix(RadiationEmitter __instance)
            {
                if (__instance != null)
                    RadiationEmitter_OnSpawn_Patch.RadiationEmitters.Remove(__instance);
            }
        }

        [HarmonyPatch(typeof(DiseaseDropper.Instance))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(IStateMachineTarget), typeof(DiseaseDropper.Def) })]
        public static class DiseaseDropperInstance_Initialize_Patch
        {
            public static Components.Cmps<DiseaseDropper.Instance> DiseaseDroppers;

            public static void Postfix(DiseaseDropper.Instance __instance)
            {
                UpdateList(__instance);
            }

            public static void UpdateList(DiseaseDropper.Instance inst)
            {
                if (DiseaseDroppers == null)
                    DiseaseDroppers = new Components.Cmps<DiseaseDropper.Instance>();
                DiseaseDroppers.Add(inst);
            }
        }

        [HarmonyPatch(typeof(KSelectable), "ApplyHighlight")]
        public class KSelectable_ApplyHighlight_Patch
        {
            public static void Postfix(KSelectable __instance, float highlight)
            {
                if (__instance == null || __instance.name != RandomEvents.Configs.PaleSlicksterConfig.ID || __instance.gameObject == null)
                    return;

                ApplyHighlight(__instance.gameObject, highlight);
            }

            public static void ApplyHighlight(GameObject go, float value)
            {
                Color paleColor = RandomEvents.Configs.PaleSlicksterConfig.PaleHighlight;
                value *= 0.3f;

                KBatchedAnimController kbac = go.GetComponent<KBatchedAnimController>();
                if (kbac != null)
                    kbac.HighlightColour = new Color(paleColor.r + value, paleColor.g + value, paleColor.b + value);
            }
        }

        [HarmonyPatch(typeof(KSelectable), "ClearHighlight")]
        public class KSelectable_ClearHighlight_Patch
        {
            public static void Postfix(KSelectable __instance)
            {
                if (__instance == null || __instance.name != RandomEvents.Configs.PaleSlicksterConfig.ID || __instance.gameObject == null)
                    return;

                KSelectable_ApplyHighlight_Patch.ApplyHighlight(__instance.gameObject, 0);
            }
        }

    }
}
