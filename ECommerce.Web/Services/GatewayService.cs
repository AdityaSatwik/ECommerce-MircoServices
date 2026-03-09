using ECommerce.Web.Models;
using ECommerce.Web.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace ECommerce.Web.Services
{
    /// <summary>
    /// Implementation of the IGatewayService that communicates with the API Gateway.
    /// </summary>
    public class GatewayService : IGatewayService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="GatewayService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client instance.</param>
        public GatewayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }
 
        /// <summary>
        /// Retrieves all products from the catalog service with optional filtering.
        /// </summary>
        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync(string? search = null)
        {
            var url = "/api/catalog/catalog";
            if (!string.IsNullOrEmpty(search)) url += $"?search={Uri.EscapeDataString(search)}";
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ProductViewModel>>(content, _options) ?? new List<ProductViewModel>();
        }

        /// <summary>
        /// Retrieves detailed information for a single product.
        /// </summary>
        public async Task<ProductViewModel?> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/catalog/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductViewModel>(content, _options);
        }
 
        /// <summary>
        /// Triggers the creation of a new product in the catalog.
        /// </summary>
        public async Task<bool> CreateProductAsync(CreateProductViewModel product)
        {
            var content = new StringContent(JsonSerializer.Serialize(product, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/catalog/catalog", content);
            return response.IsSuccessStatusCode;
        }
 
        /// <summary>
        /// Fetches the current stock levels from the legacy inventory service.
        /// </summary>
        public async Task<IEnumerable<InventoryItemViewModel>> GetInventoryAsync()
        {
            var response = await _httpClient.GetAsync("/api/inventory/inventory");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<InventoryItemViewModel>>(content, _options) ?? new List<InventoryItemViewModel>();
        }

        /// <summary>
        /// Updates the quantity for an inventory item.
        /// </summary>
        public async Task<bool> UpdateInventoryAsync(int id, int quantity)
        {
            var content = new StringContent(JsonSerializer.Serialize(quantity, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/inventory/inventory/{id}", content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Submits a purchase request to the order service.
        /// </summary>
        public async Task<bool> PlaceOrderAsync(int productId, int quantity)
        {
            var request = new { ProductId = productId, Quantity = quantity };
            var content = new StringContent(JsonSerializer.Serialize(request, _options), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/order/order", content);
            return response.IsSuccessStatusCode;
        }
    }
}
