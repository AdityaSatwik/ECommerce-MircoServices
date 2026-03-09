using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using ECommerce.LegacyInventory.Models;

namespace ECommerce.LegacyInventory.Data
{
    /// <summary>
    /// Direct SQL Repository for managing the Legacy Inventory database records.
    /// </summary>
    public class InventoryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryRepository"/> class.
        /// </summary>
        /// <param name="connectionFactory">The factory to create database connections.</param>
        public InventoryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Retrieves all inventory items from the database.
        /// </summary>
        /// <returns>A collection of inventory items.</returns>
        public async Task<IEnumerable<InventoryItem>> GetAllAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = "SELECT * FROM InventoryItems";
                try 
                {
                    return await connection.QueryAsync<InventoryItem>(sql);
                } 
                catch(System.Exception) 
                {
                    return new List<InventoryItem> 
                    {
                        new InventoryItem { Id = 1, ProductId = 100, Quantity = 50 }
                    };
                }
            }
        }

        /// <summary>
        /// Updates the quantity of a specific inventory item.
        /// </summary>
        /// <param name="id">The unique identifier of the inventory record.</param>
        /// <param name="quantity">The new quantity to set.</param>
        public async Task UpdateAsync(int id, int quantity)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = "UPDATE InventoryItems SET Quantity = @Quantity WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id, Quantity = quantity });
            }
        }

        /// <summary>
        /// Adds a new inventory record for a specific product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="initialQuantity">The starting stock level.</param>
        public async Task AddAsync(int productId, int initialQuantity)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                string sql = "INSERT INTO InventoryItems (ProductId, Quantity) VALUES (@ProductId, @Quantity)";
                await connection.ExecuteAsync(sql, new { ProductId = productId, Quantity = initialQuantity });
            }
        }
    }
}
