using FluentValidation;

namespace Application.Features.SocialMedias.Commands.Delete;

public class DeleteSocialMediaCommandValidator : AbstractValidator<DeleteSocialMediaCommand>
{
    public DeleteSocialMediaCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}