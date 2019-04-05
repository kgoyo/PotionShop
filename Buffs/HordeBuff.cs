using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace PotionShop.Buffs
{
    class HordeBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Horde");
            Description.SetDefault("Increases spawn rate significantly");
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PotionShopPlayer>(this.mod).Horde = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if (player.GetModPlayer<PotionShopPlayer>(this.mod).HordeStacks < 2)
            {
                player.GetModPlayer<PotionShopPlayer>(this.mod).HordeStacks++;
                Main.NewText("Super-Charged Horde Effect!",Color.BlueViolet);
            }
            player.buffTime[buffIndex] = 36000;
            return true;
        }
    }
}