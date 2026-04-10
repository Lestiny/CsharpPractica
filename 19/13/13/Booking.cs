using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarRental
{
    public class Booking : INotifyPropertyChanged
    {
        private string _carName = "";
        private string _clientName = "";
        private DateTime _startDate;
        private DateTime _endDate;

        public string CarName
        {
            get => _carName;
            set { if (_carName != value) { _carName = value; OnPropertyChanged(); } }
        }

        public string ClientName
        {
            get => _clientName;
            set { if (_clientName != value) { _clientName = value; OnPropertyChanged(); } }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set { if (_startDate != value) { _startDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(Days)); } }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set { if (_endDate != value) { _endDate = value; OnPropertyChanged(); OnPropertyChanged(nameof(Days)); } }
        }

        public int Days => Math.Max(1, (EndDate.Date - StartDate.Date).Days + 1);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
