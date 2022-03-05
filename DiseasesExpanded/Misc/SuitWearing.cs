using UnityEngine;

namespace DiseasesExpanded
{
    class SuitWearing
    {
        public static bool IsWearingLeadSuit(GameObject go)
        {
            MinionIdentity minion = go.GetComponent<MinionIdentity>();
            if (minion == null)
                return false;

            foreach (AssignableSlotInstance slot in minion.GetEquipment().Slots)
            {
                Equippable assignable = slot.assignable as Equippable;
                if (assignable != null && assignable.GetComponent<KPrefabID>().name == LeadSuitConfig.ID)
                    return true;
            }
            return false;
        }
        public static bool IsWearingAtmoSuit(GameObject go)
        {
            MinionIdentity minion = go.GetComponent<MinionIdentity>();
            if (minion == null)
                return false;

            foreach (AssignableSlotInstance slot in minion.GetEquipment().Slots)
            {
                Equippable assignable = slot.assignable as Equippable;
                if (assignable != null && assignable.GetComponent<KPrefabID>().name == AtmoSuitConfig.ID)
                    return true;
            }
            return false;
        }
    }
}
