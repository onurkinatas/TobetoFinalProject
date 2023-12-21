using FluentValidation;

namespace Application.Features.ClassSurveys.Commands.Delete;

public class DeleteClassSurveyCommandValidator : AbstractValidator<DeleteClassSurveyCommand>
{
    public DeleteClassSurveyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}