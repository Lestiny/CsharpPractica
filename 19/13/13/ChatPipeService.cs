using System.IO.Pipes;
using System.IO;
using System.Text;

namespace CarRental
{
    public class ChatPipeService : IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly string _pipeName;

        public event Action<string>? MessageReceived;

        public ChatPipeService(string pipeName)
        {
            _pipeName = pipeName;
            _ = ListenLoopAsync();
        }

        public async Task SendAsync(string message)
        {
            try
            {
                using var client = new NamedPipeClientStream(".", _pipeName, PipeDirection.Out, PipeOptions.Asynchronous);
                await client.ConnectAsync(500);
                var bytes = Encoding.UTF8.GetBytes(message);
                await client.WriteAsync(bytes);
                await client.FlushAsync();
            }
            catch
            {
            }
        }

        private async Task ListenLoopAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    using var server = new NamedPipeServerStream(_pipeName, PipeDirection.In, 10, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
                    await server.WaitForConnectionAsync(_cts.Token);

                    using var reader = new StreamReader(server, Encoding.UTF8);
                    var msg = await reader.ReadToEndAsync(_cts.Token);
                    if (!string.IsNullOrWhiteSpace(msg))
                        MessageReceived?.Invoke(msg);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch
                {
                    await Task.Delay(200, _cts.Token);
                }
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
