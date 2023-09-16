using ONITwitchLib;
using ONITwitchLib.Core;
using DiseasesExpanded.RandomEvents.Events;
using System;

namespace DiseasesExpanded.RandomEvents
{
    class AllEvents
    {
        static bool initalized = false;
        static EventManager eventInst;
        static DataManager dataInst;
        static TwitchDeckManager deckInst;

        public const int WEIGHT_NEVER = 0;
        public const int WEIGHT_ALMOST_NEVER = 1;
        public const int WEIGHT_RARE = 3;
        public const int WEIGHT_NORMAL = 10;
        public const int WEIGHT_COMMON = 30;

        public static void Init()
        {
            if (initalized)
                return;

            eventInst = EventManager.Instance;
            dataInst = DataManager.Instance;
            deckInst = TwitchDeckManager.Instance;
            
            initalized = true;
            Debug.Log($"{ModInfo.Namespace}: Initalized Twitch integration");
        }

        public static void RegisterEvent(RandomDiseaseEvent diseaseEvent)
        {
            if (diseaseEvent.AppearanceWeight == WEIGHT_NEVER)
                return;

            int weight = (int)(diseaseEvent.AppearanceWeight * Settings.Instance.RandomEvents.RelativeEventsWeight);
            if (weight < 1)
                weight = 1;

            bool showDetails = Settings.Instance.RandomEvents.ShowDetailedEventNames;

            EventInfo info;
            if (diseaseEvent.Group != null)
            {
                var group = EventGroup.GetOrCreateGroup(diseaseEvent.Group);                
                deckInst.AddGroup(group);
                info = group.AddEvent(diseaseEvent.ID, weight, diseaseEvent.GetFriendlyName(showDetails));
            }
            else
            {
                var (newInfo, group) = EventGroup.DefaultSingleEventGroup(diseaseEvent.ID, weight, diseaseEvent.GetFriendlyName(showDetails));
                info = newInfo;
                deckInst.AddGroup(group);
            }

            info.AddListener(diseaseEvent.Event);
            if (diseaseEvent.Condition != null)
                info.AddCondition(diseaseEvent.Condition);

            info.Danger = diseaseEvent.DangerLevel;
        }

        public static void RegisterAll()
        {
            if (!TwitchModInfo.TwitchIsPresent)
            {
                Debug.LogWarning($"{ModInfo.Namespace}: Twitch not enabled!");
                return;
            }

            Init();

            RegisterGeneralEvents();
            RegisterVirusEvents();
            RegisterFoodPoisoningEvents();
            RegisterSlimelungEvents();
            RegisterBogEvents();
            RegisterFrostEvents();
            RegisterSpindlyEvents();
            RegisterHungerEvents();
            RegisterGassyEvents();
            RegisterNanobotEvents();
            RegisterRadiationEvents();
            RegisterAlienEvents();
            RegisterZombieEvents();
        }

        // TODO idea list:
        // Ice Garden
        // Get Vaccinated
        // Pollination
        // Nuclear Pee
        // Med Prints

        public static void RegisterGeneralEvents()
        {
            // General
            RegisterEvent(new MandatoryTesting(WEIGHT_COMMON));
            RegisterEvent(new MiraculousRecovery(WEIGHT_RARE));
            RegisterEvent(new ResupplyFirstAidKits(WEIGHT_COMMON));
            RegisterEvent(new PanicMode(WEIGHT_NORMAL));
            RegisterEvent(new IntensePollination(WEIGHT_NORMAL));
            RegisterEvent(new GreatSanishellMigration(WEIGHT_NORMAL));
            RegisterEvent(new EradicateGerms(WEIGHT_RARE));
            RegisterEvent(new CursorDisinfecting(WEIGHT_NORMAL));

            // All
            for (byte idx = 0; idx < Db.Get().Diseases.Count; idx++)
            {
                RegisterEvent(new PrintSomeGerms(idx, WEIGHT_NEVER));
                RegisterEvent(new SpawnInfectedElement(idx, WEIGHT_RARE));
                RegisterEvent(new SpawnGermySurpriseBox(idx, WEIGHT_ALMOST_NEVER));

                if (!string.IsNullOrEmpty(AdoptStrayPet.GetPetId(idx)))
                    RegisterEvent(new AdoptStrayPet(idx, WEIGHT_RARE));
            }
            foreach (string flowerId in SproutFlowers.SupportedFlowers())
                RegisterEvent(new SproutFlowers(flowerId, WEIGHT_NORMAL));
        }

        public static void RegisterVirusEvents()
        {
            if (!Settings.Instance.MutatingVirus.IncludeDisease)
                return;

            for (int danger = (int)Danger.None; danger <= (int)Danger.Deadly; danger++)
                RegisterEvent(new SuddenVirusMutation(danger, WEIGHT_NORMAL));
            RegisterEvent(new RegressiveVirusMutation(WEIGHT_NORMAL));
        }

        public static void RegisterFoodPoisoningEvents()
        {
            RegisterEvent(new FilthyFood(WEIGHT_NORMAL));
            RegisterEvent(new HurtingTummy(gas: false, WEIGHT_NORMAL));
        }

        public static void RegisterSlimelungEvents()
        {
            RegisterEvent(new SlimyPollutedOxygen(WEIGHT_NORMAL));
        }

        public static void RegisterBogEvents()
        {
            if (!Settings.Instance.BogInsects.IncludeDisease)
                return;

            RegisterEvent(new GreatBogBugMigration(WEIGHT_NORMAL));
        }

        public static void RegisterFrostEvents()
        {
            if (!Settings.Instance.FrostPox.IncludeDisease)
                return;
        }

        public static void RegisterSpindlyEvents()
        {
            if (!Settings.Instance.SleepingCurse.IncludeDisease)
                return;

            RegisterEvent(new SpindlyPlants(WEIGHT_NORMAL));
        }

        public static void RegisterHungerEvents()
        {
            if (!Settings.Instance.HungerGerms.IncludeDisease)
                return;

            RegisterEvent(new HungryPet(WEIGHT_NORMAL));
            RegisterEvent(new PlagueOfHunger(WEIGHT_RARE));
        }

        public static void RegisterGassyEvents()
        {
            if (!Settings.Instance.MooFlu.IncludeDisease)
                return;

            RegisterEvent(new HurtingTummy(gas: true, WEIGHT_NORMAL));
            RegisterEvent(new StrayComet(isMoo: true, WEIGHT_NORMAL));
        }

        public static void RegisterNanobotEvents()
        {
            if (!Settings.Instance.MedicalNanobots.IncludeDisease)
                return;

            RegisterEvent(new NanobotUpdate(false, WEIGHT_NORMAL));
            RegisterEvent(new NanobotUpdate(true, WEIGHT_RARE));
        }

        public static void RegisterRadiationEvents()
        {
            if (!DlcManager.IsExpansion1Active())
                return;

            RegisterEvent(new SuddenPlantMutation(WEIGHT_NORMAL));
            RegisterEvent(new IntenseRadiation(WEIGHT_NORMAL));
            
            foreach(Danger danger in Enum.GetValues(typeof(Danger)))
                RegisterEvent(new CursorRadioactive(danger, WEIGHT_NORMAL));
        }

        public static void RegisterAlienEvents()
        {
            if (!Settings.Instance.AlienGoo.IncludeDisease)
                return;

            RegisterEvent(new StrayComet(isMoo: false, WEIGHT_NORMAL));
            RegisterEvent(new SpaceScream(WEIGHT_RARE));
        }

        public static void RegisterZombieEvents()
        {
            RegisterEvent(new BloomingGraves(WEIGHT_RARE));
            RegisterEvent(new NightOfTheLivingDead(WEIGHT_ALMOST_NEVER));
        }
    }
}
