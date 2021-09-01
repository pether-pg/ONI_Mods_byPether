using HarmonyLib;
using Database;
using System;
using UnityEngine;
using Klei.AI;
using System.Collections.Generic;

namespace DiseasesExpanded
{
    class DiseasesExpanded_Patches_Gas
    {
        public static ExposureType GetExposureType()
        {
            return new ExposureType()
            {
                germ_id = GassyGerms.ID,
                sickness_id = GasSickness.ID,
                exposure_threshold = 1,
                excluded_traits = new List<string>() { "Flatulence" },
                base_resistance = 2,
                excluded_effects = new List<string>()
                    {
                      GasSickness.RECOVERY_ID
                    }
            };
        }
    }
}
