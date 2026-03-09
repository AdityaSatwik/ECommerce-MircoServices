using System.Text.Json;
using System.Text;
using ECommerce.OrderService.Services.Interfaces;

namespace ECommerce.OrderService.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(HttpClient httpClient, ILogger<InventoryService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        /// <summary>
        /// Get the stocks
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int?> GetStockAsync(int productId)
        {
            var response = await _httpClient.GetAsync("/inventory");
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Could not reach inventory service");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<JsonElement>>(content);
            
            var item = items?.FirstOrDefault(i => i.GetProperty("productId").GetInt32() == productId);
            
            if (item == null) return null;
            
            return item.Value.GetProperty("quantity").GetInt32();
        }

        /// <summary>
        /// To update the stock
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newQuantity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateStockAsync(int productId, int newQuantity)
        {
             // First need to get the item ID from product ID
            var response = await _httpClient.GetAsync("/inventory");
            if (!response.IsSuccessStatusCode) return false;

            var content = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<JsonElement>>(content);
            var item = items?.FirstOrDefault(i => i.GetProperty("productId").GetInt32() == productId);
            
            if (item == null) return false;
            int itemId = item.Value.GetProperty("id").GetInt32();

            var updateContent = new StringContent(JsonSerializer.Serialize(newQuantity), Encoding.UTF8, "application/json");
            var updateResponse = await _httpClient.PutAsync($"/inventory/{itemId}", updateContent);

            return updateResponse.IsSuccessStatusCode;
        }
    }
}
