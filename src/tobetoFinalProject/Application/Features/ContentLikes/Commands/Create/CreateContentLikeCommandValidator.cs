using FluentValidation;

namespace Application.Features.ContentLikes.Commands.Create;

public class CreateContentLikeCommandValidator : AbstractValidator<CreateContentLikeCommand>
{
    public CreateContentLikeCommandValidator()
    {
        RuleFor(c => c.IsLiked).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
    }
}