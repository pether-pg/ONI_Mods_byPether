using UnityEngine;
using Klei.AI;
using System.Linq;
using System;
using System.Collections.Generic;

namespace RoomsExpanded
{
    class MuseumEffectTrigger : KMonoBehaviour
    {
        private static readonly EventSystem.IntraObjectHandler<MuseumEffectTrigger> TriggerRoomEffectsDelegate = new EventSystem.IntraObjectHandler<MuseumEffectTrigger>((System.Action<MuseumEffectTrigger, object>)((component, data) => component.TriggerRoomEffects(data)));

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.Subscribe<MuseumEffectTrigger>(-832141045, MuseumEffectTrigger.TriggerRoomEffectsDelegate);
        }

        private void TriggerRoomEffects(object data)
        {
            if (!RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumData.RoomId) 
                && !RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumHistoryData.RoomId))
                return;

            bool isHistory = RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumHistoryData.RoomId);

            GameObject gameObject = (GameObject)data;
            MinionModifiers modifiers = gameObject.GetComponent<MinionModifiers>();
            if (modifiers == null)
                return;

            Effect effect = isHistory ? RoomsExpanded_Patches_MuseumHistory.CalculateEffectBonus(modifiers)
                                        : RoomsExpanded_Patches_Museum.CalculateEffectBonus(modifiers);

            if(effect == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Error - could not create effect for {(isHistory ? "History " : "")}Museum");
                return;
            }

            Effects effects = gameObject.GetComponent<Effects>();
            if(effects != null && !effects.HasEffect(effect.Id))
                effects.Add(effect, true);
        }
    }
}
