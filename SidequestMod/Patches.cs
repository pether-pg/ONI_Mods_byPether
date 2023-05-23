using HarmonyLib;
using SidequestMod.Sidequests.TutorialQuests;

namespace SidequestMod
{
    public class Patches
    {
        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                QuestManager.RegisterQuest(new TutorialToiletsQuest());
                QuestManager.RegisterQuest(new TutorialOxygenQuest());
                QuestManager.RegisterQuest(new TutorialClinicQuest());
            }
        }

        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("DestroyInstances")]
        public class Game_DestroyInstances_Patch
        {
            public static void Prefix()
            {
                QuestManager.Clear();
            }
        }

        [HarmonyPatch(typeof(SaveGame))]
        [HarmonyPatch("OnPrefabInit")]
        public class SaveGame_OnPrefabInit_Patch
        {
            public static void Postfix(SaveGame __instance)
            {
                __instance.gameObject.AddComponent<QuestManager>();
            }
        }
    }
}
