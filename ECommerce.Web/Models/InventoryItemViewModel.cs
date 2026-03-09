namespace ECommerce.Web.Models
{
    /// <summary>
    /// View model for displaying inventory stock levels.
    /// </summary>
    public class InventoryItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
