namespace Proxmea.ILoggerN.Logger
{
    public class ServicesHelper
    {
        private static IServiceProvider? _serviceProvider;

        public static void Configure(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public static IServiceProvider GetServiceProvider()
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("ServiceProvider is not configured. Call Configure method first.");
            }
            return _serviceProvider;
        }
    }
}
