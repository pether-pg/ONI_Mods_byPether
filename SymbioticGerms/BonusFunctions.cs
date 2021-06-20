using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HarmonyLib;

namespace SymbioticGerms
{
    public static class BonusFunctions
    {

        public static void ModifyTemperature(GameObject go, float temp)
        {
            PrimaryElement goPrimary = go.GetComponent<PrimaryElement>();
            if (goPrimary == null)
                return;
            goPrimary.Temperature += temp;
        }

        public static void SpawnGas(GameObject go, byte germIdx, float maxBonus, SimHashes gasHash)
        {
            int higherGerms = Numbers.GetGermCount(go, germIdx);
            float bonus = Numbers.PercentOfMaxGerms(higherGerms);
            float amount = bonus * maxBonus;
            PrimaryElement goPrimary = go.GetComponent<PrimaryElement>();
            if (goPrimary == null)
                return;
            float temperature = goPrimary.Temperature;
            Element element = ElementLoader.FindElementByHash(gasHash);
            if (amount > 0)
                SimMessages.AddRemoveSubstance(Grid.PosToCell(go), (int)element.idx, CellEventLogger.Instance.ElementConsumerSimUpdate, amount, temperature, germIdx, higherGerms);
        }

        public static void SpawnAdditionalFood(GameObject go, byte germIdx, float maxBonus, Crop crop)
        {
            int higherGerms = Numbers.GetGermCount(go, germIdx);
            float bonus = Numbers.PercentOfMaxGerms(higherGerms);

            Crop.CropVal cropVal = crop.cropVal;
            if (string.IsNullOrEmpty(cropVal.cropId))
                return;

            PrimaryElement goPrimary = go.GetComponent<PrimaryElement>();
            if (goPrimary == null)
                return;

            GameObject gameObject = Scenario.SpawnPrefab(Grid.PosToCell(go), 0, 0, cropVal.cropId);
            if (bonus > 0 && (UnityEngine.Object)gameObject != (UnityEngine.Object)null)
            {
                gameObject.transform.SetPosition(gameObject.transform.GetPosition() + new Vector3(0.0f, 0.75f, 0.0f));
                gameObject.SetActive(true);
                PrimaryElement component1 = gameObject.GetComponent<PrimaryElement>();
                if (component1 == null)
                    return;
                component1.Units = (float)cropVal.numProduced * bonus * maxBonus;
                component1.Temperature = goPrimary.Temperature;
            }
        }

        public static void ChanceForDoubleHarvest(GameObject go, byte germIdx, float maxChance, Crop crop)
        {
            int higherGerms = Numbers.GetGermCount(go, germIdx);
            float bonus = Numbers.PercentOfMaxGerms(higherGerms);
            float chance = bonus * maxChance;
            if (UnityEngine.Random.Range(0, 1.0f) > chance)
                return;

            Crop.CropVal cropVal = crop.cropVal;
            if (string.IsNullOrEmpty(cropVal.cropId))
                return;

            PrimaryElement goPrimary = go.GetComponent<PrimaryElement>();
            if (goPrimary == null)
                return;

            GameObject gameObject = Scenario.SpawnPrefab(Grid.PosToCell(go), 0, 0, cropVal.cropId);
            if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
            {
                gameObject.transform.SetPosition(gameObject.transform.GetPosition() + new Vector3(0.0f, 0.75f, 0.0f));
                gameObject.SetActive(true);
                PrimaryElement component1 = gameObject.GetComponent<PrimaryElement>();
                if (component1 == null)
                    return;
                component1.Units = (float)cropVal.numProduced;
                component1.Temperature = goPrimary.Temperature;
            }
        }

        public static void AdditionalLiquidPoop(GameObject go, CreatureCalorieMonitor.Instance __instance, byte germIdx, float maxBonus)
        {
            int higherGerms = Numbers.GetGermCount(go, germIdx);
            float bonus = Numbers.PercentOfMaxGerms(higherGerms);

            Tag tag = Tag.Invalid;
            float amount = 0.0f;
            List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry> caloriesConsumed;
            caloriesConsumed = Traverse.Create(__instance.stomach).Field("caloriesConsumed").GetValue<List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry>>();
            if (caloriesConsumed == null || caloriesConsumed.Count == 0)
                return;
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

            amount *= bonus * maxBonus;

            Element element = ElementLoader.GetElement(tag);
            float temperature = go.GetComponent<PrimaryElement>().Temperature;
            if (element.IsLiquid && amount > 0)
                FallingWater.instance.AddParticle(Grid.PosToCell(go), element.idx, amount, temperature, germIdx, higherGerms, true);
        }

        public static void AdditionalSolidPoop(GameObject go, CreatureCalorieMonitor.Instance __instance, byte germIdx, float maxBonus)
        {
            int higherGerms = Numbers.GetGermCount(go, germIdx);
            float bonus = Numbers.PercentOfMaxGerms(higherGerms);

            Tag tag = Tag.Invalid;
            float amount = 0.0f;
            List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry> caloriesConsumed;
            caloriesConsumed = Traverse.Create(__instance.stomach).Field("caloriesConsumed").GetValue<List<CreatureCalorieMonitor.Stomach.CaloriesConsumedEntry>>();
            if (caloriesConsumed == null || caloriesConsumed.Count == 0)
                return;
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

            amount *= bonus * maxBonus;

            Element element = ElementLoader.GetElement(tag);
            float temperature = go.GetComponent<PrimaryElement>().Temperature;
            int cell = Grid.PosToCell(go.transform.GetPosition());
            if (!element.IsLiquid && amount > 0)
                element.substance.SpawnResource(Grid.CellToPosCCC(cell, Grid.SceneLayer.Ore), amount, temperature, germIdx, higherGerms);
        }
    }
}
