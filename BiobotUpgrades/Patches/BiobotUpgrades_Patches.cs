using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Database;

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
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(2.50f));
                Db.Get().effects.Add(ZombieFuelMakerStates.CreateRechargeEffect(3.00f));

                StatusItem inactiveStatus = new StatusItem(ZombieFuelMakerStates.INACTIVE_STATUS_ID, "CREATURES", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);
                StatusItem rechargingStatus = new StatusItem(ZombieFuelMakerStates.RECHARGING_STATUS_ID, "CREATURES", "status_item_info", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID);

                Db.Get().CreatureStatusItems.Add(inactiveStatus);
                Db.Get().CreatureStatusItems.Add(rechargingStatus);
            }
        }

        [HarmonyPatch(typeof(ModifierSet))]
        [HarmonyPatch(nameof(ModifierSet.CreateTrait))]
        public class ModifierSet_CreateTrait_Patch
        {
            public static void Prefix(string id, ref ChoreGroup[] disabled_chore_groups)
            {
                if (id != MorbRoverConfig.ID + "BaseTrait")
                    return;

                List<ChoreGroup> choreList = new List<ChoreGroup>();
                foreach (ChoreGroup cg in disabled_chore_groups)
                    choreList.Add(cg);

                choreList.Remove(Db.Get().ChoreGroups.Farming);
                choreList.Remove(Db.Get().ChoreGroups.LifeSupport);

                disabled_chore_groups = choreList.ToArray();
            }
        }

        [HarmonyPatch(typeof(BaseRoverConfig))]
        [HarmonyPatch(nameof(BaseRoverConfig.BaseRover))]
        public class BaseRoverConfig_BaseRover_Patch
        {
            public static void Postfix(GameObject __result)
            {
                Modifiers modifiers = __result.GetComponent<Modifiers>();
                modifiers.initialAttributes.Add(Db.Get().Attributes.Botanist.Id);
            }
        }

        [HarmonyPatch(typeof(ChorePreconditions))]
        [HarmonyPatch(MethodType.Constructor)]
        public class ChorePreconditions_Constructor_Patch
        {
            public static void Postfix(ref ChorePreconditions __instance)
            {
                Chore.PreconditionFn OriginalFn = __instance.HasSkillPerk.fn;
                __instance.HasSkillPerk.fn = ((ref Chore.Precondition.Context context, object data) =>
                {
                    if (BiobotPrecondition(context, data))
                        return true;
                    return OriginalFn(ref context, data);
                });
            }

            public static bool BiobotPrecondition(Chore.Precondition.Context context, object data)
            {
                if (context.consumerState.gameObject.name != MorbRoverConfig.ID)
                    return false;

                if (GetSkillPerkIdHash(data) == Db.Get().SkillPerks.CanDigVeryFirm.Id) return true;
                if (GetSkillPerkIdHash(data) == Db.Get().SkillPerks.CanDigSuperDuperHard.Id) return true;
                if (GetSkillPerkIdHash(data) == Db.Get().SkillPerks.CanDigNearlyImpenetrable.Id) return true;

                return false;
            }

            public static HashedString GetSkillPerkIdHash(object data)
            {
                switch (data)
                {
                    case SkillPerk _:
                        SkillPerk perk = data as SkillPerk;
                        return (HashedString)perk.Id;
                    case HashedString perkId2:
                        return perkId2;
                    case string _:
                        HashedString perkId1 = (HashedString)(string)data;
                        return perkId1;
                    default:
                        return string.Empty;
                }                
            }
        }
    }
}
