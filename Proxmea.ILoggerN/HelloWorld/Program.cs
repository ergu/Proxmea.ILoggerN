using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proxmea.ILoggerN.Logger;

namespace HelloWorld
{
    internal class Program
    {
        private static ILogger<Program>? _logger = null!;
        static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            // Add shared NLog setup
            // This is all you need to be able to retrieve the logger from anywhere in your app with DI.
            SharedLogging.ConfigureNLog(builder);
            var app = builder.Build();

            /*  *********************** 
             *  * HEY YOU! Read this! *
             * ***********************
             */

            // Before you run this, look at this applications appsettings.[environment].json file.
            // It contains the NLog configuration, which is merged with the default NLog configuration.
            // The built in/default NLog configuration is used to log messages to the console
            // and your project appsettings.[environment].json file is used to log messages to a file, and gives your the ability to override the NLog configuration.

            // Basically, you specify the basics, the default, in the Proxmea.ILoggerN.Default.AppSettings.json file, which is included in this project
            // and then you can override it in your own appsettings.[environment].json file.

            #region This is how you'd log in a static function, which this is. 
            // We need to initialize the service helper here so that the logger can be retrieved later on. So yes, if you're pure DI then you can skip this part.
            ServicesHelper.Configure(app.Services);
            // Get logger for this context, e.g. Program.cs
            _logger = LoggerHelper.GetLogger<Program>();

            // Just decent startup logging which you should basically include.
            // Just regular logging, nothing special.
            _logger?.LogInformation("Starting");
            _logger?.LogInformation($"Environment: {app.Environment.EnvironmentName}");

            // Add some properties to the logger, like the version of the app
            // There is no need to log the app name or class name, as it is already included in the log context by default.
            _logger?
                // You can inject any properties you want here, like the version of the app, or anything else you need.
                .WithProperty("Version", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version ?? new Version { })
                .LogInformation("Application started with properties.");
            // And this ends up in the log file, as well as the console, if you have configured it to do so.
            #endregion

            #region The logging in a controller later on is easy peasy
            // Look at the HelloWorldController.cs for a very regular basic DI implementation.
            #endregion

            app.MapControllers();
            app.Run();
            _logger?.LogInformation("Stopping...");
        }
    }
}
