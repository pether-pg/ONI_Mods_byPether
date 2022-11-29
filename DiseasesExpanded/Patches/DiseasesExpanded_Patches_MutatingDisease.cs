using HarmonyLib;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_MutatingDisease
    {
        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("DestroyInstances")]
        public class Game_DestroyInstances_Patch
        {
            public static void Prefix()
            {
                MutationData.Clear();
                MedicalNanobotsData.Clear();
            }
        }

        [HarmonyPatch(typeof(SaveGame))]
        [HarmonyPatch("OnPrefabInit")]
        public class SaveGame_OnPrefabInit_Patch
        {
            public static void Postfix(SaveGame __instance)
            {
                __instance.gameObject.AddComponent<MutationData>();
                __instance.gameObject.AddComponent<MedicalNanobotsData>();
            }
        }

        [HarmonyPatch(typeof(SicknessInstance))]
        [HarmonyPatch("Cure")]
        public class SicknessInstance_Cure_Patch
        {
            public static void Prefix(SicknessInstance __instance)
            {
                if (__instance.Sickness.Id == MutatingSickness.ID) 
                    return;

                GameObject go = __instance.gameObject;
                float percentCured = __instance.GetPercentCured();

                MutationData.Instance.IncreaseMutationProgress(go, percentCured);
                UpdateReinforcements(__instance.Sickness.Id, percentCured);
            }

            private static void UpdateReinforcements(string id, float scale = 1)
            {
                switch (id)
                {
                    case AlienSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stress, 3 * scale);
                        break;
                    case BogSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Damage, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Attributes, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_Replication, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_ExposureThreshold, scale);
                        break;
                    case FrostSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stress, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Breathing, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_BaseInfectionResistance, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_RadiationResistance, scale);
                        break;
                    case GasSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_EffectDuration, 3 * scale);
                        break;
                    case HungerSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Calories, 3 * scale);
                        break;
                    case SpindlySickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Calories, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stamina, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_EffectDuration, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_TemperatureResistance, scale);
                        break;
                    case FoodSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Calories, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stamina, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_RadiationResistance, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_EffectDuration, scale);
                        break;
                    case SlimeSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Damage, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Breathing, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_Replication, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_BaseInfectionResistance, scale);
                        break;
                    case ZombieSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Attributes, 3 * scale);
                        break;
                    case RadiationSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_RadiationResistance, 3 * scale);
                        break;
                    case Allergies.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stress, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stamina, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_Replication, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_ExposureThreshold, scale);
                        break;
                    case ColdBrain.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stress, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Attributes, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_TemperatureResistance, scale);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_ExposureThreshold, scale);
                        break;
                    case HeatRash.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_TemperatureResistance, 3 * scale);
                        break;
                    case Sunburn.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stamina, 3 * scale);
                        break;
                    default:
                        Debug.Log($"{ModInfo.Namespace}: Cannot reinforce mutation with unknown sickness (ID={id})");
                        break;
                }
            }
        }

    }
}
