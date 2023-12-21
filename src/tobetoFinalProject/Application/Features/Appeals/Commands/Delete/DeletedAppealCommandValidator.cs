using FluentValidation;

namespace Application.Features.Appeals.Commands.Delete;

public class DeleteAppealCommandValidator : AbstractValidator<DeleteAppealCommand>
{
    public DeleteAppealCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}