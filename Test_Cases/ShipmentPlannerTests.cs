using Assignment_Everest_Engineering.Model;
using Assignment_Everest_Engineering.Service;

namespace Assignment_Test_Cases
{
    public class ShipmentPlannerTests
    {
        [Fact]
        public void CalculateDeliveryTimes_AssignsDeliveryTimeToAllFeasiblePackages_SingleVehicle()
        {
            var planner = new ShipmentPlanner();

            var packages = new List<Package>
            {
                new Package { Id = "P1", Weight = 10, Distance = 30 },
                new Package { Id = "P2", Weight = 20, Distance = 60 }
            };

            planner.CalculateDeliveryTimes(packages, vehiclesCount: 1, maxSpeed: 60, maxWeight: 1000);

            Assert.All(packages, p => Assert.True(p.DeliveryTime > 0));
            Assert.True(planner.Vehicles.Count >= 1);
        }

        [Fact]
        public void CalculateDeliveryTimes_MaxWeight_ForcesSeparateTrips_HeavierChosenFirst()
        {
            var planner = new ShipmentPlanner();

            var packages = new List<Package>
            {
                new Package { Id = "A", Weight = 10, Distance = 30 },
                new Package { Id = "B", Weight = 20, Distance = 60 }
            };

            planner.CalculateDeliveryTimes(packages, vehiclesCount: 1, maxSpeed: 30, maxWeight: 20);

            var pkgA = packages.First(p => p.Id == "A");
            var pkgB = packages.First(p => p.Id == "B");

            Assert.Equal(2.0, pkgB.DeliveryTime, 6);

            Assert.Equal(5.0, pkgA.DeliveryTime, 6);
        }
    }
}