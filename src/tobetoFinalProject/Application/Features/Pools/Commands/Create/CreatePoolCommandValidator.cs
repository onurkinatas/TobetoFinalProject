using FluentValidation;

namespace Application.Features.Pools.Commands.Create;

public class CreatePoolCommandValidator : AbstractValidator<CreatePoolCommand>
{
    public CreatePoolCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}