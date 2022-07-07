using HarmonyLib;
using UnityEngine;
using KSerialization;
using System;
using System.Collections.Generic;
using Klei.AI;

namespace DiseasesExpanded
{
    [SerializationConfig(MemberSerialization.OptIn)]
    class MutationData: KMonoBehaviour, ISaveLoadable
    {
        private static MutationData _instance = null;
        private static bool _isReady = false;

        public static MutationData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = SaveGame.Instance.GetComponent<MutationData>();
                return _instance;
            }
        }

        [Serialize]
        private Dictionary<MutationVectors.Vectors, int> MutationLevels = new Dictionary<MutationVectors.Vectors, int>();

        [Serialize]
        private Dictionary<MutationVectors.Vectors, int> MutationReinforcement = new Dictionary<MutationVectors.Vectors, int>();

        [Serialize]
        private float nextMutationProgress = 0;

        [Serialize]
        private int lastMutationCycle = 0;

        private const int maxMutationLevel = 15;

        // called in Game_DestroyInstances_Patch
        public static void Clear()
        {
            _instance = null;
            Debug.Log($"{ModInfo.Namespace}: MutationData._instance cleared.");
        }

        public static bool IsReadyToUse()
        {
            return _isReady;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            _isReady = true;

            if (Settings.Instance.ClearVirusMutationsOnLoad)
                MutationLevels = new Dictionary<MutationVectors.Vectors, int>();

            Debug.Log($"{ModInfo.Namespace}: MutationData Spawned. Current mutation: {GetMutationsCode()}");
            Instance.UpdateAll();
        }

        public int GetMutationLevel(MutationVectors.Vectors mutation)
        {
            if (!MutationLevels.ContainsKey(mutation))
                MutationLevels.Add(mutation, 0);
            return MutationLevels[mutation];
        }

        public void BulkModifyMutation(List<MutationVectors.Vectors> vectors, int amount)
        {
            foreach(MutationVectors.Vectors v in vectors)
                if(MutationLevels.ContainsKey(v))
                {
                    MutationLevels[v] += amount;
                    if (MutationLevels[v] < 0) MutationLevels[v] = 0;
                    if (MutationLevels[v] > maxMutationLevel) MutationLevels[v] = maxMutationLevel;
                }
        }

        public string GetMutationsCode()
        {
            string pattern = STRINGS.GERMS.MUTATINGGERMS.STRAIN_PATTERN;

            string attackCode = string.Format("{0:x}{1:x}{2:x}-{3:x}{4:x}{5:x}", 
                GetMutationLevel(MutationVectors.Vectors.Att_Attributes),
                GetMutationLevel(MutationVectors.Vectors.Att_Breathing),
                GetMutationLevel(MutationVectors.Vectors.Att_Calories),
                GetMutationLevel(MutationVectors.Vectors.Att_Damage),
                GetMutationLevel(MutationVectors.Vectors.Att_Exhaustion),
                GetMutationLevel(MutationVectors.Vectors.Att_Stress)
                );
            string resCode = string.Format("{0:x}{1:x}{2:x}-{3:x}{4:x}{5:x}",
                GetMutationLevel(MutationVectors.Vectors.Res_BaseInfectionResistance),
                GetMutationLevel(MutationVectors.Vectors.Res_SicknessDuration),
                GetMutationLevel(MutationVectors.Vectors.Res_Coughing),
                GetMutationLevel(MutationVectors.Vectors.Res_InfectionExposureThreshold),
                GetMutationLevel(MutationVectors.Vectors.Res_RadiationResistance),
                GetMutationLevel(MutationVectors.Vectors.Res_TemperatureResistance)
                );

            return pattern.Replace("{ATTACK}", attackCode)
                            .Replace("{RESILIANCE}", resCode);
        }

        public string GetMutationsCodeLegend()
        {
            string attackLegend = string.Format(STRINGS.GERMS.MUTATINGGERMS.STRAIN_SEVERITY_PATTERN,
                GetMutationLevel(MutationVectors.Vectors.Att_Attributes),
                GetMutationLevel(MutationVectors.Vectors.Att_Breathing),
                GetMutationLevel(MutationVectors.Vectors.Att_Calories),
                GetMutationLevel(MutationVectors.Vectors.Att_Damage),
                GetMutationLevel(MutationVectors.Vectors.Att_Exhaustion),
                GetMutationLevel(MutationVectors.Vectors.Att_Stress)
                );
            string resistLegend = string.Format(STRINGS.GERMS.MUTATINGGERMS.STRAIN_RESILIANCE_PATTERN,
                GetMutationLevel(MutationVectors.Vectors.Res_BaseInfectionResistance),
                GetMutationLevel(MutationVectors.Vectors.Res_SicknessDuration),
                GetMutationLevel(MutationVectors.Vectors.Res_Coughing),
                GetMutationLevel(MutationVectors.Vectors.Res_InfectionExposureThreshold),
                GetMutationLevel(MutationVectors.Vectors.Res_RadiationResistance),
                GetMutationLevel(MutationVectors.Vectors.Res_TemperatureResistance)
                );

            string help = string.Format(STRINGS.GERMS.MUTATINGGERMS.MUTATION_HELP_PATTERN, STRINGS.BUILDINGS.VACCINEAPOTHECARY.NAME);
            int threatLvlPercent = (int)(100 * GetCompletionPercent());
            string threat = string.Format(STRINGS.GERMS.MUTATINGGERMS.TREAT_POTENTIAL_PATTERN, threatLvlPercent);
            string legend = $"{attackLegend}\n{resistLegend}\n\n{threat}\n{help}";

            return legend;
        }

        public int GetTotalLevel()
        {
            int sum = 0;
            foreach (MutationVectors.Vectors v in MutationLevels.Keys)
                sum += MutationLevels[v];
            return sum;
        }

        public int GetMaxTotalLevel()
        {
            int mutationCount = MutationVectors.GetAttackVectors().Count + MutationVectors.GetResilianceVectors().Count;
            return mutationCount * maxMutationLevel;
        }

        public float GetCompletionPercent()
        {
            return 1.0f * GetTotalLevel() / GetMaxTotalLevel();
        }

        public float GetExpectedMutationPeriod()
        {
            return 1.0f * Settings.Instance.UnstableVirusFinalMutationCycleEstimation / GetMaxTotalLevel();
        }

        public void IncreaseMutationProgress(GameObject infestedHost)
        {
            float radiation = 2;
            RadiationMonitor.Instance smi = infestedHost.GetSMI<RadiationMonitor.Instance>();
            if (smi != null && DlcManager.IsExpansion1Active())
                radiation = smi.sm.radiationExposure.Get(smi) / 100;

            float acc = GetAccelerationParameter();
            float mutationDelta = (1 + radiation) * acc;
            nextMutationProgress += mutationDelta;

            Debug.Log($"{ModInfo.Namespace}: Germ mutation progress increased by: {mutationDelta:F2} " +
                $"(includes rad factor = {radiation:F2} and acc = {acc:F2}). " +
                $"Current progress: {nextMutationProgress:F2} / 100.00");

            if (nextMutationProgress >= 100 && CanMutate())
            {
                Mutate();
                TryInfect(infestedHost);
            }
        }

        public void TryInfect(GameObject duplicant)
        {
            if (IsRecentlyRecovered(duplicant))
                return;

            Modifiers modifiers = duplicant.GetComponent<Modifiers>();
            if (modifiers == null)
                return;

            Sicknesses diseases = modifiers.GetSicknesses();
            if (diseases == null)
                return;

            diseases.Infect(new SicknessExposureInfo(MutatingSickness.ID, STRINGS.DISEASES.MUTATINGSICKNESS.EXPOSURE_INFO));
        }

        private bool IsRecentlyRecovered(GameObject duplicant)
        {
            if (duplicant == null)
                return false;

            Effects effects = duplicant.GetComponent<Effects>();
            return (effects != null && effects.HasEffect(MutatingSickness.RECOVERY_ID));
        }

        public float GetAccelerationParameter()
        {
            float mutationProgress = GetCompletionPercent();
            float currentMutationCycleExpectancy = mutationProgress * Settings.Instance.UnstableVirusFinalMutationCycleEstimation;

            float delta = GameClock.Instance.GetCycle() - currentMutationCycleExpectancy;
            float expectedMutationTime = GetExpectedMutationPeriod();
            float relativeDelta = delta / expectedMutationTime;

            float acceleration = relativeDelta < 0 ? -1.0f / relativeDelta : relativeDelta;
            return acceleration;
        }

        public Color32 GetGermColor()
        {
            float ratio = GetCompletionPercent();
            return Color32.Lerp(MutatingGerms.colorValue, ColorPalette.BloodyRed, ratio);
        }

        public void UpdateColor()
        {
            Dictionary<string, Color32> namedLookup = Traverse.Create(GlobalAssets.Instance.colorSet).Field("namedLookup").GetValue<Dictionary<string, Color32>>();
            Color32 color = GetGermColor();
            if (namedLookup != null && namedLookup.ContainsKey(MutatingGerms.ID))
            {
                namedLookup[MutatingGerms.ID] = color;
                //Debug.Log($"{ModInfo.Namespace}: New germ color: R = {color.r}, G = {color.g}, B = {color.b}");
            }
            else
                Debug.Log($"{ModInfo.Namespace}: Unable to update germ color...");
        }

        public void UpdateGerms()
        {
            if (!Db.Get().Diseases.Exists(MutatingGerms.ID))
                return;

            Disease dis = Db.Get().Diseases.Get((HashedString)MutatingGerms.ID);
            if (dis == null)
                return;

            ((MutatingGerms)dis).UpdateGermData();
        }

        public void UpdateExposureTable()
        {
            for(int i =0 ; i<TUNING.GERM_EXPOSURE.TYPES.Length; i++)
                if(TUNING.GERM_EXPOSURE.TYPES[i].germ_id == MutatingGerms.ID)
                {
                    TUNING.GERM_EXPOSURE.TYPES[i] = MutatingGerms.GetExposureType(
                        exposureThresholdLevel: GetMutationLevel(MutationVectors.Vectors.Res_InfectionExposureThreshold),
                        resistanceLevel: GetMutationLevel(MutationVectors.Vectors.Res_BaseInfectionResistance)
                        );
                }
        }

        public void UpdateSickness()
        {
            Sickness sick = Db.Get().Sicknesses.Get((HashedString)MutatingSickness.ID);
            if (sick == null)
                return;

            int durLvl = GetMutationLevel(MutationVectors.Vectors.Res_SicknessDuration);
            if (durLvl <= 0)
                durLvl = 0;

            float sicknessTime = 300 * (durLvl + 1);
            if (durLvl > 10)
                sicknessTime += 300 * (durLvl - 10);

            Traverse.Create(sick).Field("sicknessDuration").SetValue(sicknessTime);
        }

        public string GetLegendString()
        {
            string baseStr = STRINGS.GERMS.MUTATINGGERMS.LEGEND_HOVERTEXT.Replace("\n", "");
            string hover = $"{baseStr} ({GetMutationsCode()}) \n\nGenoms' values:\n{GetMutationsCodeLegend()}" ;
            return hover;
        }

        public void UpdateAll()
        {
            if (!_isReady)
                return;

            UpdateColor();
            UpdateGerms();
            UpdateExposureTable();
            UpdateSickness();
        }

        private bool CanMutate()
        {
            if (GetTotalLevel() >= GetMaxTotalLevel())
                return false;

            if (GameClock.Instance.GetCycle() < lastMutationCycle + Settings.Instance.UnstableVirusMinimalMutationInterval)
                return false;

            return true;
        }

        public void Mutate()
        {
            MutateAttack();
            MutateResiliance();

            UpdateAll();

            Notify();

            lastMutationCycle = GameClock.Instance.GetCycle() + 1;
            nextMutationProgress = 0;
        }

        public void CompleteMutation()
        {
            while (GetTotalLevel() < GetMaxTotalLevel())
                Mutate();
        }

        private void MutateOneOfVectors(List<MutationVectors.Vectors> vectors)
        {
            bool mutated = false;
            while(!mutated)
            {
                if (vectors == null || vectors.Count == 0)
                    break;

                int vectorIndex = UnityEngine.Random.Range(0, vectors.Count);
                MutationVectors.Vectors mutationVector = vectors[vectorIndex];
                if (!MutationLevels.ContainsKey(mutationVector))
                    MutationLevels.Add(mutationVector, 0);
                
                if(MutationLevels[mutationVector] >= maxMutationLevel)
                {
                    vectors.RemoveAt(vectorIndex);
                    continue;
                }

                MutationLevels[mutationVector] += 1;
                mutated = true;
            }
        }

        private void MutateAttack()
        {
            MutateOneOfVectors(MutationVectors.GetAttackVectors());
        }

        private void MutateEnvironmental()
        {
            MutateOneOfVectors(MutationVectors.GetEnvironmentalVectors());
        }

        private void MutateResiliance()
        {
            MutateOneOfVectors(MutationVectors.GetResilianceVectors());
        }

        private void Notify()
        {
            Debug.Log($"{ModInfo.Namespace}: {MutatingGerms.ID} mutated at cycle {lastMutationCycle}. Now it is {GetMutationsCode()} (level {GetTotalLevel()}/{GetMaxTotalLevel()})");
            
            NotificationType notiType = NotificationType.Event;
            float progress = 1.0f * GetTotalLevel() / GetMaxTotalLevel();            
            if (progress > 0.3f)
                notiType = NotificationType.Bad;
            if (progress > 0.6f)
                notiType = NotificationType.DuplicantThreatening;

            Notifier notifier = this.gameObject.AddOrGet<Notifier>();
            if (notifier != null)
                notifier.Add(new Notification(string.Format(STRINGS.NOTIFICATIONS.VIRUSMUTATED.PATTERN, GetMutationsCode()), notiType));
        }
    }
}
