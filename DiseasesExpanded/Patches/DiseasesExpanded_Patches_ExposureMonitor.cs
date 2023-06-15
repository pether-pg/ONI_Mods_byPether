using HarmonyLib;
using UnityEngine;
using Database;
using System;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_ExposureMonitor
    {
        const string INVALID_STATUS_ITEM_NAME = "INVALID_STATUS_ITEM";

        [HarmonyPatch(typeof(DuplicantStatusItems))]
        [HarmonyPatch("CreateStatusItems")]
        public static class DuplicantStatusItems_CreateStatusItems_Patch
        {
            public static void Postfix(DuplicantStatusItems __instance)
            {
                Func<string, object, string> originalExposed = __instance.ExposedToGerms.resolveStringCallback;
                __instance.ExposedToGerms.resolveStringCallback = (Func<string, object, string>)((str, data) =>
                {
                    GermExposureMonitor.ExposureStatusData exposureStatusData = (GermExposureMonitor.ExposureStatusData)data;
                    if (!Db.Get().Sicknesses.Exists(exposureStatusData.exposure_type.sickness_id))
                        return INVALID_STATUS_ITEM_NAME;
                    return originalExposed(str, data);
                });

                Func<string, object, string> originalContact = __instance.ContactWithGerms.resolveStringCallback;
                __instance.ContactWithGerms.resolveStringCallback = (Func<string, object, string>)((str, data) =>
                {
                    GermExposureMonitor.ExposureStatusData exposureStatusData = (GermExposureMonitor.ExposureStatusData)data;
                    if (!Db.Get().Sicknesses.Exists(exposureStatusData.exposure_type.sickness_id))
                        return INVALID_STATUS_ITEM_NAME;
                    return originalContact(str, data);
                });
            }
        }
        
        [HarmonyPatch(typeof(GermExposureMonitor.Instance))]
        [HarmonyPatch("RefreshStatusItems")]
        public static class GermExposureMonitorInstance_RefreshStatusItems_Patch
        {
            public static void Postfix(GermExposureMonitor.Instance __instance)
            {
                KSelectable selectable = __instance.gameObject.GetComponent<KSelectable>();
                if (selectable == null)
                    return;

                StatusItemGroup statusItemGroup = selectable.GetStatusItemGroup();
                if (statusItemGroup == null)
                    return;

                List<StatusItemGroup.Entry> toRemove = new List<StatusItemGroup.Entry>();

                foreach (StatusItemGroup.Entry entry in statusItemGroup)
                    if (entry.GetName() == INVALID_STATUS_ITEM_NAME)
                        toRemove.Add(entry);

                foreach (StatusItemGroup.Entry entry in toRemove)
                    selectable.RemoveStatusItem(entry.id);
            }
        }
    }
}
