using System;
using System.Collections.Generic;

namespace PotionShop
{
    public static class ShopHelper
    {
        public static void PadShopNames(List<string> shops)
        {
            int longestLength = 0;
            foreach (string shop in shops)
            {
                longestLength = Math.Max(longestLength, shop.Length);
            }

            for (var i = 0; i < shops.Count; i++)
            {
                shops[i] = shops[i].PadRight(longestLength);
            }
        }
    }
}