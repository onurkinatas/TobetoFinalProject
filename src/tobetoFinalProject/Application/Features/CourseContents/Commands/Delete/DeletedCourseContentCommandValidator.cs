using FluentValidation;

namespace Application.Features.CourseContents.Commands.Delete;

public class DeleteCourseContentCommandValidator : AbstractValidator<DeleteCourseContentCommand>
{
    public DeleteCourseContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}