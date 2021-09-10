using System.IO;

namespace MultiplayerStorage
{
    class SharedStorage : KMonoBehaviour
    {
        protected override void OnSpawn()
        {
            bool canControl = false;

            if (SharedStorageData.Instance.GO != null)
            {
                Debug.Log("MultiplayerStorage: Only one Storage can work in your base.");
                return;
            }

            SharedStorageData.Instance.GO = this.gameObject;
            SharedStorageData.ClearItems();
            SharedStorageData.ItemsListInit();

            Debug.Log($"MultiplayerStorage: Storage is being used by {StorageOwnershipInfo.Instance.CurrentOwner}");
            Debug.Log($"MultiplayerStorage: Storage was last opened {StorageOwnershipInfo.GetCurrentTimeString()}");

            if (StorageOwnershipInfo.CanAssumeControl() || StorageOwnershipInfo.InControl())
                canControl = true;
            else if (Settings.Instance.OneTimeEnforceControl)
            {
                canControl = true;
                Settings.Instance.OneTimeEnforceControl = false;
                JsonSerializer<Settings>.Serialize(Settings.Instance);
                Debug.Log("MultiplayerStorage: Enforcing control over the Storage...");
            }

            if (canControl)
            {
                string path = StorageBinarySerializer.GetFullPath(Settings.Instance.StorageFilePath);
                StorageBinarySerializer.Deserialize(SharedStorageData.GetStorage(), path);
                StorageOwnershipInfo.AssumeControl();
                SharedStorageData.SetActive(true);
                Debug.Log("MultiplayerStorage: Storage is now active.");
            }
            else
            {
                SharedStorageData.SetActive(false);
                Debug.Log("MultiplayerStorage: cannot use the Storage this time...");
            }
        }
    }
}
