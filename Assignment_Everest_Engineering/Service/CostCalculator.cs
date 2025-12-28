using Assignment_Everest_Engineering.Model;

namespace Assignment_Everest_Engineering.Service
{
    public class CostCalculator
    {
        private readonly OfferService _offerService;

        public CostCalculator(OfferService offerService)
        {
            _offerService = offerService;
        }

        public void Calculate(Package pkg, int baseCost)
        {
            double deliveryCost = baseCost +
                                  (pkg.Weight * 10) +
                                  (pkg.Distance * 5);

            pkg.Discount = _offerService.CalculateDiscount(pkg, deliveryCost);
            pkg.TotalCost = deliveryCost - pkg.Discount;
        }
    }

}
