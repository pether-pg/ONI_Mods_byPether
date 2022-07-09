using UnityEngine;
using Klei.AI;
using System;
using System.Linq;
using System.Collections.Generic;
using Database;
using HarmonyLib;

namespace RoomsExpanded
{
    class RoomsExpanded_Patches_MuseumSpace
    {
        public static void AddRoom(ref RoomTypes __instance)
        {
            if (!Settings.Instance.MuseumSpace.IncludeRoom)
                return;

            __instance.Add(RoomTypes_AllModded.MuseumSpace);
        }

        public static int CountUniqueArtifacts(Room room)
        {
            List<string> foundArtifacts = new List<string>();
            if (room != null)
                foreach (KPrefabID building in room.buildings)
                {
                    if (building == null) continue;
                    ItemPedestal pedestal = building.GetComponent<ItemPedestal>();
                    if (pedestal == null) continue;
                    SingleEntityReceptacle receptacle = Traverse.Create(pedestal).Field("receptacle").GetValue<SingleEntityReceptacle>();
                    if (receptacle == null || receptacle.Occupant == null) continue;
                    foreach (List<string> list in ArtifactConfig.artifactItems.Values)
                    {
                        bool shouldBreak = false;
                        foreach (string id in list)
                            if (receptacle.Occupant.name == id && !foundArtifacts.Contains(id))
                            {
                                foundArtifacts.Add(id);
                                shouldBreak = true;
                                break;
                            }
                        if (shouldBreak) break;
                    }

                }
            return foundArtifacts.Count;
        }

        private static bool CanGrantExtraBonus()
        {
            ColonyAchievementTracker tracker = SaveGame.Instance.GetComponent<ColonyAchievementTracker>();
            if (tracker == null)
                return false;

            ColonyAchievementStatus status = tracker.achievements.Where(a => a.Key == Db.Get().ColonyAchievements.CollectedArtifacts.Id).First().Value;
            if (status.success && !status.failed)
                return true;

            return !DlcManager.IsExpansion1Active();
        }

        public static Effect CalculateEffectBonus(MinionModifiers modifiers, int extraBonus = 0)
        {
            foreach (AttributeInstance inst in modifiers.attributes.AttributeTable)
                Debug.Log(inst.Name);

            AttributeInstance scienceAttrInstance = modifiers.attributes.AttributeTable.Where(p => p.Name == "Piloting").FirstOrDefault();
            if (scienceAttrInstance == null)
                return null;

            if (!Settings.Instance.MuseumSpace.Bonus.HasValue)
                return null;

            float piloting = scienceAttrInstance.GetTotalValue();
            float bonus = Settings.Instance.MuseumSpace.Bonus.Value;
            int moraleBonus = Mathf.Clamp((int)Math.Ceiling(piloting * bonus), 1, 10);
            
            if(CanGrantExtraBonus())
                moraleBonus += extraBonus;

            Effect effect = new Effect(RoomTypeMuseumSpaceData.EffectId, STRINGS.ROOMS.EFFECTS.MUSEUMSPACE.NAME, STRINGS.ROOMS.EFFECTS.MUSEUMSPACE.DESCRIPTION, 240, false, true, false);
            effect.SelfModifiers = new List<AttributeModifier>();
            effect.SelfModifiers.Add(new AttributeModifier("QualityOfLife", moraleBonus, description: STRINGS.ROOMS.EFFECTS.MUSEUMSPACE.NAME));
            return effect;
        }
    }
}
