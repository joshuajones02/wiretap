namespace WireTap.Logging
{
    public interface ILogger
    {
        void LogError(object data);
        void LogError(object data, string template);
        void LogInformation(object data);
        void LogInformation(object data, string template);
    }

    public interface ILogger<T> : ILogger
    {
    }
}
