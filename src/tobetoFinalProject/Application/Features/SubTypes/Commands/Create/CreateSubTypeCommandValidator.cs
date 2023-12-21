using FluentValidation;

namespace Application.Features.SubTypes.Commands.Create;

public class CreateSubTypeCommandValidator : AbstractValidator<CreateSubTypeCommand>
{
    public CreateSubTypeCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}