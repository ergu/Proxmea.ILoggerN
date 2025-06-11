using Microsoft.Extensions.Logging;

namespace Proxmea.ILoggerN.Logger
{
    public class LoggerWithProperties
    {
        private readonly ILogger _logger;
        private readonly IReadOnlyDictionary<string, object> _properties;

        public LoggerWithProperties(ILogger logger, IReadOnlyDictionary<string, object> properties)
        {
            _logger = logger;
            _properties = properties;
        }

        public LoggerWithProperties WithProperty(string key, object value)
        {
            var dict = new Dictionary<string, object>(_properties)
            {
                [key] = value
            };
            return new LoggerWithProperties(_logger, dict);
        }

        // TRACE
        public void LogTrace(string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogTrace(message, args);
        }
        public void LogTrace(Exception exception, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogTrace(exception, message, args);
        }

        // DEBUG
        public void LogDebug(string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogDebug(message, args);
        }
        public void LogDebug(Exception exception, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogDebug(exception, message, args);
        }

        // INFO
        public void LogInformation(string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogInformation(message, args);
        }
        public void LogInformation(Exception exception, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogInformation(exception, message, args);
        }

        // WARNING
        public void LogWarning(string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogWarning(message, args);
        }
        public void LogWarning(Exception exception, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogWarning(exception, message, args);
        }

        // ERROR
        public void LogError(string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogError(message, args);
        }
        public void LogError(Exception exception, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogError(exception, message, args);
        }

        // CRITICAL
        public void LogCritical(string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogCritical(message, args);
        }
        public void LogCritical(Exception exception, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.LogCritical(exception, message, args);
        }

        // GENERIC LOG
        public void Log(LogLevel logLevel, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.Log(logLevel, message, args);
        }
        public void Log(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            using (_logger.BeginScope(_properties))
                _logger.Log(logLevel, exception, message, args);
        }
    }
}
