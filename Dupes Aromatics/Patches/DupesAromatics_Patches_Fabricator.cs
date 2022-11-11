using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace Dupes_Aromatics.Patches
{
    class DupesAromatics_Patches_Fabricator
    {
        [HarmonyPatch(typeof(AirFilterConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        public static class AirFilterConfig_DoPostConfigureComplete_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<Operational>();
                AromaticsFabricator cf = go.AddOrGet<AromaticsFabricator>();
                cf.duplicantOperated = false;
                BuildingTemplates.CreateComplexFabricatorStorage(go, cf);

                //AromaticsFabricator.RegisterAromaticsRecipe(new ComplexRecipe.RecipeElement[1] { new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 1f) }, RoseScent.ID, "RoseScent recipe");
                //AromaticsFabricator.RegisterAromaticsRecipe(new ComplexRecipe.RecipeElement[1] { new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 1f) }, LavenderScent.ID, "LavenderScent recipe");
                //AromaticsFabricator.RegisterAromaticsRecipe(new ComplexRecipe.RecipeElement[1] { new ComplexRecipe.RecipeElement(SimHashes.SlimeMold.CreateTag(), 1f) }, SlimeGerms.ID, "SlimeGerms recipe");
            }
        }
    }
}
