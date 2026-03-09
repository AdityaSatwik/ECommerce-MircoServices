namespace ECommerce.OrderService.Models
{
    /// <summary>
    /// Request object for placing a new order.
    /// </summary>
    public class OrderRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
