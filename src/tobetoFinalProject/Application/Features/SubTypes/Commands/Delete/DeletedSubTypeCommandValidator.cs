using FluentValidation;

namespace Application.Features.SubTypes.Commands.Delete;

public class DeleteSubTypeCommandValidator : AbstractValidator<DeleteSubTypeCommand>
{
    public DeleteSubTypeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}