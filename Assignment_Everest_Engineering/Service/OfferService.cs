using Assignment_Everest_Engineering.Model;

namespace Assignment_Everest_Engineering.Service
{
    public class OfferService
    {
        private readonly Dictionary<string, Offer> _offers;

        public OfferService(IEnumerable<Offer> offers)
        {
            _offers = offers.ToDictionary(o => o.Code);
        }

        public double CalculateDiscount(Package pkg, double deliveryCost)
        {
            if (!_offers.ContainsKey(pkg.OfferCode))
                return 0;

            var offer = _offers[pkg.OfferCode];
            return offer.IsApplicable(pkg)
                ? deliveryCost * offer.DiscountPercent / 100
                : 0;
        }
    }

}
