using HarmonyLib;
using UnityEngine;

namespace IlluminationSensor
{
    class ModInfo : KMod.UserMod2
    {
        public static Harmony HarmonyInstance { get; private set; }

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

            HarmonyInstance = harmony;

            Debug.Log($"{GetType().Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{GetType().Namespace}: Mod version: {this.mod.packagedModInfo.version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({this.mod.packagedModInfo.supportedContent})");
        }
    }
}
