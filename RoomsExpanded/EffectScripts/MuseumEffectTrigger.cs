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
                && !RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumSpaceData.RoomId)
                && !RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumHistoryData.RoomId))
                return;

            bool isSpace = RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumSpaceData.RoomId);
            bool isHistory = RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumHistoryData.RoomId);

            GameObject gameObject = (GameObject)data;
            MinionModifiers modifiers = gameObject.GetComponent<MinionModifiers>();
            if (modifiers == null)
                return;

            Effect effect;
            if (isHistory)
                effect = RoomsExpanded_Patches_MuseumHistory.CalculateEffectBonus(modifiers);
            else if (isSpace)
            {
                Room room = Game.Instance.roomProber.GetRoomOfGameObject(this.gameObject);
                int uniqueArtifacts = RoomsExpanded_Patches_MuseumSpace.CountUniqueArtifacts(room);
                effect = RoomsExpanded_Patches_MuseumSpace.CalculateEffectBonus(modifiers, uniqueArtifacts);
            }
            else
                effect = RoomsExpanded_Patches_Museum.CalculateEffectBonus(modifiers);

            if(effect == null)
            {
                Debug.Log($"{ModInfo.Namespace}: Error - could not create effect for {(isHistory ? "History " : (isSpace ? "Space " : ""))}Museum");
                return;
            }

            Effects effects = gameObject.GetComponent<Effects>();
            if(effects != null && !effects.HasEffect(effect.Id))
                effects.Add(effect, true);
        }
    }
}
