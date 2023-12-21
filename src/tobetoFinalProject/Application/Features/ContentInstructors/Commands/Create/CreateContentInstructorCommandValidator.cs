using FluentValidation;

namespace Application.Features.ContentInstructors.Commands.Create;

public class CreateContentInstructorCommandValidator : AbstractValidator<CreateContentInstructorCommand>
{
    public CreateContentInstructorCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.InstructorId).NotEmpty();
    }
}