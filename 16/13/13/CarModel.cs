namespace CarRental
{
    public class CarModel
    {
        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public int PricePerDay { get; set; }
        public int FleetSize { get; set; } = 1;

        public string DisplayName => $"{Make} {Model}".Trim();
    }
}
