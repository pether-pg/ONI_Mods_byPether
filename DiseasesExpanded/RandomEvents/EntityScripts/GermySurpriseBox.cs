using System.Collections;
using KSerialization;
using ONITwitchLib;
using UnityEngine;
using System.Collections.Generic;

namespace DiseasesExpanded.RandomEvents.EntityScripts
{
	// Base on https://github.com/asquared31415/ONITwitch/blob/main/ONITwitchCore/Cmps/SurpriseBox.cs
	class GermySurpriseBox : KMonoBehaviour, ISidescreenButtonControl
	{
		public byte DiseaseIdx = byte.MaxValue;

		public string SidescreenButtonText => TryGetText();

		public string SidescreenButtonTooltip => TryGetTooltip();

		private string TryGetText()
		{
			StringEntry OniTwitchName;
			if (Strings.TryGet("STRINGS.ONITWITCH.UI.SURPRISE_BOX_SIDE_SCREEN.NAME", out OniTwitchName))
				return OniTwitchName.String;
			return "Open";
		}

		private string TryGetTooltip()
		{
			StringEntry OniTwitchName;
			if (Strings.TryGet("STRINGS.ONITWITCH.UI.SURPRISE_BOX_SIDE_SCREEN.TOOLTIP", out OniTwitchName))
				return OniTwitchName.String;
			return "Open this box to receive your surprise!";
		}

		public int HorizontalGroupID()
		{
			return -1;
		}

		public int ButtonSideScreenSortOrder() => 0;

		[Serialize] 
		private bool started;

		protected override void OnSpawn()
		{
			base.OnSpawn();
			if (started)
			{
				StartCoroutine(SpawnGifts());
			}
		}

		public void OnSidescreenButtonPressed()
		{
			StartCoroutine(SpawnGifts());
			started = true;
		}

		private GameObject GetGermyObject()
        {
			List<string> PossibleIDs = new List<string>();

			if (DiseaseIdx == GermIdx.AlienGermsIdx)
				PossibleIDs.Add(IronCometConfig.ID);

			if(DiseaseIdx == GermIdx.BogInsectsIdx)
				PossibleIDs.Add(SwampHarvestPlantConfig.SEED_ID);

			if (DiseaseIdx == GermIdx.FoodPoisoningIdx)
				PossibleIDs.Add(BasicPlantFoodConfig.ID);

			if (DiseaseIdx == GermIdx.FrostShardsIdx)
				PossibleIDs.Add(ColdBreatherConfig.SEED_ID);

			if (DiseaseIdx == GermIdx.GassyGermsIdx)
				PossibleIDs.Add(GasGrassConfig.SEED_ID);

			if (DiseaseIdx == GermIdx.HungerGermsIdx)
				PossibleIDs.Add("CritterTrapPlantSeed");

			if (DiseaseIdx == GermIdx.MedicalNanobotsIdx)
            {
				PossibleIDs.Add(NanobotUpgrade_AntiExhaustionConfig.ID);
				PossibleIDs.Add(NanobotUpgrade_AttributeBoostConfig.ID);
				PossibleIDs.Add(NanobotUpgrade_BreathingConfig.ID);
				PossibleIDs.Add(NanobotUpgrade_HealthRegenConfig.ID);
				PossibleIDs.Add(NanobotUpgrade_MetabolismBoostConfig.ID);
            }

			if (DiseaseIdx == GermIdx.MutatingGermsIdx)
				; // Maybe think of something later...

			if (DiseaseIdx == GermIdx.PollenGermsIdx)
				PossibleIDs.Add(BulbPlantConfig.SEED_ID);

			if (DiseaseIdx == GermIdx.RadiationPoisoningIdx)
				PossibleIDs.Add(SatelliteCometConfig.ID);

			if (DiseaseIdx == GermIdx.SlimelungIdx)
				PossibleIDs.Add(PuftConfig.EGG_ID);

			if (DiseaseIdx == GermIdx.ZombieSporesIdx)
				PossibleIDs.Add(EvilFlowerConfig.SEED_ID);

			if(PossibleIDs.Count == 0)
				return null;

			string id = PossibleIDs.GetRandom();
			return Assets.GetPrefab(id);
		}

		private void SpawnGerms(int cell)
		{
			if (DiseaseIdx == byte.MaxValue)
				return;
			SimMessages.ModifyDiseaseOnCell(cell, DiseaseIdx, 1000000);
			SimMessages.ModifyDiseaseOnCell(Grid.CellLeft(cell), DiseaseIdx, 1000000);
			SimMessages.ModifyDiseaseOnCell(Grid.CellRight(cell), DiseaseIdx, 1000000);
			SimMessages.ModifyDiseaseOnCell(Grid.CellAbove(cell), DiseaseIdx, 1000000);
			SimMessages.ModifyDiseaseOnCell(Grid.CellUpLeft(cell), DiseaseIdx, 1000000);
			SimMessages.ModifyDiseaseOnCell(Grid.CellUpRight(cell), DiseaseIdx, 1000000);
		}

		private IEnumerator SpawnGifts()
		{
			GetComponent<KBatchedAnimController>().Play("open");
			var spawnCount = Random.Range(3, 6);
			for (var idx = 0; idx < spawnCount; idx++)
			{
				if (DiseaseIdx == byte.MaxValue)
					break;

				SpawnGerms(Grid.PosToCell(this.gameObject));

				 GameObject prefab = GetGermyObject();
				if (prefab == null)
					continue;

				var go = Util.KInstantiate(prefab, transform.position);
				go.SetActive(true);

				if (go.TryGetComponent(out ElementChunk _) && go.TryGetComponent(out PrimaryElement primaryElement))
				{
					primaryElement.Mass = 50f;
				}

				// make it fly a little bit
				var velocity = Random.Range(1, 5) * Random.insideUnitCircle.normalized;
				velocity.y = Mathf.Abs(velocity.y);
				// whether to restore the faller after 
				var hadFaller = false;
				if (GameComps.Fallers.Has(go))
				{
					hadFaller = true;
					GameComps.Fallers.Remove(go);
				}

				GameComps.Fallers.Add(go, velocity);

				GameScheduler.Instance.Schedule(
					"TwitchRemoveSurpriseBoxFaller",
					15f,
					_ =>
					{
						if (go != null)
						{
						// only clear fallers for things that didnt have it before
						if (!hadFaller && GameComps.Fallers.Has(go))
							{
								GameComps.Fallers.Remove(go);
							}

						// trigger cell changes
						go.transform.SetPosition(go.transform.position);
						}
					}
				);

				yield return new WaitForSeconds(Random.Range(0.75f, 2.0f));
			}

			Destroy(gameObject);
		}

		public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
		{
		}

		public bool SidescreenButtonInteractable() => !started;

		public bool SidescreenEnabled() => !started;
	}
}
