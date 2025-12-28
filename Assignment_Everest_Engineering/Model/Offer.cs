namespace Assignment_Everest_Engineering.Model
{
    public class Offer
    {
        public string Code { get; set; }
        public double DiscountPercent { get; set; }
        public int MinDistance { get; set; }
        public int MaxDistance { get; set; }
        public int MinWeight { get; set; }
        public int MaxWeight { get; set; }

        public bool IsApplicable(Package pkg)
        {
            return pkg.Distance >= MinDistance &&
                   pkg.Distance <= MaxDistance &&
                   pkg.Weight >= MinWeight &&
                   pkg.Weight <= MaxWeight;
        }
    }

}
