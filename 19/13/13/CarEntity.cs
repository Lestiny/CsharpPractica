namespace CarRental
{
    public class CarEntity
    {
        public int Id { get; set; }

        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public int PricePerDay { get; set; }
        public int FleetSize { get; set; } = 1;
    }
}

