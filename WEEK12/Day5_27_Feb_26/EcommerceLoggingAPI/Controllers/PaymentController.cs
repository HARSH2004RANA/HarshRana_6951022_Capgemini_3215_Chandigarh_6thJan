using Microsoft.AspNetCore.Mvc;
using EcommerceLoggingAPI.Services;
using log4net;

namespace EcommerceLoggingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(PaymentController));

        private readonly PaymentService service =
            new PaymentService();

        [HttpPost]
        public IActionResult Pay(int orderId)
        {
            var result =
                service.ProcessPayment(orderId);

            if (!result)
            {
                log.Error("Payment failure");
                return StatusCode(500);
            }

            return Ok();
        }
    }
}