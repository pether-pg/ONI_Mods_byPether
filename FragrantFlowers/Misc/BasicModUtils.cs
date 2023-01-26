using STRINGS;

namespace FragrantFlowers
{
    class BasicModUtils
    {
        public static void MakeBuildingStrings(string id, string name, string description, string effect)
        {
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.DESC", description);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.EFFECT", effect);
        }

        public static void MakeSpiceStrings(string id, string name, string desc)
        {
            Strings.Add($"STRINGS.ITEMS.SPICES.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.ITEMS.SPICES.{id.ToUpperInvariant()}.DESC", desc);
        }
    }
}
