namespace BackGroundDotService
{


    public class BackGroundExecuteAsyncSample : BackgroundService
    {
        private readonly ILogger<BackGroundExecuteAsyncSample> _logger;

        public BackGroundExecuteAsyncSample(ILogger<BackGroundExecuteAsyncSample> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            {
                _logger.LogInformation("Background service use ExecuteAsync is running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken); // Delay between log messages
            }
        }
    }
}