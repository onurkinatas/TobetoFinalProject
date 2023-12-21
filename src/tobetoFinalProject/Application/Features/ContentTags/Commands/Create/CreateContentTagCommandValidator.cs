using FluentValidation;

namespace Application.Features.ContentTags.Commands.Create;

public class CreateContentTagCommandValidator : AbstractValidator<CreateContentTagCommand>
{
    public CreateContentTagCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.TagId).NotEmpty();
    }
}