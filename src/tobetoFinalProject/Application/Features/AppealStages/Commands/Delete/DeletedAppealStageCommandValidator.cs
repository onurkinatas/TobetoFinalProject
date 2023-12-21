using FluentValidation;

namespace Application.Features.AppealStages.Commands.Delete;

public class DeleteAppealStageCommandValidator : AbstractValidator<DeleteAppealStageCommand>
{
    public DeleteAppealStageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}