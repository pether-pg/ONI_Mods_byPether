using STRINGS;

namespace DiseasesExpanded
{
    public static class BasicModUtils
    {
        public static void MakeGermStrings(string id, string name, string hovertext)
        {
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.LEGEND_HOVERTEXT", hovertext);
        }

        public static void MakeDiseaseStrings(string id, string name, string symptomps, string description, string hover)
        {
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.DESCRIPTIVE_SYMPTOMS", symptomps);
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.DESCRIPTION", description);
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.LEGEND_HOVERTEXT", hover);
        }

        public static void MakeTraitStrings(string id, string name, string desc, string shortdesc, string shortdesc_tooltip)
        {
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.DESC", desc);
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.SHORT_DESC", shortdesc);
            Strings.Add($"STRINGS.DUPLICANTS.TRAITS.{id.ToUpperInvariant()}.SHORT_DESC_TOOLTIP", shortdesc_tooltip);
        }
    }
}
