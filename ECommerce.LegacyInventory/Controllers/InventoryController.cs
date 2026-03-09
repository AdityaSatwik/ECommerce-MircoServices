using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using ECommerce.LegacyInventory.Data;
using ECommerce.LegacyInventory.Models;

namespace ECommerce.LegacyInventory.Controllers
{
    /// <summary>
    /// Controller for managing legacy inventory via OWIN/WebAPI.
    /// </summary>
    public class InventoryController : ApiController
    {
        private readonly InventoryRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryController"/> class.
        /// </summary>
        public InventoryController()
        {
            // Poor man's DI for legacy app
            _repository = new InventoryRepository(new SqlConnectionFactory());
        }

        /// <summary>
        /// Retrieves all inventory stock items.
        /// </summary>
        /// <returns>A collection of inventory items.</returns>
        [HttpGet]
        public async Task<IEnumerable<InventoryItem>> Get()
        {
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// Updates the stock quantity for a specific record.
        /// </summary>
        /// <param name="id">The record ID.</param>
        /// <param name="quantity">The new quantity.</param>
        /// <returns>An HTTP response indicating success.</returns>
        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody] int quantity)
        {
            await _repository.UpdateAsync(id, quantity);
            return Ok();
        }

        /// <summary>
        /// Records a new inventory entry.
        /// </summary>
        /// <param name="item">The inventory item to add.</param>
        /// <returns>An HTTP response indicating success.</returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] InventoryItem item)
        {
            await _repository.AddAsync(item.ProductId, item.Quantity);
            return Ok();
        }
    }
}
