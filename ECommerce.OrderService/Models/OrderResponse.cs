namespace ECommerce.OrderService.Models
{
    /// <summary>
    /// Response object returned after an order placement.
    /// </summary>
    public class OrderResponse
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
