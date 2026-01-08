namespace Presentation.Middleware;

using Domain.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// Global exception handling middleware.
/// Catches all exceptions and returns appropriate HTTP responses.
/// </summary>
public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, response) = exception switch
        {
            NotFoundException ex => (HttpStatusCode.NotFound, CreateErrorResponse(ex.Code, ex.Message)),
            ConflictException ex => (HttpStatusCode.Conflict, CreateErrorResponse(ex.Code, ex.Message)),
            ResourceLimitExceededException ex => (HttpStatusCode.ServiceUnavailable, CreateErrorResponse(ex.Code, ex.Message)),
            Domain.Exceptions.ValidationException ex => (HttpStatusCode.BadRequest, CreateValidationErrorResponse(ex)),
            FluentValidation.ValidationException ex => (HttpStatusCode.BadRequest, CreateFluentValidationErrorResponse(ex)),
            _ => HandleUnknownException(exception)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ignore null values
        });

        await context.Response.WriteAsync(json);
    }

    private (HttpStatusCode, ErrorResponse) HandleUnknownException(Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred");
        
        return (HttpStatusCode.InternalServerError, CreateErrorResponse(
            "INTERNAL_ERROR",
            "An unexpected error occurred. Please try again later."
        ));
    }

    private static ErrorResponse CreateErrorResponse(string code, string message) => new()
    {
        Code = code,
        Message = message
    };

    private static ErrorResponse CreateValidationErrorResponse(Domain.Exceptions.ValidationException ex) => new()
    {
        Code = ex.Code,
        Message = ex.Message,
        Errors = ex.Errors.ToList()
    };

    private static ErrorResponse CreateFluentValidationErrorResponse(FluentValidation.ValidationException ex) => new()
    {
        Code = "VALIDATION_FAILED",
        Message = "One or more validation errors occurred.",
        Errors = ex.Errors.Select(e => e.ErrorMessage).ToList()
    };
}

/// <summary>
/// Standard error response format.
/// </summary>
public sealed class ErrorResponse
{
    public required string Code { get; init; }
    public required string Message { get; init; }
    public List<string>? Errors { get; init; }
}
