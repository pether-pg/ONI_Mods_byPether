using System;
using UnityEngine;

namespace RoomsExpanded
{
    class DecorProviderModifier : KMonoBehaviour, ISim1000ms
    {
        private DecorProvider provider = null;
        private EffectorValues initialEffector;
        private EffectorValues bonusEffector;

        public string RequiredRoomId = string.Empty;

        public float BonusScale = 0;

        private void InitalizeEffectors()
        {
            if (provider == null)
            {
                provider = this.gameObject.GetComponent<DecorProvider>();
                if (provider == null)
                    return;
            }

            if (initialEffector.amount == 0)
                initialEffector = new EffectorValues()
                {
                    amount = (int)provider.baseDecor,
                    radius = (int)provider.baseRadius
                };

            if (bonusEffector.amount == 0)
                bonusEffector = new EffectorValues()
                {
                    amount = (int)((1 + BonusScale) * initialEffector.amount),
                    radius = (int)((1 + BonusScale) * initialEffector.radius)
                };
        }

        public void Sim1000ms(float dt)
        {
            InitalizeEffectors();

            if (RoomTypes_AllModded.IsInTheRoom(this, RoomTypeAquariumData.RoomId))
                provider.SetValues(bonusEffector);
            else
                provider.SetValues(initialEffector);
        }
    }
}
