using FluentValidation;

namespace Application.Features.ContentCategories.Commands.Update;

public class UpdateContentCategoryCommandValidator : AbstractValidator<UpdateContentCategoryCommand>
{
    public UpdateContentCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}