using Microsoft.AspNetCore.Mvc;
using ECommerce.Web.Services.Interfaces;

namespace ECommerce.Web.Controllers
{
    /// <summary>
    /// Controller for managing the legacy inventory stock levels.
    /// </summary>
    public class InventoryController : Controller
    {
        private readonly IGatewayService _gatewayService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        /// <param name="gatewayService">The gateway service.</param>
        public InventoryController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        /// <summary>
        /// Displays the inventory list.
        /// </summary>
        /// <returns>The inventory index view.</returns>
        public async Task<IActionResult> Index()
        {
            var inventory = await _gatewayService.GetInventoryAsync();
            return View(inventory);
        }

        /// <summary>
        /// Updates the stock quantity for an item.
        /// </summary>
        /// <param name="id">The inventory item ID.</param>
        /// <param name="quantity">The new stock quantity.</param>
        /// <returns>Redirects to the inventory list.</returns>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(int id, int quantity)
        {
            var success = await _gatewayService.UpdateInventoryAsync(id, quantity);
            return RedirectToAction(nameof(Index));
        }
    }
}
