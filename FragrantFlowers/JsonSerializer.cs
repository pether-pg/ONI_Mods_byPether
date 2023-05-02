using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
namespace FragrantFlowers
{
    class JsonSerializer<T>
    {
        public static string GetDefaultFilename()
        {
            return string.Format("{0}.json", typeof(T).ToString());
        }

        public static string GetDefaultPath()
        {
            string path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(T)).Location);
            string name = GetDefaultFilename();
            return Path.Combine(path, name);
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
