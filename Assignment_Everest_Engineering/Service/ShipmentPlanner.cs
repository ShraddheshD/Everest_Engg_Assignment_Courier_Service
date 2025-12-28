using Assignment_Everest_Engineering.Model;

namespace Assignment_Everest_Engineering.Service
{
    public class ShipmentPlanner
    {
        public List<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        public void CalculateDeliveryTimes(
            List<Package> packages,
            int vehiclesCount,
            int maxSpeed,
            int maxWeight)
        {
            var vehicles = new PriorityQueue<Vehicle, double>();
            var vehicleList = new List<Vehicle>();
            for (int i = 0; i < vehiclesCount; i++)
            {
                var v = new Vehicle { Id = i + 1, AvailableAt = 0 };
                vehicleList.Add(v);
                vehicles.Enqueue(v, v.AvailableAt);
            }

            Vehicles = vehicleList;

            var remaining = new List<Package>(packages);

            while (remaining.Any())
            {
                if (!vehicles.TryDequeue(out var vehicle, out double currentTime))
                    break;

                var shipment = SelectBestShipment(remaining, maxWeight);
                if (shipment == null || shipment.Count == 0)
                {
                    break;
                }

                double maxDistanceInShipment = shipment.Max(p => p.Distance);

                foreach (var pkg in shipment)
                {
                    pkg.DeliveryTime = currentTime + pkg.Distance / (double)maxSpeed;
                }

                vehicle.Shipments.Add(shipment.Select(p => p.Id).ToList());
                vehicle.AvailableAt = currentTime + 2 * (maxDistanceInShipment / (double)maxSpeed);
                vehicles.Enqueue(vehicle, vehicle.AvailableAt);
                //Console.WriteLine($"Vehicle {vehicle.Id} will be available at {vehicle.AvailableAt:F2}");

                foreach (var pkg in shipment)
                {
                    //Console.WriteLine($"Package {pkg.Id} delivered at {pkg.DeliveryTime:F2}");
                    remaining.Remove(pkg);
                }
                    
            }
        }

        private static List<Package> SelectBestShipment(List<Package> remaining, int maxWeight)
        {
            int n = remaining.Count;
            var feasiblePackages = remaining.Where(p => p.Weight <= maxWeight).ToList();
            if (!feasiblePackages.Any())
                return new List<Package>();

            List<Package> best = new List<Package>();
            int bestCount = 0;
            int bestTotalWeight = 0;
            int bestMaxDistance = int.MaxValue;

            int m = feasiblePackages.Count;
            int subsetCount = 1 << m;
            for (int mask = 1; mask < subsetCount; mask++)
            {
                int sumWeight = 0;
                int count = 0;
                int maxDist = 0;
                for (int i = 0; i < m; i++)
                {
                    if ((mask & (1 << i)) == 0) continue;
                    var p = feasiblePackages[i];
                    sumWeight += p.Weight;
                    if (sumWeight > maxWeight) { count = -1; break; }
                    count++;
                    if (p.Distance > maxDist) maxDist = p.Distance;
                }
                if (count <= 0) continue;

                if (count > bestCount
                    || (count == bestCount && sumWeight > bestTotalWeight)
                    || (count == bestCount && sumWeight == bestTotalWeight && maxDist < bestMaxDistance))
                {
                    var chosen = new List<Package>();
                    for (int i = 0; i < m; i++)
                    {
                        if ((mask & (1 << i)) != 0)
                            chosen.Add(feasiblePackages[i]);
                    }

                    best = chosen;
                    bestCount = count;
                    bestTotalWeight = sumWeight;
                    bestMaxDistance = maxDist;
                }
            }

            return best;
        }
    }
}
