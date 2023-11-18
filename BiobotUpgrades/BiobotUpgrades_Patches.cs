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
                ZombieFuelMakerStates.Def def = inst.AddOrGetDef<ZombieFuelMakerStates.Def>();
                RobotBatteryMonitor.Instance battery = inst.GetSMI<RobotBatteryMonitor.Instance>();
                def.CreateSMI(battery.master).StartSM();
            }
        }


        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch(nameof(Db.Initialize))]
        public class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                RegisterStrings.MakeStatusItemStrings(ZombieFuelMakerStates.INACTIVE_STATUS_ID, STRINGS.REFUEL_MODULE.INACTIVE.NAME, STRINGS.REFUEL_MODULE.INACTIVE.TOOLTIP);
                RegisterStrings.MakeStatusItemStrings(ZombieFuelMakerStates.RECHARGING_STATUS_ID, STRINGS.REFUEL_MODULE.RECHARGING.NAME, STRINGS.REFUEL_MODULE.RECHARGING.TOOLTIP);

                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.00f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.10f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.25f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.50f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(0.75f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(1.00f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(1.50f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(2.00f));

                StatusItem inactiveStatus = new StatusItem(ZombieFuelMakerStates.INACTIVE_STATUS_ID, "CREATURES", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
                StatusItem rechargingStatus = new StatusItem(ZombieFuelMakerStates.RECHARGING_STATUS_ID, "CREATURES", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);

                Db.Get().CreatureStatusItems.Add(inactiveStatus);
                Db.Get().CreatureStatusItems.Add(rechargingStatus);
            }
        }

    }
}
