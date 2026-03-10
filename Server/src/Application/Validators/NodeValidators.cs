namespace Application.Validators;

using Domain.DTOs.Requests;
using FluentValidation;
using System.Text.Json;

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
            .Matches(@"^ns=\d+;s=""(.+)""$")
            .WithMessage("Node name must follow OPC UA format: ns=<number>;s=\"<identifier>\"");

        RuleFor(x => x.Value)
            .Must(v => v.ValueKind != JsonValueKind.Undefined)
            .WithMessage("Node value is required.")
            .Must(BeBoolean)
            .WithMessage("Node value must be a boolean (true or false).");
    }

    private static bool BeBoolean(JsonElement value) =>
        value.ValueKind is JsonValueKind.True or JsonValueKind.False;
}

/// <summary>
/// Validator for UpdateNodeRequest.
/// </summary>
public sealed class UpdateNodeRequestValidator : AbstractValidator<UpdateNodeRequest>
{
    public UpdateNodeRequestValidator()
    {
        RuleFor(x => x.Value)
            .Must(v => v.ValueKind != JsonValueKind.Undefined)
            .WithMessage("Node value is required.")
            .Must(v => v.ValueKind is JsonValueKind.True or JsonValueKind.False)
            .WithMessage("Node value must be a boolean (true or false).");
    }
}
