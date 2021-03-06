﻿using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using HarmonyLib;
using UnityEngine;

namespace RoomsExpanded
{
    class ModInfo : KMod.UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Debug.Log($"{GetType().Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{GetType().Namespace}: Mod version: {this.mod.packagedModInfo.version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");

            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(Settings));
            Settings.PLib_Initalize();

            Debug.Log($"{GetType().Namespace}: POptions registered!");
        }
    }
}
