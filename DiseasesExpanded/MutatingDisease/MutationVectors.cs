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
            Att_Stamina,
            Att_Attributes,

            Res_Replication,
            Res_TemperatureResistance,
            Res_RadiationResistance,
            Res_ExposureThreshold,
            Res_BaseInfectionResistance,
            Res_EffectDuration
        }

        public static List<Vectors> GetAttackVectors()
        {
            return new List<Vectors>() { 
                Vectors.Att_Stress,
                Vectors.Att_Damage,
                Vectors.Att_Calories,
                Vectors.Att_Breathing,
                Vectors.Att_Stamina,
                Vectors.Att_Attributes };
        }

        public static List<Vectors> GetEnvironmentalVectors()
        {
            return new List<Vectors>() { };
        }

        public static List<Vectors> GetResilianceVectors()
        {
            return new List<Vectors>() {
                Vectors.Res_Replication,
                Vectors.Res_TemperatureResistance,
                Vectors.Res_RadiationResistance,
                Vectors.Res_ExposureThreshold,
                Vectors.Res_BaseInfectionResistance,
                Vectors.Res_EffectDuration};
        }
    }
}
