using MediatR;
using Product.Application.Dtos;

namespace Product.Application.Command;

public record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<ProductDto>;