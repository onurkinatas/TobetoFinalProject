using FluentValidation;

namespace Application.Features.LectureCourses.Commands.Delete;

public class DeleteLectureCourseCommandValidator : AbstractValidator<DeleteLectureCourseCommand>
{
    public DeleteLectureCourseCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}