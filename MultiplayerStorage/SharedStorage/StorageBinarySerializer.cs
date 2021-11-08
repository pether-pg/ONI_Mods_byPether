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
            return "MultiplayerStorage.SharedStorage.bin";
        }

        public static string GetFullPath(string directory)
        {
            string name = GetDefaultName();
            return System.IO.Path.Combine(directory, name);
        }

        public static void Serialize(Storage data, string path = "")
        {
            if (data == null)
                return;

            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath();

            //PrepareKSerializationManager(data, path);
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                //KSerialization.Manager.SerializeDirectory(writer);
                data.Serialize(writer);
            }
        }

        public static Storage Deserialize(Storage deserialized, string path = "")
        {
            if (string.IsNullOrEmpty(path))
                path = GetDefaultPath();

            if (!File.Exists(path))
                return deserialized; // do not return null as it is reserved for invalid cases

            if (!KSerialization.Manager.HasDeserializationMapping(typeof(KPrefabID)))
                return null;

            try
            {
                byte[] bytes1 = File.ReadAllBytes(path);
                IReader reader = (IReader)new FastReader(bytes1);

                Debug.Log("MultiplayerStorage: Deserialize Storage");
                deserialized.Deserialize(reader);
                Debug.Log("MultiplayerStorage: Storage deserialized!");
            }
            catch (Exception e)
            {
                Debug.Log("MultiplayerStorage: Encountered exception during deserialization");
                Debug.Log(e.Message);
                return null;
            }

            return deserialized;
        }

        private static void PrepareKSerializationManager(Storage data, string path)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                KSerialization.Manager.Clear();

                // first serialization is to fill KSerialization.Manager's directory to be serialized in next step
                data.Serialize(writer);
            }
        }
    }
}
