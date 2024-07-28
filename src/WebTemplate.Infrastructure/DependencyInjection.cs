using Microsoft.Extensions.DependencyInjection;
using WebTemplate.Application.Common.Data;
using WebTemplate.Application.Products;
using WebTemplate.Infrastructure.Data;
using WebTemplate.Infrastructure.Repositories;

namespace WebTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
