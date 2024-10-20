using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using WebTemplate.Infrastructure.Data;

namespace WebTemplate.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddLogging();

        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            options
                .UseInMemoryDatabase("memdb")
                .UseLazyLoadingProxies();
        });

        services.AddMudServices();

        return services;
    }
}
