using FluentValidation;

namespace Application.Features.LectureViews.Commands.Delete;

public class DeleteLectureViewCommandValidator : AbstractValidator<DeleteLectureViewCommand>
{
    public DeleteLectureViewCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}