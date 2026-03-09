using ECommerce.CatalogService.Entities;
using MediatR;

namespace ECommerce.CatalogService.CQRS.Queries
{
    /// <summary>
    /// Query to retrieve a list of products with optional search filtering.
    /// </summary>
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public string? Search { get; set; }
    }
}
