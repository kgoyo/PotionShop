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
    public class AccessoryVendor : ModNPC
    {
        private List<int> VendorShop = new List<int>();
        private const int SHOP1 = 0;
        private const int SHOP2 = 1;

        private static int shopIndex = 0;

        public override bool Autoload(ref string name)
        {
            name = "Accessory Vendor";
            return mod.Properties.Autoload;
        }

        public override string Texture
        {
            get
            {
                return "PotionShop/NPCs/AccessoryVendor";
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Accessory Vendor");
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
            VendorShop.Add(SHOP1);
            VendorShop.Add(SHOP2);
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            //Spawn if EoC has been defeated
            return NPC.downedBoss1;
        }

        public override string TownNPCName()
        {
            return "Gohan";
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
                case SHOP1:
                    button1 = "1st Shop";
                    break;
                case SHOP2:
                    button1 = "2nd Shop";
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
                case SHOP1:
                    shop.item[nextSlot].SetDefaults(ItemID.Aglet);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,5);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SharkToothNecklace);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ClimbingClaws);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,5);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.ShoeSpikes);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.FrogLeg);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.HermesBoots);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.WaterWalkingBoots);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,20);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.IceSkates);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,15);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.CloudinaBottle);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BlizzardinaBottle);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,15);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.SandstorminaBottle);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,20);
                    nextSlot++;
                    shop.item[nextSlot].SetDefaults(ItemID.BandofRegeneration);
                    shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,5);
                    nextSlot++;

                    //post EoW/BoC
                    if (NPC.downedBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.BandofStarpower);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.PanicNecklace);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                        nextSlot++;
                        if (Main.ActiveWorldFileData.IsExpertMode) {
                            if (Main.ActiveWorldFileData.HasCrimson)
                            {
                                shop.item[nextSlot].SetDefaults(ItemID.WormScarf);
                                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,15);
                                nextSlot++;
                            }
                            else
                            {
                                shop.item[nextSlot].SetDefaults(ItemID.BrainOfConfusion);
                                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,15);
                                nextSlot++;
                            }
                        }
                        shop.item[nextSlot].SetDefaults(ItemID.LavaCharm);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,30);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.LuckyHorseshoe);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,20);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ShinyRedBalloon);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,25);
                        nextSlot++;
                    }

                    if (NPC.downedQueenBee)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.NaturesGift);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,15);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.AnkletoftheWind);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,20);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FeralClaws);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,20);
                        nextSlot++;
                    }

                    if (NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.CelestialMagnet);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,20);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CobaltShield);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,25);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FlyingCarpet);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,30);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.ObsidianRose);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.MagmaStone);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,10);
                        nextSlot++;
                    }

                    if (NPC.downedMechBossAny)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MagicQuiver);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FrozenTurtleShell);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TitanGlove);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0,50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.PhilosophersStone);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.CrossNecklace);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.StarCloak);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 50);
                        nextSlot++;
                    }

                    if (NPC.downedGolemBoss)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.MoonStone);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 60);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.MoonCharm);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 50);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.NeptunesShell);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 50);
                        nextSlot++;
                    }

                    break;

                case SHOP2:

                    if (NPC.downedBoss3)
                    {
                        //PDA ingredients
                        shop.item[nextSlot].SetDefaults(ItemID.FishermansGuide);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Sextant);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.WeatherRadio);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.DPSMeter);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.MetalDetector);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Stopwatch);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.Compass);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.DepthMeter);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.GoldWatch);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;

                        shop.item[nextSlot].SetDefaults(ItemID.LifeformAnalyzer);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Radar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TallyCounter);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 5);
                        nextSlot++;
                    }

                    if (NPC.downedGolemBoss)
                    {
                        //Ankh charm ingredients
                        shop.item[nextSlot].SetDefaults(ItemID.ArmorPolish);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Vitamins);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Bezoar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.AdhesiveBandage);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.FastClock);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.TrifoldMap);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Megaphone);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Nazar);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;
                        shop.item[nextSlot].SetDefaults(ItemID.Blindfold);
                        shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 10);
                        nextSlot++;

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