using FluentValidation;

namespace Application.Features.CommentSubComments.Commands.Delete;

public class DeleteCommentSubCommentCommandValidator : AbstractValidator<DeleteCommentSubCommentCommand>
{
    public DeleteCommentSubCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}