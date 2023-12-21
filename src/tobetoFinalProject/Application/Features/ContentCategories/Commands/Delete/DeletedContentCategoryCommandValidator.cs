using FluentValidation;

namespace Application.Features.ContentCategories.Commands.Delete;

public class DeleteContentCategoryCommandValidator : AbstractValidator<DeleteContentCategoryCommand>
{
    public DeleteContentCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}