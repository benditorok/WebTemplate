using Microsoft.Extensions.DependencyInjection;
using WebTemplate.UseCases.Products.Commands;
using WebTemplate.UseCases.Products.Queries;

namespace WebTemplate.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Product handlers
        services.AddScoped<GetProductsByNameQueryHandler>();
        services.AddScoped<CreateProductCommandHandler>();

        return services;
    }
}
