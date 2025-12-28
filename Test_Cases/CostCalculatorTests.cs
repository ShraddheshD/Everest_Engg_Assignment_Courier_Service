using Assignment_Everest_Engineering.Model;
using Assignment_Everest_Engineering.Service;

namespace Assignment_Test_Cases
{
    public class CostCalculatorTests
    {
        [Fact]
        public void Calculate_NoOffers_NoDiscount_TotalCostMatchesFormula()
        {
            var offers = new List<Offer>();
            var offerSvc = new OfferService(offers);
            var calc = new CostCalculator(offerSvc);

            var pkg = new Package { Id = "PKG1", Weight = 5, Distance = 5, OfferCode = "NA" };
            int baseCost = 100;

            calc.Calculate(pkg, baseCost);

            // deliveryCost = 100 + (5*10) + (5*5) = 175
            Assert.Equal(0d, pkg.Discount);
            Assert.Equal(175d, pkg.TotalCost);
        }

        [Fact]
        public void Calculate_WithApplicableOffer_AppliesDiscountCorrectly()
        {
            var offers = new List<Offer>
            {
                new Offer
                {
                    Code = "OFR003",
                    DiscountPercent = 5,
                    MinDistance = 50,
                    MaxDistance = 250,
                    MinWeight = 10,
                    MaxWeight = 150
                }
            };

            var offerSvc = new OfferService(offers);
            var calc = new CostCalculator(offerSvc);

            var pkg = new Package { Id = "PKG2", Weight = 10, Distance = 100, OfferCode = "OFR003" };
            int baseCost = 100;

            calc.Calculate(pkg, baseCost);

            // deliveryCost = 100 + 10*10 + 100*5 = 700
            // discount = 5% of 700 = 35 => total = 665
            Assert.Equal(35d, pkg.Discount);
            Assert.Equal(665d, pkg.TotalCost);
        }
    }
}