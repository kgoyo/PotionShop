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
    public class PotionVendor : ModNPC
    {
        private List<int> potionVendorShop = new List<int>();
        private const int BUFFSHOP = 0;
        private const int CONSUMESHOP = 1;
        private const int CALAMITYSHOP = 2;

        private static int shopIndex = 0;

        public override bool Autoload(ref string name)
        {
            name = "Potion Vendor";
            return mod.Properties.Autoload;
        }

        public override string Texture
        {
            get
            {
                return "PotionShop/NPCs/PotionVendor";
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potion Vendor");
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
            potionVendorShop.Add(BUFFSHOP);
            potionVendorShop.Add(CONSUMESHOP);
            if (ModLoader.GetLoadedMods().Contains("TutorialMod"))
            {
                potionVendorShop.Add(CALAMITYSHOP);
            }
            

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
            return "Goku";
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
            switch (potionVendorShop[shopIndex])
            {
                case BUFFSHOP:
                    button1 = "Buy vanilla buffs";
                    break;
                case CONSUMESHOP:
                    button1 = "Buy Consumable potions";
                    break;
                case CALAMITYSHOP:
                    button1 = "Buy Calamity potions";
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
            if (shopIndex==potionVendorShop.Count)
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
            switch (potionVendorShop[shopIndex])
            {
                case BUFFSHOP:
                    shop.item[nextSlot].SetDefaults(ItemID.PumpkinPie);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.Ale);
                    nextSlot++;

                    shop.item[nextSlot].SetDefaults(ItemID.RegenerationPotion);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SwiftnessPotion);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.IronskinPotion);
                    nextSlot++;

                    //post EoC
                    if (NPC.downedBoss1)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.GillsPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FlipperPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.WaterWalkingPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.BattlePotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CalmingPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.BuilderPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.EndurancePotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ArcheryPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.MagicPowerPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ManaRegenerationPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ThornsPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.InvisibilityPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.MiningPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.SpelunkerPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TrapsightPotion); //dangersense
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.HunterPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ShinePotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.NightOwlPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.FeatherfallPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.GravitationPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.SonarPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.WarmthPotion);
                        nextSlot++;
                    }

                    //post EoW BoC, crimson/ corruption items
                    if (NPC.downedBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.CratePotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.HeartreachPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.InfernoPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ObsidianSkinPotion);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.RagePotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.WrathPotion);
                        nextSlot++;
                    }

                    //post queen bee, jungle items
                    if (NPC.downedQueenBee)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.AmmoReservationPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FishingPotion);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.SummoningPotion);
                        nextSlot++;
                    }

                    //post skeletron, dungeon items
                    if (NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.TitanPotion);
                        nextSlot++;
                    }

                    //hardmode
                    if (Main.hardMode)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.LifeforcePotion);
                        nextSlot++;
                    }
                    break;

                case CONSUMESHOP:
                    shop.item[nextSlot].SetDefaults(ItemID.LesserHealingPotion);
                    nextSlot++;
                    break;
                case CALAMITYSHOP:
                    shop.item[nextSlot].SetDefaults(ItemID.DirtBlock);
                    nextSlot++;
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