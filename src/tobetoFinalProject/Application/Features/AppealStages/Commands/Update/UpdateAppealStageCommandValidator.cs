using FluentValidation;

namespace Application.Features.AppealStages.Commands.Update;

public class UpdateAppealStageCommandValidator : AbstractValidator<UpdateAppealStageCommand>
{
    public UpdateAppealStageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AppealId).NotEmpty();
        RuleFor(c => c.StageId).NotEmpty();
        RuleFor(c => c.Appeal).NotEmpty();
        RuleFor(c => c.Stage).NotEmpty();
    }
}