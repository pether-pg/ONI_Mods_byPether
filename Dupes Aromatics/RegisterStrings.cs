using STRINGS;

namespace Dupes_Aromatics
{
    class RegisterStrings
    {
        public static void MakePlantSpeciesStrings(string id, string name, string desc)
        {
            Strings.Add($"STRINGS.CREATURES.SPECIES.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.CREATURES.SPECIES.{id.ToUpperInvariant()}.DESC", desc);
        }

        public static void MakePlantProductStrings(string id, string name, string desc)
        {
            Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.{id.ToUpperInvariant()}.DESC", desc);
        }

        public static void MakeFoodStrings(string id, string name, string desc)
        {
            Strings.Add($"STRINGS.ITEMS.FOOD.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.ITEMS.FOOD.{id.ToUpperInvariant()}.DESC", desc);
        }

        public static void MakeSeedStrings(string id, string name, string desc)
        {
            Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.CREATURES.SPECIES.SEEDS.{id.ToUpperInvariant()}.DESC", desc);
        }

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
    }
}
