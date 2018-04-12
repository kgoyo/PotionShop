using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace PotionShop.NPCs
{
    [AutoloadHead]
    public class MaterialsVendor : ModNPC
    {
        private List<int> VendorShop = new List<int>();
        private const int MATERIALS = 0;
        private const int BARSANDGEMS = 1;

        private static int shopIndex = 0;
        private const string CALAMITYMOD = "CalamityMod";

        public override bool Autoload(ref string name)
        {
            name = "Materials Vendor";
            return mod.Properties.Autoload;
        }

        public override string Texture
        {
            get
            {
                return "PotionShop/NPCs/MaterialsVendor";
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Materials Vendor");
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 10;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 1;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;

        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;

            //create shop list
            VendorShop.Add(MATERIALS);
            VendorShop.Add(BARSANDGEMS);
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            //Spawn if Potion Vendor has spawned
            return NPC.AnyNPCs(mod.NPCType("Potion Vendor"));
        }

        public override string TownNPCName()
        {
            return "Vegeta";
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "Yo";
                case 1:
                    return "sup";
                default:
                    return "Buy ma shit bro";
            }
        }

        public override void SetChatButtons(ref string button1, ref string button2)
        {
            if (shopIndex >= VendorShop.Count)
            {
                shopIndex = 0;
            }
            switch (VendorShop[shopIndex])
            {
                case MATERIALS:
                    button1 = "Buy Assorted Materials";
                    break;
                case BARSANDGEMS:
                    button1 = "Buy Bars and Gems";
                    break;
                default:
                    button1 = "...";
                    break;
            }

            button2 = "Next shop";
        }

        private void NextShop()
        {
            shopIndex++;
            if (shopIndex == VendorShop.Count)
            {
                shopIndex = 0;
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                NextShop();
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
            //shop has 40 slots
        {
            switch (VendorShop[shopIndex])
            {
                case MATERIALS:
                    shop.item[nextSlot].SetDefaults(mod.ItemType("HomeAltar"));
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Gel);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 0, 50);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Wood);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 0, 10);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Vertebrae);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.RottenChunk);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 1);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Book);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 2);
                    nextSlot++;

                    //post EoC
                    if (NPC.downedBoss1)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.FallenStar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.GlowingMushroom);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 5);
                        nextSlot++;
                    }

                    //post EoW BoC, crimson/ corruption items
                    if (NPC.downedBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.LifeCrystal);
                        nextSlot++;
						shop.item[nextSlot].SetDefaults(ItemID.Feather );
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 25);
                        nextSlot++;
                    }

                    if (NPC.downedQueenBee)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.BeeWax);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10);
                        nextSlot++;
                    }

                    //post skeletron, dungeon items
                    if (NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Bone);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.GoldenKey);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.LockBox);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 17);
                        nextSlot++;
                    }

                    if (Main.hardMode)
                    {

                    }

                    if (NPC.downedMechBossAny)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Ichor);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 75);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CursedFlame);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 75);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.PixieDust);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.UnicornHorn);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CrystalShard);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 75);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofNight);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofLight);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10);
                        nextSlot++;

                    }

                    //Destroyer
                    if (NPC.downedMechBoss1)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofMight);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 3);
                        nextSlot++;
                    }

                    //Twins
                    if (NPC.downedMechBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofSight);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 3);
                        nextSlot++;
                    }

                    //Skeltron Prime
                    if (NPC.downedMechBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofFright);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 3);
                        nextSlot++;
                    }

                    if (NPC.downedGolemBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.BeetleHusk);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.LifeFruit);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 20);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Ectoplasm);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 1);
                        nextSlot++;
                    }

                    if (NPC.downedTowerNebula)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.FragmentNebula);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 2);
                        nextSlot++;
                    }

                    if (NPC.downedTowerSolar)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.FragmentSolar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 2);

                        nextSlot++;
                    }

                    if (NPC.downedTowerStardust)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.FragmentStardust);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 2);

                        nextSlot++;
                    }

                    if (NPC.downedTowerVortex)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.FragmentVortex);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 2);
                        nextSlot++;
                    }

                    break;

                case BARSANDGEMS:
                    //post EoW BoC, crimson/ corruption items
                    if (NPC.downedBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.CrimtaneBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 42);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.DemoniteBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 36);
                        nextSlot++;
                    }

                    //post skeletron, dungeon items
                    if (NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Diamond);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 75);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Ruby);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 75);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Emerald);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 30);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Sapphire);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 30);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Topaz);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Amethyst);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Amber);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FossilOre);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 50);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.CopperBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 2, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TinBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 3);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.IronBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 4, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.LeadBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.SilverBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 7, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TungstenBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.GoldBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 13);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.PlatinumBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 18, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.MeteoriteBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 25);
                        nextSlot++;
                    }

                    //hardmode
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.HellstoneBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 48);
                        nextSlot++;
                    }

                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.HallowedBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 70);
                        nextSlot++;
                    }

                    if (NPC.downedPlantBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.ChlorophyteBar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 95);
                        nextSlot++;
                    }

                    if (ModLoader.GetLoadedMods().Contains(CALAMITYMOD))
                    {
                        if (CalamityHelper.DownedProvidence)
                        {
                            shop.item[nextSlot].SetDefaults(ItemID.LunarBar);
                            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 2, 95);
                            nextSlot++;
                        }
                    }

                    break;
                default:
                    //no default shop
                    break;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 10;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 5;
            randExtraCooldown = 10;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ProjectileID.WoodenArrowFriendly;
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }

        public override void DrawTownAttackGun(ref float scale, ref int item, ref int closeness)
        {
            scale = 1f;
            item = ItemID.WoodenBow;
            closeness = 20;
        }
        

    }
}