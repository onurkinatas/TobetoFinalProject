using FluentValidation;

namespace Application.Features.LectureCompletionConditions.Commands.Create;

public class CreateLectureCompletionConditionCommandValidator : AbstractValidator<CreateLectureCompletionConditionCommand>
{
    public CreateLectureCompletionConditionCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.CompletionPercentage).NotEmpty();
    }
}