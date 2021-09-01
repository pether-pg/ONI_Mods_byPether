using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    public class DiseasesExpanded_Patches_AllGermDefinitions
    {
        [HarmonyPatch(typeof(ColorSet))]
        [HarmonyPatch("Init")]
        public static class ColorSet_Init_Patch
        {
            static bool initalized = false;

            public static void Postfix(ColorSet __instance)
            {
                if (initalized)
                    return;

                Dictionary<string, Color32> namedLookup = Traverse.Create(__instance).Field("namedLookup").GetValue<Dictionary<string, Color32>>();
                namedLookup.Add(HungerGerms.staticId, HungerGerms.colorValue);
                namedLookup.Add(BogInsects.staticId, BogInsects.colorValue);
                namedLookup.Add(FrostShards.staticId, FrostShards.colorValue);
                namedLookup.Add(GassyGerms.staticId, GassyGerms.colorValue);

                initalized = true;
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public static class Db_Initialize_Patch
        {
            public static void Prefix()
            {
                BasicModUtils.MakeStrings(HungerGerms.staticId, "Hungerms", "Hungry belly", "Makes you hungry", "HungerGerms - hover text");
                BasicModUtils.MakeStrings(BogInsects.staticId, "Bog Insects", "Insect bites", "Those bites can be painful", "BogInsects - hover text");
                BasicModUtils.MakeStrings(FrostShards.staticId, "Frost Shards", "Frost on the skin", "Makes you feel cold more", "FrostShards - hover text");
                BasicModUtils.MakeStrings(GassyGerms.staticId, "Gassy Germs", "Smelly farts", "Makes you fart as a Moo", "GassyGerms - hover text");

                ExpandExposureTable();
            }

            public static void Postfix()
            {
                Db.Get().effects.Add(new Effect(FrostSickness.RECOVERY_ID, FrostSickness.RECOVERY_ID, FrostSickness.RECOVERY_ID, 1200, true, true, false));
                Db.Get().effects.Add(new Effect(GasSickness.RECOVERY_ID, GasSickness.RECOVERY_ID, GasSickness.RECOVERY_ID, 1200, true, true, false));
            }

            public static void ExpandExposureTable()
            {
                List<ExposureType> exposureList = new List<ExposureType>();
                foreach (ExposureType et in TUNING.GERM_EXPOSURE.TYPES)
                    exposureList.Add(et);

                exposureList.Add(DiseasesExpanded_Patches_Frost.GetExposureType());
                exposureList.Add(DiseasesExpanded_Patches_Gas.GetExposureType());
                exposureList.Add(new ExposureType()
                {
                    germ_id = BogInsects.ID,
                    sickness_id = BogSickness.ID,
                    exposure_threshold = 1,
                    excluded_traits = new List<string>() { },
                    base_resistance = 2,
                    excluded_effects = new List<string>()
                    {
                      BogSickness.RECOVERY_ID
                    }
                });
                exposureList.Add(new ExposureType()
                {
                    germ_id = HungerGerms.ID,
                    sickness_id = HungerSickness.ID,
                    exposure_threshold = 1,
                    excluded_traits = new List<string>() { },
                    base_resistance = 2,
                    excluded_effects = new List<string>()
                    {
                      HungerSickness.RECOVERY_ID
                    }
                });

                TUNING.GERM_EXPOSURE.TYPES = exposureList.ToArray();
            }
        }
        [HarmonyPatch(typeof(Database.Sicknesses))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ResourceSet) })]
        public class Sicknesses_Constructor_Patch
        {
            public static void Postfix(ref Database.Sicknesses __instance)
            {
                __instance.Add(new HungerSickness());
                __instance.Add(new BogSickness());
                __instance.Add(new FrostSickness());
                __instance.Add(new GasSickness());
            }
        }

        [HarmonyPatch(typeof(Diseases))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ResourceSet), typeof(bool) })]
        public class Diseases_Constructor_Patch
        {
            public static void Prefix()
            {
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = HungerGerms.staticId, overlayColourName = HungerGerms.staticId });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = BogInsects.staticId, overlayColourName = BogInsects.staticId });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = FrostShards.staticId, overlayColourName = FrostShards.staticId });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = GassyGerms.staticId, overlayColourName = GassyGerms.staticId });
            }

            public static void Postfix(ref Diseases __instance, bool statsOnly)
            {
                __instance.Add(new HungerGerms(statsOnly));
                __instance.Add(new BogInsects(statsOnly));
                __instance.Add(new FrostShards(statsOnly));
                __instance.Add(new GassyGerms(statsOnly));
            }
        }

        [HarmonyPatch(typeof(SapTreeConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class SapTreeConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)HungerGerms.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = HungerGerms.ID;
            }
        }

        [HarmonyPatch(typeof(SwampHarvestPlantConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class SwampHarvestPlantConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)BogInsects.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = BogInsects.ID;
            }
        }

        [HarmonyPatch(typeof(GasGrassConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class GasGrassConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)GassyGerms.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = GassyGerms.ID;
            }
        }

        [HarmonyPatch(typeof(OverlayModes.Rooms))]
        [HarmonyPatch("GetCustomLegendData")]
        public static class Rooms_GetCustomLegendData_Patch
        {
            public static void Postfix()
            {
                foreach (MinionIdentity identity in Components.MinionIdentities)
                {
                    Debug.Log($"{identity.name} :");
                    MinionModifiers modifiers = identity.GetComponent<MinionModifiers>();
                    if (modifiers == null) return;
                    foreach (var v in modifiers.attributes.AttributeTable)
                        Debug.Log($"{v.Attribute.Id} = {v.GetTotalValue()}");
                }
            }
        }
    }
}
