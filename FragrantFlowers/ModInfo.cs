using HarmonyLib;
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

            Dictionary<string, FragrantPlantsTuning.CropsTuning> dictionary1 = new Dictionary<string, FragrantPlantsTuning.CropsTuning>();
            dictionary1.Add(Plant_SpinosaConfig.ID, FragrantPlantsTuning.SpinrosaTuning);
            dictionary1.Add(Plant_DuskLavenderConfig.ID, FragrantPlantsTuning.DuskbloomTuning);
            dictionary1.Add(Plant_RimedMallowConfig.ID, FragrantPlantsTuning.MallowTuning);
            FragrantFlowers_Patches_Worldgen.CropsDictionary = dictionary1;

            Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: DLL version: {GetType().Assembly.GetName().Version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");
        }
    }
}
