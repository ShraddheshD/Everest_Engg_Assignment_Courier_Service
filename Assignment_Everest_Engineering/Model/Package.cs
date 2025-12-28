namespace Assignment_Everest_Engineering.Model
{
    public class Package
    {
        public string Id { get; set; }
        public int Weight { get; set; }
        public int Distance { get; set; }
        public string OfferCode { get; set; }

        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public double DeliveryTime { get; set; }
    }

}
