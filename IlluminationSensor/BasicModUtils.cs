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

        public static void MakeSkinCategoryStrings(string id, string name)
        {
            Strings.Add($"STRINGS.UI.KLEI_INVENTORY_SCREEN.SUBCATEGORIES.{id.ToUpperInvariant()}", name);
        }
    }
}
