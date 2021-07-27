using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterplanarAutomation
{
    class RadioTower : KMonoBehaviour, ISim200ms
    {
        private static readonly Operational.Flag visibleSkyFlag = new Operational.Flag("VisibleSky", Operational.Flag.Type.Requirement);
        int blinkingCounter = 0;
        int framesPerBlink = 2;
        int maxBlinkingCounter = 2 * 3;
        string message = ".--./.-.././.-/..././/..././-./-..//---/-..-/-.--/--././-.///"; //please send oxygen
        List<bool> messageSignals = null;

        private void GenerateMorseSignals(string morseCode)
        {
            messageSignals = new List<bool>();
            for(int i=0; i< morseCode.Length; i++)
                switch (morseCode[i])
                {
                    case '.':
                        messageSignals.Add(true);
                        messageSignals.Add(false);
                        break;
                    case '-':
                        messageSignals.Add(true);
                        messageSignals.Add(true);
                        messageSignals.Add(true);
                        messageSignals.Add(false);
                        break;
                    case '/':
                        messageSignals.Add(false);
                        messageSignals.Add(false);
                        messageSignals.Add(false);
                        break;
                }
        }

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
            KPrefabID component = this.GetComponent<KPrefabID>();
            EnergyConsumer consumer = this.gameObject.GetComponent<EnergyConsumer>();
            Operational operational = this.gameObject.GetComponent<Operational>();
            KBatchedAnimController bac = this.GetComponent<KBatchedAnimController>();
            if (consumer == null || operational == null || component == null)
                return;

            if (messageSignals == null || messageSignals.Count() == 0)
                GenerateMorseSignals(message);

            int signal = 0;
            if (consumer.IsPowered)
            {
                if (CheckSunExposition())
                {
                    component.AddTag(GameTags.Detecting);
                    signal = RadioEther.Instance.GetSignal();
                    bool[] signalBits = new bool[4] { (signal & 1) > 0, (signal & 2) > 0, (signal & 4) > 0, (signal & 8) > 0 };

                    blinkingCounter = (blinkingCounter + 1) % (messageSignals.Count * framesPerBlink);
                    //bac.SetSymbolTint("light_blink", blinkingCounter / framesPerBlink == 0 ? UnityEngine.Color.green : UnityEngine.Color.red);
                    //bac.SetSymbolTint("light_blink", message[blinkingCounter / framesPerBlink] == '-' ? UnityEngine.Color.green : UnityEngine.Color.red);
                    bac.SetSymbolTint("light_blink", messageSignals[blinkingCounter / framesPerBlink] ? UnityEngine.Color.green : UnityEngine.Color.red);
                }
                else
                {
                    component.RemoveTag(GameTags.Detecting);
                    bac.SetSymbolTint("light_blink", UnityEngine.Color.white);
                }
            }

            LogicPorts ports = this.gameObject.GetComponent<LogicPorts>();
            if (ports != null)
                ports.SendSignal(RadioTowerConfig.ReceiverRadioPortId, signal);
        }

        public void Sim200ms(float dt)
        {
            RadioEther.Instance.CheckAndAddSender(this.gameObject);
            ScanEther();
        }
    }
}
