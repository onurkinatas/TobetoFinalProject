using FluentValidation;

namespace Application.Features.CommentSubComments.Commands.Update;

public class UpdateCommentSubCommentCommandValidator : AbstractValidator<UpdateCommentSubCommentCommand>
{
    public UpdateCommentSubCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserLectureCommentId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.SubComment).NotEmpty();
    }
}