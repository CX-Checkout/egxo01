using BeFaster.App.Solutions;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions
{
    [TestFixture]
    public class CheckoutSolutionTest
    {
        [TestCase("ABCD-", ExpectedResult = -1)]
        [TestCase("A", ExpectedResult = 50)]
        [TestCase("B", ExpectedResult = 30)]
        [TestCase("C", ExpectedResult = 20)]
        [TestCase("D", ExpectedResult = 15)]
        [TestCase("AAA", ExpectedResult = 130)]
        [TestCase("BB", ExpectedResult = 45)]
        [TestCase("ABCD", ExpectedResult = 115)]
        [TestCase("BBB", ExpectedResult = 75)]
        [TestCase("AAAAA", ExpectedResult = 200)]
        [TestCase("AAAABBB", ExpectedResult = 255)]
        public int Checkout(string skus)
        {
            return CheckoutSolution.Checkout(skus);
        }
    }
}
