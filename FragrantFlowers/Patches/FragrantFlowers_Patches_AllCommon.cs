using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FragrantFlowers
{
    class FragrantFlowers_Patches_AllCommon
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
                namedLookup.Add(RoseScent.ID, RoseScent.colorValue);
                namedLookup.Add(MallowScent.ID, MallowScent.colorValue);
                namedLookup.Add(LavenderScent.ID, LavenderScent.colorValue);

                initalized = true;
            }
        }


        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public static class Db_Initialize_Patch
        {
            public static void Prefix()
            {
                RegisterStrings.MakeGermStrings(RoseScent.ID, STRINGS.GERMS.ROSESCENT.NAME, STRINGS.GERMS.ROSESCENT.LEGEND_HOVERTEXT, STRINGS.GERMS.ROSESCENT.DESCRIPTION);
                RegisterStrings.MakeGermStrings(MallowScent.ID, STRINGS.GERMS.MALLOWSCENT.NAME, STRINGS.GERMS.MALLOWSCENT.LEGEND_HOVERTEXT, STRINGS.GERMS.MALLOWSCENT.DESCRIPTION);
                RegisterStrings.MakeGermStrings(LavenderScent.ID, STRINGS.GERMS.LAVENDERSCENT.NAME, STRINGS.GERMS.LAVENDERSCENT.LEGEND_HOVERTEXT, STRINGS.GERMS.LAVENDERSCENT.DESCRIPTION);
                
                ExpandExposureTable();
            }

            public static void Postfix()
            {
                Db.Get().effects.Add(RoseScent.GetSmellEffect());
                Db.Get().effects.Add(MallowScent.GetSmellEffect());
                Db.Get().effects.Add(LavenderScent.GetSmellEffect());
                
                Spices spices = Db.Get().Spices;
                Crop_DuskbloomConfig.CreateSpice(spices);
                Crop_CottonBollConfig.CreateSpice(spices);
                Crop_SpinosaRoseConfig.CreateSpice(spices);
            }

            public static void ExpandExposureTable()
            {
                List<ExposureType> exposureList = new List<ExposureType>();
                foreach (ExposureType et in TUNING.GERM_EXPOSURE.TYPES)
                    exposureList.Add(et);

                exposureList.Add(RoseScent.GetSmelledExposureType());
                exposureList.Add(RoseScent.GetAllergiesExposureType());
                exposureList.Add(MallowScent.GetSmelledExposureType());
                exposureList.Add(MallowScent.GetAllergiesExposureType());
                exposureList.Add(LavenderScent.GetSmelledExposureType());
                exposureList.Add(LavenderScent.GetAllergiesExposureType());

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
                //__instance.Add(new ());
            }
        }


        [HarmonyPatch(typeof(Diseases))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ResourceSet), typeof(bool) })]
        public class Diseases_Constructor_Patch
        {
            public static void Prefix()
            {
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = RoseScent.ID, overlayColourName = RoseScent.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = MallowScent.ID, overlayColourName = MallowScent.ID });
                Assets.instance.DiseaseVisualization.info.Add(new DiseaseVisualization.Info() { name = LavenderScent.ID, overlayColourName = LavenderScent.ID });
            }

            public static void Postfix(ref Diseases __instance, bool statsOnly)
            {
                __instance.Add(new RoseScent(statsOnly));
                __instance.Add(new MallowScent(statsOnly));
                __instance.Add(new LavenderScent(statsOnly));
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

        [HarmonyPatch(typeof(Assets))]
        [HarmonyPatch("OnPrefabInit")]
        public class Assets_OnPrefabInit_Patch
        {
            public static void Postfix()
            {
                LoadAndAddSprite(Crop_SpinosaRoseConfig.SPICE_SPRITE);
                LoadAndAddSprite(Crop_DuskbloomConfig.SPICE_SPRITE);
                LoadAndAddSprite(Crop_CottonBollConfig.SPICE_SPRITE);
            }

            public static void LoadAndAddSprite(string spriteName)
            {
                string maindir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string path = Path.Combine(new string[] { maindir, "Sprites", string.Format("{0}.png", spriteName) });
                byte[] data = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(data);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                HashedString key = new HashedString(spriteName);
                Assets.Sprites.Add(key, sprite);
            }
        }
    }
}
