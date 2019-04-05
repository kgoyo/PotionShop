using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PotionShop.Items
{
    class HordePotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Horde Potion");
            Tooltip.SetDefault("Increases spawn rate by a whole lot...");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 34;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 3;
            item.value = Item.buyPrice(gold: 5);
            item.buffType = this.mod.BuffType("HordeBuff");
            item.buffTime = 36000;
        }
    }
}
