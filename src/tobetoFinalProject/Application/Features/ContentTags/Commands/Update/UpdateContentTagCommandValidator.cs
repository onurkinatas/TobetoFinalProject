using FluentValidation;

namespace Application.Features.ContentTags.Commands.Update;

public class UpdateContentTagCommandValidator : AbstractValidator<UpdateContentTagCommand>
{
    public UpdateContentTagCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.TagId).NotEmpty();
    }
}