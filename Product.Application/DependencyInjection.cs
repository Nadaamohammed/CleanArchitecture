using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Behaviors;
using Product.Application.Command;

namespace Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(CreateProductCommand).Assembly));

        services.AddValidatorsFromAssembly(
            typeof(CreateProductCommand).Assembly);

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}