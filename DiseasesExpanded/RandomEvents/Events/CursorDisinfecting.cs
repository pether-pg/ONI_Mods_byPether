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
            GeneralName = STRINGS.RANDOM_EVENTS.CURSOR_DISINFECTING.NAME;
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

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.CURSOR_DISINFECTING.TOAST);
                });
        }

        public void DestroyCursor(GameObject cursor)
        {
            if (cursor == null)
                return;

            Util.KDestroyGameObject(cursor);
            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.CURSOR_DISINFECTING.TOAST_END);
        }
    }
}
