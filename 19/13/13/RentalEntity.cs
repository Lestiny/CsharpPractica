namespace CarRental
{
    public class RentalEntity
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public CarEntity? Car { get; set; }

        public string ClientName { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PricePerDay { get; set; }
    }
}

