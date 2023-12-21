using FluentValidation;

namespace Application.Features.ContentInstructors.Commands.Delete;

public class DeleteContentInstructorCommandValidator : AbstractValidator<DeleteContentInstructorCommand>
{
    public DeleteContentInstructorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}