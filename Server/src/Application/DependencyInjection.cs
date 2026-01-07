namespace Application;

using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register validators
        services.AddValidatorsFromAssemblyContaining<CreateNodeRequestValidator>();

        // Register validation service
        services.AddScoped<IValidationService, ValidationService>();

        // Register application services
        services.AddScoped<IOpcuaNodeService, OpcuaNodeService>();

        return services;
    }
}
