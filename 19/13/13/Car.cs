using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRental
{
    public class Car : INotifyPropertyChanged
    {
        private bool _isFree;

        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public int PricePerDay { get; set; }
        public CarClass Class { get; set; }
        public int PassengerSeats { get; set; }
        public int FleetSize { get; set; } = 1;

        public bool IsFree
        {
            get => _isFree;
            set
            {
                if (_isFree == value) return;
                _isFree = value;
                OnPropertyChanged();
            }
        }

        public string DisplayName => $"{Make} {Model}".Trim();

        public override string ToString() =>
            $"{DisplayName} - {PricePerDay} BYN/сут, класс: {Class}, мест: {PassengerSeats}, в парке: {FleetSize}";

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
