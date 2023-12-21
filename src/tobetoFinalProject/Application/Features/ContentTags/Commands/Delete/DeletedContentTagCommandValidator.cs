using FluentValidation;

namespace Application.Features.ContentTags.Commands.Delete;

public class DeleteContentTagCommandValidator : AbstractValidator<DeleteContentTagCommand>
{
    public DeleteContentTagCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}