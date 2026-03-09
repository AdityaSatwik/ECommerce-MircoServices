namespace ECommerce.LegacyInventory.Models
{
    /// <summary>
    /// Data model for a legacy inventory record.
    /// </summary>
    public class InventoryItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
