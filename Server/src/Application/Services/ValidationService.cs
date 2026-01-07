namespace Application.Services;

using Application.Interfaces;
using FluentValidation;

/// <summary>
/// Generic validation service that resolves validators from DI.
/// </summary>
public sealed class ValidationService : IValidationService
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAsync<T>(T request, CancellationToken cancellationToken = default) where T : class
    {
        var validator = _serviceProvider.GetService(typeof(IValidator<T>)) as IValidator<T>;
        
        if (validator is null)
        {
            return; // No validator registered, skip validation
        }

        await validator.ValidateAndThrowAsync(request, cancellationToken);
    }
}
