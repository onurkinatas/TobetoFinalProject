using FluentValidation;

namespace Application.Features.GeneralQuizs.Commands.Create;

public class CreateGeneralQuizCommandValidator : AbstractValidator<CreateGeneralQuizCommand>
{
    public CreateGeneralQuizCommandValidator()
    {
        RuleFor(c => c.QuizId).NotEmpty();
    }
}