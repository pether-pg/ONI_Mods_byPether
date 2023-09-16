using System;
using UnityEngine;
using DiseasesExpanded.RandomEvents.EntityScripts;

namespace DiseasesExpanded.RandomEvents.Events
{
    class CursorDisinfecting : RandomDiseaseEvent
    {
        public CursorDisinfecting(int weight = 1)
        {
            ID = nameof(CursorDisinfecting);
            GeneralName = "Disinfecting Cursor";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.None;

            Condition = new Func<object, bool>(
                data =>
                {
                    return true;
                });

            Event = new Action<object>(
                data =>
                {
                    GameObject cursor = new GameObject(nameof(CursorDisinfecting));
                    cursor.AddOrGet<FollowCursor>();
                    cursor.AddOrGet<DisinfectOwnLocation>();

                    cursor.SetActive(true);

                    GameScheduler.Instance.Schedule("Destroy Disinfecting Cursor", 60, obj => DestroyCursor(cursor));

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, $"Your cursor just got upgraded with portable Purification Kit!");
                });
        }

        public void DestroyCursor(GameObject cursor)
        {
            if (cursor == null)
                return;

            Util.KDestroyGameObject(cursor);
            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, "Oh no... Your cursor is broken...");
        }
    }
}
