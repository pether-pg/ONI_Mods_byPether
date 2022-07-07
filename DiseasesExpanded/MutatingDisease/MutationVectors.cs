using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasesExpanded
{
    class MutationVectors
    {
        public enum Vectors
        {
            Att_Stress,
            Att_Damage,
            Att_Calories,
            Att_Breathing,
            Att_Exhaustion,
            Att_Attributes,

            Res_Coughing,
            Res_TemperatureResistance,
            Res_RadiationResistance,
            Res_InfectionExposureThreshold,
            Res_BaseInfectionResistance,
            Res_SicknessDuration
        }

        public static List<Vectors> GetAttackVectors()
        {
            return new List<Vectors>() { 
                Vectors.Att_Stress,
                Vectors.Att_Damage,
                Vectors.Att_Calories,
                Vectors.Att_Breathing,
                Vectors.Att_Exhaustion,
                Vectors.Att_Attributes };
        }

        public static List<Vectors> GetEnvironmentalVectors()
        {
            return new List<Vectors>() { };
        }

        public static List<Vectors> GetResilianceVectors()
        {
            return new List<Vectors>() {
                Vectors.Res_Coughing,
                Vectors.Res_TemperatureResistance,
                Vectors.Res_RadiationResistance,
                Vectors.Res_InfectionExposureThreshold,
                Vectors.Res_BaseInfectionResistance,
                Vectors.Res_SicknessDuration};
        }
    }
}
