using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class IntenseRadiation :RandomDiseaseEvent
    {
        List<RadiationEmitter> ModifiedEmitters = new List<RadiationEmitter>();

        public IntenseRadiation(int weight = 1)
        {
            ID = nameof(IntenseRadiation);
            GeneralName = STRINGS.RANDOM_EVENTS.INTENSE_RADIATION.NAME;
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Medium;

            Condition = new Func<object, bool>(data => GameClock.Instance.GetCycle() > 100);

            Event = new Action<object>(
                data => 
                {
                    foreach (RadiationEmitter re in DiseasesExpanded_Patches_Twitch.RadiationEmitter_OnSpawn_Patch.RadiationEmitters)
                        if (re != null)
                        {
                            ModifyRadiation(re, 2, 5);
                            ModifiedEmitters.Add(re);
                        }

                    ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.INTENSE_RADIATION.TOAST);
                    SaveGame.Instance.StartCoroutine(WaitToRestore());
                });
        }

        private IEnumerator WaitToRestore()
        {
            yield return new WaitForSeconds(600);
            foreach (RadiationEmitter re in ModifiedEmitters)
                ModifyRadiation(re, 0.5f, 0.2f);
            ONITwitchLib.ToastManager.InstantiateToast(GeneralName, STRINGS.RANDOM_EVENTS.INTENSE_RADIATION.TOAST_END);
        }

        private void ModifyRadiation(RadiationEmitter emitter, float radiusScale = 1, float radiationScale = 1)
        {
            if (radiusScale == 1 && radiationScale == 1)
                return;

            if (emitter == null || emitter.gameObject == null) 
                return;

            emitter.emitRadiusX = (short)(emitter.emitRadiusX * radiusScale);
            emitter.emitRadiusY = (short)(emitter.emitRadiusY * radiusScale);
            emitter.emitRads *= radiationScale;
            emitter.Refresh();
        }
    }
}
