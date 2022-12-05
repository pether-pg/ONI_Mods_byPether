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
        static bool initalized = false;
        static EventManager eventInst;
        static DataManager dataInst;
        static ConditionsManager conditionsInst;
        static TwitchDeckManager deckInst;
        static DangerManager dangerInst;

        public const int WEIGHT = 10000;

        public static void Init()
        {
            if (initalized)
                return;

            eventInst = EventInterface.GetEventManagerInstance();
            dataInst = EventInterface.GetDataManagerInstance();
            conditionsInst = EventInterface.GetConditionsManager();
            deckInst = EventInterface.GetDeckManager();
            dangerInst = EventInterface.GetDangerManager();

            initalized = true;
            Debug.Log($"{ModInfo.Namespace}: Initalized Twitch integration");
        }

        public static void RegisterEvent(RandomDiseaseEvent diseaseEvent)
        {
            EventInfo info = eventInst.RegisterEvent(diseaseEvent.ID, diseaseEvent.GetFriednlyName());
            eventInst.AddListenerForEvent(info, diseaseEvent.Event);

            if (diseaseEvent.Condition != null)
                conditionsInst.AddCondition(info, diseaseEvent.Condition);

            deckInst.AddToDeck(DeckUtils.RepeatList(info, diseaseEvent.AppearanceWeight * WEIGHT));
            dangerInst.SetDanger(info, diseaseEvent.DangerLevel);
        }

        public static void RegisterAll()
        {
            if (!TwitchModInfo.TwitchIsPresent)
            {
                Debug.LogWarning($"{ModInfo.Namespace}: Twitch not enabled!");
                return;
            }

            Init();

            // General
            RegisterEvent(new MandatoryTesting());
            //RegisterEvent(new PanicMode());
            //RegisterEvent(new IntensePollination());
            //RegisterEvent(new GreatSanishellMigration());

            // Mutating Virus
            for (int danger = (int)Danger.None; danger <= (int)Danger.Deadly; danger++)
                RegisterEvent(new SuddenVirusMutation(danger));
            //RegisterEvent(new RegressiveVirusMutation());

            // All
            //for(byte idx = 0; idx < Db.Get().Diseases.Count; idx++)
            //    RegisterEvent(new PrintSomeGerms(idx));
            //foreach (string flowerId in SproutFlowers.SupportedFlowers())
            //    RegisterEvent(new SproutFlowers(flowerId));

            // Food Poisoning
            //RegisterEvent(new FilthyFood());
            //RegisterEvent(new HurtingTummy(gas: false));

            // Slimelung
            RegisterEvent(new SlimyPollutedOxygen());

            // Bog Bugs
            //RegisterEvent(new GreatBogBugMigration());

            // Frost Shards

            // Hunger Germs
            RegisterEvent(new HungryPet());
            RegisterEvent(new PlagueOfHunger());

            // Gassy Germs
            //RegisterEvent(new HurtingTummy(gas: true));
            //RegisterEvent(new StrayComet(isMoo: true));

            // Medical Nanobots
            //RegisterEvent(new NanobotUpdate(false));
            RegisterEvent(new NanobotUpdate(true));

            // Radiation
            //RegisterEvent(new SuddenPlantMutation());
            RegisterEvent(new IntenseRadiation());

            // Alien Goo
            RegisterEvent(new StrayComet(isMoo: false));
            RegisterEvent(new SpaceScream());

            // Zombie Spores
            RegisterEvent(new BloomingGraves());
            RegisterEvent(new NightOfTheLivingDead());
        }

        // TODO idea list:
        // Alien Pet
        // Spawn Infected Element
        // Ice Garden
        // Spindly Crops
        // Get Vaccinated
        // Call of the Wild
        // Pollination
        // Nuclear Pee
        // Med Prints
    }
}
