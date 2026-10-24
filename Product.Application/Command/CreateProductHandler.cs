
using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Dtos;
using Product.Application.Interface;

namespace Product.Application.Command;

public class CreateProductHandler
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<CreateProductHandler> _logger;


    public CreateProductHandler(IProductRepository repository, ILogger<CreateProductHandler> 
        logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing CreateProductHandler");

        var product = new Product.Domain.Entity.Product(
            request.Name.Trim(),
            request.Price);

        await _repository.AddAsync(product, cancellationToken);

        _logger.LogInformation(
        "Product {ProductId} saved successfully",
        product.Id);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Price);
    }
}
