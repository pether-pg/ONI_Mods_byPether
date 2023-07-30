using HarmonyLib;
using KMod;
using System.Collections.Generic;
using UnityEngine;
using System;
using ONITwitchLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;

namespace DiseasesExpanded
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace { get; private set; }
        public static List<Tag> CUSTOM_GASES = new List<Tag>
        {
            NanobotBottleConfig.BOTTLED_GERM_TAG
        };

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: DLL version: {GetType().Assembly.GetName().Version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");

            BackupConfig.Instance.RestoreBackup(JsonSerializer<Settings>.GetDefaultName());
            InitalizePlib();

            AddNewStorageFilter();
        }

        public override void OnAllModsLoaded(Harmony harmony, IReadOnlyList<Mod> mods)
        {
            base.OnAllModsLoaded(harmony, mods);
            CheckForRelatedMods(mods);
        }

        private void InitalizePlib()
        {
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(Settings));
            Settings.PLib_Initalize();

            Debug.Log($"{Namespace}: POptions registered!");
        }

        private void AddNewStorageFilter()
        {
            GameTags.MaterialCategories.Add(NanobotBottleConfig.BOTTLED_GERM_TAG);
            GameTags.AllCategories.Add(NanobotBottleConfig.BOTTLED_GERM_TAG);
            CUSTOM_GASES.AddRange(TUNING.STORAGEFILTERS.GASES);
        }

        private void CheckForRelatedMods(IReadOnlyList<Mod> mods)
        {
            if (Settings.Instance.AutoDetectRelatedMods)
            {
                bool DiseasesReimaginedFound = false;

                foreach (Mod mod in mods)
                    if (mod.staticID == "1911357229.Steam")
                    {
                        DiseasesReimaginedFound = mod.IsActive();
                        string activeString = mod.IsActive() ? "Active" : "NOT Active";
                        Debug.Log($"{Namespace}: Mod Id = \"{mod.staticID}\", Title = \"{mod.title}\", detected to be {activeString}.");
                    }

                if (Type.GetType("DiseasesReimagined.DiseasesPatch, DiseasesReimagined", false) != null)
                {
                    DiseasesReimaginedFound = true;
                    Debug.Log($"{Namespace}: Found type for DiseasesReimagined.DiseasesPatch, DiseasesReimagined");
                }

                Settings.Instance.RebalanceForDiseasesRestored = DiseasesReimaginedFound;
                JsonSerializer<Settings>.Serialize(Settings.Instance);
            }
        }
    }
}
