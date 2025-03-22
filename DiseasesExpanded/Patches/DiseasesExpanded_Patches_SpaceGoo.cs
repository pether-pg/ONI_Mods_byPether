using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_SpaceGoo
    {
        public static void EnhanceCometWithGerms(GameObject go, byte idx = byte.MaxValue, int impactCount = 1000000)
        {
            if (idx == byte.MaxValue)
                idx = Db.Get().Diseases.GetIndex((HashedString)AlienGerms.ID);

            Comet comet = go.GetComponent<Comet>();
            if (comet != null)
            {
                comet.diseaseIdx = idx;
                comet.addDiseaseCount = 1000000;
                comet.OnImpact += () => {
                    SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(comet.gameObject.transform.position), idx, impactCount);
                };
            }

            PrimaryElement element = go.GetComponent<PrimaryElement>();
            if (element != null)
                element.AddDisease(idx, 100000, "Space Origin");
        }

        /*[HarmonyPatch(typeof(GermExposureMonitor.Instance))]
        [HarmonyPatch("InfectImmediately")]
        public class GermExposureMonitorInstance_InfectImmediately_Patch
        {
            public static bool Prefix(GermExposureMonitor.Instance __instance, ExposureType exposure_type)
            {
                if (exposure_type.sickness_id != AlienSickness.ID)
                    return true;
                if (SuitWearing.IsWearingAtmoSuit(__instance.gameObject) || SuitWearing.IsWearingLeadSuit(__instance.gameObject))
                    return false;
                return true;
            }
        }*/

        [HarmonyPatch(typeof(RockCometConfig))]
        [HarmonyPatch("OnPrefabInit")]
        public class RockCometConfig_OnPrefabInit_Patch
        {
            public static void Postfix(GameObject go)
            {
                if(Settings.Instance.AlienGoo.IncludeDisease)
                    EnhanceCometWithGerms(go);
            }
        }

        [HarmonyPatch(typeof(IronCometConfig))]
        [HarmonyPatch("OnPrefabInit")]
        public class IronCometConfig_OnPrefabInit_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.AlienGoo.IncludeDisease)
                    EnhanceCometWithGerms(go);
            }
        }

        [HarmonyPatch(typeof(CopperCometConfig))]
        [HarmonyPatch("OnPrefabInit")]
        public class CopperCometConfig_OnPrefabInit_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.AlienGoo.IncludeDisease)
                    EnhanceCometWithGerms(go);
            }
        }

        [HarmonyPatch(typeof(GoldCometConfig))]
        [HarmonyPatch("OnPrefabInit")]
        public class GoldCometConfig_OnPrefabInit_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.AlienGoo.IncludeDisease)
                    EnhanceCometWithGerms(go);
            }
        }

        [HarmonyPatch(typeof(FullereneCometConfig))]
        [HarmonyPatch("OnPrefabInit")]
        public class FullereneCometConfig_OnPrefabInit_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.AlienGoo.IncludeDisease)
                    EnhanceCometWithGerms(go);
            }
        }

        [HarmonyPatch(typeof(DustCometConfig))]
        [HarmonyPatch("OnPrefabInit")]
        public class DustCometConfig_OnPrefabInit_Patch
        {
            public static void Postfix(GameObject go)
            {
                if (Settings.Instance.AlienGoo.IncludeDisease)
                    EnhanceCometWithGerms(go);
            }
        }

        [HarmonyPatch(typeof(TimeOfDay))]
        [HarmonyPatch("UpdateSunlightIntensity")]
        public class TimeOfDay_UpdateSunlightIntensity_Patch
        {
            public static void Postfix()
            {
                ShieldData.Instance.UpdateShieldStaus();
                if (ClusterManager.Instance != null && ClusterManager.Instance.WorldContainers != null)
                    foreach (WorldContainer world in ClusterManager.Instance.WorldContainers)
                        world.currentCosmicIntensity = world.cosmicRadiation * ShieldData.Instance.GetRadiationScale(world.id);
            }
        }
    }
}
