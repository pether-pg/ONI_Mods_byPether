using UnityEngine;
using SidequestMod.Sidequests;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod
{
    class NotificationManager
    {
        public static void NotifyStart(Notifier notifier, Sidequest quest)
        {
            if (notifier == null || quest == null)
                return;

            notifier.Add(new Notification($"Starting quest: {quest.Name} for {quest.RequestingDupe.name}", NotificationType.Good));
            StartingPopUp(quest);
        }

        public static void NotifyCompleted(Notifier notifier, Sidequest quest)
        {
            if (notifier == null || quest == null)
                return;

            notifier.Add(new Notification($"Quest completed: {quest.Name} for {quest.RequestingDupe.name}", NotificationType.Good));
            CompletedPopUp(quest);
        }

        public static void NorifyFailed(Notifier notifier, Sidequest quest)
        {
            if (notifier == null || quest == null)
                return;

            notifier.Add(new Notification($"Quest failed: {quest.Name} for {quest.RequestingDupe.name}", NotificationType.Good));
            FailingPopUp(quest);
        }

        public static void StartingPopUp(Sidequest quest)
        {
            EventInfoData data = new EventInfoData(quest.Name, quest.StartingText, "warpworldreveal_kanim");
            data.whenDescription = "New request:";
            data.location = quest.RequestingDupe.name;
            data.minions = new GameObject[] { quest.RequestingDupe.gameObject };
            EventInfoScreen.ShowPopup(data);
        }

        public static void CompletedPopUp(Sidequest quest)
        {
            EventInfoData data = new EventInfoData(quest.Name, quest.PassingText, "warpworldreveal_kanim");
            data.whenDescription = "Sidequest completed!";
            data.location = quest.RequestingDupe.name;
            data.minions = new GameObject[] { quest.RequestingDupe.gameObject };
            EventInfoScreen.ShowPopup(data);
        }

        public static void FailingPopUp(Sidequest quest)
        {
            EventInfoData data = new EventInfoData(quest.Name, quest.FailingText, "warpworldreveal_kanim");
            data.whenDescription = "Sidequest failed...";
            data.location = quest.RequestingDupe.name;
            data.minions = new GameObject[] { quest.RequestingDupe.gameObject };
            EventInfoScreen.ShowPopup(data);
        }
    }
}
