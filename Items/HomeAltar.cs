using System;
using Terraria;
using Terraria.ModLoader;

namespace PotionShop.Items
{
    public class HomeAltar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Your very own Demon altar for your home.");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 32;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = Item.buyPrice(0,1,0,0);
            item.createTile = mod.TileType("HomeAltar");
        }
    }
}