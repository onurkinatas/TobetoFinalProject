using FluentValidation;

namespace Application.Features.LectureViews.Commands.Update;

public class UpdateLectureViewCommandValidator : AbstractValidator<UpdateLectureViewCommand>
{
    public UpdateLectureViewCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
    }
}