namespace WireTap.Logging
{
    using System;
    using System.Collections.Generic;

    public static class LogManager
    {
        private static readonly Dictionary<string, ILogger> _loggers;

        static LogManager()
        {
            _loggers = new Dictionary<string, ILogger>();
        }

        public static ILogger GetLogger<T>() =>
            GetLogger(typeof(T));

        public static ILogger GetLogger(Type type)
        {
            var typeName = type.GetFullNameFormatted();

            if (_loggers.TryGetValue(typeName, out var logger))
                return logger;

            _loggers.TryAdd(typeName, new Logger(typeName));

            return GetLogger(type);
        }
    }
}