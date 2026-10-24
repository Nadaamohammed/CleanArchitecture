
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

        public ProductController(IMediator sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(
            CreateProductCommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                var product = await _sender.Send(
                    command, cancellationToken);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = product.Id },
                    product);
            }
            catch (ValidationException ex)
            {
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
            var products = await _sender.Send(
                new GetAllProductsQuery(), cancellationToken);

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(
            int id,
            CancellationToken cancellationToken)
        {
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
                return NotFound();

            return Ok(product);
        }
    }
}
