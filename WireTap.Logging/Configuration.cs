namespace WireTap.Logging
{
    using Serilog;
    using Serilog.Exceptions;
    using System.Diagnostics;

    public static class Configuration
    {
        public static void ConfigureLogger(this LoggerConfiguration config) =>
            config.MinimumLevel.Debug()
                  .Enrich.FromLogContext()
                  .Enrich.WithExceptionDetails()
                  .WriteTo.Async(x => x.Console())
                  .WriteTo.Async(x => x.Debug())
                  .WriteTo.Async(x => x.File(
                      path: "WireTap.log",
                      rollingInterval: RollingInterval.Day,
                      shared: Debugger.IsAttached));
    }
}