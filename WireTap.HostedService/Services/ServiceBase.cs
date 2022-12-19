namespace WireTap.HostedService
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using WireTap.Logging;

    internal abstract class ServiceBase : BackgroundService
    {
        private readonly ILogger _logger;

        public ServiceBase(ILogger logger)
        {
            _logger = logger;
        }

        protected abstract Task InternalExecuteAsync(CancellationToken cancellationToken);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
                var serviceName = Program.ServiceName;
                var logFileName = Helpers.CreateTempFileName(".log", "system-log-", serviceName);
                using (var fileStream = new FileStream(logFileName, FileMode.OpenOrCreate, FileAccess.Write))
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    var dateTime = DateTime.Now;
                    Console.SetOut(streamWriter);
                Console.WriteLine($"Starting WireTap Services at {dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}");
                _logger.LogInformation($"Starting WireTap Services at {dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}");

                try
                {
                    Console.WriteLine($"Starting {serviceName}");
                    _logger.LogInformation($"Starting {serviceName}");

                    await InternalExecuteAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Stopping {serviceName}");
                    Console.WriteLine(string.Concat($"{serviceName} : {ex.Message}", '\n' + ex.StackTrace));
                    Console.WriteLine("Program : A fatal exception has occurred" + '\n' + ex.Message + '\n' + ex.StackTrace);

                    _logger.LogError($"Stopping {serviceName}");
                    _logger.LogError(string.Concat($"{serviceName} : {ex.Message}", '\n' + ex.StackTrace));
                    _logger.LogError("Program : A fatal exception has occurred" + '\n' + ex.Message + '\n' + ex.StackTrace);

                    await Task.Delay(60_000);
                    await ExecuteAsync(cancellationToken);
                }
                finally
                {
                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
                }
            }
        }
    }
}