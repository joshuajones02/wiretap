namespace WireTap.HostedService
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using WireTap.Logging;

    internal class ScreenshotService : ServiceBase, IHostedService
    {
        private readonly ILogger _logger;

        public ScreenshotService(ILogger logger) : base(logger)
        {
            _logger = logger;
        }

        protected override async Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("ScreenshotService : StartScreenshotServiceAsync");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("ScreenshotService : Inside captureScreenshot");
                    _logger.LogInformation("ScreenshotService : Inside captureScreenshot");
                    
                    var filename = Helpers.CreateTempFileName(".jpeg", "screenshot-", "screenshot");
                    Display.CaptureImage(filename);

                    Console.WriteLine("ScreenshotService : [+] Screenshot captured at: {0}", filename);
                    _logger.LogInformation("ScreenshotService : [+] Screenshot captured at: {0}", filename);

                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + '\n' + ex.StackTrace);
                    _logger.LogError(ex, null);

                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
        }
    }
}
