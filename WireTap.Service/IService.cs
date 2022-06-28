namespace WireTap.Service
{
    using System.Threading.Tasks;

    public interface IService
    {
        Task TryExecuteAsync();
    }
}