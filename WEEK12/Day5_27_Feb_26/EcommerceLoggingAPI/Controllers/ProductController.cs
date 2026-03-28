using Microsoft.AspNetCore.Mvc;
using EcommerceLoggingAPI.Models;
using log4net;

namespace EcommerceLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(ProductController));

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            log.Info($"Product fetch request: {id}");

            if (id > 5)
            {
                log.Warn("Product not found");
                return NotFound();
            }

            var product = new Product
            {
                Id = id,
                Name = "Sample Product"
            };

            return Ok(product);
        }
    }
}