using Microsoft.Extensions.DependencyInjection;
using WebTemplate.Application.Common.Data;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
