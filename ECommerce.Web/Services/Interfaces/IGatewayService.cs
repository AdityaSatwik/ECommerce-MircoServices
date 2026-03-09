using ECommerce.Web.Models;

namespace ECommerce.Web.Services.Interfaces
{
    /// <summary>
    /// interface for the API Gateway client service.
    /// </summary>
    public interface IGatewayService
    {
        /// <summary>Retrieves all products.</summary>
        Task<IEnumerable<ProductViewModel>> GetProductsAsync(string? search = null);
        /// <summary>Retrieves a product by ID.</summary>
        Task<ProductViewModel?> GetProductByIdAsync(int id);
        /// <summary>Creates a new product.</summary>
        Task<bool> CreateProductAsync(CreateProductViewModel product);
        /// <summary>Retrieves the inventory list.</summary>
        Task<IEnumerable<InventoryItemViewModel>> GetInventoryAsync();
        /// <summary>Updates inventory stock.</summary>
        Task<bool> UpdateInventoryAsync(int id, int quantity);
        /// <summary>Places a new order.</summary>
        Task<bool> PlaceOrderAsync(int productId, int quantity);
    }
}
