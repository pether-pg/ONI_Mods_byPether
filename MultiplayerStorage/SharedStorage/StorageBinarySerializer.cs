using System;
using System.IO;
using System.Reflection;

namespace MultiplayerStorage
{
    class StorageBinarySerializer
    {
        public static string GetDefaultPath()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(StorageBinarySerializer)).Location);
            string name = GetDefaultName();
            return System.IO.Path.Combine(path, name);
        }

        public static string GetDefaultName()
        {
            return string.Format("{0}.bin", typeof(Storage).ToString());
        }

        public static string GetFullPath(string directory)
        {
            string name = GetDefaultName();
            return System.IO.Path.Combine(directory, name);
        }

        public static void Serialize(Storage data, string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath();

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                data.Serialize(writer);
            }
        }

        public static Storage Deserialize(Storage deserialized, string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath();

            try
            {

                byte[] bytes1 = File.ReadAllBytes(path);
                IReader reader = (IReader)new FastReader(bytes1);
                deserialized.Deserialize(reader);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                deserialized = null;
            }

            return deserialized;
        }
    }
}
