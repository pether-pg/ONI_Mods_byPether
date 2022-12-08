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
                namedLookup.Add(AlienGerms.ID, AlienGerms.colorValue);
                namedLookup.Add(MutatingGerms.ID, MutatingGerms.colorValue);
                namedLookup.Add(MedicalNanobots.ID, MedicalNanobots.colorValue);

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
                BasicModUtils.MakeGermStrings(BogInsects.ID, STRINGS.GERMS.BOGINSECTS.NAME, STRINGS.GERMS.BOGINSECTS.LEGEND_HOVERTEXT, STRINGS.DISEASES.BOGSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(FrostShards.ID, STRINGS.GERMS.FROSTHARDS.NAME, STRINGS.GERMS.FROSTHARDS.LEGEND_HOVERTEXT, STRINGS.DISEASES.FROSTSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(GassyGerms.ID, STRINGS.GERMS.GASSYGERMS.NAME, STRINGS.GERMS.GASSYGERMS.LEGEND_HOVERTEXT, STRINGS.DISEASES.GASSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(HungerGerms.ID, STRINGS.GERMS.HUNGERGERMS.NAME, STRINGS.GERMS.HUNGERGERMS.LEGEND_HOVERTEXT, STRINGS.DISEASES.HUNGERSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(AlienGerms.ID, STRINGS.GERMS.ALIENGERMS.NAME, STRINGS.GERMS.ALIENGERMS.LEGEND_HOVERTEXT, STRINGS.DISEASES.ALIENSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(MutatingGerms.ID, STRINGS.GERMS.MUTATINGGERMS.NAME, STRINGS.GERMS.MUTATINGGERMS.LEGEND_HOVERTEXT, STRINGS.DISEASES.MUTATINGSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(MedicalNanobots.ID, STRINGS.GERMS.MEDICALNANOBOTS.NAME, STRINGS.GERMS.MEDICALNANOBOTS.LEGEND_HOVERTEXT, STRINGS.GERMS.MEDICALNANOBOTS.DESCRIPTION);

                BasicModUtils.MakeDiseaseStrings(BogSickness.ID, STRINGS.DISEASES.BOGSICKNESS.NAME, STRINGS.DISEASES.BOGSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.BOGSICKNESS.DESCRIPTION, STRINGS.DISEASES.BOGSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(FrostSickness.ID, STRINGS.DISEASES.FROSTSICKNESS.NAME, STRINGS.DISEASES.FROSTSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.FROSTSICKNESS.DESCRIPTION, STRINGS.DISEASES.FROSTSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(GasSickness.ID, STRINGS.DISEASES.GASSICKNESS.NAME, STRINGS.DISEASES.GASSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.GASSICKNESS.DESCRIPTION, STRINGS.DISEASES.GASSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(HungerSickness.ID, STRINGS.DISEASES.HUNGERSICKNESS.NAME, STRINGS.DISEASES.HUNGERSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.HUNGERSICKNESS.DESCRIPTION, STRINGS.DISEASES.HUNGERSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(SpindlySickness.ID, STRINGS.DISEASES.SPINDLYCURSE.NAME, STRINGS.DISEASES.SPINDLYCURSE.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.SPINDLYCURSE.DESCRIPTION, STRINGS.DISEASES.SPINDLYCURSE.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(AlienSickness.ID, STRINGS.DISEASES.ALIENSICKNESS.NAME, STRINGS.DISEASES.ALIENSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.ALIENSICKNESS.DESCRIPTION, STRINGS.DISEASES.ALIENSICKNESS.LEGEND_HOVERTEXT);
                BasicModUtils.MakeDiseaseStrings(MutatingSickness.ID, STRINGS.DISEASES.MUTATINGSICKNESS.NAME, STRINGS.DISEASES.MUTATINGSICKNESS.DESCRIPTIVE_SYMPTOMS, STRINGS.DISEASES.MUTATINGSICKNESS.DESCRIPTION, STRINGS.DISEASES.MUTATINGSICKNESS.LEGEND_HOVERTEXT);

                BasicModUtils.MakeTraitStrings(InsectAllergies.ID, STRINGS.TRAITS.INSECTALLERGIES.NAME, STRINGS.TRAITS.INSECTALLERGIES.DESC, STRINGS.TRAITS.INSECTALLERGIES.SHORT_DESC, STRINGS.TRAITS.INSECTALLERGIES.SHORT_DESC_TOOLTIP);

                BasicModUtils.MakeStatusItemStrings(GermcatcherConfig.StatusItemID, STRINGS.STATUSITEMS.GATHERING.NAME, STRINGS.STATUSITEMS.GATHERING.TOOLTIP);

                ExpandExposureTable();

                DiseasesExpanded_Patches_Spindly.UpdateNarcolepsyTimes();
            }

            public static void Postfix()
            {
                float cycle = 600;
                float cycle2 = 2 * cycle;
                float cycle5 = 5 * cycle;
                float cycle10 = 10 * cycle;
                float cycle50 = 50 * cycle;


                Db.Get().effects.Add(HungerSickness.GetCritterSicknessEffect());

                Db.Get().effects.Add(new Effect(FrostSickness.RECOVERY_ID, STRINGS.EFFECTS.FROSTRECOVERY.NAME, STRINGS.EFFECTS.FROSTRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(GasSickness.RECOVERY_ID, STRINGS.EFFECTS.GASRECOVERY.NAME, STRINGS.EFFECTS.GASRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(HungerSickness.RECOVERY_ID, STRINGS.EFFECTS.HUNGERRECOVERY.NAME, STRINGS.EFFECTS.HUNGERRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(BogSickness.RECOVERY_ID, STRINGS.EFFECTS.BOGRECOVERY.NAME, STRINGS.EFFECTS.BOGRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(SpindlySickness.RECOVERY_ID, STRINGS.EFFECTS.SPINDLYRECOVERY.NAME, STRINGS.EFFECTS.SPINDLYRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(MutatingSickness.RECOVERY_ID, STRINGS.EFFECTS.MUTATEDRECOVERY.NAME, STRINGS.EFFECTS.MUTATEDRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(AlienSickness.GetRecoveryEffect());
                Db.Get().effects.Add(AlienSickness.GetAssimilationEffect());

                Db.Get().effects.Add(new Effect(GasCureConfig.EffectID, STRINGS.CURES.GASCURE.NAME, STRINGS.CURES.GASCURE.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(MudMaskConfig.EffectID, STRINGS.CURES.MUDMASK.NAME, STRINGS.CURES.MUDMASK.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(SapShotConfig.EFFECT_ID, STRINGS.CURES.SAPSHOT.NAME, STRINGS.CURES.SAPSHOT.DESC, cycle10, true, true, false));
                Db.Get().effects.Add(new Effect(RadShotConfig.EFFECT_ID, STRINGS.CURES.RADSHOT.NAME, STRINGS.CURES.RADSHOT.DESC, cycle10, true, true, false));
                Db.Get().effects.Add(new Effect(MutatingAntiviralConfig.EffectID, STRINGS.CURES.MUTATINGANTIVIRAL.NAME, STRINGS.CURES.MUTATINGANTIVIRAL.DESC, cycle5, true, true, false));
                Db.Get().effects.Add(HappyPillConfig.GetEffect());
                Db.Get().effects.Add(SuperSerumConfig.GetEffect());
                Db.Get().effects.Add(AlienSicknessCureConfig.GetEffect());
                Db.Get().effects.Add(MedicalNanobots.GetEffect());

                Db.Get().effects.Add(new Effect(AllergyVaccineConfig.EffectID, AllergyVaccineConfig.Name, AllergyVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(SlimelungVaccineConfig.EffectID, SlimelungVaccineConfig.Name, SlimelungVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(ZombieSporesVaccineConfig.EffectID, ZombieSporesVaccineConfig.Name, ZombieSporesVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(HungermsVaccineConfig.EffectID, HungermsVaccineConfig.Name, HungermsVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(GassyVaccineConfig.EffectID, GassyVaccineConfig.Name, GassyVaccineConfig.Desc, cycle50, true, true, false));

                Db.Get().effects.Add(new Effect(TestSampleConfig.EFFECT_ID, STRINGS.EFFECTS.JUSTGOTTESTED.NAME, STRINGS.EFFECTS.JUSTGOTTESTED.DESC, cycle2, true, true, false));

                StatusItem statusItem = new StatusItem(GermcatcherConfig.StatusItemID, "BUILDINGS", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
                statusItem.SetResolveStringCallback((str, data) => str += ((GermcatcherController.Instance)data).GetStatusItemProgress());
                Db.Get().BuildingStatusItems.Add(statusItem);
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
                exposureList.Add(AlienGerms.GetExposureType());
                exposureList.Add(MutatingGerms.GetExposureType());
                exposureList.Add(MedicalNanobots.GetExposureType());

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
                __instance.Add(new AlienSickness());
                __instance.Add(new MutatingSickness());
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
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = AlienGerms.ID, overlayColourName = AlienGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = MutatingGerms.ID, overlayColourName = MutatingGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = MedicalNanobots.ID, overlayColourName = MedicalNanobots.ID });
            }

            public static void Postfix(ref Diseases __instance, bool statsOnly)
            {
                __instance.Add(new FrostShards(statsOnly));
                __instance.Add(new GassyGerms(statsOnly));
                __instance.Add(new AlienGerms(statsOnly));
                __instance.Add(new MutatingGerms(statsOnly));
                __instance.Add(new MedicalNanobots(statsOnly));

                if(DlcManager.IsExpansion1Active())
                    __instance.Add(new BogInsects(statsOnly));

                if (DlcManager.IsExpansion1Active())
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
