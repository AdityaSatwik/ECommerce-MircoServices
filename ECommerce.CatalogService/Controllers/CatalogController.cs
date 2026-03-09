using ECommerce.CatalogService.CQRS.Commands;
using ECommerce.CatalogService.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.CatalogService.Controllers
{
    /// <summary>
    /// API Controller for managing product catalog operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator.</param>
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all products, with optional search filtering.
        /// </summary>
        /// <param name="search">The search term.</param>
        /// <returns>A collection of products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string? search)
        {
            var products = await _mediator.Send(new GetProductsQuery { Search = search });
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a specific product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The product details.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null) return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product record.
        /// </summary>
        /// <param name="command">The creation details.</param>
        /// <returns>The ID of the created product.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProduct), new { id }, id);
        }
    }
}
