using FluentValidation;

namespace Application.Features.LectureCourses.Commands.Create;

public class CreateLectureCourseCommandValidator : AbstractValidator<CreateLectureCourseCommand>
{
    public CreateLectureCourseCommandValidator()
    {
        RuleFor(c => c.CourseId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
    }
}