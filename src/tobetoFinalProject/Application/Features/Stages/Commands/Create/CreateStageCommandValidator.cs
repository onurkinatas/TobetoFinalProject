using FluentValidation;

namespace Application.Features.Stages.Commands.Create;

public class CreateStageCommandValidator : AbstractValidator<CreateStageCommand>
{
    public CreateStageCommandValidator()
    {
        RuleFor(c => c.Description).NotEmpty();
    }
}