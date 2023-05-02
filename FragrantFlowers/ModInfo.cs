using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using KMod;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace FragrantFlowers
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace = "";

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: DLL version: {GetType().Assembly.GetName().Version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");

            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(Settings));
            Settings.PLib_Initalize();

            FragrantFlowers_Patches_Worldgen.InitCropDictionary();
        }
    }
}
