using System.IO;
using System.Reflection;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace ConfigurableBuildMenus
{
    class SpriteHelper
    {

        public static void LoadBuilMenuIcon(Config.NewBuildMenu newBuildMenu)
        {
            Texture2D tex = LoadTextureForIcon(newBuildMenu.Icon);

            string disabledName = newBuildMenu.Icon + "_disabled";
            HashedString key = new HashedString(newBuildMenu.Icon);
            HashedString keyDisabled = new HashedString(disabledName);

            if (tex != null)
            {
                MakeAndAddSprite(tex, newBuildMenu.Icon, newBuildMenu.Icon);
                MakeAndAddSprite(Grayscale(tex), newBuildMenu.Icon, disabledName);
            }
            else if(Assets.Sprites.ContainsKey(key) && !Assets.Sprites.ContainsKey(keyDisabled))
            {
                Sprite sprite = Assets.Sprites[key];
                Assets.Sprites.Add(keyDisabled, sprite);
            }
        }

        public static string GetIconDirectory()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dir = Path.Combine(path, "icons");
            return dir;
        }

        public static string GetPathForIcon(string iconName)
        {
            string iconFile = string.Format("{0}.png", iconName);
            string path = Path.Combine(GetIconDirectory(), iconFile);
            return path;
        }

        public static Texture2D LoadTextureForIcon(string iconName)
        {
            string path = GetPathForIcon(iconName);
            if (File.Exists(path))
                return LoadTextureFromFile(path);

            Debug.Log($"{ModInfo.Namespace}: Could not find file: {path}");
            return null;
        }

        public static Texture2D CopyGameTexture(Texture2D tex)
        {
            Texture2D result  = new Texture2D(tex.width, tex.height, tex.format, 12, false);
            Graphics.CopyTexture(tex, result);
            result.Apply();
            return result;

            RenderTexture render = new RenderTexture(tex.width, tex.height, 32);
            Graphics.Blit(tex, render);

            //Texture2D result = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
            result.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            result.Apply();

            return result;
        }

        public static Texture2D LoadTextureFromFile(string filePath)
        {
            byte[] data = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(data);

            Debug.Log($"{ModInfo.Namespace}: Loaded icon file {filePath}");

            return tex;
        }

        public static void MakeAndAddSprite(Texture2D texture, string name, string keyName)
        {
            HashedString key = new HashedString(keyName);
            if (Assets.Sprites.ContainsKey(key))
            {
                Debug.Log($"{ModInfo.Namespace}: Assets.Sprites already contains {keyName} icon. Your icon will not be added.");
                return;
            }

            if (texture == null)
                return;

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            sprite.name = name;
            Assets.Sprites.Add(key, sprite);
        }

        public static Texture2D TryGrayscale(Texture2D source)
        {
            Texture2D grayscale;
            try
            {
                grayscale = Grayscale(source);
            }
            catch (UnityException ue)
            {
                grayscale = source;
            }
            return grayscale;
        }

        private static Texture2D Grayscale(Texture2D source)
        {
            Texture2D result = new Texture2D(source.width, source.height);
            for (int x = 0; x < source.width; x++)
                for (int y = 0; y < source.height; y++)
                {
                    Color pixel = source.GetPixel(x, y);
                    float gray = 0.2989f * pixel.r
                                + 0.5870f * pixel.g
                                + 0.1140f * pixel.b;
                    result.SetPixel(x, y, new Color(gray, gray, gray, pixel.a));
                }
            result.Apply();
            return result;
        }
    }
}
