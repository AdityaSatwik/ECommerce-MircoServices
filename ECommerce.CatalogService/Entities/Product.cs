namespace ECommerce.CatalogService.Entities
{
    /// <summary>
    /// Represents a product entity in the catalog database.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
