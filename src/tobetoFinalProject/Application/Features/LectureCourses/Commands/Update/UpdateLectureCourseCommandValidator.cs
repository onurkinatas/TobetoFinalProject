using FluentValidation;

namespace Application.Features.LectureCourses.Commands.Update;

public class UpdateLectureCourseCommandValidator : AbstractValidator<UpdateLectureCourseCommand>
{
    public UpdateLectureCourseCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CourseId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
    }
}