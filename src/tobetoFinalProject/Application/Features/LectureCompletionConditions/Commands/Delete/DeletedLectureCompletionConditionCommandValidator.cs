using FluentValidation;

namespace Application.Features.LectureCompletionConditions.Commands.Delete;

public class DeleteLectureCompletionConditionCommandValidator : AbstractValidator<DeleteLectureCompletionConditionCommand>
{
    public DeleteLectureCompletionConditionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}