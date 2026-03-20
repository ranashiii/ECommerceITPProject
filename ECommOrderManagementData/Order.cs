namespace ECommOrderManagementData.Models
{
    public class Order
    {
        public string OrderID { get; set; }
        public string TrackingID { get; set; }
        public string SellerName { get; set; }

        public Address DeliveryAddress { get; set; }
        public List<OrderItem> Items { get; set; }

        public string ShippingType { get; set; }
        public double ShippingFee { get; set; }

        public string PaymentMethod { get; set; }

        public double MerchandiseSubtotal { get; set; }
        public double Discount { get; set; }
        public double TotalPayment { get; set; }

        public int TotalItemCount { get; set; }

        public string OrderStatus { get; set; }
        public string EstimatedDelivery { get; set; }
    }
}
