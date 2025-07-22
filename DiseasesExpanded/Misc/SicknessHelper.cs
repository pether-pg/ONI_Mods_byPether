using System.Collections.Generic;
using UnityEngine;
using Klei.AI;

namespace DiseasesExpanded
{
    public static class SicknessHelper
    {
        public static void Infect(GameObject duplicant, string sicknessID, string exposureInfo, string message = null)
        {
            Modifiers modifiers = duplicant.GetComponent<Modifiers>();
            if (modifiers == null)
                return;

            Sicknesses diseases = modifiers.GetSicknesses();
            if (diseases == null)
                return;

            diseases.Infect(new SicknessExposureInfo(sicknessID, exposureInfo));
            NotifyOfSickness(duplicant, message);
        }

        public static void NotifyOfSickness(GameObject infestedHost = null, string message = null, NotificationType notiType = NotificationType.Bad)
        {
            Notifier notifier = SaveGame.Instance.gameObject.AddOrGet<Notifier>();
            if (notifier == null)
                return;

            if (string.IsNullOrEmpty(message))
                message = "Duplicant got infected!";

            Notification msg = new Notification(message, notiType, click_focus: infestedHost?.transform);
            notifier.Add(msg);
        }
    }
}
