using log4net;

namespace EcommerceLoggingAPI.Services
{
    public class PaymentService
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(PaymentService));

        public bool ProcessPayment(int orderId)
        {
            try
            {
                log.Info($"Payment request for order {orderId}");

                var start = DateTime.Now;

                Thread.Sleep(6000);

                var duration =
                    (DateTime.Now - start).TotalSeconds;

                if (duration > 5)
                {
                    log.Warn("Payment delay > 5 sec");
                }

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Payment failed", ex);
                return false;
            }
        }
    }
}