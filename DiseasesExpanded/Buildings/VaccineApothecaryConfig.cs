using TUNING;
using UnityEngine;

namespace DiseasesExpanded
{
    class VaccineApothecaryConfig : IBuildingConfig
    {
        public const string ID = "VaccineApothecary";
        public const float RecipeTime = 100;
        public const float MutationRecipeTime = 600;
        public const float UraniumOreCost = 20f;
        public const float RefinedCarbonCost = 200f;

        public static ComplexRecipe.RecipeElement GetMainIngridient()
        {
            if(DlcManager.IsExpansion1Active())
                return new ComplexRecipe.RecipeElement(SimHashes.UraniumOre.CreateTag(), VaccineApothecaryConfig.UraniumOreCost);
            return new ComplexRecipe.RecipeElement(SimHashes.RefinedCarbon.CreateTag(), VaccineApothecaryConfig.RefinedCarbonCost);
        }

        public static string GetAdvancedApothecaryId()
        {
            if (DlcManager.IsExpansion1Active())
                return ID;
            return ApothecaryConfig.ID;
        }

        public override string[] GetDlcIds() => DlcManager.AVAILABLE_EXPANSION1_ONLY;

        public override BuildingDef CreateBuildingDef()
        {
            float[] tieR4 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
            string[] steelMaterial = new string[1] { SimHashes.Steel.ToString() };
            EffectorValues none1 = NOISE_POLLUTION.NONE;
            EffectorValues none2 = BUILDINGS.DECOR.NONE;
            EffectorValues noise = none1;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, 3, 3, "medicine_nuclear_kanim", 100, 120f, tieR4, steelMaterial, 800f, BuildLocationRule.OnFloor, none2, noise);
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 120f;
            buildingDef.ExhaustKilowattsWhenActive = 0.25f;
            buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
            buildingDef.ViewMode = OverlayModes.Power.ID;
            buildingDef.AudioCategory = "Glass";
            buildingDef.AudioSize = "large";
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            VaccineApothecary apothecary = go.AddOrGet<VaccineApothecary>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, (ComplexFabricator)apothecary);
            go.AddOrGet<VaccineApothecaryWorkable>();
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<PoweredActiveStoppableController.Def>();

            float absorbtionTarget = 400; // how much rads you want to expose your doctor to
            float cycleTime = 600;
            float emitRads = absorbtionTarget * cycleTime / RecipeTime;

            RadiationEmitter radiationEmitter = go.AddComponent<RadiationEmitter>();
            radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
            radiationEmitter.emitRadiusX = (short)2;
            radiationEmitter.emitRadiusY = (short)2;
            radiationEmitter.radiusProportionalToRads = false;
            radiationEmitter.emissionOffset = new Vector3(0.0f, 1f, 0.0f);
            radiationEmitter.emitRads = emitRads;
            radiationEmitter.SetEmitting(false);            
        }
    }
}
