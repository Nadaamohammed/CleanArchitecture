
using FluentValidation;

namespace Product.Application.Command;

public class CreateProductValidator
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}
