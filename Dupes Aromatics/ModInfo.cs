using HarmonyLib;
using KMod;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Dupes_Aromatics
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace = "";

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            Dictionary<string, AromaticsPlantsTuning.CropsTuning> dictionary1 = new Dictionary<string, AromaticsPlantsTuning.CropsTuning>();
            dictionary1.Add(Plant_SpinosaConfig.ID, AromaticsPlantsTuning.SpinrosaTuning);
            dictionary1.Add(Plant_DuskLavenderConfig.ID, AromaticsPlantsTuning.DuskbloomTuning);
            dictionary1.Add(Plant_RimedMallowConfig.ID, AromaticsPlantsTuning.MallowTuning);
            Aromatics_Patches_Worldgen.CropsDictionary = dictionary1;

            Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: DLL version: {GetType().Assembly.GetName().Version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");
        }
    }
}
