using System;
using UnityEngine;
using Klei.AI;
using System.Linq;

namespace RoomsExpanded
{
    class GraveyardEffectTrigger : KMonoBehaviour
    {
        private static readonly EventSystem.IntraObjectHandler<GraveyardEffectTrigger> TriggerRoomEffectsDelegate = new EventSystem.IntraObjectHandler<GraveyardEffectTrigger>((System.Action<GraveyardEffectTrigger, object>)((component, data) => component.TriggerRoomEffects(data)));

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            this.Subscribe<GraveyardEffectTrigger>(-832141045, GraveyardEffectTrigger.TriggerRoomEffectsDelegate);
        }

        private void TriggerRoomEffects(object data)
        {
            if (!RoomTypes_AllModded.IsInTheRoom(this, RoomTypeGraveyardData.RoomId))
                return;

            if (!Settings.Instance.Graveyard.Bonus.HasValue)
                return;

            GameObject gameObject = (GameObject)data;
            float duration = 600 * Settings.Instance.Graveyard.Bonus.Value;
            bool positive = new System.Random().Next() % 2 == 0;
            float value = - 0.016666667f * (positive ? 1 : -1);
            string name = positive ? STRINGS.ROOMS.EFFECTS.GRAVE_GOOD.NAME : STRINGS.ROOMS.EFFECTS.GRAVE_BAD.NAME;
            string description = positive ? STRINGS.ROOMS.EFFECTS.GRAVE_GOOD.DESCRIPTION : STRINGS.ROOMS.EFFECTS.GRAVE_BAD.DESCRIPTION;

            Effects effects = gameObject.GetComponent<Effects>();
            if (effects == null || effects.HasEffect(RoomTypeGraveyardData.EffectId))
                return;
            
            // Works for DLC
            /**/
            Effect effect = new Effect(RoomTypeGraveyardData.EffectId, name, description, duration, true, true, false);
            effect.SelfModifiers = new System.Collections.Generic.List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("StressDelta", value, description: name));
            effects.Add(effect, true);
            
        }
    }
}
