using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Gas
    {
        [HarmonyPatch(typeof(GasGrassConfig))]
        [HarmonyPatch("CreatePrefab")]
        public static class GasGrassConfig_CreatePrefab_Patch
        {
            public static void Postfix(ref GameObject __result)
            {
                if (!Settings.Instance.MooFlu.IncludeDisease)
                    return;

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
            public static bool Prefix(object data)
            {
                GameObject gameObject = (GameObject)data;
                Equippable equippable = gameObject.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
                int cell = Grid.PosToCell(gameObject.transform.GetPosition());
                if (equippable == null && gameObject.GetComponent<Effects>().HasEffect(GasCureConfig.EffectID))
                {
                    SimMessages.ModifyDiseaseOnCell(cell, GermIdx.PollenGermsIdx, GasCureConfig.FlowerGermsSpawned);
                    return false;
                }

                Sicknesses sicknesses = gameObject.GetSicknesses();
                if(sicknesses != null)
                    foreach (SicknessInstance sicknessInstance in sicknesses)
                        if (sicknessInstance.modifier.Id == GasSickness.ID)
                        {
                            SimMessages.ModifyDiseaseOnCell(cell, GermIdx.GassyGermsIdx, GasSickness.GERMS_PER_FART);
                            break;
                        }

                return true;
            }
        }

        [HarmonyPatch(typeof(Butcherable))]
        [HarmonyPatch(nameof(Butcherable.CreateDrops))]
        public static class Butcherable_CreateDrops_Patch
        {
            public static void Postfix(Butcherable __instance, ref GameObject[] __result)
            {
                if (!Settings.Instance.InfectRawMeatDropsWithGerms)
                    return;

                Database.Diseases diseases = Db.Get().Diseases;
                string germId = FoodGerms.ID;
                if (Settings.Instance.MooFlu.IncludeDisease && __instance.gameObject.name == MooConfig.ID)
                    germId = GassyGerms.ID;

                foreach (GameObject go in __result)
                {
                    if (go.name != MeatConfig.ID && go.name != FishMeatConfig.ID)
                        continue;

                    if (germId == GassyGerms.ID && !Settings.Instance.MooFlu.IncludeDisease)
                        continue;

                    PrimaryElement prime = go.GetComponent<PrimaryElement>();
                    if (prime != null && !string.IsNullOrEmpty(germId))
                        prime.AddDisease(diseases.GetIndex(germId), 100000, "Infected meat");
                }
            }
        }

        [HarmonyPatch(typeof(GeyserGenericConfig))]
        [HarmonyPatch("GenerateConfigs")]
        public static class GeyserGenericConfig_GenerateConfigs_Patch
        {
            public static void Postfix(ref List<GeyserGenericConfig.GeyserPrefabParams> __result)
            {
                if (!Settings.Instance.MooFlu.IncludeDisease)
                    return;

                foreach(GeyserGenericConfig.GeyserPrefabParams param in __result)
                {
                    if (param.anim == "geyser_gas_methane_kanim")
                    {
                        param.geyserType.AddDisease(new Klei.SimUtil.DiseaseInfo()
                        {
                            idx = Db.Get().Diseases.GetIndex(GassyGerms.ID),
                            count = 5000
                        });
                        break;
                    }
                }
            }
        }


        [HarmonyPatch(typeof(GassyMooCometConfig))]
        [HarmonyPatch("OnPrefabInit")]
        public class RockCometConfig_OnPrefabInit_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (!Settings.Instance.MooFlu.IncludeDisease)
                    return;
                DiseasesExpanded_Patches_SpaceGoo.EnhanceCometWithGerms(go, GermIdx.GassyGermsIdx, 0);
            }
        }
    }
}
