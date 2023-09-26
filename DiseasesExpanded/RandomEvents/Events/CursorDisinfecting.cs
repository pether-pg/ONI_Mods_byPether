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
                    GameObject prefab = Assets.GetPrefab((Tag)"fx_disinfect_splash");
                    GameObject cursor = GameUtil.KInstantiate(prefab, Grid.CellToPosCCC(Grid.PosToCell(KInputManager.GetMousePos()), Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2);
                    cursor.AddOrGet<FollowCursor>();
                    cursor.AddOrGet<DisinfectOwnLocation>();

                    KBatchedAnimController kbac = cursor.GetComponent<KBatchedAnimController>();
                    kbac.sceneLayer = Grid.SceneLayer.FXFront2;
                    kbac.initialMode = KAnim.PlayMode.Loop;
                    kbac.enabled = false;
                    kbac.enabled = true;

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
