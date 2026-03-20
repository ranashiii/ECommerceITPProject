namespace ECommOrderManagementData.Models
{
    public class OrderItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
    }
}
