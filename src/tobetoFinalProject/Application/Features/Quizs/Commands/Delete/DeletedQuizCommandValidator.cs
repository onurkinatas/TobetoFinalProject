using FluentValidation;

namespace Application.Features.Quizs.Commands.Delete;

public class DeleteQuizCommandValidator : AbstractValidator<DeleteQuizCommand>
{
    public DeleteQuizCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}