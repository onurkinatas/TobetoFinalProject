using FluentValidation;

namespace Application.Features.Stages.Commands.Update;

public class UpdateStageCommandValidator : AbstractValidator<UpdateStageCommand>
{
    public UpdateStageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}