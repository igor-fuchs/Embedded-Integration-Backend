namespace Application.Interfaces;

/// <summary>
/// Service for validating requests using FluentValidation.
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// Validates the request and throws ValidationException if validation fails.
    /// </summary>
    /// <typeparam name="T">The type of object to validate.</typeparam>
    /// <param name="request">The object to validate.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task ValidateAsync<T>(T request, CancellationToken cancellationToken = default) where T : class;
}
