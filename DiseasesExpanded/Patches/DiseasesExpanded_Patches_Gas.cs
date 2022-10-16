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

        [HarmonyPatch(typeof(Butcherable))]
        [HarmonyPatch("OnButcherComplete")]
        public static class Butcherable_OnButcherComplete_Patch
        {
            static MethodInfo spawnPrefabMethodInfo = AccessTools.Method(
                typeof(Scenario), 
                nameof(Scenario.SpawnPrefab),
                new System.Type[] { typeof(int), typeof(int), typeof(int), typeof(string), typeof(Grid.SceneLayer)});
            
            static MethodInfo myExtraCodeMethodInfo = AccessTools.Method(
                typeof(Butcherable_OnButcherComplete_Patch), 
                nameof(Butcherable_OnButcherComplete_Patch.InfectWithGerms));

            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                if (spawnPrefabMethodInfo == null || myExtraCodeMethodInfo == null)
                    Debug.Log($"{ModInfo.Namespace}: Butcherable_OnButcherComplete_Patch encountered null MethodInfo, no changes will take place...");

                foreach (var instruction in instructions)
                {
                    // In any case, call the instruction
                    yield return instruction;

                    // Ignore extra code if it wouldn't work anyway
                    if (spawnPrefabMethodInfo == null || myExtraCodeMethodInfo == null)
                        continue;

                    // If that was Scenario.SpawnPrefab, emhance spawned prefab with germs
                    if (instruction.operand is MethodInfo m && m == spawnPrefabMethodInfo)
                    {
                        // Load on the stack 0th argument of OnButcherComplete method - Butcherable object
                        yield return new CodeInstruction(OpCodes.Ldarg_0);

                        // Call InfectWithGerms to add Food- or Gassy Germs
                        // GameObject go is already on the stack after Scenario.SpawnPrefab()
                        yield return new CodeInstruction(OpCodes.Call, myExtraCodeMethodInfo);
                    }
                }
            }

            public static GameObject InfectWithGerms(GameObject go, Butcherable butherable)
            {
                Database.Diseases diseases = Db.Get().Diseases;
                string germId = (butherable.gameObject.name == MooConfig.ID) ? GassyGerms.ID : FoodGerms.ID;

                PrimaryElement prime = go.GetComponent<PrimaryElement>();
                if (prime != null && !string.IsNullOrEmpty(germId))
                    prime.AddDisease(diseases.GetIndex(germId), 100000, "Infected meat");

                return go;
            }
        }

        [HarmonyPatch(typeof(GeyserGenericConfig))]
        [HarmonyPatch("GenerateConfigs")]
        public static class GeyserGenericConfig_GenerateConfigs_Patch
        {
            public static void Postfix(ref List<GeyserGenericConfig.GeyserPrefabParams> __result)
            {
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
    }
}
