using STRINGS;

namespace DiseasesExpanded
{
    public static class BasicModUtils
    {
        public static void MakeGermStrings(string id, string name, string hovertext, string desc)
        {
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.LEGEND_HOVERTEXT", hovertext);
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.DESC", desc);
        }

        public static void MakeDiseaseStrings(string id, string name, string symptomps, string hover)
        {
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.DESCRIPTIVE_SYMPTOMS", symptomps);
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.LEGEND_HOVERTEXT", hover);
        }

        public static void MakeTraitStrings(string id, string name, string desc, string shortdesc, string shortdesc_tooltip)
        {
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.DESC", desc);
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.SHORT_DESC", shortdesc);
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.SHORT_DESC_TOOLTIP", shortdesc_tooltip);
        }

        public static void MakeBuildingStrings(string id, string name, string description, string effect)
        {
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.DESC", description);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.EFFECT", effect);
        }

        public static void MakeStatusItemStrings(string id, string name, string tooltip)
        {
            Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.BUILDINGS.STATUSITEMS.{id.ToUpperInvariant()}.TOOLTIP", tooltip);
        }

        public static void MakeTagCategoryStrings(Tag tag, string category)
        {
            Strings.Add($"MISC.TAGS.{tag.ToString().ToUpperInvariant()}", category);
        }

        public static void MakeCuresStrings(string id, string name, string rectext, string desc)
        {
            Strings.Add($"STRINGS.ITEMS.PILLS.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.ITEMS.PILLS.{id.ToUpperInvariant()}.RECIPEDESC", rectext);
            Strings.Add($"STRINGS.ITEMS.PILLS.{id.ToUpperInvariant()}.DESC", desc);
        }

        public static void MakeCodexCategoryString(string category, string text)
        {
            Strings.Add($"STRINGS.UI.CODEX.CATEGORYNAMES.{category}", text);
        }
    }
}
