using UnityEngine;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DiseasesExpanded
{
    // Code from Aki
    class AssetLoader
    {
        public static GameObject NanobotFxPrefab = null;

        public static void LoadAssets()
        {
			var assetBundle = LoadAssetBundle("diseases_expanded_nanobots", platformSpecific: true);
			var prefab = assetBundle.LoadAsset<GameObject>("Assets/Others/DiseasesExpanded/NanobotFx.prefab");
			var texture = assetBundle.LoadAsset<Texture2D>("Assets/Others/DiseasesExpanded/plussign.png");

			if (prefab.TryGetComponent(out ParticleSystemRenderer renderer))
			{
				renderer.material = new Material(Shader.Find("Sprites/Default"))
				{
					mainTexture = texture,
					renderQueue = RenderQueues.Liquid
				};
			}

			prefab.SetActive(false);
            NanobotFxPrefab = prefab;
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
                Debug.Log($"{ModInfo.Namespace}: Failed to load AssetBundle from path {path}");
                return null;
            }

            return assetBundle;
        }
    }
}
