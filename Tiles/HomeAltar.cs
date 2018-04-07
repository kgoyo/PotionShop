using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace PotionShop.Tiles
{
    public class HomeAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16,18 };
            TileObjectData.addTile(Type);
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Home Altar");
            AddMapEntry(new Color(200, 200, 200), name);
            dustType = mod.DustType("Sparkle");
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.DemonAltar };
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType("HomeAltar"));
        }
    }
}