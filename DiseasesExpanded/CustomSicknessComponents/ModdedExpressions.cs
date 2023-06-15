using Database;

namespace DiseasesExpanded
{
    class ModdedExpressions
	{
		public static Expression FrostyExpression;

		public static void Register(Expressions __instance)
		{
			FrostyExpression = new Expression(ModdedFaces.FROSTFACE_ID, __instance, ModdedFaces.FrostyFace)
			{
				priority = __instance.SickSpores.priority + 1
			};
		}
	}
}
