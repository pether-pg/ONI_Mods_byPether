using Harmony;
using UnityEngine;
using System.Collections.Generic;

namespace SymbioticGerms
{
    public class SymbioticGerms_Patches
    {
        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
            }
        }

        [HarmonyPatch(typeof(CreatureCalorieMonitor.Instance))]
        [HarmonyPatch("Poop")]
        public class CreatureCalorieMonitor_Poop_Patch
        {
            public static void Prefix(CreatureCalorieMonitor.Instance __instance)
            {
                GameObject go = __instance.gameObject;
                if(go.name.Contains("Oilfloater")) // include modded ones
                {
                    int higherGerms = Numbers.GetGermCount(go, Numbers.IndexZombieSpores);
                    float bonus = Numbers.PercentOfMaxGerms(higherGerms);

                    Tag tag = Tag.Invalid;
                    float amount = 0.0f;
                    List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry> caloriesConsumed;
                    caloriesConsumed = Traverse.Create(__instance.stomach).Field("caloriesConsumed").GetValue<List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry>>();
                    for (int index = 0; index < caloriesConsumed.Count; ++index)
                    {
                        CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry caloriesConsumedEntry = caloriesConsumed[index];
                        if (caloriesConsumedEntry.calories > 0)
                        {
                            Diet.Info dietInfo = __instance.stomach.diet.GetDietInfo(caloriesConsumedEntry.tag);
                            if (dietInfo != null && (!(tag != Tag.Invalid) || !(tag != dietInfo.producedElement)))
                            {
                                amount += dietInfo.ConvertConsumptionMassToProducedMass(dietInfo.ConvertCaloriesToConsumptionMass(caloriesConsumedEntry.calories));
                                tag = dietInfo.producedElement;
                            }
                        }
                    }

                    amount *= bonus * Settings.Instance.MaxSlicksterBonus;

                    Element element = ElementLoader.GetElement(tag);
                    float temperature = go.GetComponent<PrimaryElement>().Temperature;
                    if (element.IsLiquid && amount > 0)
                        FallingWater.instance.AddParticle(Grid.PosToCell(go), element.idx, amount, temperature, Numbers.IndexZombieSpores, higherGerms, true);
                }
            }
        }

        [HarmonyPatch(typeof(Crop))]
        [HarmonyPatch("SpawnFruit")]
        public class Crop_SpawnFruit_Patch
        {
            public static void Prefix(Crop __instance)
            {
                GameObject go = __instance.gameObject;
                if(go.name == "MushroomPlant")
                {
                    int higherGerms = Numbers.GetGermCount(go, Numbers.IndexSlimeLung);
                    float bonus = Numbers.PercentOfMaxGerms(higherGerms);
                    float amount = bonus * Settings.Instance.MaxDuskCupBonus;
                    float temperature = go.GetComponent<PrimaryElement>().Temperature;
                    Element element = ElementLoader.FindElementByHash(SimHashes.ContaminatedOxygen);

                    if (amount > 0)
                        SimMessages.AddRemoveSubstance(Grid.PosToCell(go), (int)element.idx, CellEventLogger.Instance.ElementConsumerSimUpdate, amount, temperature, Numbers.IndexSlimeLung, higherGerms);
                }

                if(go.name == "BasicSingleHarvestPlant")
                {
                    int higherGerms = Numbers.GetGermCount(go, Numbers.IndexFoodPoisoning);
                    float bonus = Numbers.PercentOfMaxGerms(higherGerms);

                    Crop.CropVal cropVal = __instance.cropVal;
                    if (string.IsNullOrEmpty(cropVal.cropId))
                        return;
                    GameObject gameObject = Scenario.SpawnPrefab(Grid.PosToCell(go), 0, 0, cropVal.cropId);
                    if (bonus > 0 && (UnityEngine.Object)gameObject != (UnityEngine.Object)null)
                    {
                        gameObject.transform.SetPosition(gameObject.transform.GetPosition() + new Vector3(0.0f, 0.75f, 0.0f));
                        gameObject.SetActive(true);
                        PrimaryElement component1 = gameObject.GetComponent<PrimaryElement>();
                        component1.Units = (float)cropVal.numProduced * bonus * Settings.Instance.MaxMealLiceBonus;
                        component1.Temperature = go.GetComponent<PrimaryElement>().Temperature;
                    }
                }
            }
        }
    }
}
