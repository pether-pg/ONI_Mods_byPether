using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidequestMod
{
    class Requirements
    {
        public static bool HasInterest(MinionIdentity mi, string interestSkillId)
        {
            MinionResume mr = mi.GetComponent<MinionResume>();
            if (mr == null)
                return false;

            bool result = mr.HasSkillAptitude(Db.Get().Skills.Get(interestSkillId));
            Debug.Log($"HasInterest( {mi.name}, {interestSkillId} ) = {result}");
            return result;
        }
    }
}
