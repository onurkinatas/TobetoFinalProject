using FluentValidation;

namespace Application.Features.ClassSurveys.Commands.Create;

public class CreateClassSurveyCommandValidator : AbstractValidator<CreateClassSurveyCommand>
{
    public CreateClassSurveyCommandValidator()
    {
        RuleFor(c => c.StudentClassId).NotEmpty();
        RuleFor(c => c.SurveyId).NotEmpty();
    }
}