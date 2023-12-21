using FluentValidation;

namespace Application.Features.ClassSurveys.Commands.Update;

public class UpdateClassSurveyCommandValidator : AbstractValidator<UpdateClassSurveyCommand>
{
    public UpdateClassSurveyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
        RuleFor(c => c.SurveyId).NotEmpty();
    }
}