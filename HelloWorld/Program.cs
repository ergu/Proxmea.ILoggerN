using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proxmea.ILoggerN;

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

            // Basically, you specify the basics, the default, in the ILoggerN.Default.AppSettings.json file, which is included in this project
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
                .LogInformation("This is a simple log message with properties.");
            // And this ends up in the log file, as well as the console, if you have configured it to do so.
            _logger?.LogWarning("This is a simple log message with a warning.");
            _logger?.LogError("This is a simple log message with a error.");
            _logger?.LogCritical("This is a simple log message with a critical error.");
            #endregion


            #region The logging in a controller later on is easy peasy
            // Look at the HelloWorldController.cs for a very regular basic DI implementation.
            #endregion

            #region Demonstrate logging unhandled exceptions
            // Start a task and throw an exception there, to show that unhandled exceptions are logged too.
            Task.Run(() =>
            {
                // Wait a bit to let the main thread finish first
                Task.Delay(2000).Wait();
                // Note, this will only be caught on the next GC taking care of this task
                // which can take some time. It's just the way it is, but it is still caught and logged.
                throw new Exception("This is an unhandled exception from a task.");
            });

            // Start a GC event after 5 seconds (so that you don't have to wait foreeeever for this demo to prove itself)
            Task.Run(() =>
            {
                // Wait a bit to let the main app getting started first
                Task.Delay(5000).Wait();
                GC.Collect();
            });

            // Start a thread and throw an exception there, to show that unhandled exceptions are logged too.
            // This one will be fatal for the application, but it will be logged first ;).  
            //Thread thread = new Thread(() =>
            //{
            //    // Wait a bit to let the main app getting started first
            //    Thread.Sleep(7000);
            //    throw new Exception("This is an unhandled exception from a thread.");
            //});
            //thread.Start();
            #endregion

            app.MapControllers();
            app.Run();
            _logger?.LogInformation("Stopping...");
        }
    }
}
