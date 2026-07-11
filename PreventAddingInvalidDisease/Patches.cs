using HarmonyLib;

namespace PreventAddingInvalidDisease
{
    public class Patches
    {
        [HarmonyPatch(typeof(PrimaryElement))]
        [HarmonyPatch("AddDisease")]
        public class PrimaryElement_AddDisease_Patch
        {
            public static bool Prefix(byte disease_idx)
            {
                if (disease_idx >= Db.Get().Diseases.Count)
                    return false;
                return true;
            }
        }
    }
}
