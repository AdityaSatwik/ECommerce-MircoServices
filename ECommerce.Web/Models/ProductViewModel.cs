namespace ECommerce.Web.Models
{
    /// <summary>
    /// View model representing a product in the web interface.
    /// </summary>
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = "No description available for this premium product.";
    }
}
