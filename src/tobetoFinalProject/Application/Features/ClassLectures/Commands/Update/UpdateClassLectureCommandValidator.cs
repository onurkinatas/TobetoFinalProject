using FluentValidation;

namespace Application.Features.ClassLectures.Commands.Update;

public class UpdateClassLectureCommandValidator : AbstractValidator<UpdateClassLectureCommand>
{
    public UpdateClassLectureCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}