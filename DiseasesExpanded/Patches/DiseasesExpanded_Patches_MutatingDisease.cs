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
            }
        }

        [HarmonyPatch(typeof(SaveGame))]
        [HarmonyPatch("OnPrefabInit")]
        public class SaveGame_OnPrefabInit_Patch
        {
            public static void Postfix(SaveGame __instance)
            {
                __instance.gameObject.AddComponent<MutationData>();
            }
        }

        [HarmonyPatch(typeof(Sickness))]
        [HarmonyPatch("Cure")]
        public class Sickness_Cure_Patch
        {
            public static void Postfix(Sickness __instance, GameObject go)
            {
                if (__instance.Id == MutatingSickness.ID) return;

                MutationData.Instance.IncreaseMutationProgress(go);
                UpdateReinforcements(__instance.Id);
            }

            private static void UpdateReinforcements(string id)
            {
                switch (id)
                {
                    case AlienSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stress, 3);
                        break;
                    case BogSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Damage);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_InfectionExposureThreshold);
                        break;
                    case FrostSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stress);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_BaseInfectionResistance);
                        break;
                    case GasSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_SicknessDuration, 3);
                        break;
                    case HungerSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Calories, 3);
                        break;
                    case SpindlySickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Exhaustion);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_SicknessDuration);
                        break;
                    case FoodSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Calories);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_RadiationResistance);
                        break;
                    case SlimeSickness.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Breathing);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_Coughing);
                        break;
                    case ZombieSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Attributes, 3);
                        break;
                    case RadiationSickness.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_RadiationResistance, 3);
                        break;
                    case Allergies.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Stress, 3);
                        break;
                    case ColdBrain.ID: // more common
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Att_Attributes);
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_TemperatureResistance);
                        break;
                    case HeatRash.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_TemperatureResistance, 3);
                        break;
                    case Sunburn.ID:
                        MutationData.Instance.Reinforce(MutationVectors.Vectors.Res_InfectionExposureThreshold, 3);
                        break;
                    default:
                        Debug.Log($"{ModInfo.Namespace}: Cannot reinforce mutation with unknown sickness (ID={id})");
                        break;
                }
            }
        }

    }
}
