using STRINGS;

namespace DiseasesExpanded
{
    public static class BasicModUtils
    {
        public static void MakeStrings(string id, string name, string symptomps, string description, string hover)
        {
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.DESCRIPTIVE_SYMPTOMS", symptomps);
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.DESCRIPTION", description);
            Strings.Add($"STRINGS.DUPLICANTS.DISEASES.{id.ToUpperInvariant()}.LEGEND_HOVERTEXT", hover);
        }
    }
}
