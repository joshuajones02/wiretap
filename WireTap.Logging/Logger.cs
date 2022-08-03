namespace WireTap.Logging
{
    using Serilog;

    public class Logger<T> : Logger, ILogger<T>
    {
        internal Logger() : base(typeof(T).Name)
        {
        }
    }

    public class Logger : ILogger
    {
        private readonly string _typeName;

        public Logger(string typeName)
        {
            _typeName = typeName;
        }

        protected string PrependTypeName(string template) => $"{_typeName} : {template}";

        public void LogError(object data) => LogError(data, null);

        public void LogError(object data, string template) => Log.Error(PrependTypeName(template), data);

        public void LogInformation(object data) => LogInformation(null, data.ToJson());

        public void LogInformation(object data, string template) => Log.Information(PrependTypeName(template), data);
    }
}
