using FluentValidation;

namespace Application.Features.AppealStages.Commands.Create;

public class CreateAppealStageCommandValidator : AbstractValidator<CreateAppealStageCommand>
{
    public CreateAppealStageCommandValidator()
    {
        RuleFor(c => c.AppealId).NotEmpty();
        RuleFor(c => c.StageId).NotEmpty();
        RuleFor(c => c.Appeal).NotEmpty();
        RuleFor(c => c.Stage).NotEmpty();
    }
}