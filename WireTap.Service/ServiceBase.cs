namespace WireTap.Service
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal abstract class ServiceBase
    {
        internal abstract Task ExecuteAsync();

        public async Task TryExecuteAsync()
        {
            var serviceName = GetType().Name;

            try
            {
                Console.WriteLine($"Starting {serviceName}");
                await ExecuteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Stopping {serviceName}");
                Console.WriteLine(string.Concat($"{serviceName} : {ex.Message}", '\n' + ex.StackTrace));
                await TryExecuteAsync();
            }
        }
    }
}