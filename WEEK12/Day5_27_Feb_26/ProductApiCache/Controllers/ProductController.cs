using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProductApiCache.Data;
using ProductApiCache.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ApplicationDbContext _context;

        public ProductController(IMemoryCache cache, ApplicationDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            string cacheKey = "product_list";

            if (!_cache.TryGetValue(cacheKey, out List<Product> products))
            {
                // 🟢 Fetch from DB
                products = _context.Products.ToList();

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, products, options);
            }

            return Ok(products);
        }
    }
}