using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.InMemory;
using WebTemplate.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace WebTemplate.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseInMemoryDatabase("memdb")
                .UseLazyLoadingProxies();
        });

        return services;
    }
}
