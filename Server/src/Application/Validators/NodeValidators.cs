namespace Application.Validators;

using Application.DTOs.Requests;
using FluentValidation;

/// <summary>
/// Validator for CreateNodeRequest.
/// </summary>
public sealed class CreateNodeRequestValidator : AbstractValidator<CreateNodeRequest>
{
    public CreateNodeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Node name is required.")
            .MaximumLength(256)
            .WithMessage("Node name must not exceed 256 characters.")
            .Matches(@"^[a-zA-Z0-9_\-\.]+$")
            .WithMessage("Node name can only contain letters, numbers, underscores, hyphens, and dots.");

        RuleFor(x => x.Value)
            .Must(v => v.ValueKind != System.Text.Json.JsonValueKind.Undefined)
            .WithMessage("Node value is required.");
    }
}

/// <summary>
/// Validator for UpdateNodeRequest.
/// </summary>
public sealed class UpdateNodeRequestValidator : AbstractValidator<UpdateNodeRequest>
{
    public UpdateNodeRequestValidator()
    {
        RuleFor(x => x.Value)
            .Must(v => v.ValueKind != System.Text.Json.JsonValueKind.Undefined)
            .WithMessage("Node value is required.");
    }
}
