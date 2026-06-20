using HarmonyLib;

namespace CombinedConduitDisplay
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace = string.Empty;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
			var requiredDlcs = this.mod.packagedModInfo.GetRequiredDlcIds();
			string dlcInfo = "All";
			if (requiredDlcs != null)
				dlcInfo = string.Join(", ", requiredDlcs);
			Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: Mod version: {this.mod.packagedModInfo.version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({dlcInfo})");
        }
    }
}
