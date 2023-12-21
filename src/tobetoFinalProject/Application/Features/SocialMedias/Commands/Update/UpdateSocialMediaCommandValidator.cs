using FluentValidation;

namespace Application.Features.SocialMedias.Commands.Update;

public class UpdateSocialMediaCommandValidator : AbstractValidator<UpdateSocialMediaCommand>
{
    public UpdateSocialMediaCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.LogoUrl).NotEmpty();
    }
}