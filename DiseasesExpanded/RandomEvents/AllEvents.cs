using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONITwitchLib;
using DiseasesExpanded.RandomEvents.Events;

namespace DiseasesExpanded.RandomEvents
{
    class AllEvents
    {
        static EventManager eventInst;
        static DataManager dataInst;
        static ConditionsManager conditionsInst;
        static TwitchDeckManager deckInst;
        static DangerManager dangerInst;

        public const int WEIGHT = 10000;

        public static void Init()
        {
            eventInst = EventInterface.GetEventManagerInstance();
            dataInst = EventInterface.GetDataManagerInstance();
            conditionsInst = EventInterface.GetConditionsManager();
            deckInst = EventInterface.GetDeckManager();
            dangerInst = EventInterface.GetDangerManager();
        }

        public static void RegisterEvent(DiseaseEvent diseaseEvent)
        {
            EventInfo info = eventInst.RegisterEvent(diseaseEvent.ID, diseaseEvent.GetFriednlyName());
            eventInst.AddListenerForEvent(info, diseaseEvent.Event);

            deckInst.AddToDeck(DeckUtils.RepeatList(info, diseaseEvent.AppearanceWeight * WEIGHT));
            dangerInst.SetDanger(info, diseaseEvent.DangerLevel);
        }

        public static void RegisterAll()
        {
            RegisterEvent(new SpawnSomeGerms(GermIdx.AlienGermsIdx));
        }
    }
}
