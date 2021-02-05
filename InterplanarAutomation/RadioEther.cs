using System.Collections.Generic;
using UnityEngine;
using System;

namespace InterplanarAutomation
{
    class RadioEther
    {
        private List<GameObject> senders;
        private List<GameObject> vanished;
        private static RadioEther _instance = null;

        public static RadioEther Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RadioEther();
                return _instance;
            }
        }

        private RadioEther()
        {
            senders = new List<GameObject>();
            vanished = new List<GameObject>();
        }

        private void AddVanished(GameObject go)
        {
            if (vanished == null)
                vanished = new List<GameObject>();
            vanished.Add(go);
        }

        public void CheckAndAddSender(GameObject go)
        {
            if (senders == null)
                senders = new List<GameObject>();
            if(!senders.Contains(go))
                senders.Add(go);
        }

        private void ClearVanished()
        {
            foreach(GameObject go in vanished)
            {
                if (senders.Contains(go))
                    senders.Remove(go);
            }
            vanished.Clear();
        }

        public int GetSignal()
        {
            int signal = 0;

            foreach(GameObject sender in senders)
            {
                if (sender == null)
                    AddVanished(sender);
                else
                {
                    EnergyConsumer consumer = sender.GetComponent<EnergyConsumer>();
                    LogicPorts ports = sender.GetComponent<LogicPorts>();
                    RadioTower tower = sender.GetComponent<RadioTower>();
                    if (consumer != null && ports != null && tower != null && consumer.IsPowered && tower.CheckSunExposition())
                        signal |= ports.GetInputValue(RadioTowerConfig.SnederRadioPortId);
                }
            }
            ClearVanished();
            return signal;
        }
    }
}
