using System.Collections.Generic;

namespace Assignment_Everest_Engineering.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public double AvailableAt { get; set; }
        public List<List<string>> Shipments { get; } = new List<List<string>>();
    }

}
