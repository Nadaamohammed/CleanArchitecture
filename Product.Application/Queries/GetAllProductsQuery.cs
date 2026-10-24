using MediatR;
using Product.Application.Dtos;

namespace Product.Application.Queries;

public record GetAllProductsQuery
    : IRequest<List<ProductDto>>;
