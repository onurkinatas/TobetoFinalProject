using FluentValidation;

namespace Application.Features.CommentSubComments.Commands.Create;

public class CreateCommentSubCommentCommandValidator : AbstractValidator<CreateCommentSubCommentCommand>
{
    public CreateCommentSubCommentCommandValidator()
    {
        RuleFor(c => c.UserLectureCommentId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.SubComment).NotEmpty();
    }
}