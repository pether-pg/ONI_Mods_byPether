using HarmonyLib;
using UnityEngine;

namespace BiobotUpgrades
{
    public class BiobotUpgrades_Patches
    {
        [HarmonyPatch(typeof(MorbRoverConfig))]
        [HarmonyPatch("OnSpawn")]
        public class MorbRoverConfig_OnSpawn_Patch
        {
            public static void Postfix(GameObject inst)
            {
                inst.AddOrGetDef<ZombieFuelMakerStates.Def>();
            }
        }


        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch(nameof(Db.Initialize))]
        public class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.00f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.10f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.25f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.50f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.75f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(1.00f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(1.50f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(2.00f));
            }
        }

    }
}
