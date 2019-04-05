﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace PotionShop
{
    public class PotionShopPlayer : ModPlayer
    {
        public bool Horde;
        public int HordeStacks;

        public override void ResetEffects()
        {
            if (!Horde) 
                HordeStacks = 1;
            Horde = false;
        }
    }
}