using System;
using UnityEngine;
using DiseasesExpanded.RandomEvents.EntityScripts;

namespace DiseasesExpanded.RandomEvents.Events
{
    class CursorRadioactive : RandomDiseaseEvent
    {
        public CursorRadioactive(ONITwitchLib.Danger danger, int weight = 1)
        {
            GeneralName = STRINGS.RANDOM_EVENTS.CURSOR_RADIOACTIVE.NAME;
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

                    GameObject cursor = new GameObject(nameof(CursorRadioactive));
                    cursor.AddOrGet<FollowCursor>();

                    RadiationEmitter emitter = cursor.AddOrGet<RadiationEmitter>();
                    emitter.emitRadiusX = (short)(2 * ((int)danger + 1));
                    emitter.emitRadiusY = emitter.emitRadiusX;
                    emitter.emitRads = Math.Max(currentCycle, 100 * (int)danger);
                    emitter.enabled = true;
                    emitter.emitRate = 0;

                    Light2D light2D = cursor.AddOrGet<Light2D>();
                    light2D.overlayColour = ColorPalette.RadiationGreen;
                    light2D.Color = ColorPalette.RadiationGreen;
                    light2D.Range = 2;
                    light2D.Angle = 0.0f;
                    light2D.shape = LightShape.Circle;
                    light2D.drawOverlay = true;
                    light2D.Lux = 1800;                    

                    cursor.SetActive(true);
                    
                    float time = 45 * ((int)danger + 1);
                    GameScheduler.Instance.Schedule("Destroy Radioactive Cursor", time, obj => DestroyCursor(cursor));

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, string.Format(STRINGS.RANDOM_EVENTS.CURSOR_RADIOACTIVE.TOAST, emitter.emitRads));
                });
        }

        public void DestroyCursor(GameObject cursor)
        {
            if (cursor == null)
                return;

            Util.KDestroyGameObject(cursor);
            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.CURSOR_RADIOACTIVE.TOAST_END);
        }
    }
}
