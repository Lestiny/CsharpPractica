using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRental
{
    public class RentalModel : INotifyPropertyChanged
    {
        private string _carName = "";
        private string _clientName = "";
        private DateTime _startDate;
        private DateTime _endDate;
        private int _pricePerDay;

        public string CarName
        {
            get => _carName;
            set
            {
                if (_carName == value) return;
                _carName = value;
                OnPropertyChanged();
            }
        }

        public string ClientName
        {
            get => _clientName;
            set
            {
                if (_clientName == value) return;
                _clientName = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate == value) return;
                _startDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Days));
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate == value) return;
                _endDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Days));
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        public int Days => Math.Max(1, (EndDate.Date - StartDate.Date).Days + 1);

        public int PricePerDay
        {
            get => _pricePerDay;
            set
            {
                if (_pricePerDay == value) return;
                _pricePerDay = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        public int TotalCost => PricePerDay * Days;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
