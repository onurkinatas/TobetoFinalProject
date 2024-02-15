using FluentValidation;

namespace Application.Features.Pools.Commands.Delete;

public class DeletePoolCommandValidator : AbstractValidator<DeletePoolCommand>
{
    public DeletePoolCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}