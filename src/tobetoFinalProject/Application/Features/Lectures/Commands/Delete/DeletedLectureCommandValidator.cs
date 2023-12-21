using FluentValidation;

namespace Application.Features.Lectures.Commands.Delete;

public class DeleteLectureCommandValidator : AbstractValidator<DeleteLectureCommand>
{
    public DeleteLectureCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}