using Microsoft.Extensions.DependencyInjection;
using WebTemplate.Core.Products;

namespace WebTemplate.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        // Product handlers
        services.AddScoped<GetProductsByNameQueryHandler>();
        services.AddScoped<CreateProductCommandHandler>();

        return services;
    }
}
