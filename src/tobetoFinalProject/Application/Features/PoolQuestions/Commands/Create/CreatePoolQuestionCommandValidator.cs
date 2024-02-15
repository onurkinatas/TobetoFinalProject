using FluentValidation;

namespace Application.Features.PoolQuestions.Commands.Create;

public class CreatePoolQuestionCommandValidator : AbstractValidator<CreatePoolQuestionCommand>
{
    public CreatePoolQuestionCommandValidator()
    {
        RuleFor(c => c.PoolId).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
    }
}