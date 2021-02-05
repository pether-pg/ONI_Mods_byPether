using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RocketPresenceAutomationPort
{
    public class PresencePort : KMonoBehaviour, ISim200ms
    {
        public HashedString PortName;
          
        private bool? currentState = null;
        CommandModule relatedCM = null;
        Spacecraft relatedSpacecraft = null;


        void GetRocketComponents()
        {
            List<GameObject> attachedNetwork = AttachableBuilding.GetAttachedNetwork(this.GetComponent<AttachableBuilding>());
            foreach (GameObject gameObject in attachedNetwork)
            {
                CommandModule component = gameObject.GetComponent<CommandModule>();
                if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                    relatedCM = component;
            }

            if (relatedCM == null) return;

            LaunchConditionManager conditionManager = relatedCM.gameObject.GetComponent<LaunchConditionManager>();
            if (conditionManager == null)
                return;
            relatedSpacecraft = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(conditionManager);
        }

        public void GetPresence()
        {
            currentState = true;

            if(relatedCM == null)
                GetRocketComponents();

            if (relatedSpacecraft == null)
                return;

            var spacecraftState = relatedSpacecraft.state;
            if (spacecraftState != Spacecraft.MissionState.Grounded && spacecraftState != Spacecraft.MissionState.Destroyed)
                currentState = false;
        }

        public void SendSignal()
        {
            LogicPorts presencePort = this.gameObject.GetComponent<LogicPorts>();
            presencePort.SendSignal(this.PortName, currentState.Value == true ? 1 : 0);
        }

        public void Sim200ms(float dt)
        {
            GetPresence(); 
            SendSignal();
        }
    }
}
