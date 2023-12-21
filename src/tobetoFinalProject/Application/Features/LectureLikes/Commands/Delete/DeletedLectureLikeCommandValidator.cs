using FluentValidation;

namespace Application.Features.LectureLikes.Commands.Delete;

public class DeleteLectureLikeCommandValidator : AbstractValidator<DeleteLectureLikeCommand>
{
    public DeleteLectureLikeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}