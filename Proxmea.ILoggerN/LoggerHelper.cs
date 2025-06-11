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
    }
}
