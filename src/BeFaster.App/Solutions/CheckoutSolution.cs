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
            },    
            ['N'] = new Dictionary<char, Tuple<int, int>>
            {
                ['M'] = new Tuple<int, int>(3, 1)
            },    
            ['R'] = new Dictionary<char, Tuple<int, int>>
            {
                ['Q'] = new Tuple<int, int>(3, 1)
            },
            ['U'] = new Dictionary<char, Tuple<int, int>>
            {
                ['U'] = new Tuple<int, int>(4, 1)
            }    
        };
        
        private static Dictionary<string, Dictionary<int, int>> groupOffers = new Dictionary<string, Dictionary<int, int>>
        {
            ["STXYZ"] = new Dictionary<int, int>
            {
                [3] = 45
            }
        };
        
        private static Dictionary<char, Dictionary<int, int>> prices = new Dictionary<char, Dictionary<int, int>>
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
            },
            ['G'] = new Dictionary<int, int>
            {
                [1] = 20
            },
            ['H'] = new Dictionary<int, int>
            {
                [10] = 80, 
                [5] = 45, 
                [1] = 10
            },
            ['I'] = new Dictionary<int, int>
            {
                [1] = 35
            },
            ['J'] = new Dictionary<int, int>
            {
                [1] = 60
            },
            ['K'] = new Dictionary<int, int>
            {
                [2] = 120, [1] = 70
            },
            ['L'] = new Dictionary<int, int>
            {
                [1] = 90
            },
            ['M'] = new Dictionary<int, int>
            {
                [1] = 15
            },
            ['N'] = new Dictionary<int, int>
            {
                [1] = 40
            },
            ['O'] = new Dictionary<int, int>
            {
                [1] = 10
            },
            ['P'] = new Dictionary<int, int>
            {
                [5] = 200, 
                [1] = 50
            },
            ['Q'] = new Dictionary<int, int>
            {
                [3] = 80, 
                [1] = 30
            },
            ['R'] = new Dictionary<int, int>
            {
                [1] = 50
            },
            ['S'] = new Dictionary<int, int>
            {
                [1] = 20
            },
            ['T'] = new Dictionary<int, int>
            {
                [1] = 20
            },
            ['U'] = new Dictionary<int, int>
            {
                [1] = 40
            },
            ['V'] = new Dictionary<int, int>
            {
                [3] = 130, 
                [2] = 90, 
                [1] = 50
            },
            ['W'] = new Dictionary<int, int>
            {
                [1] = 20
            },
            ['X'] = new Dictionary<int, int>
            {
                [1] = 17
            },
            ['Y'] = new Dictionary<int, int>
            {
                [1] = 20
            },
            ['Z'] = new Dictionary<int, int>
            {
                [1] = 21
            }
        };

        public static int Checkout(string skus)
        {
            var availableSkus = prices.Keys.ToList();

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

            // Free offers
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

            var totalSum = 0;
            
            // Group offers
            foreach (var groupOffer in groupOffers)
            {
                var basketGroupOfferSkus = basketSkus.Where(s => groupOffer.Key.Contains(s.Name)).ToList();
                var totalGroupCount = basketGroupOfferSkus.Sum(s => s.Count);

                var reduced = 0;
                
                foreach (var offer in groupOffer.Value)
                {
                    if (offer.Key > totalGroupCount) 
                        continue;

                    var offerTimes = totalGroupCount / offer.Key;

                    totalSum += offer.Value * offerTimes;
                    totalGroupCount -= offer.Key * offerTimes;
                    reduced += offer.Key * offerTimes;
                }

                foreach (var sku in basketGroupOfferSkus.OrderBy(s => prices[s.Name][1]))
                {
                    if (reduced <= 0)
                        break;

                    sku.Count 
                }
            }
            
            totalSum += 0;
            
            // Individual prices
            totalSum += basketSkus
                .Sum(s =>
            {
                var skuCheckoutSum = 0;
                
                var skuPrices = prices[s.Name];
                
                List<int> offers = skuPrices.Keys.OrderByDescending(k => k).ToList();
                
                foreach (var offer in offers)
                {
                    if (offer > s.Count)
                        continue;

                    var offerTimes = s.Count / offer;

                    skuCheckoutSum += skuPrices[offer] * offerTimes;
                    s.Count -= offer * offerTimes;
                }
                return skuCheckoutSum;
            });

            return totalSum;
        }

        private class Sku
        {
            public char Name { get; set; }
            
            public int Count { get; set; }
        }
    }
}
