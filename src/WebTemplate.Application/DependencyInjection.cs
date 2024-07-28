using Microsoft.Extensions.DependencyInjection;
using WebTemplate.Application.Products;

namespace WebTemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Product handlers
        services.AddScoped<GetProductsByNameQueryHandler>();
        services.AddScoped<CreateProductCommandHandler>();

        return services;
    }
}
