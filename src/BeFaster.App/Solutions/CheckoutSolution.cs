using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions
{
    public static class CheckoutSolution
    {
        private static Dictionary<char, Dictionary<char, Tuple<int, int>>> free = new Dictionary<char, Dictionary<char, Tuple<int, int>>>
        {
            ['E'] = new Dictionary<char, Tuple<int, int>>
            {
                ['B'] = new Tuple<int, int>(2, 1)
            },
            ['F'] = new Dictionary<char, Tuple<int, int>>
            {
                ['F'] = new Tuple<int, int>(3, 1)
            }    
        };
        
        private static Dictionary<char, Dictionary<int, int>> items = new Dictionary<char, Dictionary<int, int>>
        {
            ['A'] = new Dictionary<int, int>
            {
                [5] = 200,
                [3] = 130,
                [1] = 50
            },
            ['B'] = new Dictionary<int, int>
            {
                [2] = 45,
                [1] = 30
            },
            ['C'] = new Dictionary<int, int>
            {
                [1] = 20
            },
            ['D'] = new Dictionary<int, int>
            {
                [1] = 15
            },
            ['E'] = new Dictionary<int, int>
            {
                [1] = 40
            },
            ['F'] = new Dictionary<int, int>
            {
                [1] = 10
            }
        };

        public static int Checkout(string skus)
        {
            var availableSkus = items.Keys.ToList();

            if (skus.Any(sku => !availableSkus.Contains(sku)))
            {
                return -1;
            }

            var basketSkus = skus
                .GroupBy(sku => sku)
                .Select(sku => new Sku
                {
                    Name = sku.Key,
                    Count = sku.Count()
                })
                .ToList();

            foreach (var sku in basketSkus.Where(s => free.ContainsKey(s.Name)))
            {
                foreach (var skuFree in free[sku.Name])
                {
                    var toReduce = basketSkus.FirstOrDefault(b => b.Name == skuFree.Key);
                    if (toReduce == null)
                        continue;

                    var freeItems = sku.Count / skuFree.Value.Item1;

                    toReduce.Count -= freeItems * skuFree.Value.Item2;

                    if (toReduce.Count < 0)
                        toReduce.Count = 0;
                }
            }
            
            return basketSkus
                .Sum(s =>
            {
                var skuCheckoutSum = 0;
                
                var skuPrices = items[s.Name];
                
                List<int> offers = skuPrices.Keys.OrderByDescending(k => k).ToList();
                
                foreach (var offer in offers)
                {
                    if (offer > s.Count)
                        continue;

                    var offerTimes = s.Count / offer;

                    skuCheckoutSum += skuPrices[offer] * offerTimes;
                    s.Count = s.Count - offer * offerTimes;
                }
                return skuCheckoutSum;
            });
        }

        private class Sku
        {
            public char Name { get; set; }
            
            public int Count { get; set; }
        }
    }
}