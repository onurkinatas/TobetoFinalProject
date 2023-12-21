using FluentValidation;

namespace Application.Features.CourseContents.Commands.Create;

public class CreateCourseContentCommandValidator : AbstractValidator<CreateCourseContentCommand>
{
    public CreateCourseContentCommandValidator()
    {
        RuleFor(c => c.CourseId).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
    }
}