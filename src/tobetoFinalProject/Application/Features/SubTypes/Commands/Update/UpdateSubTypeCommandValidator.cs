using FluentValidation;

namespace Application.Features.SubTypes.Commands.Update;

public class UpdateSubTypeCommandValidator : AbstractValidator<UpdateSubTypeCommand>
{
    public UpdateSubTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}