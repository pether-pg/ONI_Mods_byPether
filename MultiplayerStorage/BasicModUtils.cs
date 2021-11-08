using STRINGS;
using System.Collections.Generic;

namespace MultiplayerStorage
{
    public static class BasicModUtils
    {
        public static void MakeStrings(string id, string name, string description, string effect)
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
