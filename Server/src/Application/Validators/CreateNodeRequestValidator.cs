namespace Application.Validators;

using Domain.DTOs;
using FluentValidation;

/// <summary>
/// Validator for CreateNodeRequest.
/// </summary>
public class CreateNodeRequestValidator : AbstractValidator<NodeOpcuaDto>
{
    public CreateNodeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Node name is required.")
            .MaximumLength(256).WithMessage("Node name must not exceed 256 characters.")
            .Matches(@"^[a-zA-Z0-9_\-\.]+$").WithMessage("Node name can only contain letters, numbers, underscores, hyphens, and dots.");

        RuleFor(x => x.Value)
            .NotNull().WithMessage("Node value is required.");
    }
}
