using ONITwitchLib;
using ONITwitchLib.Utils;
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

        public const int WEIGHT = 1;
        public const int WEIGHT_ALMOST_NEVER = 1;
        public const int WEIGHT_RARE = 3;
        public const int WEIGHT_NORMAL = 10;
        public const int WEIGHT_COMMON = 30;

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

            deckInst.AddToDeck(info, diseaseEvent.AppearanceWeight * WEIGHT);
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
            RegisterEvent(new MandatoryTesting(WEIGHT_COMMON));
            RegisterEvent(new PanicMode(WEIGHT_NORMAL));
            RegisterEvent(new IntensePollination(WEIGHT_NORMAL));
            RegisterEvent(new GreatSanishellMigration(WEIGHT_NORMAL));
            RegisterEvent(new EradicateGerms(WEIGHT_RARE));

            // All
            for(byte idx = 0; idx < Db.Get().Diseases.Count; idx++)
            {
                RegisterEvent(new PrintSomeGerms(idx, WEIGHT_RARE));
                RegisterEvent(new SpawnInfectedElement(idx, WEIGHT_NORMAL));

                if (!string.IsNullOrEmpty(AdoptStrayPet.GetPetId(idx)))
                    RegisterEvent(new AdoptStrayPet(idx, WEIGHT_RARE));
            }
            foreach (string flowerId in SproutFlowers.SupportedFlowers())
                RegisterEvent(new SproutFlowers(flowerId, WEIGHT_NORMAL));

            // Mutating Virus
            for (int danger = (int)Danger.None; danger <= (int)Danger.Deadly; danger++)
                RegisterEvent(new SuddenVirusMutation(danger, WEIGHT_NORMAL));
            RegisterEvent(new RegressiveVirusMutation(WEIGHT_NORMAL));

            // Food Poisoning
            RegisterEvent(new FilthyFood(WEIGHT_NORMAL));
            RegisterEvent(new HurtingTummy(gas: false, WEIGHT_NORMAL));

            // Slimelung
            RegisterEvent(new SlimyPollutedOxygen(WEIGHT_NORMAL));

            // Bog Bugs
            RegisterEvent(new GreatBogBugMigration(WEIGHT_NORMAL));

            // Frost Shards

            // Spindly Curse
            RegisterEvent(new SpindlyPlants(WEIGHT_NORMAL));

            // Hunger Germs
            RegisterEvent(new HungryPet(WEIGHT_NORMAL));
            RegisterEvent(new PlagueOfHunger(WEIGHT_RARE));

            // Gassy Germs
            RegisterEvent(new HurtingTummy(gas: true, WEIGHT_NORMAL));
            RegisterEvent(new StrayComet(isMoo: true, WEIGHT_NORMAL));

            // Medical Nanobots
            RegisterEvent(new NanobotUpdate(false, WEIGHT_NORMAL));
            RegisterEvent(new NanobotUpdate(true, WEIGHT_RARE));

            // Radiation
            RegisterEvent(new SuddenPlantMutation(WEIGHT_NORMAL));
            RegisterEvent(new IntenseRadiation(WEIGHT_NORMAL));

            // Alien Goo
            RegisterEvent(new StrayComet(isMoo: false, WEIGHT_NORMAL));
            RegisterEvent(new SpaceScream(WEIGHT_RARE));

            // Zombie Spores
            RegisterEvent(new BloomingGraves(WEIGHT_RARE));
            RegisterEvent(new NightOfTheLivingDead(WEIGHT_ALMOST_NEVER));
        }

        // TODO idea list:
        // Ice Garden
        // Get Vaccinated
        // Pollination
        // Nuclear Pee
        // Med Prints
    }
}
