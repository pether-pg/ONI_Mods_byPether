using System;
using UnityEngine;

namespace DiseasesExpanded.RandomEvents.Events
{
    class AdoptStrayPet : RandomDiseaseEvent
    {
        public AdoptStrayPet(byte germIdx, int weight = 1)
        {
            if (germIdx > Db.Get().Diseases.Count)
                germIdx = 0;

            GeneralName = STRINGS.RANDOM_EVENTS.ADOPT_STRAY_PET.NAME;
            NameDetails = Db.Get().Diseases[germIdx].Id;
            ID = GenerateId(nameof(AdoptStrayPet), NameDetails);
            Group = nameof(AdoptStrayPet);
            AppearanceWeight = weight;
            DangerLevel = Helpers.EstimateGermDanger(germIdx);

            Condition = new Func<object, bool>(data => !string.IsNullOrEmpty(GetPetId(germIdx)) && DlcManager.IsContentSubscribed(DlcManager.EXPANSION1_ID) && GameClock.Instance.GetCycle() > (int)DangerLevel * 100);

            Event = new Action<object>(
                data =>
                {
                    if (Components.Telepads.Count == 0)
                        return;

                    string petId = GetPetId(germIdx);
                    if (string.IsNullOrEmpty(petId))
                        return;

                    GameObject pet = GameUtil.KInstantiate(Assets.GetPrefab(petId), Components.Telepads[0].gameObject.transform.position, Grid.SceneLayer.Creatures);
                    pet.SetActive(true);

                    ONITwitchLib.ToastManager.InstantiateToastWithPosTarget(GeneralName, STRINGS.RANDOM_EVENTS.ADOPT_STRAY_PET.TOAST, pet.transform.position);
                });
        }

        public static string GetPetId(byte germIdx)
        {
            if (germIdx == GermIdx.PollenGermsIdx)
                return Configs.LittleSunshineConfig.ID;
            //if (germIdx == GermIdx.FoodPoisoningIdx)
            //    return HatchVeggieConfig.ID;
            if (germIdx == GermIdx.SlimelungIdx)
                return Configs.FluffyPuffyConfig.ID;
            if (germIdx == GermIdx.ZombieSporesIdx)
                return Configs.PaleSlicksterConfig.ID;
            if (germIdx == GermIdx.RadiationPoisoningIdx)
                return Configs.HarmlessBeeConfig.ID;

            //if (germIdx == GermIdx.FrostShardsIdx)
            //    return LightBugBlueConfig.ID;
            if (germIdx == GermIdx.BogInsectsIdx)
                return Configs.PlagueSlugConfig.ID;
            if (germIdx == GermIdx.AlienGermsIdx)
                return Configs.MorbAlienConfig.ID;
            //if (germIdx == GermIdx.GassyGermsIdx)
            //    return MooConfig.ID;
            //if (germIdx == GermIdx.MedicalNanobotsIdx)
            //    return CrabFreshWaterConfig.ID;

            return string.Empty;
        }
    }
}
