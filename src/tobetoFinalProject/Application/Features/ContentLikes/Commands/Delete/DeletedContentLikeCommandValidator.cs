using FluentValidation;

namespace Application.Features.ContentLikes.Commands.Delete;

public class DeleteContentLikeCommandValidator : AbstractValidator<DeleteContentLikeCommand>
{
    public DeleteContentLikeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}