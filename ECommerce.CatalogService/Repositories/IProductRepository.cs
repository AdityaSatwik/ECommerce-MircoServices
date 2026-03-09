using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.CatalogService.Entities;

namespace ECommerce.CatalogService.Repositories
{
    /// <summary>
    /// interface for the product repository in the catalog service.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>Retrieves a product by its unique ID.</summary>
        Task<Product?> GetByIdAsync(int id);
        /// <summary>Retrieves all available products.</summary>
        Task<IEnumerable<Product>> GetAllAsync();
        /// <summary>Adds a new product to the repository.</summary>
        Task AddAsync(Product product);
        /// <summary>Persists all changes to the data store.</summary>
        Task SaveChangesAsync();
    }
}
