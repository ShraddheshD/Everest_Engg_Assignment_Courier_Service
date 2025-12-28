using Assignment_Everest_Engineering.Model;
using Assignment_Everest_Engineering.Service;

namespace Assignment_Test_Cases
{
    public class OfferServiceTests
    {
        [Fact]
        public void CalculateDiscount_ReturnsZero_WhenOfferCodeNotFound()
        {
            var svc = new OfferService(new List<Offer>());

            var pkg = new Package { Id = "P1", OfferCode = "UNKNOWN", Weight = 10, Distance = 10 };

            double discount = svc.CalculateDiscount(pkg, deliveryCost: 200);

            Assert.Equal(0d, discount);
        }

        [Fact]
        public void CalculateDiscount_ReturnsZero_WhenOfferNotApplicableToPackage()
        {
            var offers = new List<Offer>
            {
                new Offer
                {
                    Code = "OFR001",
                    DiscountPercent = 10,
                    MinDistance = 200,
                    MaxDistance = 500,
                    MinWeight = 70,
                    MaxWeight = 200
                }
            };

            var svc = new OfferService(offers);
            var pkg = new Package { Id = "P2", OfferCode = "OFR001", Weight = 10, Distance = 50 };

            double discount = svc.CalculateDiscount(pkg, deliveryCost: 500);

            Assert.Equal(0d, discount);
        }

        [Fact]
        public void CalculateDiscount_AppliesCorrectPercentage_WhenOfferApplicable()
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

            var svc = new OfferService(offers);
            var pkg = new Package { Id = "P3", OfferCode = "OFR003", Weight = 20, Distance = 100 };

            double deliveryCost = 800;
            double discount = svc.CalculateDiscount(pkg, deliveryCost);

            Assert.Equal(deliveryCost * 0.05, discount);
        }

        [Fact]
        public void CalculateDiscount_ReturnsZero_WhenDeliveryCostIsZeroEvenIfOfferApplicable()
        {
            var offers = new List<Offer>
            {
                new Offer
                {
                    Code = "OFR002",
                    DiscountPercent = 7,
                    MinDistance = 50,
                    MaxDistance = 150,
                    MinWeight = 100,
                    MaxWeight = 250
                }
            };

            var svc = new OfferService(offers);
            var pkg = new Package { Id = "P4", OfferCode = "OFR002", Weight = 120, Distance = 100 };

            double discount = svc.CalculateDiscount(pkg, deliveryCost: 0);

            Assert.Equal(0d, discount);
        }

        [Fact]
        public void CalculateDiscount_IsCaseSensitive_OnOfferCodeLookup()
        {
            var offers = new List<Offer>
            {
                new Offer
                {
                    Code = "OFRabc",
                    DiscountPercent = 10,
                    MinDistance = 0,
                    MaxDistance = 1000,
                    MinWeight = 0,
                    MaxWeight = 1000
                }
            };

            var svc = new OfferService(offers);
            var pkgExact = new Package { Id = "P5", OfferCode = "OFRabc", Weight = 10, Distance = 10 };
            var pkgDifferentCase = new Package { Id = "P6", OfferCode = "ofrABC", Weight = 10, Distance = 10 };

            double d1 = svc.CalculateDiscount(pkgExact, deliveryCost: 1000);
            double d2 = svc.CalculateDiscount(pkgDifferentCase, deliveryCost: 1000);

            Assert.Equal(1000 * 0.10, d1);
            Assert.Equal(0d, d2);
        }
    }
}
