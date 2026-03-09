using ECommerce.CatalogService.CQRS.Commands;
using ECommerce.CatalogService.CQRS.Queries;
using ECommerce.CatalogService.Entities;
using ECommerce.CatalogService.Repositories;
using MediatR;

namespace ECommerce.CatalogService.CQRS.Handlers
{
    /// <summary>
    /// Implements MediatR handlers for product-related commands and queries.
    /// </summary>
    public class ProductHandlers : 
        IRequestHandler<CreateProductCommand, int>,
        IRequestHandler<GetProductsQuery, IEnumerable<Product>>,
        IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductRepository REPOSITORY;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductHandlers"/> class.
        /// </summary>
        /// <param name="repository">The data repository for products.</param>
        public ProductHandlers(IProductRepository repository)
        {
            REPOSITORY = repository;
        }
 
        /// <summary>
        /// Handles the creation of a new product.
        /// </summary>
        /// <param name="request">The product creation command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The generated ID of the new product.</returns>
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price
            };
 
            await REPOSITORY.AddAsync(product);
            await REPOSITORY.SaveChangesAsync();

            // POC Integration: Create initial inventory record in the legacy system
            try
            {
                using var client = new HttpClient();
                var inventoryUrl = "http://api-gateway:8080/api/inventory/inventory";
                var inventoryJson = System.Text.Json.JsonSerializer.Serialize(new { ProductId = product.Id, Quantity = 10 });
                var content = new StringContent(inventoryJson, System.Text.Encoding.UTF8, "application/json");
                await client.PostAsync(inventoryUrl, content);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Inventory creation failed: {ex.Message}");
            }
 
            return product.Id;
        }
 
        /// <summary>
        /// Handles retrieving all products, with optional search filtering.
        /// </summary>
        /// <param name="request">The product retrieval query.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A collection of matching products.</returns>
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await REPOSITORY.GetAllAsync();
            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                products = System.Linq.Enumerable.Where(products, p => p.Name.ToLower().Contains(search));
            }
            return products;
        }
 
        /// <summary>
        /// Handles retrieving a single product by its ID.
        /// </summary>
        /// <param name="request">The query for a specific product ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await REPOSITORY.GetByIdAsync(request.Id);
        }
    }
}
