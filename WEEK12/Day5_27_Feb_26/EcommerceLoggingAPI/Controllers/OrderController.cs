using Microsoft.AspNetCore.Mvc;
using EcommerceLoggingAPI.Services;
using log4net;

namespace EcommerceLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(OrderController));

        private readonly OrderService orderService =
            new OrderService();

        [HttpPost]
        public IActionResult CreateOrder(int userId)
        {
            var result =
                orderService.CreateOrder(userId);

            if (result)
            {
                log.Info("Order success");
                return Ok();
            }

            log.Error("Order failed");
            return StatusCode(500);
        }
    }
}