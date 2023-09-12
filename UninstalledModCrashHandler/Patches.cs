using HarmonyLib;

namespace UninstalledModCrashHandler
{
    public class Patches
    {
        [HarmonyPatch(typeof(PrimaryElement))]
        [HarmonyPatch(nameof(PrimaryElement.AddDisease))]
        public static class PrimaryElement_AddDisease_Patch
        {
            // Log info for issue https://github.com/pether-pg/ONI_Mods_byPether/issues/60
            public static void Prefix(PrimaryElement __instance, ref byte disease_idx)
            {
                if (disease_idx >= Db.Get().Diseases.Count && disease_idx != byte.MaxValue)
                    Debug.Log($"UninstalledModCrashHandler: expecting crash to happen " +
                        $"- PrimaryElement.AddDisease() called with invalid disease_idx = {disease_idx} " +
                        $"for PrimaryElement = {__instance.tag} of gameObject = {__instance.gameObject.name} at grid location " +
                        $"x = {Grid.PosToXY(__instance.gameObject.transform.position).x}; y = {Grid.PosToXY(__instance.gameObject.transform.position).y} ");
            }
        }
    }
}
