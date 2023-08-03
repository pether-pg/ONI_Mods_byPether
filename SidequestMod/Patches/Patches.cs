using HarmonyLib;
using SidequestMod.Sidequests.TutorialQuests;
using SidequestMod.Sidequests.InterestQuest.Piloting;

namespace SidequestMod
{
    public class Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            public static void Prefix()
            {
                //AssetLoader.LoadAssets();
            }

            public static void Postfix()
            {
                SidequestManager.RegisterQuest(new TutorialToiletsQuest());
                //QuestManager.RegisterQuest(new TutorialOxygenQuest());
                //QuestManager.RegisterQuest(new TutorialClinicQuest());
                SidequestManager.RegisterQuest(new PilotingInterest_SurfaceBreach_Quest());
            }
        }

        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("DestroyInstances")]
        public class Game_DestroyInstances_Patch
        {
            public static void Prefix()
            {
                SidequestManager.Clear();
            }
        }

        [HarmonyPatch(typeof(SaveGame))]
        [HarmonyPatch("OnPrefabInit")]
        public class SaveGame_OnPrefabInit_Patch
        {
            public static void Postfix(SaveGame __instance)
            {
                __instance.gameObject.AddComponent<SidequestManager>();
            }
        }
    }
}
