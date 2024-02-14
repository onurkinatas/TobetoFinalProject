using FluentValidation;

namespace Application.Features.Options.Commands.Delete;

public class DeleteOptionCommandValidator : AbstractValidator<DeleteOptionCommand>
{
    public DeleteOptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}