using FluentValidation;

namespace Application.Features.ContentLikes.Commands.Update;

public class UpdateContentLikeCommandValidator : AbstractValidator<UpdateContentLikeCommand>
{
    public UpdateContentLikeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.IsLiked).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
    }
}