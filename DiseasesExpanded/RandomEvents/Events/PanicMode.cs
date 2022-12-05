using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STRINGS;
using System.Threading.Tasks;

namespace DiseasesExpanded.RandomEvents.Events
{
    class PanicMode : RandomDiseaseEvent
    {
        public PanicMode(int weight = 1)
        {
            ID = nameof(PanicMode);
            GeneralName = "PANIC MODE!!!";
            AppearanceWeight = weight;
            DangerLevel = ONITwitchLib.Danger.Small;

            Condition = new Func<object, bool>(data => true);

            Event = new Action<object>(
                data => 
                {
                    SaveGame.Instance.StartCoroutine(SpamWarnings());
                });
        }

        private IEnumerator SpamWarnings()
        {
            List<Notification> messages = CreateMessages();
            for (int i = 1; i <= 120; i++)
            {
                Notification noti = messages.GetRandom();

                Notifier notifier = SaveGame.Instance.gameObject.AddOrGet<Notifier>();
                if (noti != null && notifier != null)
                    notifier.Add(noti);

                yield return new WaitForSeconds(0.5f * UnityEngine.Random.Range(1.0f, i));
            }
        }

        private List<Notification> CreateMessages()
        {
            List<Notification> all = new List<Notification>();

            for (int i = 0; i < 4; i++)
            {
                all.Add(MutationData.Instance.GetNotification());
                all.Add(new Notification(CREATURES.STATUSITEMS.SCALDING.NOTIFICATION_NAME, NotificationType.DuplicantThreatening));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.SUFFOCATING.NOTIFICATION_NAME, NotificationType.DuplicantThreatening));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.ENTOMBEDCHORE.NOTIFICATION_NAME, NotificationType.DuplicantThreatening));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.INCAPACITATED.NOTIFICATION_NAME, NotificationType.DuplicantThreatening));
            }

            for (int i = 0; i < 2; i++)
            {
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.NERVOUSBREAKDOWN.NOTIFICATION_NAME, NotificationType.Bad));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.NORATIONSAVAILABLE.NOTIFICATION_NAME, NotificationType.Bad));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.STARVING.NOTIFICATION_NAME, NotificationType.Bad));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.SEVEREWOUNDS.NOTIFICATION_NAME, NotificationType.Bad));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.FIGHTING.NOTIFICATION_NAME, NotificationType.Bad));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.FLEEING.NOTIFICATION_NAME, NotificationType.Bad));
                all.Add(new Notification(DUPLICANTS.STATUSITEMS.LASHINGOUT.NOTIFICATION_NAME, NotificationType.Bad));
            }

            all.Add(new Notification(DUPLICANTS.STATUSITEMS.VOMITING.NOTIFICATION_NAME, NotificationType.BadMinor));
            all.Add(new Notification(DUPLICANTS.STATUSITEMS.COUGHING.NAME, NotificationType.BadMinor));
            all.Add(new Notification(DUPLICANTS.STATUSITEMS.LOWOXYGEN.NOTIFICATION_NAME, NotificationType.BadMinor));
            all.Add(new Notification(DUPLICANTS.STATUSITEMS.LOWIMMUNITY.NOTIFICATION_NAME, NotificationType.BadMinor));
            all.Add(new Notification(DUPLICANTS.STATUSITEMS.TOILETUNREACHABLE.NOTIFICATION_NAME, NotificationType.BadMinor));
            all.Add(new Notification(DUPLICANTS.STATUSITEMS.NOTOILETS.NOTIFICATION_NAME, NotificationType.BadMinor));
            all.Add(new Notification(DUPLICANTS.STATUSITEMS.COLD.NAME, NotificationType.BadMinor));
            all.Add(new Notification(DUPLICANTS.STATUSITEMS.HOT.NAME, NotificationType.BadMinor));

            return all;
        }
    }
}
