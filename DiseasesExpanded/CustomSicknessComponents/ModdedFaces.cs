using Database;

namespace DiseasesExpanded
{
    public class ModdedFaces
    {
		public static Face FrostyFace;
		public const string FROSTFACE_ID = "DiseasesExpanded_FrostyFace";
		public const string HEAD_FX_SYMBOL = "headfx_de_frostshards";

		public static void Register(Faces __instance)
		{
			FrostyFace = __instance.Add(new Face(FROSTFACE_ID, HEAD_FX_SYMBOL)
			{
				hash = new HashedString("Neutral")
			});
		}
	}
}
