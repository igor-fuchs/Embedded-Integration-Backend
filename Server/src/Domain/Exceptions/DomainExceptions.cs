namespace Domain.Exceptions;

/// <summary>
/// Base exception for domain-related errors.
/// </summary>
public abstract class DomainException : Exception
{
    public string Code { get; }

    protected DomainException(string code, string message) : base(message)
    {
        Code = code;
    }
}

/// <summary>
/// Exception thrown when a requested entity is not found.
/// </summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string entityName, string identifier)
        : base("NOT_FOUND", $"{entityName} '{identifier}' was not found.")
    {
    }
}

/// <summary>
/// Exception thrown when trying to create an entity that already exists.
/// </summary>
public class ConflictException : DomainException
{
    public ConflictException(string entityName, string identifier)
        : base("CONFLICT", $"{entityName} '{identifier}' already exists.")
    {
    }
}

/// <summary>
/// Exception thrown when a resource limit is exceeded.
/// </summary>
public class ResourceLimitExceededException : DomainException
{
    public ResourceLimitExceededException(string resourceName, int limit)
        : base("RESOURCE_LIMIT_EXCEEDED", $"Maximum number of {resourceName} ({limit}) has been reached.")
    {
    }
}

/// <summary>
/// Exception thrown when validation fails.
/// </summary>
public class ValidationException : DomainException
{
    public IReadOnlyList<string> Errors { get; }

    public ValidationException(IEnumerable<string> errors)
        : base("VALIDATION_FAILED", "One or more validation errors occurred.")
    {
        Errors = errors.ToList().AsReadOnly();
    }

    public ValidationException(string error)
        : this(new[] { error })
    {
    }
}
