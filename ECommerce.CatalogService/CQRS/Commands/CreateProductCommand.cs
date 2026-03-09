using MediatR;

namespace ECommerce.CatalogService.CQRS.Commands
{
    /// <summary>
    /// Command to create a new product in the catalog.
    /// </summary>
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
