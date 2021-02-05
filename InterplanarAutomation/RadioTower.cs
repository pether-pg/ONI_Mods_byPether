using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterplanarAutomation
{
    class RadioTower : KMonoBehaviour, ISim200ms
    {
        private static readonly Operational.Flag visibleSkyFlag = new Operational.Flag("VisibleSky", Operational.Flag.Type.Requirement);

        public bool CheckSunExposition(int radius = 2)
        {
            int exposed = 0;
            int location = Grid.PosToCell(this.gameObject);
            for(int i=-radius; i<=radius; i++)
            {
                int cell = Grid.OffsetCell(location, i, Math.Abs(i));
                if (Grid.IsValidCell(cell) && Grid.ExposedToSunlight[cell] >= (byte)253)
                    exposed++;
            }
            return exposed > radius;
        }

        public void ScanEther()
        {
            EnergyConsumer consumer = this.gameObject.GetComponent<EnergyConsumer>();
            Operational operational = this.gameObject.GetComponent<Operational>();
            if (consumer == null || operational == null)
                return;

            int signal = 0;
            if (consumer.IsPowered && operational.IsOperational)
                signal = RadioEther.Instance.GetSignal();

            LogicPorts ports = this.gameObject.GetComponent<LogicPorts>();
            if (ports != null)
                ports.SendSignal(RadioTowerConfig.ReceiverRadioPortId, signal);
        }

        public void Sim200ms(float dt)
        {
            RadioEther.Instance.CheckAndAddSender(this.gameObject);

            bool exposed = CheckSunExposition();
            this.gameObject.GetComponent<Operational>().SetFlag(visibleSkyFlag, exposed);

            ScanEther();
        }
    }
}
