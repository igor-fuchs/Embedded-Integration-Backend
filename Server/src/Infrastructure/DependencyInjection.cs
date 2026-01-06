namespace Infrastructure;

using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<INodeOpcuaService, NodeOpcuaService>();

        return services;
    }
}
