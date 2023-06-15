using Database;

namespace DiseasesExpanded
{
    class ModdedAccesories
	{
		public const string FROSTY_FACE_KANIM = "de_frostshards_kanim";

		public static void Register(Accessories accessories, AccessorySlots slots)
		{
			KAnimFile limpetFace = Assets.GetAnim(FROSTY_FACE_KANIM);

			AddAccessories(limpetFace, slots.HeadEffects, accessories);
		}

		public static void AddAccessories(KAnimFile file, AccessorySlot slot, ResourceSet parent)
		{
			var build = file.GetData().build;
			var id = slot.Id.ToLower();

			for (var i = 0; i < build.symbols.Length; i++)
			{
				var symbolName = HashCache.Get().Get(build.symbols[i].hash);
				if (symbolName.StartsWith(id))
				{
					var accessory = new Accessory(symbolName, parent, slot, file.batchTag, build.symbols[i]);
					slot.accessories.Add(accessory);
					HashCache.Get().Add(accessory.IdHash.HashValue, accessory.Id);

					Debug.Log($"{ModInfo.Namespace}: Added accessory: " + accessory.Id);
				}
			}
		}
	}
}
