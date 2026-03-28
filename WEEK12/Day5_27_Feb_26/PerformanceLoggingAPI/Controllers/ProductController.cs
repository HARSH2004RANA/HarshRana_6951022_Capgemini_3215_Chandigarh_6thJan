using Microsoft.AspNetCore.Mvc;

namespace PerformanceLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("fast")]
        public IActionResult FastAPI()
        {
            return Ok("Fast API Response");
        }

        [HttpGet("slow")]
        public IActionResult SlowAPI()
        {
            // Simulate slow API

            Thread.Sleep(5000);

            return Ok("Slow API Response");
        }

        [HttpGet("error")]
        public IActionResult ErrorAPI()
        {
            throw new Exception(
                "Simulated Exception");
        }
    }
}