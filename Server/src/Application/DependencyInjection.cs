namespace Application;

using Application.Interfaces;
using Application.Options;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Register options
        services.Configure<SimulationFrontOptions>(configuration.GetSection(SimulationFrontOptions.SectionName));

        // Register validators
        services.AddValidatorsFromAssemblyContaining<CreateNodeRequestValidator>();

        // Register validation service
        services.AddScoped<IValidationService, ValidationService>();

        // Register application services
        services.AddScoped<IOpcuaNodeService, OpcuaNodeService>();

        return services;
    }
}
