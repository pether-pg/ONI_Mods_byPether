using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace SidequestMod
{
    // Code from Aki
    class AssetLoader
    {
        public static KScreen SidequestScreen = null;
        public static GameObject SidequestControl = null;

        public static void LoadAssets()
        {
            var assetBundle = LoadAssetBundle("questmodui", platformSpecific: true);
            var prefab = assetBundle.LoadAsset<KScreen>("Assets/QuestScreen.prefab");

            SidequestScreen = prefab;
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

            Debug.Log($"{ModInfo.Namespace}: Trying to load assets from: {path}");
            var assetBundle = AssetBundle.LoadFromFile(path);

            if (assetBundle == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Failed to load AssetBundle from path {path}");
                return null;
            }

            return assetBundle;
        }
    }
}
