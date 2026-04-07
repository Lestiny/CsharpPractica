using System.IO.MemoryMappedFiles;
using System.Text;

namespace CarRental
{
    public class BookingNotificationService : IDisposable
    {
        private readonly string _mapName;
        private readonly MemoryMappedFile _mmf;
        private long _lastTicks;
        private readonly Timer _timer;

        public event Action<string>? NotificationReceived;

        public BookingNotificationService(string mapName)
        {
            _mapName = mapName;
            _mmf = MemoryMappedFile.CreateOrOpen(_mapName, 4096);
            _timer = new Timer(_ => Poll(), null, 500, 700);
        }

        public void Publish(string message)
        {
            var payload = $"{DateTime.UtcNow.Ticks}|{message}";
            var bytes = Encoding.UTF8.GetBytes(payload);
            using var accessor = _mmf.CreateViewAccessor(0, 4096);
            accessor.Write(0, bytes.Length);
            accessor.WriteArray(4, bytes, 0, Math.Min(bytes.Length, 4000));
        }

        private void Poll()
        {
            try
            {
                using var accessor = _mmf.CreateViewAccessor(0, 4096, MemoryMappedFileAccess.Read);
                var length = accessor.ReadInt32(0);
                if (length <= 0 || length > 4000) return;
                var bytes = new byte[length];
                accessor.ReadArray(4, bytes, 0, length);
                var payload = Encoding.UTF8.GetString(bytes);
                var parts = payload.Split('|', 2);
                if (parts.Length != 2 || !long.TryParse(parts[0], out var ticks)) return;
                if (ticks <= _lastTicks) return;
                _lastTicks = ticks;
                NotificationReceived?.Invoke(parts[1]);
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
            _mmf.Dispose();
        }
    }
}
