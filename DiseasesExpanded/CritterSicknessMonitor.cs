using Klei.AI;

namespace DiseasesExpanded
{
    class CritterSicknessMonitor : KMonoBehaviour, ISim4000ms
    {
        [MyCmpGet]
        Effects effects;

        float lastRollTime = 0;
        const int SPAWNED_GERMS = 1000;
        const float MIN_ROLL_INTERVAL = 600;
        const float INFECTION_CHANCE = 0.25f;

        bool hadSickness = false;

        public void Sim4000ms(float dt)
        {
            if (IsSick())
                SpawnGerms();

            if (hadSickness && !IsSick())
                Recover();
            else if (!IsRecovered() && !IsSick())
                CheckExposure();
        }

        public bool IsSick()
        {
            return effects.HasEffect(HungerSickness.CRITTER_EFFECT_ID);
        }

        public void SpawnGerms()
        {
            SimMessages.ModifyDiseaseOnCell(Grid.PosToCell(this.gameObject), GermIdx.HungerGermsIdx, SPAWNED_GERMS);
        }

        public void CheckExposure()
        {
            if (GameClock.Instance.GetTime() < lastRollTime + MIN_ROLL_INTERVAL)
                return;

            int cell = Grid.PosToCell(this.gameObject);
            int count = Grid.DiseaseIdx[cell] == GermIdx.HungerGermsIdx ? Grid.DiseaseCount[cell] : 0;
            if (count > HungerGerms.GetExposureType().exposure_threshold)
                RollForInfection();
        }

        public void RollForInfection()
        {
            lastRollTime = GameClock.Instance.GetTime();
            if(UnityEngine.Random.Range(0, 1.0f) < INFECTION_CHANCE)
                Infect();
        }

        public void Infect()
        {
            hadSickness = true;
            effects.Add(HungerSickness.CRITTER_EFFECT_ID, true);
            //SourceVisibility(HungerGerms.ID);
            this.gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = HungerGerms.ID;

            PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, STRINGS.DISEASES.HUNGERSICKNESS.NAME, this.gameObject.transform);
        }

        public bool IsRecovered()
        {
            return effects.HasEffect(HungerSickness.RECOVERY_ID);
        }

        public void Recover()
        {
            hadSickness = false;
            SourceVisibility(string.Empty);
            effects.Add(HungerSickness.RECOVERY_ID, true);
        }

        public void SourceVisibility(string germId)
        {
            DiseaseSourceVisualizer source = this.gameObject.AddOrGet<DiseaseSourceVisualizer>();
            source.alwaysShowDisease = germId;
            source.UpdateVisibility();
        }
    }
}
