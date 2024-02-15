using FluentValidation;

namespace Application.Features.PoolQuestions.Commands.Delete;

public class DeletePoolQuestionCommandValidator : AbstractValidator<DeletePoolQuestionCommand>
{
    public DeletePoolQuestionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}