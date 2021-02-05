using UnityEngine;
using System.Collections.Generic;

namespace InterplanarAutomation
{
    class LogicRibbonWarpBridge : KMonoBehaviour, ISim200ms
    {
        public GameObject sender = null;
        public GameObject receiver = null;
        public LogicPorts senderPorts = null;
        public LogicPorts receiverPorts = null;

        public void Sim200ms(float dt)
        {
            if (sender == null || receiver == null)
                return;

            if(senderPorts == null)
                senderPorts = sender.GetComponent<LogicPorts>();
            if(receiverPorts == null)
                receiverPorts = receiver.GetComponent<LogicPorts>();

            int signal = senderPorts.GetInputValue(WarpBridgeData.InId);
            receiverPorts.SendSignal(WarpBridgeData.OutId, signal);
        }
    }
}
