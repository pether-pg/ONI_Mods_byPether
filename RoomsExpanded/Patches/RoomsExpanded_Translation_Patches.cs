﻿using System;
using HarmonyLib;
using KMod;
using System.IO;
using static Localization;
using System.Reflection;
using STRINGS;

namespace RoomsExpanded
{
    class RoomsExpanded_Translation_Patches
    {
        [HarmonyPatch(typeof(Localization), "Initialize")]
        public class Localization_Initialize_Patch
        {
            // following tutorial by Aki:
            // https://forums.kleientertainment.com/forums/topic/123339-guide-for-creating-translatable-mods


            public static void Postfix() => Translate(typeof(STRINGS));

            public static void Translate(Type root)
            {
                // Basic intended way to register strings, keeps namespace
                RegisterForTranslation(root);

                // Load user created translation files
                LoadStrings();

                // Register strings without namespace
                // because we already loaded user transltions, custom languages will overwrite these
                LocString.CreateLocStringKeys(root, null);

                // Creates template for users to edit
                GenerateStringsTemplate(root, GetTranslationDir());

                Debug.Log($"{ModInfo.Namespace}: using translation done by {STRINGS.TRANSLATION.AUTHOR.NAME}");
            }

            private static string GetTranslationDir()
            {
                string dir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Translations");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }

            private static void LoadStrings()
            {
                string code = GetLocale()?.Code;
                if(string.IsNullOrEmpty(code))
                    code = GetCurrentLanguageCode();
                if (!string.IsNullOrEmpty(Settings.Instance.EnforcedLanguage))
                    code = Settings.Instance.EnforcedLanguage;

                string path = Path.Combine(GetTranslationDir(), code + ".po");

                Debug.Log($"{ModInfo.Namespace}: Loading translation file: {path}");
                if (File.Exists(path))
                    OverloadStrings(LoadStringsFile(path, false));
                else
                    Debug.Log($"{ModInfo.Namespace}: Translation file not found, using default strings.");
            }
        }
    }
}
