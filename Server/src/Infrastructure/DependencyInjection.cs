namespace Infrastructure;

using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IOpcuaNodeRepository, OpcuaNodeCacheRepository>();

        return services;
    }
}
