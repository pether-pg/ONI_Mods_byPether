﻿using STRINGS;

namespace SignsTagsAndRibbons
{
    class BasicModUtils
    {

        public static void MakeBuildingStrings(string id, string name, string description, string effect)
        {
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.DESC", description);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.EFFECT", effect);
        }

        public static void MakeBuildMenuSubcatagory(string id, string name)
        {
            Strings.Add($"STRINGS.UI.NEWBUILDCATEGORIES." + id.ToUpper() + ".BUILDMENUTITLE", name);
        }

        public static void MakeSideScreenStrings(string key, string name)
        {
            Strings.Add(key, name);
        }
    }
}
