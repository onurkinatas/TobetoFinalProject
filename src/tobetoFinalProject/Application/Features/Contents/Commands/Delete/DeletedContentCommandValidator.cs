using FluentValidation;

namespace Application.Features.Contents.Commands.Delete;

public class DeleteContentCommandValidator : AbstractValidator<DeleteContentCommand>
{
    public DeleteContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}