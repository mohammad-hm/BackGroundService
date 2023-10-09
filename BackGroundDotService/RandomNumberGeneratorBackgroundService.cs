using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackGroundDotService
{



    public class RandomNumberGeneratorBackgroundService : IHostedService, IDisposable
    {
        private readonly ILogger<RandomNumberGeneratorBackgroundService> _logger;
        private Timer? _timer;

        public RandomNumberGeneratorBackgroundService(ILogger<RandomNumberGeneratorBackgroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RandomNumberGeneratorBackgroundService is starting.");

            // Create a timer to generate and log random numbers every 5 seconds
            _timer = new Timer(callback: GenerateAndLogRandomNumber, state: null, dueTime: TimeSpan.Zero, period: TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RandomNumberGeneratorBackgroundService is stopping.");

            // Stop the timer
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void GenerateAndLogRandomNumber(object state)
        {
            var randomNumber = new Random().Next(1, 1000);
            _logger.LogInformation($"Generated random number: {randomNumber}");
        }
    }

}