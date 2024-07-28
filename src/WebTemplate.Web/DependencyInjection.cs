using Microsoft.EntityFrameworkCore;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options
                .UseInMemoryDatabase("memdb")
                .UseLazyLoadingProxies();
        });

        return services;
    }
}
