namespace WireTap.HostedService
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using WireTap.Logging;

    public class Program
    {
        public static string ServiceName { get; set; }

        public static IHostEnvironment HostEnvironment { get; set; }

        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                  .ConfigureServices((context, services) => services.ConfigureServices(context.Configuration))
                  .UseSerilog((_, config) => config.ConfigureLogger(Debugger.IsAttached ? "WireTap.log" : Helpers.CreateTempFileName("log", "service")))
                  .UseWindowsService(options => options.ServiceName = "AAA001")
                  .Build();

            try
            {
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}