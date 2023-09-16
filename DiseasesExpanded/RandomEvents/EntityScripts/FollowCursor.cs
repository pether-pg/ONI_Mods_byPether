using System;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.EntityScripts
{
    class FollowCursor : KMonoBehaviour, ISim33ms
    {
        public void Sim33ms(float dt)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos());
            this.transform.SetPosition(position);
        }
    }
}
