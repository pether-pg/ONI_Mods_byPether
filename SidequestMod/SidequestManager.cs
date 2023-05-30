using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SidequestMod.Sidequests;
using UnityEngine;

namespace SidequestMod
{
    class SidequestManager : KMonoBehaviour, ISim4000ms
    {
        private static SidequestManager _instance;
        public static SidequestManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = SaveGame.Instance.GetComponent<SidequestManager>();
                if (_instance == null)
                    _instance = SaveGame.Instance.gameObject.AddComponent<SidequestManager>();
                return _instance;
            }
        }

        public static void Clear()
        {
            _instance = null;
        }

        float TimeWithEmptyQuestSlot = 0;
        public const int MAX_ACTIVE_QUESTS = 3;
        public const int MAX_TIME_WITH_EMPTY_SLOT = 5;

        List<Sidequest> ActiveQuests = new List<Sidequest>();
        static List<Sidequest> AllQuests = new List<Sidequest>();

        public void Sim4000ms(float dt)
        {
            foreach(Sidequest quest in ActiveQuests)
            {
                quest.Update(dt);
                QuestStatus status = quest.QuickStatusCheck();
                if (status != QuestStatus.COMPLETED && quest.RemainingTime <= 0)
                    status = QuestStatus.FAILED;
                HandleStatus(quest, status);
            }

            RemoveFinishedQuests();
            UpdateEmptyTimer(dt);
        }

        public static void RegisterQuest(Sidequest quest)
        {
            if (AllQuests == null)
                AllQuests = new List<Sidequest>();
            if (!AllQuests.Any(q => q.ID == quest.ID))
                AllQuests.Add(quest);
        }

        public void HandleStatus(Sidequest quest, QuestStatus status)
        {
            if (status == QuestStatus.COMPLETED)
            {
                quest.CompleteQuest();
                quest.IsRunning = false;
                NotificationManager.NotifyCompleted(this.gameObject.AddOrGet<Notifier>(), quest);
            }

            if (status == QuestStatus.FAILED)
            {
                quest.FailQuest();
                quest.IsRunning = false;
                NotificationManager.NorifyFailed(this.gameObject.AddOrGet<Notifier>(), quest);
            }
        }

        private void RemoveFinishedQuests()
        {
            for (int i = 0; i < ActiveQuests.Count; i++)
                if (!ActiveQuests[i].IsRunning)
                {
                    Debug.Log($"Removing quest {ActiveQuests[i].Name}");
                    ActiveQuests.RemoveAt(i--);
                }
        }

        public void UpdateEmptyTimer(float dt)
        {
            if (ActiveQuests.Count < MAX_ACTIVE_QUESTS)
                TimeWithEmptyQuestSlot += dt;
            else TimeWithEmptyQuestSlot = 0;

            if (TimeWithEmptyQuestSlot > MAX_TIME_WITH_EMPTY_SLOT)
                StartNewQuest();
        }

        private void StartNewQuest()
        {
            Sidequest newQuest = RollNewQuest();
            if (newQuest == null)
                return;

            MinionIdentity mi = SelectDupeForQuest(newQuest);
            if (mi == null)
                return;

            StartQuest(newQuest, mi);
        }

        private Sidequest RollNewQuest()
        {
            Sidequest rolledQuest = null;
            List<Sidequest> availableQuests = AllQuests.Where(q => q.CanBeStarted()).ToList();
            float weightSum = 0;
            foreach (Sidequest q in availableQuests)
                weightSum += q.QuestWeight;

            float randomRoll = UnityEngine.Random.Range(0, weightSum);
            foreach (Sidequest q in availableQuests)
                if (randomRoll <= q.QuestWeight)
                {
                    rolledQuest = q;
                    break;
                }
                else
                    randomRoll -= q.QuestWeight;

            return rolledQuest;
        }

        private MinionIdentity SelectDupeForQuest(Sidequest quest)
        {
            if (quest == null)
                return null;

            List<MinionIdentity> available = new List<MinionIdentity>();
            foreach (MinionIdentity mi in Components.MinionIdentities)
                if (mi != null && quest.MinionIdentityRequirement(mi))
                    available.Add(mi);

            if (available.Count == 0)
                return null;

            available.Shuffle();
            return available[0];
        }

        private void StartQuest(Sidequest quest, MinionIdentity mi)
        {
            if (quest == null || mi == null)
                return;

            ActiveQuests.Add(quest);
            quest.StartForDuplicant(mi);
            NotificationManager.NotifyStart(this.gameObject.AddOrGet<Notifier>(), quest);
        }
    }
}
