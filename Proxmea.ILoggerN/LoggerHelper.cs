using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Proxmea.ILoggerN
{
    public class LoggerHelper
    {
        public static ILogger<T> GetLogger<T>()
        {
            if (ServicesHelper.GetServiceProvider() == null)
                throw new InvalidOperationException("LoggerHelper is not configured.");

            return ServicesHelper.GetServiceProvider().GetRequiredService<ILogger<T>>();
        }
        public static ILogger GetLogger(Type type)
        {
            if (ServicesHelper.GetServiceProvider() == null)
                throw new InvalidOperationException("LoggerHelper is not configured.");
            return (ILogger)ServicesHelper.GetServiceProvider().GetRequiredService(typeof(ILogger<>).MakeGenericType(type));
        }
    }
}
