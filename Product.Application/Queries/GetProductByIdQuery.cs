using MediatR;
using Product.Application.Dtos;

namespace Product.Application.Queries;

public record GetProductByIdQuery(int Id)
    : IRequest<ProductDto?>;
