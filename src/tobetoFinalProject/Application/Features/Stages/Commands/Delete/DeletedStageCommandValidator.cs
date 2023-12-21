using FluentValidation;

namespace Application.Features.Stages.Commands.Delete;

public class DeleteStageCommandValidator : AbstractValidator<DeleteStageCommand>
{
    public DeleteStageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}