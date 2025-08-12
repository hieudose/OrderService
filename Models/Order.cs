namespace LegacyOrderService.Models
{
    public class Order
    {
        public int Id { get; set; } // PK
        public string CustomerName { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total => Price * Quantity;
    }
}
