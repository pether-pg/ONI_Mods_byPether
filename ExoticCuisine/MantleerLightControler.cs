using UnityEngine;
using System.Collections.Generic;
using KSerialization;

namespace ExoticCuisine
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class MantleerLightControler : GameStateMachine<MantleerLightControler, MantleerLightControler.Instance>, ISaveLoadable
    {
        public State off;
        public State on;

        public override void InitializeStates(out StateMachine.BaseState default_state)
        {
            default_state = off;
            off.Enter(smi => smi.EnableLight(false))
                .UpdateTransition(on, (smi, dt) => smi.HarvestReady() == true, UpdateRate.SIM_1000ms);
            on.Enter(smi => smi.EnableLight(true))
                .UpdateTransition(off, (smi, dt) => smi.HarvestReady() == false, UpdateRate.SIM_1000ms);
        }

        public class Def : StateMachine.BaseDef
        {
        }

        [SerializationConfig(MemberSerialization.OptIn)]
        public new class Instance : GameStateMachine<MantleerLightControler, MantleerLightControler.Instance, IStateMachineTarget, object>.GameInstance, ISaveLoadable
        {
            public void Log(string msg)
            {
                Debug.Log(msg);
                PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, msg, this.gameObject.transform);
            }

            public bool HarvestReady()
            {
                Harvestable harvest = this.gameObject.GetComponent<Harvestable>();
                if (harvest == null)
                    return false;

                return harvest.CanBeHarvested;
            }

            public void EnableLight(bool enable)
            {
                Light2D light = this.gameObject.GetComponent<Light2D>();
                if (light == null)
                    return;

                light.enabled = enable;
            }

            public Instance(IStateMachineTarget master, MantleerLightControler.Def def)
              : base(master)
            {
            }
        }
    }
}
