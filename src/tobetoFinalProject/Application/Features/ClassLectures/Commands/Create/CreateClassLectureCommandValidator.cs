using FluentValidation;

namespace Application.Features.ClassLectures.Commands.Create;

public class CreateClassLectureCommandValidator : AbstractValidator<CreateClassLectureCommand>
{
    public CreateClassLectureCommandValidator()
    {
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}