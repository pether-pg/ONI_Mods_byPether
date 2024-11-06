﻿using UnityEngine;
using System.Collections.Generic;
using Klei.AI;

namespace DiseasesExpanded
{
    class TestSampleConfig : IEntityConfig
    {
        public const string ID = "TestSample";
        public const string EFFECT_ID = "JustGotTested";
        public static ComplexRecipe recipe;

        public string[] GetDlcIds() => DlcManager.AVAILABLE_ALL_VERSIONS;

        public void OnPrefabInit(GameObject inst)
        {
        }

        public void OnSpawn(GameObject inst)
        {
        }

        public void DefineRecipe()
        {

            ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
            {
                new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 1f),
                new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 1f),
            };
            ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
            {
                new ComplexRecipe.RecipeElement((Tag) ID, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(ApothecaryConfig.ID, ingredients, results), ingredients, results)
            {
                time = 50,
                description = STRINGS.GERMFLASKSAMPLE.DESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag>() { ApothecaryConfig.ID },
                sortOrder = 41
            };
        }

        public GameObject CreatePrefab()
        {
            GameObject looseEntity = EntityTemplates.CreateLooseEntity(
                ID,
                STRINGS.GERMFLASKSAMPLE.NAME,
                STRINGS.GERMFLASKSAMPLE.DESC,
                1f,
                true,
                Assets.GetAnim(Kanims.GermFlaskKanim),
                "object",
                Grid.SceneLayer.Front,
                EntityTemplates.CollisionShape.RECTANGLE,
                0.8f,
                0.4f,
                true);


            DefineRecipe();

            MedicineInfo info = new MedicineInfo(ID, EFFECT_ID, MedicineInfo.MedicineType.Booster, null, null);
            GameObject medical = EntityTemplates.ExtendEntityToMedicine(looseEntity, info);
            return medical;
        }

        public static void OnEatComplete(object data)
        {
            GameObject worker = (GameObject)data;
            if (worker == null)
                return;

            Modifiers modifiers = worker.GetComponent<Modifiers>();
            if (modifiers == null)
                return;

            Sicknesses sicknesses = modifiers.GetSicknesses();
            if (sicknesses == null)
                return;

            List<string> infectingGerms = new List<string>();

            foreach(var s in sicknesses)
            {
                string germId;
                if (TryGetSicknessGerm(s.Sickness.Id, out germId))
                    infectingGerms.Add(germId);
            }

            RadiationMonitor.Instance smi = worker.GetSMI<RadiationMonitor.Instance>();

            if (smi != null && smi.sm.isSick.Get(smi))
                infectingGerms.Add(RadiationPoisoning.ID);

            if (infectingGerms.Count == 0)
                return;

            infectingGerms.Shuffle();
            string spawnedGermId = infectingGerms[0];
            SpawnFlask(Db.Get().Diseases.GetIndex(spawnedGermId), worker);
        }

        private static bool TryGetSicknessGerm(string sicknessId, out string germId)
        {
            germId = string.Empty;

            foreach (ExposureType et in TUNING.GERM_EXPOSURE.TYPES)
                if(et.sickness_id == sicknessId)
                {
                    germId = et.germ_id;
                    return true;
                }
            
            return false;
        }

        private static void SpawnFlask(byte idx, GameObject worker)
        {
            if (!Germcatcher.SpawnedFlasks.ContainsKey(idx))
                return;
            string id = Germcatcher.SpawnedFlasks[idx];

            if (!string.IsNullOrEmpty(id))
            {
                GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(id), worker.transform.GetPosition() + new Vector3(-0.2f, 1.0f, 0), Grid.SceneLayer.Ore);
                if (gameObject != null)
                {
                    PrimaryElement element = gameObject.GetComponent<PrimaryElement>();
                    if (element != null)
                        element.AddDisease(idx, Germcatcher.GatherThreshold, "Gathered germs");
                    gameObject.SetActive(true);
                }
            }
        }
    }
}

