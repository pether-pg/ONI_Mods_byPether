using System;
using UnityEngine;
using DiseasesExpanded.RandomEvents.EntityScripts;

namespace DiseasesExpanded.RandomEvents.Events
{
    class CursorRadioactive : RandomDiseaseEvent
    {
        public CursorRadioactive(ONITwitchLib.Danger danger, int weight = 1)
        {
            GeneralName = "Radioactive Cursor";
            NameDetails = danger.ToString();
            ID = GenerateId(nameof(CursorRadioactive), NameDetails);
            AppearanceWeight = weight;
            DangerLevel = danger;

            Condition = new Func<object, bool>(
                data =>
                {
                    if (!DlcManager.IsExpansion1Active())
                        return false;

                    int minCycle = 100 * (int)danger;
                    int maxCycle = danger == ONITwitchLib.Danger.Deadly ? int.MaxValue : 100 * ((int)danger + 1); 
                    int currentCycle = GameClock.Instance.GetCycle();

                    return minCycle <= currentCycle && currentCycle < maxCycle;
                });

            Event = new Action<object>(
                data =>
                {
                    int currentCycle = GameClock.Instance.GetCycle();

                    GameObject mouse = new GameObject(nameof(CursorRadioactive));
                    mouse.AddOrGet<FollowCursor>();
                    RadiationEmitter emitter = mouse.AddOrGet<RadiationEmitter>();
                    emitter.emitRadiusX = (short)(2 * ((int)danger + 1));
                    emitter.emitRadiusY = emitter.emitRadiusX;
                    emitter.emitRads = currentCycle;
                    emitter.enabled = true;

                    mouse.SetActive(true);
                    
                    float time = 45 * ((int)danger + 1);
                    GameScheduler.Instance.Schedule("Destroy Radioactive Cursor", time, obj => DestroyCursor(mouse));

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, $"Your cursor just got upgraded with portable Radiation Emitter! \nIt now glows with around {emitter.emitRads} rads!");
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
