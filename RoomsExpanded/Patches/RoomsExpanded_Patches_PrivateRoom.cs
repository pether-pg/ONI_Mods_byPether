using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Database;
using Klei.AI;
using UnityEngine;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_PrivateRoom
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (Settings.Instance.PrivateBedroom.IncludeRoom)
            {
                __instance.Add(RoomTypes_AllModded.PrivateRoom);

                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.PrivateRoom, __instance.Barracks);
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.PrivateRoom, __instance.Bedroom);
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.PrivateRoom, __instance.MassageClinic);
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.PrivateRoom, __instance.RecRoom);
                RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.PrivateRoom, __instance.PlumbedBathroom);
                RoomConstraintTags.AddStompInConflict(__instance.Hospital, RoomTypes_AllModded.PrivateRoom);

                if(Settings.Instance.Bathroom.IncludeRoom)
                    RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.PrivateRoom, RoomTypes_AllModded.BathroomRoom);

                if (Settings.Instance.Museum.IncludeRoom)
                    RoomConstraintTags.AddStompInConflict(RoomTypes_AllModded.PrivateRoom, RoomTypes_AllModded.Museum);
            }
        }

        [HarmonyPatch(typeof(RoomType))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { 
            typeof(string), 
            typeof(string), 
            typeof(string), 
            typeof(string), 
            typeof(string), 
            typeof(RoomTypeCategory), 
            typeof(RoomConstraints.Constraint), 
            typeof(RoomConstraints.Constraint[]), 
            typeof(RoomDetails.Detail[]), 
            typeof(int), 
            typeof(RoomType[]), 
            typeof(bool), 
            typeof(bool), 
            typeof(string[]),
            typeof(int) 
        })]
        public static class RoomType_Constructor_Patch
        {
            public static void Prefix(string id, ref RoomType[] upgrade_paths)
            {
                if (!Settings.Instance.PrivateBedroom.IncludeRoom) return;

                if(id == "Barracks" || id == "Bedroom")
                {
                    List<RoomType> list = new List<RoomType>();
                    if (upgrade_paths != null)
                        foreach (RoomType rt in upgrade_paths)
                            list.Add(rt);
                    list.Add(RoomTypes_AllModded.PrivateRoom);
                    upgrade_paths = list.ToArray();
                }
            }
        }

        [HarmonyPatch(typeof(Bed))]
        [HarmonyPatch("AddEffects")]
        public static class BedConfig_AddEffects_Patch
        {
            public static void Prefix(Bed __instance)
            {
                if (!Settings.Instance.PrivateBedroom.IncludeRoom) return;
                if (!RoomTypes_AllModded.IsInTheRoom(__instance, RoomTypePrivateRoomData.RoomId)) return;

                Sleepable sleepable = Traverse.Create(__instance).Field("sleepable").GetValue<Sleepable>();
                if (sleepable == null) return;

                if (__instance.gameObject.name == "BedComplete" // from base ONI
                    || __instance.gameObject.name == "LadderBedComplete" // from ONI DLC
                    || __instance.gameObject.name == "DoubleBedComplete" // from |ScientisT|RU|'s https://steamcommunity.com/sharedfiles/filedetails/?id=2584828134
                    )
                    sleepable.worker.GetComponent<Effects>().Add(RoomTypePrivateRoomData.BasicEffectId, true);
                else if (__instance.gameObject.name == "LuxuryBedComplete" // from base ONI
                    || __instance.gameObject.name == "CozyBedComplete" // from Ronivan's https://steamcommunity.com/sharedfiles/filedetails/?id=2051486552
                    || __instance.gameObject.name == "WoodenBedComplete" // from Ronivan's https://steamcommunity.com/sharedfiles/filedetails/?id=2051486552
                    )
                    sleepable.worker.GetComponent<Effects>().Add(RoomTypePrivateRoomData.LuxuryEffectId, true);
                else
                    Debug.Log($"RoomsExpanded: invalid bed name in Private Bedroom: {__instance.gameObject.name}");
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public static class Db_Initialize_Patch
        {
            public static void Postfix()
            {
                if (!Settings.Instance.PrivateBedroom.IncludeRoom)
                    return;

                Effect basicEffect = new Effect(RoomTypePrivateRoomData.BasicEffectId, STRINGS.ROOMS.EFFECTS.PRIVATEROOM.NAME, STRINGS.ROOMS.EFFECTS.PRIVATEROOM.DESCRIPTION, 500, false, false, false);
                basicEffect.SelfModifiers = new List<AttributeModifier>();
                basicEffect.SelfModifiers.Add(new AttributeModifier("QualityOfLife", 2, description: STRINGS.ROOMS.EFFECTS.PRIVATEROOM.NAME));

                Effect luxuryEffect = new Effect(RoomTypePrivateRoomData.LuxuryEffectId, STRINGS.ROOMS.EFFECTS.LUXURYPRIVATEROOM.NAME, STRINGS.ROOMS.EFFECTS.LUXURYPRIVATEROOM.DESCRIPTION, 500, false, false, false);
                luxuryEffect.SelfModifiers = new List<AttributeModifier>();
                luxuryEffect.SelfModifiers.Add(new AttributeModifier("QualityOfLife", 3, description: STRINGS.ROOMS.EFFECTS.LUXURYPRIVATEROOM.NAME));

                Db.Get().effects.Add(basicEffect);
                Db.Get().effects.Add(luxuryEffect);
            }
        }
    }
}
