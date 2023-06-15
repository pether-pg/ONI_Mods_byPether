using Klei.AI;
using UnityEngine;

namespace DiseasesExpanded
{
	public class ParticleEffectSickness : Sickness.SicknessComponent
	{
		private readonly GameObject prefab;
		private readonly Vector3 offset;

		public ParticleEffectSickness(GameObject prefab, Vector3 offset)
		{
			this.prefab = prefab;
			this.offset = offset;
		}

		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			if (prefab == null)
			{
				Debug.Log($"{ModInfo.Namespace}: Particle prefab is null");
				return null;
			}

			return ParticleHelper.StartParticleSystem(prefab, go, offset);
		}

		public override void OnCure(GameObject go, object data)
		{
			if (data is ParticleSystem particles)
			{
				ParticleHelper.FadeDownParticles(particles);
			}
		}
	}
}
