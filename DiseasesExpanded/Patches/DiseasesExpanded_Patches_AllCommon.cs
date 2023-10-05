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
                namedLookup.Add(AbandonedGerms.ID, AbandonedGerms.ColorValue); 
                namedLookup.Add(HungerGerms.ID, HungerGerms.ColorValue);
                namedLookup.Add(BogInsects.ID, BogInsects.ColorValue);
                namedLookup.Add(FrostShards.ID, FrostShards.ColorValue);
                namedLookup.Add(SpindlyGerms.ID, SpindlyGerms.ColorValue);
                namedLookup.Add(GassyGerms.ID, GassyGerms.ColorValue);
                namedLookup.Add(AlienGerms.ID, AlienGerms.ColorValue);
                namedLookup.Add(MutatingGerms.ID, MutatingGerms.ColorValue);
                namedLookup.Add(MedicalNanobots.ID, MedicalNanobots.ColorValue);

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
                BasicModUtils.MakeGermStrings(AbandonedGerms.ID, STRINGS.GERMS.ABANDONED.NAME, STRINGS.GERMS.ABANDONED.LEGEND_HOVERTEXT, STRINGS.GERMS.ABANDONED.DESCRIPTION);
                BasicModUtils.MakeGermStrings(BogInsects.ID, STRINGS.GERMS.BOGINSECTS.NAME, STRINGS.GERMS.BOGINSECTS.LEGEND_HOVERTEXT, STRINGS.DISEASES.BOGSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(FrostShards.ID, STRINGS.GERMS.FROSTHARDS.NAME, STRINGS.GERMS.FROSTHARDS.LEGEND_HOVERTEXT, STRINGS.DISEASES.FROSTSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(GassyGerms.ID, STRINGS.GERMS.GASSYGERMS.NAME, STRINGS.GERMS.GASSYGERMS.LEGEND_HOVERTEXT, STRINGS.DISEASES.GASSICKNESS.DESCRIPTION);
                BasicModUtils.MakeGermStrings(SpindlyGerms.ID, STRINGS.DISEASES.SPINDLYCURSE.NAME, STRINGS.DISEASES.SPINDLYCURSE.LEGEND_HOVERTEXT, STRINGS.DISEASES.SPINDLYCURSE.DESCRIPTION);
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
                BasicModUtils.MakeTraitStrings(NotWashingHands.ID, STRINGS.TRAITS.NOTWASHINGHANDS.NAME, STRINGS.TRAITS.NOTWASHINGHANDS.DESC, STRINGS.TRAITS.NOTWASHINGHANDS.SHORT_DESC, STRINGS.TRAITS.NOTWASHINGHANDS.SHORT_DESC_TOOLTIP);

                BasicModUtils.MakeStatusItemStrings(GermcatcherConfig.StatusItemID, STRINGS.STATUSITEMS.GATHERING.NAME, STRINGS.STATUSITEMS.GATHERING.TOOLTIP);
                BasicModUtils.MakeStatusItemStrings(ShieldGeneratorConfig.StatusItemID, STRINGS.STATUSITEMS.SHIELDPOWERUP.NAME, STRINGS.STATUSITEMS.SHIELDPOWERUP.TOOLTIP);

                ExpandExposureTable();

                DiseasesExpanded_Patches_Spindly.UpdateNarcolepsyTimes();

                AssetLoader.LoadAssets();
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
                Db.Get().effects.Add(new Effect(GasSickness.RECOVERY_ID, STRINGS.EFFECTS.GASRECOVERY.NAME, STRINGS.EFFECTS.GASRECOVERY.DESC, cycle5, true, true, false));
                Db.Get().effects.Add(new Effect(HungerSickness.RECOVERY_ID, STRINGS.EFFECTS.HUNGERRECOVERY.NAME, STRINGS.EFFECTS.HUNGERRECOVERY.DESC, cycle5, true, true, false));
                Db.Get().effects.Add(new Effect(BogSickness.RECOVERY_ID, STRINGS.EFFECTS.BOGRECOVERY.NAME, STRINGS.EFFECTS.BOGRECOVERY.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(SpindlySickness.RECOVERY_ID, STRINGS.EFFECTS.SPINDLYRECOVERY.NAME, STRINGS.EFFECTS.SPINDLYRECOVERY.DESC, cycle5, true, true, false));
                Db.Get().effects.Add(new Effect(MutatingSickness.RECOVERY_ID, STRINGS.EFFECTS.MUTATEDRECOVERY.NAME, STRINGS.EFFECTS.MUTATEDRECOVERY.DESC, cycle5, true, true, false));
                Db.Get().effects.Add(new Effect(DiseasesExpanded_Patches_Spindly.SPINDLY_PLANTS_EFFECT_ID, STRINGS.EFFECTS.SPINDLYTHORNS.NAME, STRINGS.EFFECTS.SPINDLYTHORNS.DESC, cycle5, true, false, true));
                Db.Get().effects.Add(AlienSickness.GetRecoveryEffect());
                Db.Get().effects.Add(AlienSickness.GetAssimilationEffect());

                Db.Get().effects.Add(new Effect(GasCureConfig.EffectID, STRINGS.CURES.GASCURE.NAME, STRINGS.CURES.GASCURE.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(MudMaskConfig.EffectID, STRINGS.CURES.MUDMASK.NAME, STRINGS.CURES.MUDMASK.DESC, cycle2, true, true, false));
                Db.Get().effects.Add(new Effect(SapShotConfig.EFFECT_ID, STRINGS.CURES.SAPSHOT.NAME, STRINGS.CURES.SAPSHOT.DESC, cycle10, true, true, false));
                Db.Get().effects.Add(new Effect(RadShotConfig.EFFECT_ID, STRINGS.CURES.RADSHOT.NAME, STRINGS.CURES.RADSHOT.DESC, cycle10, true, true, false));
                Db.Get().effects.Add(new Effect(MutatingAntiviralConfig.EffectID, STRINGS.CURES.MUTATINGANTIVIRAL.NAME, STRINGS.CURES.MUTATINGANTIVIRAL.DESC, cycle5, true, true, false));
                Db.Get().effects.Add(HappyPillConfig.GetEffect());
                Db.Get().effects.Add(SerumSuperConfig.GetEffect());
                Db.Get().effects.Add(SerumTummyConfig.GetEffect());
                Db.Get().effects.Add(SerumYummyConfig.GetEffect());
                Db.Get().effects.Add(SerumDeepBreathConfig.GetEffect());
                Db.Get().effects.Add(AlienSicknessCureConfig.GetEffect());
                Db.Get().effects.Add(MedicalNanobots.GetEffect());

                Db.Get().effects.Add(new Effect(AllergyVaccineConfig.EFFECT_ID, AllergyVaccineConfig.Name, AllergyVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(SlimelungVaccineConfig.EFFECT_ID, SlimelungVaccineConfig.Name, SlimelungVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(ZombieSporesVaccineConfig.EFFECT_ID, ZombieSporesVaccineConfig.Name, ZombieSporesVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(HungermsVaccineConfig.EffectID, HungermsVaccineConfig.Name, HungermsVaccineConfig.Desc, cycle50, true, true, false));
                Db.Get().effects.Add(new Effect(GassyVaccineConfig.EffectID, GassyVaccineConfig.Name, GassyVaccineConfig.Desc, cycle50, true, true, false));

                Db.Get().effects.Add(new Effect(TestSampleConfig.EFFECT_ID, STRINGS.EFFECTS.JUSTGOTTESTED.NAME, STRINGS.EFFECTS.JUSTGOTTESTED.DESC, cycle2, true, true, false));

                StatusItem statusItem = new StatusItem(GermcatcherConfig.StatusItemID, "BUILDINGS", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
                statusItem.SetResolveStringCallback((str, data) => str += ((GermcatcherController.Instance)data).GetStatusItemProgress());
                Db.Get().BuildingStatusItems.Add(statusItem);

                StatusItem shieldStatus = new StatusItem(ShieldGeneratorConfig.StatusItemID, "BUILDINGS", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
                shieldStatus.SetResolveStringCallback((str, data) => str += ((ShieldGenerator.SMInstance)data).GetStatusItemProgress());
                Db.Get().BuildingStatusItems.Add(shieldStatus);
            }

            public static void ExpandExposureTable()
            {
                List<ExposureType> exposureList = new List<ExposureType>();
                foreach (ExposureType et in TUNING.GERM_EXPOSURE.TYPES)
                {
                    if (et.sickness_id == Allergies.ID)
                    {
                        if (et.excluded_effects == null)
                            et.excluded_effects = new List<string>();
                        et.excluded_effects.Add(AllergyVaccineConfig.EFFECT_ID);
                    }
                    if (et.germ_id == FoodGerms.ID)
                    {
                        if (et.excluded_effects == null)
                            et.excluded_effects = new List<string>();
                        et.excluded_effects.Add(SerumTummyConfig.EFFECT_ID);
                    }
                    if (et.germ_id == SlimeGerms.ID)
                    {
                        if (et.excluded_effects == null)
                            et.excluded_effects = new List<string>();
                        et.excluded_effects.Add(SlimelungVaccineConfig.EFFECT_ID);
                        et.excluded_effects.Add(SerumDeepBreathConfig.EFFECT_ID);
                    }
                    if (et.germ_id == ZombieSpores.ID)
                    {
                        if (et.excluded_effects == null)
                            et.excluded_effects = new List<string>();
                        et.excluded_effects.Add(ZombieSporesVaccineConfig.EFFECT_ID);
                        et.excluded_effects.Add(SerumSuperConfig.EFFECT_ID);
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
                if (Settings.Instance.HungerGerms.IncludeDisease)   __instance.Add(new HungerSickness());
                if (Settings.Instance.BogInsects.IncludeDisease)    __instance.Add(new BogSickness());
                if (Settings.Instance.FrostPox.IncludeDisease)      __instance.Add(new FrostSickness());
                if (Settings.Instance.MooFlu.IncludeDisease)        __instance.Add(new GasSickness());
                if (Settings.Instance.SleepingCurse.IncludeDisease) __instance.Add(new SpindlySickness());
                if (Settings.Instance.AlienGoo.IncludeDisease)      __instance.Add(new AlienSickness());
                if (Settings.Instance.MutatingVirus.IncludeDisease) __instance.Add(new MutatingSickness());
            }
        }

        [HarmonyPatch(typeof(Diseases))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ResourceSet), typeof(bool) })]
        public class Diseases_Constructor_Patch
        {
            public static void Prefix()
            {
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = AbandonedGerms.ID, overlayColourName = AbandonedGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = HungerGerms.ID, overlayColourName = HungerGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = BogInsects.ID, overlayColourName = BogInsects.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = FrostShards.ID, overlayColourName = FrostShards.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = GassyGerms.ID, overlayColourName = GassyGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = SpindlyGerms.ID, overlayColourName = SpindlyGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = AlienGerms.ID, overlayColourName = AlienGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = MutatingGerms.ID, overlayColourName = MutatingGerms.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = MedicalNanobots.ID, overlayColourName = MedicalNanobots.ID });
            }

            public static void Postfix(ref Diseases __instance, bool statsOnly)
            {
                //__instance.Add(new AbandonedGerms(statsOnly));

                if (Settings.Instance.FrostPox.IncludeDisease)          __instance.Add(new FrostShards(statsOnly));
                if (Settings.Instance.MooFlu.IncludeDisease)            __instance.Add(new GassyGerms(statsOnly));
                if (Settings.Instance.AlienGoo.IncludeDisease)          __instance.Add(new AlienGerms(statsOnly));
                if (Settings.Instance.MutatingVirus.IncludeDisease)     __instance.Add(new MutatingGerms(statsOnly));
                if (Settings.Instance.MedicalNanobots.IncludeDisease)   __instance.Add(new MedicalNanobots(statsOnly));

                if (DlcManager.IsExpansion1Active() && Settings.Instance.BogInsects.IncludeDisease)
                    __instance.Add(new BogInsects(statsOnly));

                if (DlcManager.IsExpansion1Active() && Settings.Instance.HungerGerms.IncludeDisease)
                    __instance.Add(new HungerGerms(statsOnly));

                if (DlcManager.IsExpansion1Active() && Settings.Instance.SleepingCurse.IncludeDisease)
                    __instance.Add(new SpindlyGerms(statsOnly));
            }
        }


        [HarmonyPatch(typeof(OverlayModes.Disease))]
        [HarmonyPatch("GetCustomLegendData")]
        public static class OverlayModesDisease_GetCustomLegendData_Patch
        {
            public static void Postfix(ref List<LegendEntry> __result)
            {
                __result.RemoveAll(entry => entry.name.Contains(STRINGS.GERMS.ABANDONED.NAME));
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
