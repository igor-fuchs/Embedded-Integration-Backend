namespace Application.Validators;

using Domain.DTOs;
using FluentValidation;

/// <summary>
/// Validator for UpdateNodeRequest.
/// </summary>
public class UpdateNodeRequestValidator : AbstractValidator<NodeValueOpcuaDto>
{
    public UpdateNodeRequestValidator()
    {
        RuleFor(x => x.Value)
            .NotNull().WithMessage("Node value is required.");
    }
}
