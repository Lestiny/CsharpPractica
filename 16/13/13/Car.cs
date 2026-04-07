namespace CarRental
{
    public class Car
    {
        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public int PricePerDay { get; set; }
        public CarClass Class { get; set; }
        public int PassengerSeats { get; set; }
        public int FleetSize { get; set; } = 1;

        public string DisplayName => $"{Make} {Model}".Trim();

        public override string ToString() =>
            $"{DisplayName} - {PricePerDay} BYN/сут, класс: {Class}, мест: {PassengerSeats}, в парке: {FleetSize}";
    }
}
