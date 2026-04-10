using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRental
{
    public class CarModel : INotifyPropertyChanged
    {
        private bool _isFree = true;
        private int _occupiedCount;

        public string Make { get; set; } = "";
        public string Model { get; set; } = "";
        public int PricePerDay { get; set; }
        public int FleetSize { get; set; } = 1;

        public int OccupiedCount
        {
            get => _occupiedCount;
            set
            {
                if (_occupiedCount == value) return;
                _occupiedCount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsFree));
                OnPropertyChanged(nameof(IsFullyOccupied));
                OnPropertyChanged(nameof(IsPartiallyOccupied));
            }
        }

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

        public bool IsFullyOccupied => OccupiedCount >= FleetSize && FleetSize > 0;

        public bool IsPartiallyOccupied => OccupiedCount > 0 && OccupiedCount < FleetSize;

        public string DisplayName => $"{Make} {Model}".Trim();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
