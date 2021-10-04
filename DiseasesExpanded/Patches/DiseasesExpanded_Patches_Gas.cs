using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Gas
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = GassyGerms.ID,
                sickness_id = GasSickness.ID,
                exposure_threshold = 1,
                excluded_traits = new List<string>() { "Flatulence" },
                base_resistance = 2,
                excluded_effects = new List<string>()
                    {
                      GasSickness.RECOVERY_ID
                    }
            };
        }

        [HarmonyPatch(typeof(GasGrassConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class GasGrassConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                DiseaseDropper.Def def = __result.AddOrGetDef<DiseaseDropper.Def>();
                def.diseaseIdx = Db.Get().Diseases.GetIndex((HashedString)GassyGerms.ID);
                def.emitFrequency = 1f;
                def.averageEmitPerSecond = 1000;
                def.singleEmitQuantity = 100000;
                __result.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = GassyGerms.ID;
            }
        }

        [HarmonyPatch(typeof(Flatulence))]
        [HarmonyPatch("Emit")]
        public static class Flatulence_Emit_Patch
        {
            static byte disease_idx = 0;

            public static bool Prefix(object data)
            {
                GameObject gameObject = (GameObject)data;
                Equippable equippable = gameObject.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
                if (equippable == null && gameObject.GetComponent<Effects>().HasEffect(GasCureConfig.EffectID))
                {
                    int cell = Grid.PosToCell(gameObject.transform.GetPosition());
                    if (disease_idx == 0)
                        disease_idx = Db.Get().Diseases.GetIndex((HashedString)"PollenGerms");
                    SimMessages.ModifyDiseaseOnCell(cell, disease_idx, GasCureConfig.FlowerGermsSpawned);

                    return false;
                }
                return true;
            }
        }
    }
}
