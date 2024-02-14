using FluentValidation;

namespace Application.Features.QuestionOptions.Commands.Create;

public class CreateQuestionOptionCommandValidator : AbstractValidator<CreateQuestionOptionCommand>
{
    public CreateQuestionOptionCommandValidator()
    {
        RuleFor(c => c.OptionId).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
    }
}