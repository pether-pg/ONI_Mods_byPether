using System.IO;

namespace MultiplayerStorage
{
    class SharedStorage : KMonoBehaviour
    {
        protected override void OnSpawn()
        {
            if (SharedStorageData.Instance.GO != null)
            {
                Debug.Log("MultiplayerStorage: Only one Storage can work in your base.");
                return;
            }

            SharedStorageData.Instance.GO = this.gameObject;
            SharedStorageData.ClearItems();
            SharedStorageData.ItemsListInit();

            if (CheckOrEnforceControl())
            {
                string path = StorageBinarySerializer.GetFullPath(Settings.Instance.StorageFilePath);
                if(StorageBinarySerializer.Deserialize(SharedStorageData.GetStorage(), path) != null)
                {
                    StorageOwnershipInfo.AssumeControl();
                    SharedStorageData.SetActive(true);
                    Debug.Log("MultiplayerStorage: Storage is now active.");
                }
                else
                {
                    SetStatusItem();
                    SharedStorageData.SetActive(false);
                    Debug.Log("MultiplayerStorage: Error during deserialization, please reload the game.");
                }
            }
            else
            {
                SharedStorageData.SetActive(false);
                Debug.Log("MultiplayerStorage: cannot use the Storage this time...");
            }
        }

        private bool CheckOrEnforceControl()
        {
            Debug.Log($"MultiplayerStorage: Storage is being used by: {StorageOwnershipInfo.Instance.CurrentOwner ?? "(nobody)"}");
            Debug.Log($"MultiplayerStorage: Storage was last opened: {StorageOwnershipInfo.GetCurrentTimeString()}");

            if (StorageOwnershipInfo.CanAssumeControl() || StorageOwnershipInfo.InControl())
                return true;
            else if (Settings.Instance.OneTimeEnforceControl)
            {
                Settings.Instance.OneTimeEnforceControl = false;
                JsonSerializer<Settings>.Serialize(Settings.Instance);
                Debug.Log("MultiplayerStorage: Enforcing control over the Storage...");
                return true;
            }
            return false;
        }

        private void SetStatusItem()
        {
            StatusItem statusItem = new StatusItem(SharedStorageConfig.statusItemId, "BUILDINGS", "status_item_exclamation", StatusItem.IconType.Exclamation, NotificationType.Bad, false, OverlayModes.None.ID);
            statusItem.AddNotification(null, STRINGS.STATUSITEMS.REBOOTREQUIRED.NAME, STRINGS.STATUSITEMS.REBOOTREQUIRED.TOOLTIP);
            this.gameObject.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Stored, statusItem);
        }
    }
}
