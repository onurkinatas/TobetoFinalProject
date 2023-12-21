using FluentValidation;

namespace Application.Features.LectureCompletionConditions.Commands.Update;

public class UpdateLectureCompletionConditionCommandValidator : AbstractValidator<UpdateLectureCompletionConditionCommand>
{
    public UpdateLectureCompletionConditionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.CompletionPercentage).NotEmpty();
    }
}