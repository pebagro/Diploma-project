
using EFinzynierka.Models;
using EFinzynierka.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace EFinzynierka.Services
{
    public class RFIDReaderService : IHostedService, IDisposable
    {
        private readonly ILogger<RFIDReaderService> _logger;
        private readonly string _portName;
        private readonly IServiceProvider _serviceProvider;
        private SerialPort _serialPort;
        private Task _executingTask;
        private CancellationTokenSource _cts;

        public RFIDReaderService(ILogger<RFIDReaderService> logger, string portName, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _portName = portName;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Otwórz port szeregowy
            _serialPort = new SerialPort(_portName, 9600);
            if (_serialPort.IsOpen)
            {
                _logger.LogWarning("RFID reader port is already open.");
                return Task.CompletedTask; ;
            }

            try
            {
                _serialPort.Open();
            }
            catch (IOException ex)
            {
                _logger.LogError(ex, "Error opening RFID reader port.");
                return Task.CompletedTask; ;
            }

            _logger.LogInformation("RFID reader service started");

            // Uruchom zadanie w tle
            _cts = new CancellationTokenSource();
            _executingTask = ExecuteAsync(_cts.Token);

            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Anuluj zadanie w tle, aby zatrzymać usługę
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _cts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(-1, cancellationToken));
            }

            _logger.LogInformation("RFID reader service stopped");
        }

        public void Dispose()
        {
            _serialPort.Close();
        }

        private async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // Czekaj na odczytanie karty RFID
                string data = _serialPort.ReadLine();
                _logger.LogInformation($"Received data from RFID reader: {data}");

                
                // Parse the data to extract the RFID card ID
    int prefixLength = "RFID Tag UID: ".Length;
                string cardId = data.Substring(prefixLength).Trim();
                cardId = cardId.Replace(" ", "");
                // Zapisz odczyt karty do bazy danych
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DbEFinzynierkaContext>();
                    var employee = context.Employees.Where(e => e.RFIDLog.RFIDCardID == cardId).FirstOrDefault();
                    if (employee == null)
                    {
                        _logger.LogInformation($"Nie znaleziono pracownika z kartą RFID o ID {cardId}.");
                        continue;
                    }

                    var rfidLog = new RFIDLog
                    {
                        EmployeeID = employee.Id,
                        RFIDCardID = cardId,
                        Timestamp = DateTime.UtcNow,
                        IsEntry = _portName == "COM6" // ustaw wartość true dla odczytów z COM6, false dla pozostałych portów
                    };
                    context.RFIDLogs.Add(rfidLog);
                    context.SaveChanges();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
    