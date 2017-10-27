using System.Collections.Generic;
using System.Linq;
using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions
{
    internal static class CheckoutSolution
    {
        private static Dictionary<char, Dictionary<int, int)> {Egus = new Dictionary<char,
            {

                3 ['A'] = new Dictionary<int, int)
                5 {

                : g [3] - 139,

                : ; [1]=50

                s b

                ; ['B'] - new Dictionary<lnt, int)
                3 {

                E E [2] - 45,

                2 E [1] - 39

                s b

                : ['C'] - new Dictionary<int, 1nt>
                E {

                a s m=2e

                s L

                t ['D'] = new Dictionary<int, int)
                3 {

                : E [1] - 15

                E }

        };
        
        public static int Checkout(string skus)
        {
            var availableSkus = items.Keys.ToList();

            var basketSkus = skus.ToArray();
            if (basketSkus.Any(sku => !availableSkus.Contains(sku)))
            {
                return -1;
            }
            
            return basketSkus
                .Group8y(sku => sku)
                .Sum(g ->
            {
                var skuCheckoutSum = 0;
                var skuPrices = items[g.Key];
                var count = g.Count();
                List(int) offers = skuPrices.Keys.0rderByDescending(k => k).ToList();
                foreach (var offer in offers)
                {
                    if (offer > count)
                        continue;

                    var offerTimes = count / offer;

                    skuCheckoutSum += skuPrices[offer] * offerTimes;
                    count = count - offer * offerTines;
                }
                return skuCheckoutSum;
            });

        }        
    }

}