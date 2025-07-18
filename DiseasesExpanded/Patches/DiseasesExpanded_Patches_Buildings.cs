using HarmonyLib;
using UnityEngine;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Buildings
    {
        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                BasicModUtils.MakeBuildingStrings(GermcatcherConfig.ID,
                                        STRINGS.BUILDINGS.GERMCATCHER.NAME,
                                        STRINGS.BUILDINGS.GERMCATCHER.DESC,
                                        STRINGS.BUILDINGS.GERMCATCHER.EFFECT);

                BasicModUtils.MakeBuildingStrings(VaccineApothecaryConfig.ID,
                                        STRINGS.BUILDINGS.VACCINEAPOTHECARY.NAME,
                                        STRINGS.BUILDINGS.VACCINEAPOTHECARY.DESC,
                                        STRINGS.BUILDINGS.VACCINEAPOTHECARY.EFFECT);

                BasicModUtils.MakeBuildingStrings(ShieldGeneratorConfig.ID,
                                        STRINGS.BUILDINGS.SHIELDGENERATOR.NAME,
                                        STRINGS.BUILDINGS.SHIELDGENERATOR.DESC,
                                        STRINGS.BUILDINGS.SHIELDGENERATOR.EFFECT);

                BasicModUtils.MakeBuildingStrings(NanobotReplicatorConfig.ID,
                                        STRINGS.BUILDINGS.NANOBOT_REPLICATOR.NAME,
                                        STRINGS.BUILDINGS.NANOBOT_REPLICATOR.DESC,
                                        STRINGS.BUILDINGS.NANOBOT_REPLICATOR.EFFECT);

                BasicModUtils.MakeBuildingStrings(NanobotForgeConfig.ID,
                                        STRINGS.BUILDINGS.NANOBOT_FORGE.NAME,
                                        STRINGS.BUILDINGS.NANOBOT_FORGE.DESC,
                                        STRINGS.BUILDINGS.NANOBOT_FORGE.EFFECT);

                ModUtil.AddBuildingToPlanScreen("Medical", GermcatcherConfig.ID, "wellness");
                ModUtil.AddBuildingToPlanScreen("Medical", VaccineApothecaryConfig.ID, "medical");
                ModUtil.AddBuildingToPlanScreen("Medical", ShieldGeneratorConfig.ID, "wellness");
                ModUtil.AddBuildingToPlanScreen("Medical", NanobotForgeConfig.ID, "wellness");
                ModUtil.AddBuildingToPlanScreen("Medical", NanobotReplicatorConfig.ID, "wellness");
            }
        }

        [HarmonyPatch(typeof(Database.Techs))]
        [HarmonyPatch("Init")]
        public static class Techs_Init_Patch
        {
            public static void Postfix(Database.Techs __instance)
            {
                Tech tech1 = __instance.TryGet("MedicineI");
                if (tech1 != null)
                {
                    tech1.unlockedItemIDs.Add(GermcatcherConfig.ID);

                    if (Settings.Instance.EnableMedicalResearchPoints)
                        tech1.unlockedItemIDs.Add(MedicalResearchDataBank.ID);
                }
                Tech tech2 = __instance.TryGet("MedicineIV");
                if (tech2 != null)
                {
                    tech2.unlockedItemIDs.Add(NanobotForgeConfig.ID);
                    tech2.unlockedItemIDs.Add(NanobotReplicatorConfig.ID);
                    tech2.unlockedItemIDs.Add(VaccineApothecaryConfig.ID);
                    tech2.unlockedItemIDs.Add(ShieldGeneratorConfig.ID);
                }
            }
        }

        [HarmonyPatch(typeof(FlushToiletConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class FlushToiletConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.GetComponent<KPrefabID>().AddTag(GameTags.NotRoomAssignable);
            }
        }

        [HarmonyPatch(typeof(OuthouseConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        public static class OuthouseConfig_ConfigureBuildingTemplate_Patch
        {
            public static void Postfix(GameObject go)
            {
                go.GetComponent<KPrefabID>().AddTag(GameTags.NotRoomAssignable);
            }
        }
    }
}
