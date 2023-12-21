using FluentValidation;

namespace Application.Features.Contents.Commands.Update;

public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
{
    public UpdateContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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