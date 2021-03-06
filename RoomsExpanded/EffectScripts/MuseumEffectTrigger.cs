using UnityEngine;
using Klei.AI;
using System.Linq;
using System;

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
            if (!RoomTypes_AllModded.IsInTheRoom(this, RoomTypeMuseumData.RoomId))
                return;

            if (!Settings.Instance.Museum.Bonus.HasValue)
                return;

            GameObject gameObject = (GameObject)data;
            MinionModifiers modifiers = gameObject.GetComponent<MinionModifiers>();
            if (modifiers == null)
                return;

            AttributeInstance attributeInstance = modifiers.attributes.AttributeTable.Where(p => p.Name == "Creativity").FirstOrDefault();
            if (attributeInstance == null)
                return;

            float creativity = attributeInstance.GetTotalValue();
            int moraleBonus = (int)Math.Ceiling(creativity * Settings.Instance.Museum.Bonus.Value);
            if (moraleBonus < 1)
                moraleBonus = 1;
            if (moraleBonus > 10)
                moraleBonus = 10;

            Effect effect = new Effect(RoomTypeMuseumData.EffectId, STRINGS.ROOMS.EFFECTS.MUSEUM.NAME, STRINGS.ROOMS.EFFECTS.MUSEUM.DESCRIPTION, 240, false, true, false);
            effect.SelfModifiers = new System.Collections.Generic.List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("QualityOfLife", moraleBonus, description: STRINGS.ROOMS.EFFECTS.MUSEUM.NAME));

            Effects effects = gameObject.GetComponent<Effects>();
            if(effects != null && !effects.HasEffect(RoomTypeMuseumData.EffectId))
                effects.Add(effect, true);
        }
    }
}
