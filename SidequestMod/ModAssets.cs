using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod
{
    class ModAssets
    {
        public static GameObject CustomGameSettings;
        public static void LoadAssets()
        {
            AssetBundle bundle = LoadAssetBundle("customgamesettings_assets", platformSpecific: true);
            CustomGameSettings = bundle.LoadAsset<GameObject>("Assets/CustomGameSettingsChangerPrefab.prefab");

            //UIUtils.ListAllChildren(CustomGameSettings.transform);

            var TMPConverter = new TMPConverter();
            TMPConverter.ReplaceAllText(CustomGameSettings);

        }
        public static AssetBundle LoadAssetBundle(string assetBundleName, string path = null, bool platformSpecific = false)
        {
            foreach (var bundle in AssetBundle.GetAllLoadedAssetBundles())
            {
                if (bundle.name == assetBundleName)
                {
                    return bundle;
                }
            }

            if (path.IsNullOrWhiteSpace())
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assets");
            }

            if (platformSpecific)
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsPlayer:
                        path = Path.Combine(path, "windows");
                        break;
                    case RuntimePlatform.LinuxPlayer:
                        path = Path.Combine(path, "linux");
                        break;
                    case RuntimePlatform.OSXPlayer:
                        path = Path.Combine(path, "mac");
                        break;
                }
            }

            path = Path.Combine(path, assetBundleName);

            var assetBundle = AssetBundle.LoadFromFile(path);

            if (assetBundle == null)
            {
                Debug.Log($"Failed to load AssetBundle from path {path}");
                return null;
            }

            return assetBundle;
        }
    }
}
