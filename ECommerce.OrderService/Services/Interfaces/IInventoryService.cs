using System.Text.Json;

namespace ECommerce.OrderService.Services.Interfaces
{
    /// <summary>
    /// InventoryService
    /// </summary>
    public interface IInventoryService
    {
        Task<int?> GetStockAsync(int productId);
        Task<bool> UpdateStockAsync(int productId, int newQuantity);
    }
}
