using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace ConfigurableBuildMenus
{
    class JsonSerializer<T>
    {
        public static string GetDefaultPath()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(T)).Location);
            string name = string.Format("{0}.json", typeof(T).ToString());
            return System.IO.Path.Combine(path, name);
        }
        public static bool FileExists(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath();

            return File.Exists(path);
        }

        public static void Serialize(T data, string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath();

            using (StreamWriter writer = new StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                writer.Write(json);
            }
        }

        public static T Deserialize(string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath();

            T deserialized;

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = reader.ReadToEnd();
                    deserialized = JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                deserialized = default(T);
            }

            return deserialized;
        }
    }
}
