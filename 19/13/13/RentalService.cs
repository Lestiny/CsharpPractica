using System.Collections.ObjectModel;
using System.Linq;

namespace CarRental
{
    public class RentalService
    {
        public async Task<RentalModel> CreateRentalAsync(
            CarModel car,
            string clientName,
            DateTime startDate,
            DateTime endDate,
            ObservableCollection<RentalModel> activeRentals)
        {
            await Task.Delay(4000);

            var activeForCar = activeRentals.Count(r => r.CarName == car.DisplayName);
            if (activeForCar >= car.FleetSize)
            {
                throw new InvalidOperationException("Для выбранного автомобиля нет свободных единиц в парке.");
            }

            return new RentalModel
            {
                CarName = car.DisplayName,
                ClientName = clientName.Trim(),
                StartDate = startDate.Date,
                EndDate = endDate.Date,
                PricePerDay = car.PricePerDay
            };
        }

        public void ReturnRental(RentalModel rental, ObservableCollection<RentalModel> activeRentals)
        {
            activeRentals.Remove(rental);
        }
    }
}
