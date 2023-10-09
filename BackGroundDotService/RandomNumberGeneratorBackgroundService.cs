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
        private int _executionCount;

        public RandomNumberGeneratorBackgroundService(ILogger<RandomNumberGeneratorBackgroundService> logger)
        {
            _logger = logger;
            _executionCount = 0;
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
            _logger.LogInformation("TimerBackgroundService is stopping.");

            // Stop the timer
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        // Custom method to stop the background service
        public void Stop()
        {
            _logger.LogInformation("Stopping TimerBackgroundService manually.");
            StopAsync(CancellationToken.None).Wait(); // You may want to use a more graceful way to stop it
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void GenerateAndLogRandomNumber(object state)
        {
            var randomNumber = new Random().Next(1, 1000);
            _logger.LogInformation($"Generated random number: {randomNumber}");
            // Check your custom condition here
            if (_executionCount >= 5)
            {
                Stop();
            }

            _executionCount++;

        }
    }

}