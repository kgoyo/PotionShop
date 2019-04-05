using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PotionShop.Items
{
    class CelestialStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Stick");
            Tooltip.SetDefault("Swinging the stick makes it either dawn/dusk, depending on the current time");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 49;
            item.height = 49;
            item.useStyle = 25;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.maxStack = 30;
            item.consumable = false;
            item.rare = 3;
            item.value = Item.buyPrice(gold: 30);
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
