using STRINGS;

namespace InterplanarAutomation
{
    static class BasicModUtils
    {
        public static void MakeStrings(string id, string name, string description, string effect)
        {
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, id));
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.DESC", description);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.EFFECT", effect);
        }

        public static void AddToTech(string techName, string id)
        {
            // worked on vanilla 445739
            //var techList = new List<string>(Database.Techs.TECH_GROUPING[tech]) { id };
            //Database.Techs.TECH_GROUPING[tech] = techList.ToArray();

            // works on DLC
            Tech tech = Db.Get().Techs.TryGet(techName);
            tech.unlockedItemIDs.Add(id);
        }
    }
}
