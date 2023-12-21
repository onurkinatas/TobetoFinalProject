using FluentValidation;

namespace Application.Features.Tags.Commands.Delete;

public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}