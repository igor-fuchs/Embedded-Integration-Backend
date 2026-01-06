namespace Application;

using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register all validators from the Application assembly (only need one call)
        services.AddValidatorsFromAssemblyContaining<CreateNodeRequestValidator>();

        return services;
    }
}
