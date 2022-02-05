using UnityEngine;
using HarmonyLib;
using Database;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Research
    {
        [HarmonyPatch(typeof(ResearchTypes))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] {})]
        public class ResearchTypes_Constructor_Patch
        {   
            public static void Postfix(ResearchTypes __instance)
            {
                string spriteName = Assets_OnPrefabInit_Patch.SpriteName;

                __instance.Types.Add(new ResearchType(
                    MedicalResearchDataBank.MedicalResearchTypeId,
                    STRINGS.MEDICALRESEARCH.NAME,
                    STRINGS.MEDICALRESEARCH.DESC,
                    Assets.GetSprite((HashedString)spriteName),
                    ColorPalette.ToColor(ColorPalette.HospitalPink),
                    null,
                    2400f,
                    (HashedString)"research_center_kanim",
                    new string[1] { ApothecaryConfig.ID },
                    STRINGS.MEDICALRESEARCH.RECIPEDESC
                ));
            }
        }

        [HarmonyPatch(typeof(Techs))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ResourceSet) })]
        public class Techs_Constructor_Patch
        {
            public static int FirstExtraTier = 0;

            public static void Postfix(Techs __instance)
            {
                List<List<Tuple<string, float>>> TECH_TIERS = Traverse.Create(__instance).Field("TECH_TIERS").GetValue<List<List<Tuple<string, float>>>>();
                if (TECH_TIERS == null)
                {
                    Debug.Log("TECH_TIERS is null...");
                    return;
                }
                FirstExtraTier = TECH_TIERS.Count;
                TECH_TIERS.Add(new List<Tuple<string, float>>()
                {
                    new Tuple<string, float>("basic", 20f),
                    new Tuple<string, float>(MedicalResearchDataBank.MedicalResearchTypeId, 20f)
                });
                TECH_TIERS.Add(new List<Tuple<string, float>>()
                {
                    new Tuple<string, float>("basic", 30f),
                    new Tuple<string, float>("advanced", 20f),
                    new Tuple<string, float>(MedicalResearchDataBank.MedicalResearchTypeId, 60f)
                });
                TECH_TIERS.Add(new List<Tuple<string, float>>()
                {
                    new Tuple<string, float>("basic", 35f),
                    new Tuple<string, float>("advanced", 30f),
                    new Tuple<string, float>(MedicalResearchDataBank.MedicalResearchTypeId, 140f)
                });
            }
        }

        [HarmonyPatch(typeof(Assets))]
        [HarmonyPatch("OnPrefabInit")]
        public class Assets_OnPrefabInit_Patch
        {
            public const string SpriteName = "medical_research";
            public static void Postfix()
            {
                string maindir = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string path = Path.Combine(new string[] { maindir, "anim", "assets", "custom_research_icon", "custom_research_icon.png" });
                byte[] data = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(256, 256);
                tex.LoadImage(data);
                Sprite sprite = Sprite.Create(tex, new Rect(0, (256-74), 74, 74), new Vector2(0.5f, 0.5f));
                HashedString key = new HashedString(Assets_OnPrefabInit_Patch.SpriteName);
                Assets.Sprites.Add(key, sprite);
            }
        }

        [HarmonyPatch(typeof(Techs))]
        [HarmonyPatch("GetTier")]
        public class Techs_GetTier_Patch
        {
            public static void Postfix(Techs __instance, Tech tech, ref int __result)
            {
                if (Techs_Constructor_Patch.FirstExtraTier == 0)
                    return;
                if (tech.Id == "MedicineII") // Medicine III and IV will advance as well due to GetTier()'s recursion
                    __result = Techs_Constructor_Patch.FirstExtraTier;
                else if (tech.Id == "RadiationProtection") // Prevent recursion to exceed available tiers
                    __result = 5;
            }
        }

        [HarmonyPatch(typeof(DoctorStation))]
        [HarmonyPatch(nameof(DoctorStation.CompleteDoctoring))]
        [HarmonyPatch(new Type[] { })]
        public class DoctorStation_CompleteDoctoring_Patch
        {
            public static void Prefix(DoctorStation __instance)
            {
                if (__instance.requiredSkillPerk == Db.Get().SkillPerks.CanAdvancedMedicine.Id)
                    MedicalResearchDataBank.GrantResearchPoints(__instance.gameObject, 4);
                else
                    MedicalResearchDataBank.GrantResearchPoints(__instance.gameObject, 2);
            }
        }
        
        [HarmonyPatch(typeof(Clinic.ClinicSM.Instance))]
        [HarmonyPatch(nameof(Clinic.ClinicSM.Instance.StartDoctorChore))]
        public class ClinicSMInstance_StartDoctorChore_Patch
        {
            public static void Postfix(Clinic.ClinicSM.Instance __instance)
            {
                WorkChore<DoctorChoreWorkable> doctorChore = Traverse.Create(__instance).Field("doctorChore").GetValue<WorkChore<DoctorChoreWorkable>>();
                if (doctorChore != null)
                    doctorChore.onComplete = doctorChore.onComplete + (System.Action<Chore>)(chore => MedicalResearchDataBank.GrantResearchPoints(__instance.gameObject, 1));
            }
        }

        [HarmonyPatch(typeof(TechItems))]
        [HarmonyPatch(nameof(TechItems.Init))]
        public class TechItems_Init_Patch
        {
            public static void Postfix(TechItems __instance)
            {
                __instance.AddTechItem(
                    MedicalResearchDataBank.ID,
                    STRINGS.MEDICALRESEARCH.NAME,
                    STRINGS.MEDICALRESEARCH.DESC,
                    (anim, centered) => Assets.GetSprite((HashedString)Assets_OnPrefabInit_Patch.SpriteName),
                    DlcManager.AVAILABLE_ALL_VERSIONS
                );
            }
        }
    }
}
