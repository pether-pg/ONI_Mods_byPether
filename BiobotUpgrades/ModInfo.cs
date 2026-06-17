using HarmonyLib;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;

namespace BiobotUpgrades
{
    class ModInfo : KMod.UserMod2
    {
        public static string Namespace { get; private set; }

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);

			var requiredDlcs = this.mod.packagedModInfo.GetRequiredDlcIds();
			string dlcInfo = "All";
			if(requiredDlcs != null) 
				dlcInfo =string.Join(", ", requiredDlcs);

			Namespace = GetType().Namespace;
            Debug.Log($"{Namespace}: Loaded from: {this.mod.ContentPath}");
            Debug.Log($"{Namespace}: DLL version: {GetType().Assembly.GetName().Version} " +
                        $"supporting game build {this.mod.packagedModInfo.minimumSupportedBuild} ({dlcInfo})");

            InitalizePOptions();
        }

        private void InitalizePOptions()
        {
            PUtil.InitLibrary();
            new POptions().RegisterOptions(this, typeof(Settings));
            Settings.PLib_Initalize();

            Debug.Log($"{Namespace}: POptions registered!");
        }
    }
}
