using FluentValidation;

namespace Application.Features.GeneralQuizs.Commands.Update;

public class UpdateGeneralQuizCommandValidator : AbstractValidator<UpdateGeneralQuizCommand>
{
    public UpdateGeneralQuizCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.QuizId).NotEmpty();
    }
}