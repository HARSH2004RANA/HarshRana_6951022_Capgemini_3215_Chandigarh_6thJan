namespace ECommerceApp.Models
{
    public class ShippingDetail
    {
        public int ShippingDetailId { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
