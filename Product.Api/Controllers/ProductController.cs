using Microsoft.AspNetCore.Mvc;
using Product.Application.Command;
using Product.Application.Dtos;
using Product.Application.Queries;
using MediatR;
using FluentValidation;

namespace Product.Api.Controllers
{
    [ApiController]
    [Route("api/products")]

    public class ProductController : ControllerBase
    {
        private readonly IMediator _sender;
        private readonly ILogger<ProductController> _logger;


        public ProductController(IMediator sender , ILogger<ProductController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(
            CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "Creating a new product with name {ProductName}",
            command.Name);
            try
            {
                var product = await _sender.Send(
                    command, cancellationToken);
                _logger.LogInformation(
               "Product created successfully with ID {ProductId}",
               product.Id);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = product.Id },
                    product);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(
               "Product creation failed validation. Error count: {ErrorCount}",
               ex.Errors.Count());
                return BadRequest(new
                {
                    errors = ex.Errors.Select(e => new
                    {
                        field = e.PropertyName,
                        message = e.ErrorMessage
                    })
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all products");

            var products = await _sender.Send(
                new GetAllProductsQuery(), cancellationToken);
            _logger.LogInformation(
           "Retrieved {ProductCount} products",
           products.Count);

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "Retrieving product with ID {ProductId}", id);
            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Id must be positive."
                });
            }

            var product = await _sender.Send(
                new GetProductByIdQuery(id), cancellationToken);

            if (product is null)
            {
                _logger.LogWarning(
               "Product with ID {ProductId} was not found", id);
                return NotFound();
            }

            return Ok(product);
        }
    }
}
