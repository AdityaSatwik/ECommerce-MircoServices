using Microsoft.AspNetCore.Mvc;
using ECommerce.Web.Services.Interfaces;
using ECommerce.Web.Models;

namespace ECommerce.Web.Controllers
{
    /// <summary>
    /// Controller for the Catalog area of the web application.
    /// </summary>
    public class CatalogController : Controller
    {
        private readonly IGatewayService _gatewayService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogController"/> class.
        /// </summary>
        /// <param name="gatewayService">The service for interacting with the API Gateway.</param>
        public CatalogController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        /// <summary>
        /// Displays the product catalog index page.
        /// </summary>
        /// <param name="search">Optional search term to filter products.</param>
        /// <returns>The catalog view with a list of products.</returns>
        public async Task<IActionResult> Index(string? search)
        {
            var products = await _gatewayService.GetProductsAsync(search);
            ViewData["Search"] = search;
            return View(products);
        }

        /// <summary>
        /// Displays details for a specific product.
        /// </summary>
        /// <param name="id">The unique identifier of the product.</param>
        /// <returns>The product details view.</returns>
        public async Task<IActionResult> Details(int id)
        {
            var product = await _gatewayService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        /// <summary>
        /// Logic for placing an order from the catalog list.
        /// </summary>
        /// <param name="id">The product ID to purchase.</param>
        /// <returns>Redirects back to the index with feedback messages.</returns>
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int id)
        {
            var success = await _gatewayService.PlaceOrderAsync(id, 1); // Buy 1 for POC
            if (success) TempData["Message"] = "Order placed successfully!";
            else TempData["Error"] = "Order failed (Insufficient stock?)";
            return RedirectToAction(nameof(Index));
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await _gatewayService.CreateProductAsync(model);
                if (success) return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
