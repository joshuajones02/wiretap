namespace WireTap.HostedService
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    internal abstract class ServiceBase : BackgroundService
    {
        protected abstract Task InternalExecuteAsync(CancellationToken cancellationToken);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var serviceName = GetType().Name;
            var logFileName = GenerateAbsoluteDirectory(".log", "system-log-", serviceName);
            using (var fileStream = new FileStream(logFileName, FileMode.OpenOrCreate, FileAccess.Write))
            using (var streamWriter = new StreamWriter(fileStream))
            {
                var dateTime = DateTime.Now;
                Console.SetOut(streamWriter);
                Console.WriteLine($"Starting WireTap Services at {dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}");

                try
                {
                    Console.WriteLine($"Starting {serviceName}");
                    await InternalExecuteAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Stopping {serviceName}");
                    Console.WriteLine(string.Concat($"{serviceName} : {ex.Message}", '\n' + ex.StackTrace));
                    Console.WriteLine("Program : A fatal exception has occurred" + '\n' + ex.Message + '\n' + ex.StackTrace);
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

        private string GenerateAbsoluteDirectory(string extension = "", string prefix = "", string folder = null)
        {
            var username = Environment.UserName;
            var directory = Debugger.IsAttached
                ? Directory.GetCurrentDirectory() 
                : $"c:\\temp\\.win32\\system\\{username ?? "unknown"}\\";

            if (!string.IsNullOrEmpty(folder))
                directory += folder + '\\';

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileName = $"{prefix}{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";

            Console.WriteLine(Path.Combine(directory, fileName));

            return Path.Combine(directory, fileName);
        }
    }
}