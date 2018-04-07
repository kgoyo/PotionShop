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
            //Can spawn if any player has a swiftness potion
            foreach (Player player in Main.player)
            {
                foreach (Item item in player.inventory)
                {
                    if (item.type == ItemID.SwiftnessPotion)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string TownNPCName()
        {
            return "Vegeta";
        }

        public override string GetChat()
        {
            return "" + shopIndex;
            /*switch (Main.rand.Next(3))
            {
                case 0:
                    return "Yo";
                case 1:
                    return "sup";
                default:
                    return "Buy ma shit bro";
            }*/
        }

        public override void SetChatButtons(ref string button1, ref string button2)
        {
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
            if (shopIndex==VendorShop.Count)
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
                    shop.item[nextSlot].SetDefaults(ItemID.Gel);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Wood);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Vertebrae);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.RottenChunk);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Book);
                    nextSlot++;
                    //post EoC
                    if (NPC.downedBoss1)
                    { 
                        shop.item[nextSlot].SetDefaults(ItemID.FallenStar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 50);
                        nextSlot++;
                    }

                    //post EoW BoC, crimson/ corruption items
                    if (NPC.downedBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.CrimtaneBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.DemoniteBar);
                        nextSlot++;
               
                        shop.item[nextSlot].SetDefaults(ItemID.LifeCrystal);
                        nextSlot++;
                    }

                    //post queen bee, jungle items
                    if (NPC.downedQueenBee)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Stinger);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Vine);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.JungleSpores);
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
                    }


                    
                    if (Main.hardMode)
                    {
                        
                    }
                    
                    //Destroyer
                    if (NPC.downedMechBoss1)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofMight);
                        nextSlot++;
                    }

                    //Twins
                    if (NPC.downedMechBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofSight);
                        nextSlot++;
                    }

                    //Skeltron Prime
                    if (NPC.downedMechBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.SoulofFright);
                        nextSlot++;
                    }

                    if (NPC.downedMechBossAny)
                    {
                        
                    }

                    if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.HallowedBar);
                        nextSlot++;
                    }

                    break;

                case BARSANDGEMS:
                    //post EoC
                    if (NPC.downedBoss1)
                    {
                       
                    }

                    //post EoW BoC, crimson/ corruption items
                    if (NPC.downedBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.CrimtaneBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.DemoniteBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Amber);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FossilOre);
                        nextSlot++;
                    }

                    //post queen bee, jungle items
                    if (NPC.downedQueenBee)
                    {
  
                    }

                    //post skeletron, dungeon items
                    if (NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.Diamond);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Ruby);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Emerald);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Sapphire);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Topaz);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Amethyst);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Amber);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.FossilOre);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.CopperBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TinBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.IronBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.LeadBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.SilverBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TungstenBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.GoldBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.PlatinumBar);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.MeteoriteBar);
                        nextSlot++;
                    }

                    //hardmode
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.HellstoneBar);
                        nextSlot++;
                    }
                    break;
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