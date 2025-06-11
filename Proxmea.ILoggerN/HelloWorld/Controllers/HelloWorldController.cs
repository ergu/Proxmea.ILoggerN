using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloWorld.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        // Logger for this controller
        private readonly ILogger<HelloWorldController> _logger;

        // Constructor makes use of DI to get a ILogger<HelloWorldController> 
        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Dummy endpoint to return a simple string "HelloBack". Test it at http://localhost:5000/HelloWorld
        /// ...but it's logging with DI with NLog and all that it brings ;)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetHello()
        {
            // Log an information message
            _logger.LogInformation("GetHello called.");

            return Ok("HelloBack");
        }
    }
}
