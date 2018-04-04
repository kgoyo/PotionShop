using Terraria.ModLoader;

namespace PotionShop
{
	class PotionShop : Mod
	{
		public PotionShop()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
	}
}
