namespace Presentation;

using Application.Interfaces;
using Presentation.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSignalR();
        services.AddScoped<IOpcuaNodeNotificationService, OpcuaNodeNotificationService>();

        return services;
    }
}
