using Terraria;
using Terraria.ModLoader;

namespace PotionShop.NPCs
{
    public class PotionShopGlobalNPC : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.GetModPlayer<PotionShopPlayer>(this.mod).Horde)
            {
                if (player.GetModPlayer<PotionShopPlayer>(this.mod).HordeStacks < 2)
                {
                    spawnRate = (int)(spawnRate * 0.1);
                }
                else
                {
                    spawnRate = (int)(spawnRate * 0.01);
                }
                maxSpawns *= 5;
            }

            if (player.GetModPlayer<PotionShopPlayer>(this.mod).Peace)
            {
                spawnRate = (int)(spawnRate * 50.0);
                maxSpawns *= (int)(1.0/100);
            }
               
        }
    }
}