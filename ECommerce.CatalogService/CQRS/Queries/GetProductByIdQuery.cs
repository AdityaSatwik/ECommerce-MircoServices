using ECommerce.CatalogService.Entities;
using MediatR;

namespace ECommerce.CatalogService.CQRS.Queries
{
    /// <summary>
    /// Query to retrieve a specific product by its unique identifier.
    /// </summary>
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The product ID to retrieve.</param>
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }
}
