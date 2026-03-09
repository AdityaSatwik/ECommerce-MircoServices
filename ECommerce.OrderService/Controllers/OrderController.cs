using ECommerce.OrderService.Models;
using ECommerce.OrderService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.OrderService.Controllers
{
    /// <summary>
    /// API Controller for processing customer orders.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<OrderController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="inventoryService">The inventory service.</param>
        /// <param name="logger">The logger instance.</param>
        public OrderController(IInventoryService inventoryService, ILogger<OrderController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        /// <summary>
        /// Processes an order request, validating stock before confirmation.
        /// </summary>
        /// <param name="request">The order placement details.</param>
        /// <returns>An order confirmation if successful.</returns>
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest request)
        {
            _logger.LogInformation("Placing order for Product: {ProductId}, Quantity: {Quantity}", request.ProductId, request.Quantity);

            // 1. Get current inventory
            var currentQty = await _inventoryService.GetStockAsync(request.ProductId);
            
            if (currentQty == null) return BadRequest("Product not found in inventory");
            
            if (currentQty < request.Quantity) return BadRequest("Insufficient stock");

            // 2. Deduct stock
            int newQty = currentQty.Value - request.Quantity;
            var success = await _inventoryService.UpdateStockAsync(request.ProductId, newQty);

            if (!success) return StatusCode(500, "Failed to update inventory");

            return Ok(new OrderResponse { OrderId = Guid.NewGuid(), Status = "Success" });
        }
    }
}
