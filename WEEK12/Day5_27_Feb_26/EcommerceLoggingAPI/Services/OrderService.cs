using log4net;

namespace EcommerceLoggingAPI.Services
{
    public class OrderService
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(OrderService));

        public bool CreateOrder(int userId)
        {
            try
            {
                log.Info($"Order started for user {userId}");

                bool success = true;

                if (success)
                {
                    log.Info("Order created successfully");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                log.Error("Order creation failed", ex);
                return false;
            }
        }
    }
}