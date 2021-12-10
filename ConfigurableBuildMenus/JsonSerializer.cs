using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConfigurableBuildMenus
{
    class JsonSerializer<T>
    {
        public static bool Dirty { get; private set; }

        public static string GetDefaultFilename()
        {
            return string.Format("{0}.json", typeof(T).ToString());
        }

        public static string GetDefaultPath()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(T)).Location);
            string name = GetDefaultFilename();
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
            List<string> errors = new List<string>();
            JsonSerializerSettings settings = new JsonSerializerSettings 
            {
                Error = new EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>(
                    (sender, args) =>
                    {
                        errors.Add(args.ErrorContext.Error.Message);
                        args.ErrorContext.Handled = true;
                        Dirty = true;
                    }
                )
            };

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = reader.ReadToEnd();
                    deserialized = JsonConvert.DeserializeObject<T>(json, settings);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                deserialized = default(T);
            }

            PrintErrors(errors);

            return deserialized;
        }

        private static void PrintErrors(List<string> errors)
        {
            Debug.Log($"{ModInfo.Namespace}: {GetDefaultFilename()} deserialization finished!");
            if (errors.Count > 0)
            {
                Debug.Log($"{ModInfo.Namespace}: Following errors ({errors.Count}) were encountered while reading {GetDefaultFilename()}:");

                foreach (string error in errors)
                    Debug.Log(error);
            }
        }
    }
}
