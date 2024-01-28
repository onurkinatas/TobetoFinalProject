using FluentValidation;

namespace Application.Features.ContentLikes.Commands.Create;

public class CreateContentLikeCommandValidator : AbstractValidator<CreateContentLikeCommand>
{
    public CreateContentLikeCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
    }
}