using FluentValidation;

namespace Application.Features.LectureViews.Commands.Create;

public class CreateLectureViewCommandValidator : AbstractValidator<CreateLectureViewCommand>
{
    public CreateLectureViewCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
    }
}