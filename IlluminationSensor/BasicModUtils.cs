using STRINGS;
using System.Collections.Generic;

namespace IlluminationSensor
{
    public static class BasicModUtils
    {
        public static void MakeStrings(string id, string name, string description, string effect)
        {
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.DESC", description);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.EFFECT", effect);
        }

        public static void AddToTech(string tech, string id)
        {
            // worked on vanilla 445739
            //var techList = new List<string>(Database.Techs.TECH_GROUPING[tech]) { id };
            //Database.Techs.TECH_GROUPING[tech] = techList.ToArray();
        }
    }
}
