using FluentValidation;

namespace Application.Features.PoolQuestions.Commands.Update;

public class UpdatePoolQuestionCommandValidator : AbstractValidator<UpdatePoolQuestionCommand>
{
    public UpdatePoolQuestionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.PoolId).NotEmpty();
        RuleFor(c => c.QuestionId).NotEmpty();
    }
}