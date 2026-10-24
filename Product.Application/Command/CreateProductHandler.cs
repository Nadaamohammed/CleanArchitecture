
using MediatR;
using Product.Application.Dtos;
using Product.Application.Interface;

namespace Product.Application.Command;

public class CreateProductHandler
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product.Domain.Entity.Product(
            request.Name.Trim(),
            request.Price);

        await _repository.AddAsync(product, cancellationToken);

        return new ProductDto(
            product.Id,
            product.Name,
            product.Price);
    }
}
