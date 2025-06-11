using Microsoft.Extensions.Logging;

namespace Proxmea.ILoggerN
{
    public static class LoggerExtensions
    {
        public static LoggerWithProperties WithProperty(this ILogger logger, string key, object value)
        {
            // Start with empty property bag
            return new LoggerWithProperties(logger, new Dictionary<string, object> { [key] = value });
        }
    }
}
