using HarmonyLib;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Effects
    {
        [HarmonyPatch(typeof(Effects))]
        [HarmonyPatch("OnSpawn")]
        public class Effects_OnSpawn_Patch
        {
            public static void Postfix(Effects __instance)
            {
                __instance.Subscribe((int)GameHashes.EffectAdded, new System.Action<object>(effect => OnEffectAdded(effect, __instance.gameObject)));
                __instance.Subscribe((int)GameHashes.EffectRemoved, new System.Action<object>(effect => OnEffectRemoved(effect, __instance.gameObject)));
            }

            public static Dictionary<GameObject, ParticleSystem> ActiveParticles = new Dictionary<GameObject, ParticleSystem>();

            public static void OnEffectAdded(object e, GameObject go)
            {
                if (!(e is Effect effect))
                    return;

                if (effect.Id == MedicalNanobots.EFFECT_ID) 
                    ApplyNanobotParticles(go);
            }

            public static void OnEffectRemoved(object e, GameObject go)
            {
                if (!(e is Effect effect))
                    return;

                if (effect.Id == MedicalNanobots.EFFECT_ID)
                    RemoveNanobotParticles(go);
            }

            public static void ApplyNanobotParticles(GameObject go)
            {
                Vector3 offset = new Vector3(0.0f, 1.0f, -0.0f);
                GameObject prefab = AssetLoader.NanobotFxPrefab;
                ParticleSystem nanobotParticles = ParticleHelper.StartParticleSystem(prefab, go, offset);

                if (!ActiveParticles.ContainsKey(go))
                    ActiveParticles.Add(go, null);
                ActiveParticles[go] = nanobotParticles;
            }

            public static void RemoveNanobotParticles(GameObject go)
            {
                if (!ActiveParticles.ContainsKey(go) || ActiveParticles[go] == null)
                    return;

                ParticleSystem particles = ActiveParticles[go];
                ActiveParticles.Remove(go);
                ParticleHelper.FadeDownParticles(particles);
            }
        }
    }
}
