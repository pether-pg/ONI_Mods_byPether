using HarmonyLib;

namespace FreeSkillpoints
{
    public class FreeSkillpoints_Patches
    {
        [HarmonyPatch(typeof(SkillWidget))]
        [HarmonyPatch("OnPointerClick")]
        public class SkillWidget_OnPointerClick_Patch
        {
            public static void Prefix(SkillWidget __instance)
            {
                MinionIdentity minionIdentity;
                SkillsScreen skillsScreen = Traverse.Create(__instance).Field("skillsScreen").GetValue<SkillsScreen>();
                skillsScreen.GetMinionIdentity(skillsScreen.CurrentlySelectedMinion, out minionIdentity, out StoredMinionIdentity _);
                if (!((UnityEngine.Object)minionIdentity != (UnityEngine.Object)null))
                    return;
                MinionResume component = minionIdentity.GetComponent<MinionResume>();
                if (CanGrantBonus() && NeedGrantedBonus(component))
                    component.ForceAddSkillPoint();
            }

            private static bool CanGrantBonus()
            {
                return SaveGame.Instance.sandboxEnabled || Settings.Instance.UseWithoutSandbox;
            }

            private static bool NeedGrantedBonus(MinionResume component)
            {
                return component.AvailableSkillpoints < 1;
            }
        }
    }
}
