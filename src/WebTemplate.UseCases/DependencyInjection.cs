using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebTemplate.Core.Common.Messaging;
using WebTemplate.UseCases.Products.Commands;
using WebTemplate.UseCases.Products.Queries;

namespace WebTemplate.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        // Add all commands
        services.AddScopedServicesFromAssembly<IBaseCommandHandler>(Assembly.GetExecutingAssembly());
        
        // Add all queries
        services.AddScopedServicesFromAssembly<IBaseQueryHandler>(Assembly.GetExecutingAssembly());
        
        return services;
    }

    private static void AddScopedServicesFromAssembly<T>(this IServiceCollection services, Assembly assembly)
    {
        // Get the type of the marker interface
        var markerInterfaceType = typeof(T);

        // Find all classes that implement the marker interface
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && markerInterfaceType.IsAssignableFrom(t))
            .ToList();

        // Register each class as a scoped service
        foreach (var type in types)
        {
            services.AddScoped(type);
        }
    }
}
