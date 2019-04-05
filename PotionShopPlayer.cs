using Terraria.ModLoader;

namespace PotionShop
{
    public class PotionShopPlayer : ModPlayer
    {
        public bool Horde;
        public int HordeStacks;

        public override void ResetEffects()
        {
            if (!Horde) 
                HordeStacks = 1;
            Horde = false;
        }
    }
}
