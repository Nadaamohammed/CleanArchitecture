using MediatR;
using Product.Application.Dtos;
using Product.Application.Interface;

namespace Product.Application.Queries;

public class GetProductByIdHandler
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(
            request.Id, cancellationToken);

        if (product is null)
            return null;

        return new ProductDto(
            product.Id,
            product.Name,
            product.Price);
    }
}
