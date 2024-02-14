using FluentValidation;

namespace Application.Features.Options.Commands.Create;

public class CreateOptionCommandValidator : AbstractValidator<CreateOptionCommand>
{
    public CreateOptionCommandValidator()
    {
        RuleFor(c => c.Text).NotEmpty();
    }
}