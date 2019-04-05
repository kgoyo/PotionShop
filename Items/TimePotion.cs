using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PotionShop.Items
{
    class TimePotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Potion");
            Tooltip.SetDefault("Makes it dawn/dusk, depending on the current time");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 24;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 3;
            item.value = Item.buyPrice(gold: 1);
        }

        public override bool UseItem(Player player)
        {
            if (!Main.dayTime)
            {
                Main.time = 0;
                Main.dayTime = true;
            }
            else
            {
                Main.time = 0;
                Main.dayTime = false;
                Main.moonPhase = Main.moonPhase + 1;
                if (Main.moonPhase >= 8)
                    Main.moonPhase = 0;
            }
            if (Main.netMode == 2)
                NetMessage.SendData(7); 
            return true;
        }
    }
}
