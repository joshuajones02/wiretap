namespace WireTap.WorkerService
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebCamWorker : BackgroundService
    {
        private readonly ILogger<WebCamWorker> _logger;

        public WebCamWorker(ILogger<WebCamWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                var tempFile = Helpers.CreateTempFileName(".jpeg", "webcam-");
                WebCam.CaptureImage(tempFile);
                Console.WriteLine("[+] WebCam captured at: {0}", tempFile);

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}