using Assignment_Everest_Engineering.Model;
using Assignment_Everest_Engineering.Service;

class Program
{
    static void Main()
    {
        int baseCost = 100;

        var packages = new List<Package>
        {
            new() { Id="PKG1", Weight=5, Distance=5, OfferCode="OFR001" },
            new() { Id="PKG2", Weight=10, Distance=100, OfferCode="OFR003" },
            new() { Id="PKG3", Weight=50, Distance=30, OfferCode="OFR001" },
            new() { Id="PKG4", Weight=75, Distance=125, OfferCode="OFR0008" },
            new() { Id="PKG5", Weight=175, Distance=100, OfferCode="OFR003" },
            new() { Id="PKG6", Weight=110, Distance=60, OfferCode="OFR002" },
            new() { Id="PKG7", Weight=155, Distance=95, OfferCode="OFR003" }
        };

        var offers = new List<Offer>
        {
            new() { Code="OFR001", DiscountPercent=10, MinDistance=200, MaxDistance=500, MinWeight=70, MaxWeight=200 },
            new() { Code="OFR002", DiscountPercent=7, MinDistance=50, MaxDistance=150, MinWeight=100, MaxWeight=250 },
            new() { Code="OFR003", DiscountPercent=5, MinDistance=50, MaxDistance=250, MinWeight=10, MaxWeight=150 }
        };

        var offerService = new OfferService(offers);
        var costCalculator = new CostCalculator(offerService);

        foreach (var pkg in packages)
            costCalculator.Calculate(pkg, baseCost);

        var planner = new ShipmentPlanner();
        planner.CalculateDeliveryTimes(
            packages,
            vehiclesCount: 2,
            maxSpeed: 70,
            maxWeight: 200
        );

        foreach (var pkg in packages)
            Console.WriteLine($"{pkg.Id} {pkg.Discount:F0} {pkg.TotalCost:F0} {pkg.DeliveryTime:F2}");
    }
}
