namespace WireTap.HostedService.Services
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using WireTap.Logging;

    internal class GarbageCollectionService : ServiceBase, IHostedService
    {
        public GarbageCollectionService(ILogger logger) : base(logger)
        {
        }

        protected override async Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var file in Directory.EnumerateFiles(Helpers.BasePath))
                {
                }

                var baseDirectory = Directory.EnumerateDirectories(Helpers.BasePath);

                foreach (var file in baseDirectory)
                await Task.Delay(TimeSpan.FromMinutes(60));
            }
        }
    }
}
