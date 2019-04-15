using PotionShop.Buffs;
using Terraria.ModLoader;

namespace PotionShop
{
    public class PotionShopPlayer : ModPlayer
    {
        public bool Horde;
        public bool Peace;
        public int HordeStacks;

        public override void ResetEffects()
        {
            if (!Horde) 
                HordeStacks = 1;
            Horde = false;
            Peace = false;
        }
    }
}
