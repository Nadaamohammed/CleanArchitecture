
using MediatR;
using Product.Application.Dtos;
using Product.Application.Interface;

namespace Product.Application.Queries;

public class GetAllProductsHandler
    : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> Handle(
        GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);

        return products
            .Select(p => new ProductDto(p.Id, p.Name, p.Price))
            .ToList();
    }
}
