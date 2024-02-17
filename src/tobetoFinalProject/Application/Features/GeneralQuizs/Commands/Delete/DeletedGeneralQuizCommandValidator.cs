using FluentValidation;

namespace Application.Features.GeneralQuizs.Commands.Delete;

public class DeleteGeneralQuizCommandValidator : AbstractValidator<DeleteGeneralQuizCommand>
{
    public DeleteGeneralQuizCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}