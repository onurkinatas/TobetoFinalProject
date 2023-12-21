using FluentValidation;

namespace Application.Features.Contents.Commands.Create;

public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
{
    public CreateContentCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.LanguageId).NotEmpty();
        RuleFor(c => c.SubTypeId).NotEmpty();
        RuleFor(c => c.VideoUrl).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.ManufacturerId).NotEmpty();
        RuleFor(c => c.Duration).NotEmpty();
        RuleFor(c => c.ContentCategoryId).NotEmpty();
    }
}