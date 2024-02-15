using FluentValidation;

namespace Application.Features.Pools.Commands.Update;

public class UpdatePoolCommandValidator : AbstractValidator<UpdatePoolCommand>
{
    public UpdatePoolCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}