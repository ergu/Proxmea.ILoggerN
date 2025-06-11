# Proxmea.ILoggerN

## Overview

Proxmea.ILoggerN provides a shared, configurable logging setup for .NET 8 applications using NLog. 
It enables consistent, centralized logging across your application, supporting both console, file and Elastic outputs by default. 
It also adds missing functionality to be able to log individual properties. 
The package is designed for easy integration with ASP.NET Core projects and leverages dependency injection for logger access.

It's utilizing NLog as the logging provider, mainly because of its high performance.

## What It Does

- Sets up NLog as the logging provider for your .NET application.
- ILogger doesn't play well with `WithProperty`, so we've implemented that here. Easy peasy to use.
- Merges default logging configuration with your environment-specific `appsettings.[environment].json`. This effectivly makes your NLog section very small.
- Allows you to retrieve and use loggers anywhere in your app, including static contexts.
- Supports logging to both console, file and Elastic by default, with configuration overrides per environment.
- Provides helpers for adding custom properties to log entries (e.g., application version).

## Usage

1. **Add Controllers and Logging:**
   In your `Program.cs`, add controllers and configure logging:
```
var builder = WebApplication.CreateBuilder(args); 
SharedLogging.ConfigureNLog(builder); 
1. var app = builder.Build();
```


2. **Initialize Logger in Static Contexts:**
```
ServicesHelper.Configure(app.Services); 
var logger = LoggerHelper.GetLogger<Program>(); 
logger.LogInformation("Starting");
```


3. **Log with Custom Properties:**
```
logger 
  .WithProperty("Version", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) 
  .LogInformation("Application started with properties.");
```


4. **Controller Logging (Dependency Injection):**
   In your controller, inject `ILogger<T>` and use as needed:
```
public class HelloWorldController : Controller { private readonly ILogger<HelloWorldController> _logger; 
	public HelloWorldController(ILogger<HelloWorldController> logger) { 
		_logger = logger; 
	}
	
	[HttpGet] 
	public IActionResult GetHello() { 
		_logger.LogInformation("GetHello called."); 
		return Ok("HelloBack"); 
	} 
}
```


5. **Configuration:**
   - Default NLog settings are in `ILoggerN.Default.AppSettings.json`.
	
	 This serves as boilerplate config for NLog.
   - Override or extend logging in your own `appsettings.[environment].json`.

     This is where you'd put your application specific minimal NLog config.

# 📜License 
This project is available under a **Mozilla Public License 2.0 (MPL-2.0)**:  
 
  - ✅ **You are free to use this project** (or snippets from it) in any application, including commercial ones.
  - ✅ **You must provide attribution** by linking back to this repository.
  - ✅ **If you modify and distribute this code**, you **must open-source** the modified files under the same license.
  - 🔄 **Forks and improvements are encouraged** – If you enhance this project, consider submitting a **pull request** so everyone benefits.  
  - 📖 Full license text: [MPL-2.0](https://choosealicense.com/licenses/mpl-2.0/)
	 
## 📢 Author
Developed and maintained by **jrnker**@Proxmea. For inquiries, issues, or contributions, check out the repository or open a pull request.  
