using UnityEngine;

namespace DiseasesExpanded
{
    class ParticleHelper
    {
        public static ParticleSystem StartParticleSystem(GameObject particlesPrefab, GameObject parent, Vector3 offset)
        {
            var effect = Object.Instantiate(particlesPrefab);
            effect.transform.position = parent.transform.position + offset;
            effect.transform.SetParent(parent.transform);
            effect.SetActive(true);

            effect.TryGetComponent(out ParticleSystem particleSystem);
            particleSystem.Play();

            return particleSystem;
        }

        public static void FadeDownParticles(ParticleSystem particleSystem)
        {
            // this allows the last particles to gracefully disappear
            var emission = particleSystem.emission;
            emission.rateOverTime = 0;

            GameScheduler.Instance.Schedule("remove particles", 5f, _ => particleSystem.Stop());
        }
    }
}
