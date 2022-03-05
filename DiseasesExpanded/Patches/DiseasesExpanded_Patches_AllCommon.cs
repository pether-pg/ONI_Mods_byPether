using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    public class DiseasesExpanded_Patches_AllCommon
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
                namedLookup.Add(HungerGerms.ID, HungerGerms.colorValue);
                namedLookup.Add(BogInsects.ID, BogInsects.colorValue);
                namedLookup.Add(FrostShards.ID, FrostShards.colorValue);
                namedLookup.Add(GassyGerms.ID, GassyGerms.colorValue);

                initalized = true;
                //LogColors(namedLookup);
            }

            static void LogColors(Dictionary<string, Color32> namedLookup)
            {
                foreach (string key in namedLookup.Keys)
                    if (namedLookup.ContainsKey(key))
                        Debug.Log($"Color of {key}: R = {namedLookup[key].r}, G = {namedLookup[key].g}, B = {namedLookup[key].b}, A = {namedLookup[key].a}");                    
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public static class Db_Initialize_Patch
        {
            public static void Prefix()
            {
                BasicModUtils.MakeGermStrings(BogInsects.ID, STRINGS.GERMS.BOGINSECTS.NAME, STRINGS.GERMS.BOGINSECTS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeGermStrings(FrostShards.ID, STRINGS.GERMS.FROSTHARDS.NAME, STRINGS.GERMS.FROSTHARDS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeGermStrings(GassyGerms.ID, STRINGS.GERMS.GASSYGERMS.NAME, STRINGS.GERMS.GASSYGERMS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeGermStrings(HungerGerms.ID, STRINGS.GERMS.HUNGERGERMS.NAME, STRINGS.GERMS.HUNGERGERMS.LEGEND_HOVERTEXT);

                BasicModUtils.MakeDiseaseStrings(BogSickness.ID, STRINGS.DISEASES.BOGSICKNESS.NAME, STRINGS.DISEASES.BOGSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.BOGSICKNESS.DESCRIPTION, STRINGS.DISEASES.BOGSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(FrostSickness.ID, STRINGS.DISEASES.FROSTSICKNESS.NAME, STRINGS.DISEASES.FROSTSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.FROSTSICKNESS.DESCRIPTION, STRINGS.DISEASES.FROSTSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(GasSickness.ID, STRINGS.DISEASES.GASSICKNESS.NAME, STRINGS.DISEASES.GASSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.GASSICKNESS.DESCRIPTION, STRINGS.DISEASES.GASSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(HungerSickness.ID, STRINGS.DISEASES.HUNGERSICKNESS.NAME, STRINGS.DISEASES.HUNGERSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.HUNGERSICKNESS.DESCRIPTION, STRINGS.DISEASES.HUNGERSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(SpindlySickness.ID, STRINGS.DISEASES.SPINDLYCURSE.NAME, STRINGS.DISEASES.SPINDLYCURSE.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.SPINDLYCURSE.DESCRIPTION, STRINGS.DISEASES.SPINDLYCURSE.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(TemporalDisplacementSickness.ID, STRINGS.DISEASES.TEMPORALDISPLACEMENT.NAME, STRINGS.DISEASES.TEMPORALDISPLACEMENT.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.TEMPORALDISPLACEMENT.DESCRIPTION, STRINGS.DISEASES.TEMPORALDISPLACEMENT.LEGEND_HOVERTEXT);

                BasicModUtils.MakeTraitStrings(InsectAllergies.ID, STRINGS.TRAITS.INSECTALLERGIES.NAME, STRINGS.TRAITS.INSECTALLERGIES.DESC, STRINGS.TRAITS.INSECTALLERGIES.SHORT_DESC, STRINGS.TRAITS.INSECTALLERGIES.SHORT_DESC_TOOLTIP);

                ExpandExposureTable();

                DiseasesExpanded_Patches_Spindly.UpdateNarcolepsyTimes();
            }

            public static void Postfix()
            {
                float cycle = 600;
                float cycle2 = 2 * cycle;
                float cycle10 = 10 * cycle;
                float cycle50 = 50 * cycle;

                Db.Get().effects.Add(new Effect(FrostSickness.RECOVERY_ID, STRINGS.EFFECTS.FROSTRECOVERY.NAME, STRINGS.EFFECTS.FROSTRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(GasSickness.RECOVERY_ID, STRINGS.EFFECTS.GASRECOVERY.NAME, STRINGS.EFFECTS.GASRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(HungerSickness.RECOVERY_ID, STRINGS.EFFECTS.HUNGERRECOVERY.NAME, STRINGS.EFFECTS.HUNGERRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(BogSickness.RECOVERY_ID, STRINGS.EFFECTS.BOGRECOVERY.NAME, STRINGS.EFFECTS.BOGRECOVERY.DESC, cycle2, true, true, false));
                
                Db.Get().effects.Add(new Effect(GasCureConfig.EffectID, STRINGS.CURES.GASCURE.NAME, STRINGS.CURES.GASCURE.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(MudMaskConfig.EffectID, STRINGS.CURES.MUDMASK.NAME, STRINGS.CURES.MUDMASK.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(SapShotConfig.EffectID, STRINGS.CURES.SAPSHOT.NAME, STRINGS.CURES.SAPSHOT.DESC, cycle10, true, true, false));

                Db.Get().effects.Add(new Effect(AllergyVaccineConfig.EffectID, AllergyVaccineConfig.Name, AllergyVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(SlimelungVaccineConfig.EffectID, SlimelungVaccineConfig.Name, SlimelungVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(ZombieSporesVaccineConfig.EffectID, ZombieSporesVaccineConfig.Name, ZombieSporesVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(HungermsVaccineConfig.EffectID, HungermsVaccineConfig.Name, HungermsVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(GassyVaccineConfig.EffectID, GassyVaccineConfig.Name, GassyVaccineConfig.Desc, cycle50, true, true, false));
            }

            public static void ExpandExposureTable()
            {
                List<ExposureType> exposureList = new List<ExposureType>();
                foreach (ExposureType et in TUNING.GERM_EXPOSURE.TYPES)
                {
                    if (et.sickness_id == "Allergies")
                    {
                        if (et.excluded_effects == null)
                            et.excluded_effects = new List<string>();
                        et.excluded_effects.Add(AllergyVaccineConfig.EffectID);
                    }
                    if (et.germ_id == "SlimeLung")
                    {
                        if (et.excluded_effects == null)
                            et.excluded_effects = new List<string>();
                        et.excluded_effects.Add(SlimelungVaccineConfig.EffectID);
                    }
                    if (et.germ_id == "ZombieSpores")
                    {
                        if (et.excluded_effects == null)
                            et.excluded_effects = new List<string>();
                        et.excluded_effects.Add(ZombieSporesVaccineConfig.EffectID);
                    }
                    exposureList.Add(et);
                }

                exposureList.Add(BogInsects.GetExposureType());
                exposureList.Add(FrostShards.GetExposureType());
                exposureList.Add(GassyGerms.GetExposureType());
                exposureList.Add(HungerGerms.GetExposureType());

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
                __instance.Add(new SpindlySickness());
            }
        }

        [HarmonyPatch(typeof(Diseases))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ResourceSet), typeof(bool) })]
        public class Diseases_Constructor_Patch
        {
            public static void Prefix()
            {
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = HungerGerms.ID, overlayColourName = HungerGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = BogInsects.ID, overlayColourName = BogInsects.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = FrostShards.ID, overlayColourName = FrostShards.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = GassyGerms.ID, overlayColourName = GassyGerms.ID });
            }

            public static void Postfix(ref Diseases __instance, bool statsOnly)
            {
                __instance.Add(new BogInsects(statsOnly));
                __instance.Add(new FrostShards(statsOnly));
                __instance.Add(new GassyGerms(statsOnly));
                __instance.Add(new HungerGerms(statsOnly));
            }
        }

        [HarmonyPatch(typeof(StandardCropPlant.States))]
        [HarmonyPatch("InitializeStates")]
        public static class StandardCropPlant_InitializeStates_Patch
        {
            public static void Postfix(StandardCropPlant.States __instance)
            {
                __instance.dead.ToggleTag(GameTags.PreventEmittingDisease);
                __instance.alive.wilting.ToggleTag(GameTags.PreventEmittingDisease);
            }
        }
    }
}
