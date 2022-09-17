namespace WireTap.HostedService
{
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.Threading.Tasks;
    using WireTap.Logging;

    public class Program
    {
        static Program()
        {
            ServiceName = Environment.GetEnvironmentVariable("SERVICE_NAME");
        }

        public static string ServiceName { get; }

        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                  .ConfigureServices((_, services) => services.ConfigureServices())
                  .UseSerilog((_, config) => config.ConfigureLogger())
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