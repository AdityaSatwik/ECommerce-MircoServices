namespace ECommerce.Web.Models
{
    /// <summary>
    /// View model for product creation input.
    /// </summary>
    public class CreateProductViewModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
