using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions
{
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
}));

}
}